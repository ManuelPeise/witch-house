import { UserRoleEnum } from './enums/UserRoleEnum';
import { ModuleConfiguration } from './types';

export type UserDataSync = {
  userGuid: string;
  firstName: string;
  lastName: string;
  userName: string;
  birthday: string;
};

export type UserModule = {
  userId: string;
  moduleId: string;
  moduleType: ModuleTypeEnum;
  isActive: boolean;
};

export type SchoolSettings = {
  allowAddition: boolean;
  allowSubtraction: boolean;
  allowMultiply: boolean;
  allowDivide: boolean;
  minValue: number;
  maxValue: number;
  maxWordLength: number;
};

export type ModuleSettings = {
  userId: string;
  moduleType: ModuleTypeEnum;
  moduleSettingsType: ModuleSettingsTypeEnum;
  settings: SchoolSettings;
};

export type SchoolModuleSync = {
  userModule: UserModule;
  moduleSettings: ModuleSettings;
};

export type DataSyncModel = {
  userData: UserDataSync;
  moduleConfiguration: ModuleConfiguration;
  schoolModulesSettings: ModuleSettings[];
};

export type DataSyncImportModel = {
  userId: string;
  familyId: string;
  roleId: UserRoleEnum;
};
