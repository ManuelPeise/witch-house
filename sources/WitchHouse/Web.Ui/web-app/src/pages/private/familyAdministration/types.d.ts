import { UserRoleEnum } from '../../../lib/enums/UserRoleEnum';
import { ModuleTypeEnum } from '../../../lib/enums/ModuleTypeEnum';
import { ModuleSettingsTypeEnum } from '../../../lib/enums/ModuleSettingsTypeEnum';

export type UserDataModel = {
  userId: string;
  familyGuid: string;
  firstName: string;
  lastName: string;
  userName: string;
  isActive: boolean;
  role: UserRoleEnum;
  moduleSettings: UserModule[];
};

export type ModuleConfiguration = {
  userGuid: string;
  modules: UserModule[];
};

export type UserModule = {
  userId: string;
  moduleId: string;
  isActive: boolean;
  moduleType: ModuleTypeEnum;
};

export type ModuleProps = {
  userId: string;
  moduleId: string;
  isActive: boolean;
  moduleType: ModuleTypeEnum;
};

export type MathSettings = {
  allowAddition: boolean;
  allowSubtraction: boolean;
  allowMultiply: boolean;
  allowDivide: boolean;
  maxValue: number;
};

export type GermanSettings = {
  maxWordLength: number;
};

export type UserModuleRequestModel = {
  userGuid: string;
  familyGuid: string;
  roleId: UserRoleEnum;
};

export type ModuleSettings = {
  userId: string;
  moduleType: ModuleTypeEnum;
  moduleSettingsType: ModuleSettingsTypeEnum;
  settings: string;
};
