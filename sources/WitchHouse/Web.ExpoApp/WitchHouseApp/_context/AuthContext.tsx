import React, { PropsWithChildren } from 'react';
import { AuthState, LoginRequest, LoginResult } from '../_lib/types';
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
  const { apiIsAvailable, setApiIsAvailable, saveLoginResult } = useDataSync();
  const [loginResult, setLoginResult] = React.useState<LoginResult | null>(null);
  const [isLoading, setIsLoading] = React.useState<boolean>(false);
  const [isAuthenticated, setIsAuthenticated] = React.useState<boolean>(false);

  React.useEffect(() => {
    checkApiIsAvailable(setApiIsAvailable);
  }, []);

  const loadLoginResultFromStorage = React.useCallback((): LoginResult | null => {
    const resultJson = SecureStore.getItem(SecureStoreKeyEnum.LoginResult);

    if (resultJson == null) {
      return null;
    }
    const model: LoginResult = JSON.parse(resultJson);

    return model ?? null;
  }, []);

  React.useEffect(() => {
    var result = loadLoginResultFromStorage();

    if (result != null) {
      setLoginResult(result);
      setIsAuthenticated(true);
    }
  }, []);

  const onLogin = React.useCallback(async (data: LoginRequest) => {
    setIsLoading(true);

    try {
      await axiosClient.post(endPoints.auth.login, JSON.stringify(data)).then(async (res) => {
        if (res.status === 200) {
          const responseData: ResponseMessage<LoginResult> = res.data;

          if (responseData.success && responseData.data) {
            setLoginResult(responseData.data);

            const tokenData: JwtData = {
              jwtToken: responseData.data.jwt,
              refreshToken: responseData.data.refreshToken,
            };

            SecureStore.setItem(SecureStoreKeyEnum.LoginResult, JSON.stringify(responseData.data));
            SecureStore.setItem(SecureStoreKeyEnum.Jwt, JSON.stringify(tokenData));

            await saveLoginResult(responseData.data);

            axiosClient.defaults.headers.common['Authorization'] = `bearer ${responseData.data.jwt}`;
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
    await SecureStore.deleteItemAsync(SecureStoreKeyEnum.LoginResult);
    await SecureStore.deleteItemAsync(SecureStoreKeyEnum.Jwt);
    setLoginResult(null);
    setIsAuthenticated(false);
  }, []);

  const model: AuthState = { isLoading, isAuthenticated, apiIsAvailable, loginResult, onLogin, onLogout };

  return <AuthContext.Provider value={model}>{children}</AuthContext.Provider>;
};

export default AuthContextProvider;
