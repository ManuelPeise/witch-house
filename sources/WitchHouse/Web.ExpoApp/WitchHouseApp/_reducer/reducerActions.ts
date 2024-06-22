import {
  CredentialTableModel,
  ModuleTableModel,
  SqLiteDatabase,
  SyncTableModel,
  UserTableModel,
} from '../_lib/_types/sqLite';

export enum ReducerActions {
  InitializeData = 'InitializeData',
  SetSyncData = 'SetSyncData',
  SetUserData = 'SetUserData',
  SetCredentialData = 'SetCredentialData',
  SetUserModulesData = 'SetUserModulesData',
}

export type InitializeData = {
  type: typeof ReducerActions.InitializeData;
  payload: SqLiteDatabase;
};

export type SetSyncData = {
  type: typeof ReducerActions.SetSyncData;
  payload: SyncTableModel;
};

export type SetUserData = {
  type: typeof ReducerActions.SetUserData;
  payload: UserTableModel;
};

export type SetCredentialData = {
  type: typeof ReducerActions.SetCredentialData;
  payload: CredentialTableModel;
};

export type SetUserModulesData = {
  type: typeof ReducerActions.SetUserModulesData;
  payload: ModuleTableModel[];
};

export type UserDataReducerState = {
  userData: UserTableModel;
  credentials: CredentialTableModel;
};

export type ReducerAction = InitializeData | SetSyncData | SetUserData | SetCredentialData | SetUserModulesData;
