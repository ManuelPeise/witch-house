import React from 'react';
import * as SecureStore from 'expo-secure-store';
import axiosClient from '../_lib/api/axiosClient';
import { JwtData } from '../_lib/api/types';
import { SecureStoreKeyEnum } from '../_lib/enums/SecureStoreKeyEnum';
import { endPoints } from '../_lib/api/apiConfiguration';
import { checkApiIsAvailable } from '../_lib/api/apiUtils';
import { DataSyncImportModel, DataSyncModel } from '../_lib/sync';

export const useDataSync = () => {
  const [apiIsAvailable, setApiIsAvailable] = React.useState<boolean>(false);
  const [syncedData, setSyncedData] = React.useState<DataSyncModel | null>(null);
  const [isLoading, setIsLoading] = React.useState<boolean>(false);

  React.useEffect(() => {
    checkApiIsAvailable(setApiIsAvailable);
  }, []);

  // TODO refactor to sync unitSettings and unitResults

  const syncAppData = React.useCallback(async (userGuid: string) => {
    const model: DataSyncImportModel = { userId: userGuid };
    const tokenDataJson = SecureStore.getItem(SecureStoreKeyEnum.Jwt) ?? null;

    if (tokenDataJson != null) {
      const tokenData: JwtData = JSON.parse(tokenDataJson);
      try {
        setIsLoading(true);

        axiosClient.defaults.headers.common['Authorization'] = `bearer ${tokenData.jwtToken}`;

        await axiosClient.post(endPoints.sync.syncAppData, JSON.stringify(model)).then(async (res) => {
          if (res.status === 200) {
            setSyncedData(res.data);
          } else if (res.status === 401) {
            console.log('Unauthorized');
          }
        });
      } catch (err) {
        console.log(err);
      } finally {
        setIsLoading(false);
      }
    }
  }, []);

  return {
    isLoading,
    data: syncedData,
    apiIsAvailable,
    setApiIsAvailable,
  };
};
