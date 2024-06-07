import React from 'react';
import { ApiOptions, ApiResult } from '../_lib/api/types';

export const useApi = <TModel>(): ApiResult<TModel> => {
  const [isLoading, setIsLoading] = React.useState<boolean>(false);
  const [data, setData] = React.useState<TModel | null>(null);

  const get = React.useCallback(async (options: ApiOptions) => {}, []);

  const post = React.useCallback(async (options: ApiOptions, model: any) => {}, []);

  return {
    isLoading,
    data,
    get,
    post,
  };
};
