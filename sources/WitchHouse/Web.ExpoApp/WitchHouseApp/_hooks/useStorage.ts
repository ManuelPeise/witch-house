import React from 'react';
import { AsyncStorageKeyEnum } from '../_lib/enums/AsyncStorageKeyEnum';
import AsyncStorage from '@react-native-async-storage/async-storage';

export const useStorage = <TModel>(key: AsyncStorageKeyEnum) => {
  const [json, setJson] = React.useState<string | null>(null);

  const loadDataFromStorage = React.useCallback(async () => {
    const jsonString = await AsyncStorage.getItem(key);

    if (jsonString != null && jsonString.length) {
      setJson(jsonString);
    }
  }, []);

  React.useEffect(() => {
    loadDataFromStorage();
  }, [loadDataFromStorage]);

  const storeItem = React.useCallback(
    async (data: TModel) => {
      console.log(model);
      await AsyncStorage.setItem(key, JSON.stringify(data)).then(async () => {
        await loadDataFromStorage();
      });
    },
    [loadDataFromStorage]
  );

  const model = React.useMemo((): TModel | null => {
    if (json == null || !json.length) {
      return null;
    }

    const model: TModel = JSON.parse(json);

    return model;
  }, [json]);

  return {
    model: model,
    storeItem,
  };
};
