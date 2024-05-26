import React from 'react';
import axiosClient from '../lib/api/axiosClient';
import { ApiResult } from '../lib/types';

export const useApi = <T>(): ApiResult<T> => {
  const [error, setError] = React.useState<string>('');
  const [isLoading, setIsLoading] = React.useState<boolean>(false);
  const [data, setData] = React.useState<T | null>(null);

  const get = React.useCallback(async (url: string) => {
    try {
      setIsLoading(true);
      await axiosClient.get(url, { headers: { 'Content-Type': 'application/json' } }).then((res) => {
        //console.log('useApi - 17 - ', res.statusText);
        if (res.status === 200) {
          const responseData: T = res.data;

          setData(responseData);
        }
      });
    } catch (err) {
      setError(err as string);
    } finally {
      setIsLoading(false);
    }
  }, []);

  const post = React.useCallback(async (url: string, json: string) => {
    try {
      setIsLoading(true);

      await axiosClient.post(url, json).then((res) => {
        if (res.status === 200) {
          const responseData: T = res.data;

          if (responseData) {
            setData(responseData);
          }
        }
      });
    } catch (err) {
      setError(err as string);
    } finally {
      setIsLoading(false);
    }
  }, []);

  return {
    data,
    error,
    isLoading,
    get,
    post,
  };
};
