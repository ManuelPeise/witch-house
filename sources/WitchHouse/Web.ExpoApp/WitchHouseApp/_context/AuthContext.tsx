import React, { PropsWithChildren } from 'react';
import { AppSettings, AuthState, LoginRequest, MobileLoginResult, UserData, UserDataStorageModel } from '../_lib/types';
import axiosClient from '../_lib/api/axiosClient';
import { JwtData, ResponseMessage } from '../_lib/api/types';
import { endPoints } from '../_lib/api/apiConfiguration';
import * as SecureStore from 'expo-secure-store';
import { SecureStoreKeyEnum } from '../_lib/enums/SecureStoreKeyEnum';
import { useDataSync } from '../_hooks/useDataSync';
import { useStorage } from '../_hooks/useStorage';
import { AsyncStorageKeyEnum } from '../_lib/enums/AsyncStorageKeyEnum';
import AsyncStorage from '@react-native-async-storage/async-storage';

export const AuthContext = React.createContext<AuthState>({} as AuthState);

const fiveMinuteInterval = 300000;

const AuthContextProvider: React.FC<PropsWithChildren> = (props) => {
  const { children } = props;
  const appSettingsStorage = useStorage<AppSettings>(AsyncStorageKeyEnum.AppSettings);
  const [userData, setUserData] = React.useState<UserData | null>(null);
  const [isLoading, setIsLoading] = React.useState<boolean>(false);
  const [isAuthenticated, setIsAuthenticated] = React.useState<boolean>(false);

  const { executeDataSync, syncAppData } = useDataSync();

  const LoadSyncData = React.useCallback(() => {
    var jwtJson = SecureStore.getItem(SecureStoreKeyEnum.Jwt) ?? null;
    var userDataJson = SecureStore.getItem(SecureStoreKeyEnum.UserData) ?? null;

    if (jwtJson != null && jwtJson.length && userDataJson != null && userDataJson.length) {
      const jwtData: JwtData = JSON.parse(jwtJson);
      const userData: UserData = JSON.parse(userDataJson);

      return { userData: userData, jwt: jwtData.jwtToken };
    }

    return { userData: null, jwt: null };
  }, []);

  const silentLogin = React.useCallback(async () => {
    setIsLoading(true);
    const syncDataModel = LoadSyncData();

    if (syncDataModel?.userData != null && syncDataModel?.jwt != null) {
      const model = await syncAppData(syncDataModel.userData, syncDataModel.jwt);
      if (model != null) {
        const userDataUpdate = { ...syncDataModel.userData, ...model.userData };
        setUserData(userDataUpdate);
        setIsAuthenticated(userDataUpdate !== undefined);
      }
    }
    setIsLoading(false);
  }, [syncAppData]);

  // interval for sync data
  React.useEffect(() => {
    const syncDataModel = LoadSyncData();
    if (appSettingsStorage.model?.syncData) {
      if (isAuthenticated === true && syncDataModel?.jwt != null && syncDataModel?.userData != null) {
        const interval = setInterval(async () => {
          await executeDataSync(syncDataModel.userData, syncDataModel.jwt);
        }, fiveMinuteInterval);

        return () => clearInterval(interval);
      }
    }
  }, [isAuthenticated, appSettingsStorage?.model]);

  // try load data from storage
  React.useEffect(() => {
    silentLogin();
  }, []);

  const storeUserData = React.useCallback(async (responseData: MobileLoginResult) => {
    const userDataStorageModel: UserDataStorageModel = {
      userId: responseData.userData.userId,
      familyGuid: responseData.userData.familyGuid,
      userName: responseData.userData.userName,
      userRole: responseData.userData.userRole,
      language: responseData.userData.language,
    };

    SecureStore.setItem(SecureStoreKeyEnum.UserData, JSON.stringify(userDataStorageModel));
    await AsyncStorage.setItem(AsyncStorageKeyEnum.ProfileImage, responseData.userData.profileImage);
    SecureStore.setItem(SecureStoreKeyEnum.Jwt, JSON.stringify(responseData.jwtData));
    SecureStore.setItem(SecureStoreKeyEnum.ModuleConfiguration, JSON.stringify(responseData.moduleConfiguration));
    SecureStore.setItem(SecureStoreKeyEnum.TrainingModuleSettings, JSON.stringify(responseData.trainingModuleSettings));
  }, []);

  const onLogin = React.useCallback(
    async (data: LoginRequest) => {
      setIsLoading(true);

      try {
        await axiosClient.post(endPoints.auth.login, JSON.stringify(data)).then(async (res) => {
          if (res.status === 200) {
            const responseData: ResponseMessage<MobileLoginResult> = res.data;

            if (responseData.success && responseData.data) {
              setUserData(responseData.data.userData);

              await storeUserData(responseData.data);

              axiosClient.defaults.headers.common['Authorization'] = `bearer ${responseData.data.jwtData.jwtToken}`;
              setIsAuthenticated(true);
            }
          }
        });
      } catch (err) {
        console.log(err);
      } finally {
        setIsLoading(false);
      }
    },
    [storeUserData]
  );

  const onLogout = React.useCallback(async () => {
    await SecureStore.deleteItemAsync(SecureStoreKeyEnum.UserData);
    await SecureStore.deleteItemAsync(SecureStoreKeyEnum.Jwt);
    await SecureStore.deleteItemAsync(SecureStoreKeyEnum.ModuleConfiguration);
    await SecureStore.deleteItemAsync(SecureStoreKeyEnum.TrainingModuleSettings);

    setUserData(null);
    setIsAuthenticated(false);
  }, []);

  const model: AuthState = { isLoading, isAuthenticated, userData, onLogin, onLogout };

  return <AuthContext.Provider value={model}>{children}</AuthContext.Provider>;
};

export default AuthContextProvider;
