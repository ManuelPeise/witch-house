import { UnitTypeEnum } from '../../../_lib/enums/UnitTypeEnum';
import { SchoolSettings } from '../../../_lib/sync';
import { letters } from './constants';
import { TrainingData, TrainingOption } from './types';

export class UnitCreator {
  private _rule: UnitTypeEnum;
  private _settings: SchoolSettings;
  private _unitCount = 5;

  constructor(rule: UnitTypeEnum, settings: SchoolSettings) {
    this._rule = rule;
    this._settings = settings;
  }

  public getTrainingData(): TrainingData[] {
    switch (this._rule) {
      case UnitTypeEnum.Addition:
        return this.getMathUnitTrainingData();
      case UnitTypeEnum.Subtract:
        return this.getMathUnitTrainingData();
      case UnitTypeEnum.Multiply:
        return this.getMathUnitTrainingData();
      case UnitTypeEnum.Divide:
        return this.getMathUnitTrainingData();
      case UnitTypeEnum.Letters:
        return this.getLetterTrainingData();
    }
  }

  private getMathUnitTrainingData(): TrainingData[] {
    const trainingDataCollection: TrainingData[] = [];

    do {
      const firstValue = this.getRandomNumber(this._settings.maxValue, 1);
      const secondValue = this.getRandomNumber(this._settings.maxValue, 1);

      if (this.isValidCalculation(firstValue, secondValue, trainingDataCollection)) {
        trainingDataCollection.push({
          unitLabel: this.getUnitLabel(firstValue, secondValue),
          result: null,
          options: this.getMathOptions(firstValue, secondValue),
        });
      } else {
      }
    } while (trainingDataCollection.length < this._unitCount);

    return trainingDataCollection;
  }

  private getMathOptions(firstValue: number, secondValue: number): TrainingOption[] {
    let key: number = 1;
    const excluded: number[] = [this.getMathResult(firstValue, secondValue)];
    const options: TrainingOption[] = [{ key: 0, value: excluded[0].toString() }];

    do {
      const value = this.getRandomNumber(letters.length - 1, 0);

      if (excluded.includes(value)) {
        continue;
      }

      options.push({ key: key, value: value.toString() });
      excluded.push(value);
    } while (options.length < 4);

    return this.shuffleOptions(options);
  }

  private getMathResult(firstValue: number, secondValue: number): number {
    switch (this._rule) {
      case UnitTypeEnum.Addition:
        return firstValue + secondValue;
      case UnitTypeEnum.Subtract:
        return Math.max(firstValue, secondValue) - Math.min(firstValue, secondValue);
      case UnitTypeEnum.Multiply:
        return firstValue * secondValue;
      case UnitTypeEnum.Divide:
        return Math.max(firstValue, secondValue) / Math.min(firstValue, secondValue);
    }
  }

  private getUnitLabel(firstValue: number, secondValue: number): string {
    let label = '';

    switch (this._rule) {
      case UnitTypeEnum.Addition:
        return `${firstValue} + ${secondValue} =`;
      case UnitTypeEnum.Subtract:
        return `${Math.max(firstValue, secondValue)} - ${Math.min(firstValue, secondValue)} =`;
      case UnitTypeEnum.Multiply:
        return `${firstValue} x ${secondValue} =`;
      case UnitTypeEnum.Divide:
        return `${Math.max(firstValue, secondValue)} : ${Math.min(firstValue, secondValue)} =`;
    }

    return label;
  }

  private isValidCalculation(firstValue: number, secondValue: number, trainingData: TrainingData[]) {
    let result: number = 0;

    switch (this._rule) {
      case UnitTypeEnum.Addition:
        result = firstValue + secondValue;
        break;
      case UnitTypeEnum.Subtract:
        result = Math.max(firstValue, secondValue) - Math.min(firstValue, secondValue);
        break;
      case UnitTypeEnum.Multiply:
        result = firstValue * secondValue;
        break;
      case UnitTypeEnum.Divide:
        result = Math.max(firstValue, secondValue) / Math.min(firstValue, secondValue);
        break;
    }

    if (this._rule === UnitTypeEnum.Divide) {
      return result % 1 === 0 && result <= this._settings.maxValue;
    }

    const existingEntries = trainingData?.filter((x) => x?.result?.value === result.toString()) ?? [];
    return result <= this._settings.maxValue && existingEntries.length === 0;
  }

  private getLetterTrainingData(): TrainingData[] {
    const trainingDataCollection: TrainingData[] = [];

    do {
      const randomIndex = this.getRandomNumber(letters.length - 1, 0);

      trainingDataCollection.push({
        unitLabel: letters[randomIndex],
        result: null,
        options: this.getLetterOptions(randomIndex),
      });
    } while (trainingDataCollection.length < this._unitCount);

    return trainingDataCollection;
  }

  private getLetterOptions(excludedIndex: number): TrainingOption[] {
    let key: number = 1;
    const options: TrainingOption[] = [{ key: 0, value: letters[excludedIndex].toLocaleLowerCase() }];

    const excludedIndexes: number[] = [excludedIndex];

    do {
      const index = this.getRandomNumber(letters.length - 1, 0);

      if (excludedIndexes.includes(index)) {
        continue;
      }

      options.push({ key: key, value: letters[index].toLocaleLowerCase() });
      excludedIndexes.push(index);
      key = key + 1;
    } while (options.length < 4);

    return this.shuffleOptions(options);
  }

  private shuffleOptions(array: TrainingOption[]): TrainingOption[] {
    for (let i = 0; i < array.length; i++) {
      const index = Math.floor(Math.random() * i + 1);
      const tmp = array[i];
      array[i] = array[index];
      array[index] = tmp;
    }

    return array;
  }
  private getRandomNumber(max: number, min: number): number {
    return Math.floor(Math.random() * (max - min + 1) + min);
  }
}
