import React, { PropsWithChildren } from 'react';
import { AuthState, LoginRequest, LoginResult, MobileLoginResult, UserData } from '../_lib/types';
import axiosClient from '../_lib/api/axiosClient';
import { JwtData, ResponseMessage } from '../_lib/api/types';
import { endPoints } from '../_lib/api/apiConfiguration';
import { checkApiIsAvailable } from '../_lib/api/apiUtils';
import * as SecureStore from 'expo-secure-store';
import { SecureStoreKeyEnum } from '../_lib/enums/SecureStoreKeyEnum';
import { useDataSync } from '../_hooks/useDataSync';

export const AuthContext = React.createContext<AuthState>({} as AuthState);

const AuthContextProvider: React.FC<PropsWithChildren> = (props) => {
  const { children } = props;
  const { apiIsAvailable, setApiIsAvailable } = useDataSync();
  const [userData, setUserData] = React.useState<UserData | null>(null);
  const [isLoading, setIsLoading] = React.useState<boolean>(false);
  const [isAuthenticated, setIsAuthenticated] = React.useState<boolean>(false);

  React.useEffect(() => {
    checkApiIsAvailable(setApiIsAvailable);
  }, []);

  // const loadLoginResultFromStorage = React.useCallback((): MobileLoginResult | null => {
  //   const resultJson = SecureStore.getItem(SecureStoreKeyEnum.LoginResult);

  //   if (resultJson == null) {
  //     return null;
  //   }
  //   const model: MobileLoginResult = JSON.parse(resultJson);

  //   return model ?? null;
  // }, []);

  // React.useEffect(() => {
  //   var result = loadLoginResultFromStorage();

  //   if (result != null) {
  //     setLoginResult(result);
  //     setIsAuthenticated(true);
  //   }
  // }, []);

  const onLogin = React.useCallback(async (data: LoginRequest) => {
    setIsLoading(true);

    try {
      await axiosClient.post(endPoints.auth.login, JSON.stringify(data)).then(async (res) => {
        if (res.status === 200) {
          const responseData: ResponseMessage<MobileLoginResult> = res.data;

          if (responseData.success && responseData.data) {
            setUserData(responseData.data.userData);

            SecureStore.setItem(SecureStoreKeyEnum.UserData, JSON.stringify(responseData.data.userData));
            SecureStore.setItem(SecureStoreKeyEnum.Jwt, JSON.stringify(responseData.data.jwtData));
            SecureStore.setItem(
              SecureStoreKeyEnum.ModuleConfiguration,
              JSON.stringify(responseData.data.moduleConfiguration)
            );
            SecureStore.setItem(
              SecureStoreKeyEnum.TrainingModuleSettings,
              JSON.stringify(responseData.data.trainingModuleSettings)
            );

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
  }, []);

  const onLogout = React.useCallback(async () => {
    await SecureStore.deleteItemAsync(SecureStoreKeyEnum.UserData);
    await SecureStore.deleteItemAsync(SecureStoreKeyEnum.Jwt);
    await SecureStore.deleteItemAsync(SecureStoreKeyEnum.ModuleConfiguration);
    await SecureStore.deleteItemAsync(SecureStoreKeyEnum.TrainingModuleSettings);

    setUserData(null);
    setIsAuthenticated(false);
  }, []);

  const model: AuthState = { isLoading, isAuthenticated, apiIsAvailable, userData, onLogin, onLogout };

  return <AuthContext.Provider value={model}>{children}</AuthContext.Provider>;
};

export default AuthContextProvider;
