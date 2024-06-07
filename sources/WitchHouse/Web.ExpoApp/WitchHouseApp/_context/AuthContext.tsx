import React, { PropsWithChildren } from 'react';
import { AuthState, LoginRequest, LoginResult } from '../_lib/types';
import axiosClient from '../_lib/api/axiosClient';
import { ResponseMessage } from '../_lib/api/types';
import { endPoints } from '../_lib/api/apiConfiguration';
import { checkApiIsAvailable } from '../_lib/api/apiUtils';
import { useDataSync } from '../_lib/api/useDataSync';
import * as SecureStore from 'expo-secure-store';
import { SecureStoreKeyEnum } from '../_lib/enums/SecureStoreKeyEnum';

export const AuthContext = React.createContext<AuthState>({} as AuthState);

const AuthContextProvider: React.FC<PropsWithChildren> = (props) => {
  const { children } = props;
  const { apiIsAvailable, setApiIsAvailable, executeDataSync } = useDataSync();
  const [loginResult, setLoginResult] = React.useState<LoginResult | null>(null);
  const [isLoading, setIsLoading] = React.useState<boolean>(false);
  const [isAuthenticated, setIsAuthenticated] = React.useState<boolean>(false);

  React.useEffect(() => {
    checkApiIsAvailable(setApiIsAvailable);
  }, []);

  const loadLoginResultFromStorage = React.useCallback(async () => {
    const resultJson = await SecureStore.getItemAsync(SecureStoreKeyEnum.LoginResult);

    if (resultJson != null) {
      const model: LoginResult = JSON.parse(resultJson);

      console.log('Result Model', model.userName);
      if (model !== undefined) {
        setLoginResult(model);
      }
    }
  }, []);

  React.useEffect(() => {
    if (!apiIsAvailable) {
      loadLoginResultFromStorage();
    } else {
      // loadLoginResultFromStorage();
    }
  }, [apiIsAvailable]);

  const onLogin = React.useCallback(async (data: LoginRequest) => {
    setIsLoading(true);
    if (apiIsAvailable) {
      try {
        await axiosClient.post(endPoints.auth.login, JSON.stringify(data)).then(async (res) => {
          if (res.status === 200) {
            const responseData: ResponseMessage<LoginResult> = res.data;

            if (responseData.success && responseData.data) {
              axiosClient.defaults.headers.common['Authorization'] = `bearer ${responseData.data.jwt}`;
              setLoginResult(responseData.data);

              SecureStore.setItem(SecureStoreKeyEnum.LoginResult, JSON.stringify(responseData.data));

              await executeDataSync(responseData.data.userId);

              setIsAuthenticated(true);
            }
          }
        });
      } catch (err) {
        console.log(err);
      }

      setIsLoading(false);
    }
  }, []);

  const onLogout = React.useCallback(async () => {
    await SecureStore.deleteItemAsync(SecureStoreKeyEnum.LoginResult);
    setLoginResult(null);
    setIsAuthenticated(false);
  }, []);

  const model: AuthState = { isLoading, isAuthenticated, apiIsAvailable, loginResult, onLogin, onLogout };

  return <AuthContext.Provider value={model}>{children}</AuthContext.Provider>;
};

export default AuthContextProvider;
