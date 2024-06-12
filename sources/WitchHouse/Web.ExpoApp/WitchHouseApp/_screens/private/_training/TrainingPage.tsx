import React from 'react';
import PrivatePageWrapper from '../../../_components/_wrappers/PrivatePageWrapper';
import books from '../../../img/schoolBooks.png';
import { RouteProp, useNavigation, useRoute } from '@react-navigation/native';
import { TrainingRouteParam, RootParamList } from '../../../_stacks/PrivateStack';
import { UnitTypeEnum } from '../../../_lib/enums/UnitTypeEnum';
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
import { Training, TrainingResultExportModel, TrainingResultModel } from './types';
import { UnitCreator } from './unitCreator';
import TrainingResult from './TrainingResult';
import { NavigationTypeEnum } from '../../../_lib/enums/NavigationTypeEnum';
import { useAuth } from '../../../_hooks/useAuth';
import { useApi } from '../../../_hooks/useApi';
import { endPoints } from '../../../_lib/api/apiConfiguration';

const TrainingPage: React.FC = () => {
  const { getResource } = useI18n();
  const { navigate } = useNavigation();
  const { params } = useRoute<RouteProp<RootParamList>>();
  const { model } = useStorage<SchoolModuleSync[]>(AsyncStorageKeyEnum.SchoolModules);
  const { loginResult } = useAuth();
  const trainingResultStorage = useStorage<TrainingResultModel[]>(AsyncStorageKeyEnum.TrainingResults);
  const { sendPostRequest } = useApi<TrainingResultExportModel>();

  const [training, setTraining] = React.useState<Training | null>(null);

  const existingTrainingResults = React.useMemo(() => {
    return trainingResultStorage.model != null ? trainingResultStorage.model.slice() : [];
  }, [trainingResultStorage]);

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

  const handleSaveTrainingResults = React.useCallback(async () => {
    const exportModels: TrainingResultExportModel[] = existingTrainingResults.map((res) => {
      return {
        userId: res.userId,
        unitType: res.unitKey,
        success: res.success,
        failed: res.failed,
        timeStamp: res.timeStamp,
      };
    });

    if (exportModels.length) {
      await sendPostRequest(endPoints.training.saveTrainingResults, exportModels).then(async (res) => {
        if (res) {
          trainingResultStorage.storeItem([]);
        }
      });
    }
  }, [existingTrainingResults]);

  React.useEffect(() => {
    handleSaveTrainingResults();
  }, [handleSaveTrainingResults]);

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
    const result = getResultValues();

    const date: Date = new Date();

    const trainingResult: TrainingResultExportModel = {
      userId: loginResult.userId,
      unitType: routeParam.rule,
      success: result.success,
      failed: result.failed,
      timeStamp: `${date.getFullYear()}-${date.getMonth()}-${date.getDate()} ${date.getHours()}:${date.getMinutes()}`,
    };

    await sendPostRequest(endPoints.training.saveTrainingResult, trainingResult).then(async (res) => {
      if (!res) {
        const resultToAdd: TrainingResultModel = {
          unitKey: training.unitType,
          userId: loginResult.userId,
          familyId: loginResult.familyGuid,
          timeStamp: `${date.getFullYear()}-${date.getMonth()}-${date.getDate()}`,
          success: result.success,
          failed: result.failed,
        };

        existingTrainingResults.push(resultToAdd);

        await trainingResultStorage.storeItem(existingTrainingResults);
      }
    });
  }, [loginResult, trainingResultStorage, getResultValues, sendPostRequest]);

  const handleFinishTraining = React.useCallback(async () => {
    setTraining({ ...training, isRunning: false, isFinished: true });
  }, [training, saveTrainingResult]);

  const handleQuit = React.useCallback(async () => {
    await saveTrainingResult();
    setTraining({ ...training, isRunning: false, isFinished: false });
    navigate(NavigationTypeEnum.TrainingOverview as never);
  }, [training, navigate]);

  const handleRestart = React.useCallback(async () => {
    await saveTrainingResult();
    const moduleSettingsType =
      routeParam.rule === UnitTypeEnum.Letters ? ModuleSettingsTypeEnum.GermanUnits : ModuleSettingsTypeEnum.MathUnits;

    const moduleSettings =
      model.find((x) => x.moduleSettings.moduleSettingsType === moduleSettingsType)?.moduleSettings.settings ?? null;

    const unitCreator = new UnitCreator(routeParam.rule, moduleSettings);
    setTraining({ ...training, trainingData: unitCreator.getTrainingData(), isRunning: true, isFinished: false });
  }, [routeParam, training, saveTrainingResult]);

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
