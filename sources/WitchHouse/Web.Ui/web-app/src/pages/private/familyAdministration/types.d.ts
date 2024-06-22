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
  roles: UserRoleEnum[];
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

export type ModuleBase = {
  userId: string;
  moduleId: string;
  moduleSettingsType: ModuleSettingsTypeEnum;
  moduleType: ModuleTypeEnum;
  isActive: Boolean;
};

export type SchoolModule = ModuleBase & {
  settings: SchoolSettings | null;
};

export type UserModuleRequestModel = {
  userGuid: string;
  familyGuid: string;
  roles: UserRoleEnum[];
};

export type ModuleSettings = {
  userId: string;
  moduleGuid: string;
  moduleType: ModuleTypeEnum;
  settings: string | null;
};

export type SchoolSettings = {
  moduleType: ModuleTypeEnum;
  allowAddition: boolean;
  allowSubtraction: boolean;
  allowMultiply: boolean;
  allowDivide: boolean;
  allowDoubling: boolean;
  minValue: number;
  maxValue: number;
  maxWordLength: number;
};
