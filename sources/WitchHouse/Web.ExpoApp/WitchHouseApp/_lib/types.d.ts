import { JwtData } from './api/types';
import { ModuleSettingsTypeEnum } from './enums/ModuleSettingsTypeEnum';
import { ModuleTypeEnum } from './enums/ModuleTypeEnum';
import { UnitTypeEnum } from './enums/UnitTypeEnum';
import { UserRoleEnum } from './enums/UserRoleEnum';

export type UserModule = {
  userId: string;
  moduleId: string;
  moduleType: ModuleTypeEnum;
  isActive: boolean;
};

export type ModuleConfiguration = {
  userGuid: string;
  modules: UserModule[];
};

export type UserData = {
  userId: string;
  familyGuid: string;
  userName: string;
  userRole: UserRoleEnum;
  language: 'en' | 'de';
};

export type MobileLoginResult = {
  userData: UserData;
  jwtData: JwtData;
  moduleConfiguration: ModuleConfiguration;
  trainingModuleSettings: ModuleSettings[];
};

export type ModuleSettings = {
  userId: string;
  moduleType: ModuleTypeEnum;
  moduleSettingsType: ModuleSettingsTypeEnum;
  settings: SchoolSettings;
};

export type LoginResult = {
  userId: string;
  familyGuid: string;
  userRole: UserRoleEnum;
  language: 'de' | 'en';
  userName: string;
  jwt: string;
  refreshToken: string;
  userModules: ModuleConfiguration;
};

export type AuthState = {
  isLoading: boolean;
  isAuthenticated: boolean;
  apiIsAvailable: boolean;
  userData: UserData;
  onLogin: (data: LoginRequest) => Promise<void>;
  onLogout: () => Promise<void>;
};

export type LoginRequest = {
  userName: string;
  password: string;
};

export type AppSettings = {
  syncData: boolean;
};

export type UnitResultStatisticEntry = {
  timeStamp: string;
  success: number;
  failed: number;
};

export type UnitResultStatisticModel = {
  unitType: UnitTypeEnum;
  entries: UnitResultStatisticEntry[];
};

export type TrainingStatisticRequestModel = {
  userId: string;
  from: Date;
  to: Date;
};
