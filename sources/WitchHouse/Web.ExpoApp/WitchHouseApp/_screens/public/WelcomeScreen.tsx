import React from 'react';
import { StyleSheet, Text, View } from 'react-native';
import AuthPageWrapper from '../../_components/_wrappers/AuthPageWrapper';
import LinkButton from '../../_components/_inputs/LinkButton';
import { NavigationTypeEnum } from '../../_lib/enums/NavigationTypeEnum';
import { useI18n } from '../../_hooks/useI18n';
import { useStorage } from '../../_hooks/useStorage';
import { AppSettings } from '../../_lib/types';
import { AsyncStorageKeyEnum } from '../../_lib/enums/AsyncStorageKeyEnum';
import { ColorEnum } from '../../_lib/enums/ColorEnum';
import { FontSizeEnum } from '../../_lib/enums/FontSizeEnum';
import { BorderRadiusEnum } from '../../_lib/enums/BorderRadiusEnum';

const WelcomeScreen: React.FC = () => {
  const { getResource } = useI18n();
  const { model, storeItem } = useStorage<AppSettings>(AsyncStorageKeyEnum.AppSettings);

  const setDefaultStorageData = React.useCallback(async () => {
    if (model == null) {
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
        <LinkButton
          backGround="transparent"
          padding={3}
          color={ColorEnum.White}
          fontSize={FontSizeEnum.md}
          borderColor={ColorEnum.White}
          to={NavigationTypeEnum.Login}
          borderRadius={BorderRadiusEnum.Medium}
          label={getResource('common:labelStart')}
        />
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
