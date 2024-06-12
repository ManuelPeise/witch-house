import React from 'react';
import { ApiOptions, ApiResult, JwtData } from '../_lib/api/types';
import * as SecureStore from 'expo-secure-store';
import { AsyncStorageKeyEnum } from '../_lib/enums/AsyncStorageKeyEnum';
import { LoginResult } from '../_lib/types';
import axiosClient from '../_lib/api/axiosClient';
import { SecureStoreKeyEnum } from '../_lib/enums/SecureStoreKeyEnum';

export const useApi = <TModel>(): ApiResult<TModel> => {
  const [isLoading, setIsLoading] = React.useState<boolean>(false);
  const [data, setData] = React.useState<TModel | null>(null);

  const get = React.useCallback(async (options: ApiOptions) => {}, []);

  const post = React.useCallback(async (options: ApiOptions, model: any) => {}, []);

  const sendPostRequest = React.useCallback(async (serviceUrl: string, requestModel: any): Promise<boolean> => {
    let result = false;
    try {
      const auth: JwtData = await JSON.parse(await SecureStore.getItemAsync(SecureStoreKeyEnum.Jwt));
      if (auth.jwtToken) {
        axiosClient.defaults.headers.common['Authorization'] = `bearer ${auth.jwtToken}`;

        await axiosClient.post(serviceUrl, JSON.stringify(requestModel)).then(async (res) => {
          if (res.status === 200) {
            result = true;
          }
        });
      } else {
        result = false;
      }
    } catch (err) {
      console.log(err);
    } finally {
      return result;
    }
  }, []);

  return {
    isLoading,
    data,
    get,
    post,
    sendPostRequest,
  };
};
