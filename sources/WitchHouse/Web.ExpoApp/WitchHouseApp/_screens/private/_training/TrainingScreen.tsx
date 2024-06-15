import React from 'react';
import { StyleSheet, View } from 'react-native';
import TrainingOverview from './TrainingOverview';
import PrivatePageWrapper from '../../../_components/_wrappers/PrivatePageWrapper';
import school from '../../../img/schoolBg.jpg';
import { ModuleSettings } from '../../../_lib/types';
import * as SecureStore from 'expo-secure-store';
import { SecureStoreKeyEnum } from '../../../_lib/enums/SecureStoreKeyEnum';

const TrainingScreen: React.FC = () => {
  const trainingSettings = React.useMemo((): ModuleSettings[] => {
    let config: ModuleSettings[] = null;
    const json = SecureStore.getItem(SecureStoreKeyEnum.TrainingModuleSettings);

    if (json != null && json.length) {
      config = JSON.parse(json);
    }

    return config;
  }, []);

  if (trainingSettings == null) {
    return null;
  }

  return (
    <PrivatePageWrapper image={school}>
      <View style={styles.container}>
        {trainingSettings?.map((module, index) => {
          return <TrainingOverview key={index} settingsType={module.moduleSettingsType} />;
        })}
      </View>
    </PrivatePageWrapper>
  );
};

const styles = StyleSheet.create({
  container: {
    display: 'flex',
    gap: 20,
    padding: 10,
    height: '100%',
  },
});
export default TrainingScreen;
