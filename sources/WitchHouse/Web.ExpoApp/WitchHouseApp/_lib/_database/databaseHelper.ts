import {
  CredentialTableModel,
  ModuleTableModel,
  SqLiteDatabase,
  SyncTableModel,
  UserTableModel,
} from '../_types/sqLite';
import { UserModule } from '../types';
import {
  getGenericSelectQuery,
  getInsertCredentialDataQuery,
  getInsertModuleDataQuery,
  getInsertSyncDataQuery,
  getInsertUserDataDataQuery,
  sqLiteTables,
} from './databaseQueries';
import { Database } from './sqLiteDatabase';

export const ensureDatabaseUpToDate = async (modelFromApi: SqLiteDatabase): Promise<SqLiteDatabase> => {
  try {
    const db = new Database();

    const [syncResult, userDataResult, credentialDataResult, userModulesResult] = await Promise.all([
      await db.getInsertedOrUpdatedModel<SyncTableModel>({
        query: getGenericSelectQuery(
          sqLiteTables.syncTable,
          `WHERE syncId = '${modelFromApi?.syncTableModel?.syncId}'`
        ),
        insertOrUpdateQuery: getInsertSyncDataQuery(modelFromApi?.syncTableModel),
      }),
      await db.getInsertedOrUpdatedModel<UserTableModel>({
        query: getGenericSelectQuery(sqLiteTables.userTable, `WHERE userId = '${modelFromApi.userTableModel.userId}'`),
        insertOrUpdateQuery: getInsertUserDataDataQuery(modelFromApi.userTableModel),
      }),
      await db.getInsertedOrUpdatedModel<CredentialTableModel>({
        query: getGenericSelectQuery(
          sqLiteTables.credentialTable,
          `WHERE credentialsId = '${modelFromApi.credentialTableModel.credentialsId}'`
        ),
        insertOrUpdateQuery: getInsertCredentialDataQuery(modelFromApi.credentialTableModel),
      }),
      await db.getInsertedOrUpdatedArray<ModuleTableModel>(
        modelFromApi?.moduleTableModels?.map((module) => {
          return {
            query: getGenericSelectQuery(sqLiteTables.userModuleTable, `WHERE moduleId = '${module.moduleId}'`),
            insertOrUpdateQuery: getInsertModuleDataQuery(module),
          };
        })
      ),
    ]);

    return {
      syncTableModel: syncResult,
      userTableModel: userDataResult,
      credentialTableModel: credentialDataResult,
      moduleTableModels: userModulesResult,
    };
  } catch (err) {
    console.log('Error on database helper  ~52', err);
  }

  return modelFromApi;
};
