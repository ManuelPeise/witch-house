import { UnitTypeEnum } from '../../../_lib/enums/UnitTypeEnum';
import { ModuleSettings, SchoolSettings } from '../../../_lib/sync';

export type Training = {
  title: string;
  isRunning: boolean;
  isFinished: boolean;
  unitType: UnitTypeEnum;
  settings: SchoolSettings;
  trainingData: TrainingData[];
};

export type TrainingOption = {
  key: number;
  value: string;
};

export type TrainingData = {
  unitLabel: string;
  result: TrainingOption | null;
  options: TrainingOption[];
};

export type TrainingResultModel = {
  userId: string;
  familyId: string;
  unitKey: UnitTypeEnum;
  timeStamp: string;
  success: number;
  failed: number;
};

export type TrainingResultExportModel = {
  unitType: UnitTypeEnum;
  userId: string;
  success: number;
  failed: number;
  timeStamp: string;
};
