import React, { PropsWithChildren, createContext, useContext } from 'react';
import axiosClient from './api/axiosClient';
import { endpoints } from './api/apiConfiguration';
import { useI18n } from '../hooks/useI18n';
import { AuthResult, LoginData, LoginResult, ResponseMessage } from './api/types';

const AuthContext = createContext<AuthResult>({} as AuthResult);

const AuthenticationContext: React.FC<PropsWithChildren> = (props) => {
  const { children } = props;
  const { changeLanguage } = useI18n();
  const [loginResult, setLoginResult] = React.useState<LoginResult | null>(null);

  const onLogin = React.useCallback(
    async (loginData: LoginData) => {
      await axiosClient.post(endpoints.login, loginData).then((res) => {
        if (res.status === 200) {
          const result: ResponseMessage<LoginResult> = res.data;

          if (result && result.data.jwtData.jwtToken) {
            axiosClient.defaults.headers.common['Authorization'] = `bearer ${result.data.jwtData.jwtToken}`;

            setLoginResult(result.data);

            changeLanguage(result.data.userData.language ?? 'en');
          }
        }
      });
    },
    [changeLanguage]
  );

  const onLogout = React.useCallback(() => {
    axiosClient.defaults.headers.common['Authorization'] = ``;
    setLoginResult(null);
  }, []);

  const onRegister = React.useCallback(async (json: string) => {
    let registerResult = false;
    await axiosClient.post(endpoints.registerFamily, json).then((res) => {
      if (res.status === 200) {
        const responseData: boolean = res.data;

        registerResult = responseData;
      }
    });

    return registerResult;
  }, []);

  return <AuthContext.Provider value={{ loginResult, onLogin, onLogout, onRegister }}>{children}</AuthContext.Provider>;
};

export default AuthenticationContext;

export const useAuth = () => {
  return useContext(AuthContext);
};
