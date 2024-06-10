import React from 'react';
import { StyleSheet, Text, View } from 'react-native';
import { useStorage } from '../../../_hooks/useStorage';
import { AsyncStorageKeyEnum } from '../../../_lib/enums/AsyncStorageKeyEnum';
import { SchoolModuleSync } from '../../../_lib/sync';
import TrainingOverview from './TrainingOverview';
import PrivatePageWrapper from '../../../_components/_wrappers/PrivatePageWrapper';
import school from '../../../img/schoolBg.jpg';
const TrainingScreen: React.FC = () => {
  const { model } = useStorage<SchoolModuleSync[]>(AsyncStorageKeyEnum.SchoolModules);

  if (model == null) {
    return null;
  }

  return (
    <PrivatePageWrapper image={school}>
      <View style={styles.container}>
        {model?.map((module, index) => {
          return <TrainingOverview key={index} settings={module.moduleSettings} />;
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
