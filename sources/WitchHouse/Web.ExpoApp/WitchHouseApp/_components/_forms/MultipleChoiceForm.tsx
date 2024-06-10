import React from 'react';
import { StyleSheet, View, Text, Pressable } from 'react-native';
import { BorderRadiusEnum } from '../../_lib/enums/BorderRadiusEnum';
import { ColorEnum } from '../../_lib/enums/ColorEnum';
import { Training } from '../../_screens/private/_training/types';

interface IProps {
  training: Training;
  handleFinishTraining: () => void;
}

const MultipleChoiceForm: React.FC<IProps> = (props) => {
  const { training, handleFinishTraining } = props;

  const [currentUnit, setCurrentUnit] = React.useState<number>(0);

  const handleResolve = React.useCallback(
    (optionKey: number) => {
      const selectedOption = training.trainingData[currentUnit].options.find((x) => x.key === optionKey);

      if (selectedOption !== undefined) {
        training.trainingData[currentUnit].result = selectedOption;

        if (currentUnit + 1 <= training.trainingData.length - 1) {
          setCurrentUnit(currentUnit + 1);
        } else {
          handleFinishTraining();
        }
      }
    },
    [currentUnit, training, handleFinishTraining]
  );
  return (
    <View style={styles.container}>
      <View style={styles.unitTitleContainer}>
        <Text style={styles.unitTitle}>{training.title}</Text>
      </View>
      <View style={styles.unitLabelContainer}>
        <Text style={styles.unitLabel}>{training.trainingData[currentUnit].unitLabel}</Text>
      </View>
      <View style={styles.optionContainer}>
        {training.trainingData[currentUnit].options.map((opt, index) => {
          return (
            <Pressable key={index} style={styles.option} onPress={handleResolve.bind(null, opt.key)}>
              <Text style={styles.optionText}>{opt.value}</Text>
            </Pressable>
          );
        })}
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    padding: 30,
    height: 'auto',
    width: '100%',
    display: 'flex',
    opacity: 0.9,
    backgroundColor: ColorEnum.Lightblue,
    borderRadius: BorderRadiusEnum.Medium,
  },
  unitTitleContainer: {
    width: '100%',
    padding: 2,
    marginBottom: 30,
  },
  unitTitle: {
    textAlign: 'center',
    fontSize: 30,
  },
  unitLabelContainer: {
    borderWidth: 2,
    borderColor: ColorEnum.Blue,
    borderRadius: BorderRadiusEnum.Medium,
    width: '100%',
    padding: 5,
    marginBottom: 10,
  },
  unitLabel: {
    textAlign: 'center',
    fontSize: 30,
    fontWeight: '800',
  },
  optionContainer: {
    marginTop: 10,
    display: 'flex',
  },
  option: {
    margin: 10,
    padding: 10,
    width: 'auto',
    borderWidth: 2,
    borderColor: ColorEnum.White,
    borderRadius: BorderRadiusEnum.Small,
  },
  optionText: {
    fontSize: 28,
    textAlign: 'center',
    fontWeight: '600',
  },
});
export default MultipleChoiceForm;
