import React from 'react';
import * as SecureStore from 'expo-secure-store';
import axiosClient from '../_lib/api/axiosClient';
import { SecureStoreKeyEnum } from '../_lib/enums/SecureStoreKeyEnum';
import { endPoints } from '../_lib/api/apiConfiguration';
import { checkApiIsAvailable } from '../_lib/api/apiUtils';
import { DataSyncImportModel, DataSyncModel } from '../_lib/sync';
import { UserData } from '../_lib/types';

export const useDataSync = () => {
  const [apiIsAvailable, setApiIsAvailable] = React.useState<boolean>(false);

  React.useEffect(() => {
    checkApiIsAvailable(setApiIsAvailable);
  }, []);

  const syncAppData = React.useCallback(async (userData: UserData, jwt: string): Promise<DataSyncModel | null> => {
    let responseData: DataSyncModel = null;
    try {
      axiosClient.defaults.headers.common['Authorization'] = `bearer ${jwt}`;

      const model: DataSyncImportModel = {
        userId: userData.userId,
        familyId: userData.familyGuid,
        roleId: userData.userRole,
      };

      await axiosClient.post(endPoints.sync.syncAppData, JSON.stringify(model)).then(async (res) => {
        if (res.status === 200) {
          responseData = res.data;

          SecureStore.setItem(SecureStoreKeyEnum.UserData, JSON.stringify(responseData.userData));
          SecureStore.setItem(SecureStoreKeyEnum.ModuleConfiguration, JSON.stringify(responseData.moduleConfiguration));
          SecureStore.setItem(
            SecureStoreKeyEnum.TrainingModuleSettings,
            JSON.stringify(responseData.schoolModulesSettings)
          );
        } else if (res.status === 401) {
          console.log('Unauthorized');
        }
      });
    } catch (err) {
      console.log(err);
    } finally {
    }

    return responseData;
  }, []);

  const executeDataSync = React.useCallback(
    async (userData: UserData, jwt: string) => {
      await syncAppData(userData, jwt);
    },
    [syncAppData]
  );

  return {
    apiIsAvailable,
    setApiIsAvailable,
    executeDataSync,
    syncAppData,
  };
};
