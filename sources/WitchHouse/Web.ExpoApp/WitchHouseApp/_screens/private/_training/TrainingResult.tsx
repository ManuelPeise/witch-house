import React from 'react';
import { StyleSheet, View, Text } from 'react-native';
import { Training } from './types';
import { ColorEnum } from '../../../_lib/enums/ColorEnum';
import { BorderRadiusEnum } from '../../../_lib/enums/BorderRadiusEnum';
import SubmitButton from '../../../_components/_inputs/SubmitButton';
import { useI18n } from '../../../_hooks/useI18n';
import { FontSizeEnum } from '../../../_lib/enums/FontSizeEnum';

type UnitResult = {
  question: string;
  answer: string;
};

interface IProps {
  training: Training;
  handleRestart: () => Promise<void>;
  handleQuit: () => Promise<void>;
}

const TrainingResult: React.FC<IProps> = (props) => {
  const { training, handleRestart, handleQuit } = props;
  const { getResource } = useI18n();

  const { unitCount, failedUnits, success } = React.useMemo(() => {
    let success = 0;
    let failed = 0;
    const failedUnits: UnitResult[] = [];

    training.trainingData.forEach((unit) => {
      const solution = unit.options.find((x) => x.key === 0);

      if (unit.result.value === solution.value) {
        success = success + 1;
      } else {
        failed = failed + 1;
        failedUnits.push({ question: unit.unitLabel, answer: unit.result.value });
      }
    });

    return { unitCount: training.trainingData.length, failedUnits, success };
  }, [training]);

  return (
    <View style={styles.container}>
      <View style={styles.resultContainer}>
        <Text style={styles.resultLabel}>{`${success}/${unitCount}`}</Text>
      </View>
      <View style={styles.failedResultsContainer}>
        {failedUnits?.map((unit, index) => {
          return (
            <View key={index} style={styles.failedResult}>
              <View style={styles.result}>
                <Text style={[styles.resultText]}>{unit.question}</Text>
                <Text style={[styles.resultText, styles.failedResultText]}>{unit.answer}</Text>
              </View>
            </View>
          );
        })}
        {!failedUnits.length && (
          <View style={styles.failedResult}>
            <Text style={styles.successResult}>{getResource('common:labelAllSuccess')}</Text>
          </View>
        )}
      </View>
      <View style={styles.buttonContainer}>
        <SubmitButton
          label={getResource('common:labelQuit')}
          backGround="blue"
          width={100}
          borderColor={ColorEnum.Blue}
          color={ColorEnum.White}
          borderRadius={BorderRadiusEnum.Medium}
          fontSize={FontSizeEnum.md}
          onPress={handleQuit}
        />
        <SubmitButton
          label={getResource('common:labelRestart')}
          width={100}
          backGround="blue"
          borderColor={ColorEnum.Blue}
          color={ColorEnum.White}
          borderRadius={BorderRadiusEnum.Medium}
          fontSize={FontSizeEnum.md}
          onPress={handleRestart}
        />
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    padding: 30,
    height: 'auto',
    width: '100%',
    backgroundColor: ColorEnum.Lightblue,
    borderRadius: BorderRadiusEnum.Medium,
  },
  resultContainer: {
    display: 'flex',
    alignItems: 'center',
    width: '100%',
    padding: 20,
  },
  resultLabel: {
    textAlign: 'center',
    fontSize: 30,
    fontWeight: '600',
  },
  failedResultsContainer: {
    display: 'flex',
    alignContent: 'center',
    justifyContent: 'center',
  },
  failedResult: {
    width: '100%',
    display: 'flex',
    flexDirection: 'row',
    justifyContent: 'center',
  },
  result: {
    display: 'flex',
    justifyContent: 'center',
    flexDirection: 'row',
  },
  successResult: {
    width: '100%',
    textAlign: 'center',
    fontSize: 20,
    color: 'green',
  },
  resultText: {
    width: '50%',
    textAlign: 'center',
    fontSize: 20,
  },
  failedResultText: {
    color: 'red',
  },
  buttonContainer: {
    marginTop: 40,
    width: '100%',
    display: 'flex',
    justifyContent: 'flex-end',
    flexDirection: 'row',
    gap: 10,
  },
});
export default TrainingResult;
