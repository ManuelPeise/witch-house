import { ModuleSettingsTypeEnum } from '../enums/ModuleSettingsTypeEnum';
import { ModuleTypeEnum } from '../enums/ModuleTypeEnum';

export type SqLiteDatasetBase = {
  createdBy: string;
  createdAt: string;
  updatedBy: string;
  updatedAt: string;
};

export type SyncTableModel = SqLiteDatasetBase & {
  syncId: string;
  userGuid: string;
  lastSync: string;
};

export type UserTableModel = SqLiteDatasetBase & {
  userId: string;
  familyId: string;
  firstName: string;
  lastName: string;
  userName: string;
  dateOfBirth: string;
  isActive: boolean;
  culture: string;
  profileImage?: string;
};

export type CredentialTableModel = SqLiteDatasetBase & {
  credentialsId: string;
  mobilePin: number;
  jwtToken: string;
  refreshToken: string;
};

export type ModuleTableModel = SqLiteDatasetBase & {
  accountGuid: string;
  moduleId: string;
  moduleName: string;
  moduleType: ModuleTypeEnum;
  moduleSettingsType: ModuleSettingsTypeEnum;
  settingsJson?: string;
  isActive: boolean;
};

export type SqLiteDatabase = {
  syncTableModel: SyncTableModel;
  userTableModel: UserTableModel;
  credentialTableModel: CredentialTableModel;
  moduleTableModels: ModuleTableModel[];
};

export type DatabaseQueryResult<TModel> = {
  data: TModel | TModel[] | null;
  error?: string | null;
};

export type SqLiteArrayQuery = {
  query: string;
  insertOrUpdateQuery: string;
};
