import React from 'react';
import { StyleSheet, View } from 'react-native';
import TrainingOverview from './TrainingOverview';
import PrivatePageWrapper from '../../../_components/_wrappers/PrivatePageWrapper';
import school from '../../../img/schoolBg.jpg';
import { ModuleSettings } from '../../../_lib/types';
import { useAuth } from '../../../_hooks/useAuth';
import { ModuleTypeEnum } from '../../../_lib/enums/ModuleTypeEnum';
import { ModuleSettingsTypeEnum } from '../../../_lib/enums/ModuleSettingsTypeEnum';

const TrainingScreen: React.FC = () => {
  const { getUserModule } = useAuth();

  const userModule = getUserModule(ModuleTypeEnum.SchoolTraining);

  const moduleSettings = React.useMemo((): ModuleSettings[] => {
    return [
      {
        userId: userModule?.accountGuid,
        moduleType: userModule?.moduleType,
        moduleSettingsType: ModuleSettingsTypeEnum.MathUnits,
        settings: userModule?.settingsJson != null ? JSON.parse(userModule.settingsJson) : {},
      },
      {
        userId: userModule?.accountGuid,
        moduleType: userModule?.moduleType,
        moduleSettingsType: ModuleSettingsTypeEnum.GermanUnits,
        settings: userModule?.settingsJson != null ? JSON.parse(userModule.settingsJson) : {},
      },
    ];
  }, [userModule]);

  if (moduleSettings == null) {
    return null;
  }

  return (
    <PrivatePageWrapper image={school}>
      <View style={styles.container}>
        {moduleSettings.map((module, index) => {
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
