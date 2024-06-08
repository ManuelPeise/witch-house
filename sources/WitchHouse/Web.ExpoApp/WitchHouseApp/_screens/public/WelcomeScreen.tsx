import React from 'react';
import { StyleSheet, Text, View } from 'react-native';
import AuthPageWrapper from '../../_components/_wrappers/AuthPageWrapper';
import LinkButton from '../../_components/_inputs/LinkButton';
import { NavigationTypeEnum } from '../../_lib/enums/NavigationTypeEnum';
import { useI18n } from '../../_hooks/useI18n';
import { useStorage } from '../../_hooks/useStorage';
import { AppSettings } from '../../_lib/types';
import { AsyncStorageKeyEnum } from '../../_lib/enums/AsyncStorageKeyEnum';

const WelcomeScreen: React.FC = () => {
  const { getResource } = useI18n();
  const { model, storeItem } = useStorage<AppSettings>(AsyncStorageKeyEnum.AppSettings);

  const setDefaultStorageData = React.useCallback(async () => {
    if (model == null) {
      console.log('[welcome screen - ~17]Set app settings..');
      const settings: AppSettings = { syncData: true };
      await storeItem(settings);
    }
  }, []);

  React.useEffect(() => {
    setDefaultStorageData();
  }, [model]);

  return (
    <AuthPageWrapper>
      <View style={styles.container}>
        <LinkButton to={NavigationTypeEnum.Login} label={getResource('common:labelStart')} />
      </View>
    </AuthPageWrapper>
  );
};

const styles = StyleSheet.create({
  container: {
    height: '100%',
    width: '100%',
    display: 'flex',
    justifyContent: 'flex-end',
    padding: 8,
    paddingBottom: 32,
  },
});
export default WelcomeScreen;
