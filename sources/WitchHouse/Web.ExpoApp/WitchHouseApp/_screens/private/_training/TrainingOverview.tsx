import React from 'react';
import { StyleSheet, Text, View } from 'react-native';
import { BorderRadiusEnum } from '../../../_lib/enums/BorderRadiusEnum';
import { ColorEnum } from '../../../_lib/enums/ColorEnum';
import { ModuleSettingsTypeEnum } from '../../../_lib/enums/ModuleSettingsTypeEnum';
import { useI18n } from '../../../_hooks/useI18n';
import { ModuleSettings } from '../../../_lib/sync';
import LinkButton from '../../../_components/_inputs/LinkButton';
import { NavigationTypeEnum } from '../../../_lib/enums/NavigationTypeEnum';
import { UnitTypeEnum } from '../../../_lib/enums/MathUnitTypeEnum';
import { FontSizeEnum } from '../../../_lib/enums/FontSizeEnum';

interface IProps {
  settings: ModuleSettings;
}

const TrainingOverview: React.FC<IProps> = (props) => {
  const { settings } = props;
  const { getResource } = useI18n();

  const title = React.useMemo(() => {
    switch (settings.moduleSettingsType) {
      case ModuleSettingsTypeEnum.MathUnits:
        return getResource('common:titleMathUnits');
      case ModuleSettingsTypeEnum.GermanUnits:
        return getResource('common:titleGermanUnits');
    }
  }, [settings, getResource]);

  return (
    <View style={styles.container}>
      <Text style={styles.title}>{title}</Text>
      <View style={styles.navContainer}>
        {/* render math settings buttons */}
        {settings.moduleSettingsType === ModuleSettingsTypeEnum.MathUnits && settings.settings.allowAddition && (
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
        {settings.moduleSettingsType === ModuleSettingsTypeEnum.MathUnits && settings.settings.allowSubtraction && (
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
        {settings.moduleSettingsType === ModuleSettingsTypeEnum.MathUnits && settings.settings.allowMultiply && (
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
        {settings.moduleSettingsType === ModuleSettingsTypeEnum.MathUnits && settings.settings.allowDivide && (
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
        {/* render german settings buttons */}
        {settings.moduleSettingsType === ModuleSettingsTypeEnum.GermanUnits && (
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
