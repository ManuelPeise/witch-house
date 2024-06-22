import { Database } from '../_lib/_database/sqLiteDatabase';
import {
  CredentialTableModel,
  ModuleTableModel,
  SqLiteDatabase,
  SyncTableModel,
  UserTableModel,
} from '../_lib/_types/sqLite';
import { ReducerAction, ReducerActions } from './reducerActions';
import { sqLiteTables } from '../_lib/_database/databaseQueries';

export const initialState: SqLiteDatabase = {
  syncTableModel: {} as SyncTableModel,
  userTableModel: {} as UserTableModel,
  credentialTableModel: {} as CredentialTableModel,
  moduleTableModels: [],
};

const AppReducer = (state = initialState, action: ReducerAction): SqLiteDatabase => {
  const { type, payload } = action;

  switch (type) {
    case ReducerActions.InitializeData:
      return {
        ...state,
        ...payload,
      };
    case ReducerActions.SetSyncData:
      return {
        ...state,
        syncTableModel: payload,
      };
    case ReducerActions.SetUserData:
      return {
        ...state,
        userTableModel: payload,
      };
    case ReducerActions.SetCredentialData:
      return {
        ...state,
        credentialTableModel: payload,
      };
    case ReducerActions.SetUserModulesData:
      return {
        ...state,
        moduleTableModels: payload,
      };
    default:
      console.log('Reducer Default State:', state);
      return state;
  }
};

export const getInitialStateFromDatabase = async (userId?: string): Promise<SqLiteDatabase> => {
  if (userId == null) {
    return initialState;
  }

  const db = new Database();

  const model: SqLiteDatabase = {
    syncTableModel: (
      await db.executeSingleGetQuery<SyncTableModel>(
        `SELECT * FROM ${sqLiteTables.syncTable} WHERE userId = ${userId};`
      )
    ).data as SyncTableModel,
    userTableModel: (
      await db.executeSingleGetQuery<UserTableModel>(
        `SELECT * FROM ${sqLiteTables.userTable} WHERE userId = ${userId};`
      )
    ).data as UserTableModel,
    credentialTableModel: (
      await db.executeSingleGetQuery<CredentialTableModel>(
        `SELECT * FROM ${sqLiteTables.credentialTable} WHERE userId = ${userId};`
      )
    ).data as CredentialTableModel,
    moduleTableModels: (
      await db.executeArrayGetQuery<ModuleTableModel>(
        `SELECT * FROM ${sqLiteTables.userModuleTable} WHERE userId = ${userId};`
      )
    ).data as ModuleTableModel[],
  };

  return model;
};

export default AppReducer;
