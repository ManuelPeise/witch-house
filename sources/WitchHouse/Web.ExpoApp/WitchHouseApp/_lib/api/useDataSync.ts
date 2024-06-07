import AsyncStorage from '@react-native-async-storage/async-storage';
import React from 'react';
import { AsyncStorageKeyEnum } from '../enums/AsyncStorageKeyEnum';
import { checkApiIsAvailable } from './apiUtils';
import axiosClient from './axiosClient';
import { endPoints } from './apiConfiguration';
import { DataSyncImportModel, DataSyncModel, SchoolModuleSync, UserDataSync } from '../sync';

export const useDataSync = () => {
  const [apiIsAvailable, setApiIsAvailable] = React.useState<boolean>(false);
  const [syncedData, setSyncedData] = React.useState<DataSyncModel | null>(null);

  const saveSyncedData = React.useCallback(async () => {
    await AsyncStorage.setItem(AsyncStorageKeyEnum.UserData, JSON.stringify(syncedData.userData));
    await AsyncStorage.setItem(AsyncStorageKeyEnum.SchoolModules, JSON.stringify(syncedData.schoolModules));
  }, [syncedData]);

  React.useEffect(() => {
    checkApiIsAvailable(setApiIsAvailable);
  }, []);

  React.useEffect(() => {
    saveSyncedData();
  }, [syncedData]);

  const getUpdatedModel = React.useCallback(async (model: DataSyncModel): Promise<DataSyncModel> => {
    const [userDataJson, schoolModulesJson] = await Promise.all([
      await AsyncStorage.getItem(AsyncStorageKeyEnum.UserData),
      await AsyncStorage.getItem(AsyncStorageKeyEnum.SchoolModules),
    ]);

    const existingUserData: UserDataSync = JSON.parse(userDataJson);
    const schoolModules: SchoolModuleSync = JSON.parse(schoolModulesJson);

    const update: DataSyncModel = {
      userData: { ...existingUserData, ...model.userData },
      schoolModules: { ...schoolModules, ...model.schoolModules },
    };

    return update;
  }, []);

  const syncAppData = React.useCallback(async (userGuid: string) => {
    const model: DataSyncImportModel = { userId: userGuid };

    axiosClient.post(endPoints.sync.syncAppData, model).then(async (res) => {
      if (res.status === 200) {
        const data: DataSyncModel = res.data;
        const update = await getUpdatedModel(data);

        setSyncedData(update);

        console.log('Model from sync...', update);
      }
    });
  }, []);

  const executeDataSync = React.useCallback(async (userId: string) => {
    if (apiIsAvailable) {
      await syncAppData(userId);
    }
  }, []);

  return {
    apiIsAvailable,
    setApiIsAvailable,
    executeDataSync,
  };
};
