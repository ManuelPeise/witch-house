import React from 'react';
import PrivatePageWrapper from '../../../_components/_wrappers/PrivatePageWrapper';
import books from '../../../img/schoolBooks.png';
import { RouteProp, useNavigation, useRoute } from '@react-navigation/native';
import { TrainingRouteParam, RootParamList } from '../../../_stacks/PrivateStack';
import { UnitTypeEnum } from '../../../_lib/enums/MathUnitTypeEnum';
import { StyleSheet, Text, View } from 'react-native';
import { useStorage } from '../../../_hooks/useStorage';
import { SchoolModuleSync } from '../../../_lib/sync';
import { AsyncStorageKeyEnum } from '../../../_lib/enums/AsyncStorageKeyEnum';
import { ModuleSettingsTypeEnum } from '../../../_lib/enums/ModuleSettingsTypeEnum';
import SubmitButton from '../../../_components/_inputs/SubmitButton';
import { useI18n } from '../../../_hooks/useI18n';
import { FontSizeEnum } from '../../../_lib/enums/FontSizeEnum';
import { ColorEnum } from '../../../_lib/enums/ColorEnum';
import { BorderRadiusEnum } from '../../../_lib/enums/BorderRadiusEnum';
import MultipleChoiceForm from '../../../_components/_forms/MultipleChoiceForm';
import { Training, TrainingResultModel } from './types';
import { UnitCreator } from './unitCreator';
import TrainingResult from './TrainingResult';
import { NavigationTypeEnum } from '../../../_lib/enums/NavigationTypeEnum';
import { useAuth } from '../../../_hooks/useAuth';

const TrainingPage: React.FC = () => {
  const { getResource } = useI18n();
  const { navigate } = useNavigation();
  const { params } = useRoute<RouteProp<RootParamList>>();
  const { model } = useStorage<SchoolModuleSync[]>(AsyncStorageKeyEnum.SchoolModules);
  const { loginResult } = useAuth();
  const trainingResultStorage = useStorage<TrainingResultModel[]>(AsyncStorageKeyEnum.TrainingResults);

  const [training, setTraining] = React.useState<Training | null>(null);

  const routeParam: TrainingRouteParam = React.useMemo(() => {
    return params as TrainingRouteParam;
  }, [params]);

  const trainingTitle = React.useMemo(() => {
    switch (routeParam.rule) {
      case UnitTypeEnum.Addition:
      case UnitTypeEnum.Subtract:
      case UnitTypeEnum.Multiply:
      case UnitTypeEnum.Divide:
        return '';
      case UnitTypeEnum.Letters:
        return getResource('common:titleLetterUnit');
    }
  }, [routeParam, getResource]);

  React.useEffect(() => {
    if (routeParam != null && model) {
      const moduleSettingsType =
        routeParam.rule === UnitTypeEnum.Letters
          ? ModuleSettingsTypeEnum.GermanUnits
          : ModuleSettingsTypeEnum.MathUnits;

      const moduleSettings =
        model.find((x) => x.moduleSettings.moduleSettingsType === moduleSettingsType)?.moduleSettings.settings ?? null;

      setTraining({
        title: trainingTitle,
        isRunning: false,
        isFinished: false,
        unitType: routeParam.rule,
        settings: moduleSettings,
        trainingData: [],
      });
    }
  }, [routeParam, trainingTitle, model]);

  const handleStart = React.useCallback(async () => {
    const moduleSettingsType =
      routeParam.rule === UnitTypeEnum.Letters ? ModuleSettingsTypeEnum.GermanUnits : ModuleSettingsTypeEnum.MathUnits;

    const moduleSettings =
      model.find((x) => x.moduleSettings.moduleSettingsType === moduleSettingsType)?.moduleSettings.settings ?? null;

    const unitCreator = new UnitCreator(routeParam.rule, moduleSettings);

    setTraining({ ...training, trainingData: unitCreator.getTrainingData(), isRunning: true });
  }, [routeParam, training]);

  const handleRestart = React.useCallback(async () => {
    const moduleSettingsType =
      routeParam.rule === UnitTypeEnum.Letters ? ModuleSettingsTypeEnum.GermanUnits : ModuleSettingsTypeEnum.MathUnits;

    const moduleSettings =
      model.find((x) => x.moduleSettings.moduleSettingsType === moduleSettingsType)?.moduleSettings.settings ?? null;

    const unitCreator = new UnitCreator(routeParam.rule, moduleSettings);
    setTraining({ ...training, trainingData: unitCreator.getTrainingData(), isRunning: true, isFinished: false });
  }, [routeParam, training]);

  const getResultValues = React.useCallback(() => {
    let success = 0;
    let failed = 0;

    training.trainingData.forEach((t) => {
      if (t.result.key === 0) {
        success = success + 1;
      } else {
        failed = failed + 1;
      }
    });

    return { success: success, failed: failed };
  }, [training]);

  const saveTrainingResult = React.useCallback(async () => {
    const existingResults = trainingResultStorage.model != null ? trainingResultStorage.model.slice() : [];

    console.log(existingResults);
    const date: Date = new Date();

    const result = getResultValues();

    const resultToAdd: TrainingResultModel = {
      unitKey: training.unitType,
      userId: loginResult.userId,
      familyId: loginResult.familyGuid,
      timeStamp: `${date.getFullYear()}-${date.getMonth()}-${date.getDate()}`,
      success: result.success,
      failed: result.failed,
    };

    existingResults.push(resultToAdd);

    await trainingResultStorage.storeItem(existingResults);
  }, [loginResult, trainingResultStorage, getResultValues]);

  const handleFinishTraining = React.useCallback(async () => {
    await saveTrainingResult();
    setTraining({ ...training, isRunning: false, isFinished: true });
  }, [training]);

  const handleQuit = React.useCallback(async () => {
    await saveTrainingResult();
    setTraining({ ...training, isRunning: false, isFinished: false });
    navigate(NavigationTypeEnum.TrainingOverview as never);
  }, [training, navigate]);

  return (
    <PrivatePageWrapper image={books}>
      {training != null && (
        <View style={styles.container}>
          {training.isRunning && !training.isFinished && (
            <MultipleChoiceForm training={training} handleFinishTraining={handleFinishTraining} />
          )}
          {training.isFinished && (
            <TrainingResult training={training} handleRestart={handleRestart} handleQuit={handleQuit} />
          )}

          {!training.isRunning && !training.isFinished && (
            <View style={styles.buttonContainer}>
              <SubmitButton
                label={getResource('common:labelStart')}
                color={ColorEnum.White}
                backGround="blue"
                borderColor={ColorEnum.Blue}
                fontSize={FontSizeEnum.md}
                borderRadius={BorderRadiusEnum.Large}
                onPress={handleStart}
              />
            </View>
          )}
        </View>
      )}
    </PrivatePageWrapper>
  );
};

const styles = StyleSheet.create({
  container: {
    height: '100%',
    margin: 20,
    display: 'flex',
    justifyContent: 'center',
  },
  buttonContainer: {
    height: '100%',
    padding: 30,
    display: 'flex',
    justifyContent: 'flex-end',
  },
});
export default TrainingPage;
