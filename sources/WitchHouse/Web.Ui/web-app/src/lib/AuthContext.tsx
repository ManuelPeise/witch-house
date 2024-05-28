import React, { PropsWithChildren, createContext, useContext } from 'react';
import axiosClient from './api/axiosClient';
import { endpoints } from './api/apiConfiguration';
import { useI18n } from '../hooks/useI18n';
import { UserRoleEnum } from './enums/UserRoleEnum';

type LoginData = {
  userName: string;
  secret: string;
};

export type LoginResult = {
  success: boolean;
  userId: string;
  userName: string;
  language: 'en' | 'de';
  jwt: string;
  refreshToken: string;
  userRole: UserRoleEnum;
};

export type AuthResult = {
  loginResult: LoginResult | null;
  onLogin: (loginData: LoginData) => Promise<void>;
  onLogout: () => void;
  onRegister: (json: string) => Promise<boolean>;
};

const AuthContext = createContext<AuthResult>({} as AuthResult);

const AuthenticationContext: React.FC<PropsWithChildren> = (props) => {
  const { children } = props;
  const { changeLanguage } = useI18n();
  const [loginResult, setLoginResult] = React.useState<LoginResult | null>(null);

  const onLogin = React.useCallback(
    async (loginData: LoginData) => {
      await axiosClient.post(endpoints.login, loginData).then((res) => {
        if (res.status === 200) {
          const result: LoginResult = res.data;

          if (result && result.jwt) {
            axiosClient.defaults.headers.common['Authorization'] = `bearer ${result.jwt}`;

            setLoginResult(result);

            changeLanguage(result.language ?? 'en');
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
