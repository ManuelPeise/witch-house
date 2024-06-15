import React from 'react';
import { View, Text, StyleSheet } from 'react-native';
import PrivatePageWrapper from '../../../_components/_wrappers/PrivatePageWrapper';
import settings from '../../../img/settings.jpg';
import { useStorage } from '../../../_hooks/useStorage';
import { AppSettings } from '../../../_lib/types';
import { AsyncStorageKeyEnum } from '../../../_lib/enums/AsyncStorageKeyEnum';
import SettingsItem from './SettingsItem';
import { useI18n } from '../../../_hooks/useI18n';
import SwitchWithLabel from '../../../_components/_inputs/SwitchWithLabel';

const SettingsScreen: React.FC = () => {
  const { getResource } = useI18n();
  const { model, storeItem } = useStorage<AppSettings>(AsyncStorageKeyEnum.AppSettings);

  const onDataSyncChanged = React.useCallback(async (checked: boolean) => {
    const appSettings = { ...model, syncData: checked };
    await storeItem(appSettings);
  }, []);

  if (model == null) {
    return null;
  }

  return (
    <PrivatePageWrapper image={settings}>
      <View style={styles.wrapper}>
        <SettingsItem title={getResource('common:labelDataSettings')}>
          <SwitchWithLabel
            label={getResource('common:labelSyncData')}
            checked={model?.syncData ?? false}
            onChange={onDataSyncChanged}
          />
        </SettingsItem>
      </View>
    </PrivatePageWrapper>
  );
};

const styles = StyleSheet.create({
  wrapper: {
    padding: 4,
    marginTop: 8,
  },
  buttonWrapper: {
    marginTop: 10,
    padding: 4,
  },
});
export default SettingsScreen;
