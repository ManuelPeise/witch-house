import React, { PropsWithChildren, useReducer } from 'react';
import { AuthState, LoginRequest } from '../_lib/types';
import axiosClient from '../_lib/api/axiosClient';
import { ResponseMessage } from '../_lib/api/types';
import { endPoints } from '../_lib/api/apiConfiguration';
import * as SecureStore from 'expo-secure-store';
import { SecureStoreKeyEnum } from '../_lib/enums/SecureStoreKeyEnum';
import { LoginResult } from '../_lib/_types/user';
import { SqLiteDatabase, UserTableModel } from '../_lib/_types/sqLite';
import { ReducerActions, UserDataReducerState } from '../_reducer/reducerActions';
import { useDispatch, useSelector } from 'react-redux';

export const AuthContext = React.createContext<AuthState>({} as AuthState);

const AuthContextProvider: React.FC<PropsWithChildren> = (props) => {
  const { children } = props;
  const [loginResult, setLoginResult] = React.useState<LoginResult | null>(null);
  const [isLoading, setIsLoading] = React.useState<boolean>(false);
  const [isAuthenticated, setIsAuthenticated] = React.useState<boolean>(false);

  const dispatch = useDispatch();

  const storeUserData = React.useCallback(async (responseData: LoginResult) => {
    SecureStore.setItem(SecureStoreKeyEnum.LoginResult, JSON.stringify(responseData));
  }, []);

  const loadAppData = React.useCallback(
    async (userId: string) => {
      await axiosClient.get(endPoints.sync.loadAppData.replace('{id}', userId)).then(async (res) => {
        if (res.status === 200) {
          const responseMessage: ResponseMessage<SqLiteDatabase> = await JSON.parse(JSON.stringify(res.data));
          setIsAuthenticated(true);
          dispatch({ type: ReducerActions.InitializeData, payload: responseMessage.data });
        }
      });
    },
    [dispatch]
  );

  const onLogin = React.useCallback(
    async (data: LoginRequest) => {
      setIsLoading(true);

      try {
        await axiosClient.post(endPoints.auth.login, JSON.stringify(data)).then(async (res) => {
          if (res.status === 200) {
            const responseData: ResponseMessage<LoginResult> = res.data;

            if (responseData.success && responseData.data) {
              axiosClient.defaults.headers.common['Authorization'] = `bearer ${responseData.data.jwtToken}`;
              await loadAppData(responseData.data.userGuid);
              await storeUserData(responseData.data);
              setLoginResult(responseData.data);
            }
          }
        });
      } catch (err) {
        console.log(err);
      } finally {
        setIsLoading(false);
      }
    },
    [storeUserData, loadAppData]
  );

  const onLogout = React.useCallback(async () => {
    await SecureStore.deleteItemAsync(SecureStoreKeyEnum.LoginResult);

    setLoginResult(null);
    setIsAuthenticated(false);
  }, []);

  const getUserDataReducerState = (): UserTableModel => {
    return useSelector<SqLiteDatabase, UserTableModel>((x) => x.userTableModel);
  };

  const model: AuthState = {
    isLoading,
    isAuthenticated,
    loginResult,
    onLogin,
    onLogout,
    getUserDataReducerState,
  };

  return <AuthContext.Provider value={model}>{children}</AuthContext.Provider>;
};

export default AuthContextProvider;
