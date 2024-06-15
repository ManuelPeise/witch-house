import React from 'react';
import { StyleSheet, Text, View } from 'react-native';
import { BorderRadiusEnum } from '../../../_lib/enums/BorderRadiusEnum';
import { ColorEnum } from '../../../_lib/enums/ColorEnum';
import { ModuleSettingsTypeEnum } from '../../../_lib/enums/ModuleSettingsTypeEnum';
import { useI18n } from '../../../_hooks/useI18n';
import LinkButton from '../../../_components/_inputs/LinkButton';
import { NavigationTypeEnum } from '../../../_lib/enums/NavigationTypeEnum';
import { UnitTypeEnum } from '../../../_lib/enums/UnitTypeEnum';
import { FontSizeEnum } from '../../../_lib/enums/FontSizeEnum';
import { ModuleSettings } from '../../../_lib/types';
import * as SecureStore from 'expo-secure-store';
import { SecureStoreKeyEnum } from '../../../_lib/enums/SecureStoreKeyEnum';

interface IPops {
  settingsType: ModuleSettingsTypeEnum;
}

const TrainingOverview: React.FC<IPops> = (props) => {
  const { settingsType } = props;
  const { getResource } = useI18n();

  const loadTrainingSettings = React.useCallback((): ModuleSettings[] => {
    let config: ModuleSettings[] = null;
    const json = SecureStore.getItem(SecureStoreKeyEnum.TrainingModuleSettings);
    if (json != null && json.length) {
      config = JSON.parse(json);
    }
    return config;
  }, []);

  const trainingSettings = React.useMemo(() => {
    const allSettings = loadTrainingSettings();

    return allSettings.find((x) => x.moduleSettingsType === settingsType);
  }, [settingsType, loadTrainingSettings]);

  const title = React.useMemo(() => {
    switch (settingsType) {
      case ModuleSettingsTypeEnum.MathUnits:
        return getResource('common:titleMathUnits');
      case ModuleSettingsTypeEnum.GermanUnits:
        return getResource('common:titleGermanUnits');
    }
  }, [settingsType, getResource]);

  return (
    <View style={styles.container}>
      <Text style={styles.title}>{title}</Text>
      <View style={styles.navContainer}>
        {/* render math settings buttons */}
        {settingsType === ModuleSettingsTypeEnum.MathUnits && trainingSettings?.settings.allowAddition && (
          <LinkButton
            to={NavigationTypeEnum.SchoolTraining}
            label={getResource('common:labelAddition')}
            color={ColorEnum.Blue}
            backGround="blue"
            fontSize={FontSizeEnum.md}
            borderColor={ColorEnum.Blue}
            borderRadius={BorderRadiusEnum.Medium}
            padding={3}
            param={{ rule: UnitTypeEnum.Addition }}
          />
        )}
        {settingsType === ModuleSettingsTypeEnum.MathUnits && trainingSettings?.settings.allowSubtraction && (
          <LinkButton
            to={NavigationTypeEnum.SchoolTraining}
            label={getResource('common:labelSubtract')}
            color={ColorEnum.Blue}
            fontSize={FontSizeEnum.md}
            borderColor={ColorEnum.Blue}
            borderRadius={BorderRadiusEnum.Medium}
            padding={3}
            param={{ rule: UnitTypeEnum.Subtract }}
          />
        )}
        {settingsType === ModuleSettingsTypeEnum.MathUnits && trainingSettings?.settings.allowMultiply && (
          <LinkButton
            to={NavigationTypeEnum.SchoolTraining}
            label={getResource('common:labelMultiply')}
            color={ColorEnum.Blue}
            fontSize={FontSizeEnum.md}
            borderColor={ColorEnum.Blue}
            borderRadius={BorderRadiusEnum.Medium}
            padding={3}
            param={{ rule: UnitTypeEnum.Multiply }}
          />
        )}
        {settingsType === ModuleSettingsTypeEnum.MathUnits && trainingSettings?.settings.allowDivide && (
          <LinkButton
            to={NavigationTypeEnum.SchoolTraining}
            label={getResource('common:labelDivide')}
            color={ColorEnum.Blue}
            fontSize={FontSizeEnum.md}
            borderColor={ColorEnum.Blue}
            borderRadius={BorderRadiusEnum.Medium}
            padding={3}
            param={{ rule: UnitTypeEnum.Divide }}
          />
        )}
        {settingsType === ModuleSettingsTypeEnum.MathUnits && trainingSettings?.settings.allowDoubling && (
          <LinkButton
            to={NavigationTypeEnum.SchoolTraining}
            label={getResource('common:labelDoubling')}
            color={ColorEnum.Blue}
            backGround="blue"
            fontSize={FontSizeEnum.md}
            borderColor={ColorEnum.Blue}
            borderRadius={BorderRadiusEnum.Medium}
            padding={3}
            param={{ rule: UnitTypeEnum.Doubling }}
          />
        )}
        {/* render german settings buttons */}
        {settingsType === ModuleSettingsTypeEnum.GermanUnits && (
          <LinkButton
            to={NavigationTypeEnum.SchoolTraining}
            label={getResource('common:labelLetters')}
            color={ColorEnum.Blue}
            fontSize={FontSizeEnum.md}
            borderColor={ColorEnum.Blue}
            borderRadius={BorderRadiusEnum.Medium}
            padding={3}
            param={{ rule: UnitTypeEnum.Letters }}
          />
        )}
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    margin: 0,
    width: '100%',
    height: 'auto',
    padding: 16,
    borderRadius: BorderRadiusEnum.Small,
    backgroundColor: ColorEnum.White,
    opacity: 0.8,
    display: 'flex',
    flexDirection: 'column',
  },
  title: {
    fontSize: 20,
  },
  navContainer: {
    marginTop: 20,
    display: 'flex',
    gap: 15,
  },
});
export default TrainingOverview;
