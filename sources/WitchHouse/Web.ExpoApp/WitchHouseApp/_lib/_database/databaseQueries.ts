import { CredentialTableModel, ModuleTableModel, SyncTableModel, UserTableModel } from '../_types/sqLite';
import {
  credentialColumnDefinition,
  credentialColumns,
  moduleTableColumnDefinition,
  moduleTableColumns,
  syncTableColumnDefinition,
  syncTableColumns,
  userTableColumnDefinition,
  userTableColumns,
} from './databaseConstants';

export const sqLiteTables = {
  credentialTable: 'credentialTable',
  syncTable: 'syncTable',
  userTable: 'userTable',
  userModuleTable: 'userModuleTable',
};

export const createTableQueries = {
  syncTable: `CREATE TABLE IF NOT EXISTS ${sqLiteTables.syncTable} (${syncTableColumnDefinition});`,
  credentialTable: `CREATE TABLE IF NOT EXISTS ${sqLiteTables.credentialTable} (${credentialColumnDefinition});`,
  userTable: `CREATE TABLE IF NOT EXISTS ${sqLiteTables.userTable} (${userTableColumnDefinition});`,
  userModuleTable: `CREATE TABLE IF NOT EXISTS ${sqLiteTables.userModuleTable} (${moduleTableColumnDefinition});`,
};

//#region setQueryParts

export const getSetEntireSyncDataQueryPart = (model: SyncTableModel) =>
  `lastSync = ${model.lastSync},
  createdBy = ${model.createdBy},
  createdAt = ${model.createdAt},
  updatedBy = ${model.updatedBy},
  updatedAt = ${model.updatedAt}`
    .replaceAll('\n', '')
    .replaceAll('\t', '');

export const getSetEntireUserDataQueryPart = (model: UserTableModel): string =>
  `firstName = ${model.firstName},
  lastName = ${model.lastName},
  userName = ${model.userName},
  dateOfBirth = ${model.dateOfBirth},
  isActive = ${model.isActive},
  culture = ${model.culture},
  profileImage = ${model.profileImage},
  createdBy = ${model.createdBy},
  createdAt = ${model.createdAt},
  updatedBy = ${model.updatedBy},
  updatedAt = ${model.updatedAt}`
    .replaceAll('\n', '')
    .replaceAll('\t', '');

export const getSetEntireCredentialDataQueryPart = (model: CredentialTableModel) =>
  `mobilePin = ${model.mobilePin}, 
  jwtToken = ${model.jwtToken}, 
  refreshToken = ${model.refreshToken}, 
  createdBy = ${model.createdBy}, 
  createdAt = ${model.createdAt}, 
  updatedBy = ${model.updatedBy}, 
  updatedAt = ${model.updatedAt}`
    .replaceAll('\n', '')
    .replaceAll('\t', '');

export const getSetEntireModuleDataQueryPart = (model: ModuleTableModel) =>
  `moduleName = ${model.moduleName}, 
  moduleType = ${model.moduleType}, 
  moduleSettingsType = ${model.moduleSettingsType}, 
  settingsJson = ${model.settingsJson}, 
  createdBy = ${model.createdBy}, 
  createdAt = ${model.createdAt}, 
  updatedBy = ${model.updatedBy}, 
  updatedAt = ${model.updatedAt}`
    .replaceAll('\n', '')
    .replaceAll('\t', '');

//#endregion

//#region insert queries

export const getInsertSyncDataQuery = (data: SyncTableModel) => {
  console.log('from sync', data);
  if (data == null) {
    return '';
  }
  return `insert into ${sqLiteTables.syncTable} (${syncTableColumns}) values(
  '${data.syncId}',
  '${data.userGuid}',
  '${data.lastSync}',
  '${data.createdBy}',
  '${data.createdAt}',
  '${data.updatedBy}',
  '${data.updatedAt}');`
    .replaceAll('\n', '')
    .replaceAll('\t', '');
};

export const getInsertUserDataDataQuery = (data: UserTableModel) =>
  `insert into ${sqLiteTables.userTable} (${userTableColumns}) values(
'${data.userId}',
'${data.familyId}',
'${data.firstName}',
'${data.lastName}',
'${data.userName}',
'${data.dateOfBirth}',
${data.isActive},
'${data.culture}',
'${data.profileImage}',
'${data.createdBy}',
'${data.createdAt}',
'${data.updatedBy}',
'${data.updatedAt}');`
    .replaceAll('\n', '')
    .replaceAll('\t', '');

export const getInsertCredentialDataQuery = (data: CredentialTableModel) =>
  `insert into ${sqLiteTables.credentialTable} (${credentialColumns}) values(
'${data.credentialsId}',
${data.mobilePin},
'${data.jwtToken}',
'${data.refreshToken}',
'${data.createdBy}',
'${data.createdAt}',
'${data.updatedBy}',
'${data.updatedAt}');`
    .replaceAll('\n', '')
    .replaceAll('\t', '');

export const getInsertModuleDataQuery = (data: ModuleTableModel) =>
  `insert into ${sqLiteTables.userModuleTable} (${moduleTableColumns}) values(
'${data.moduleId}',
'${data.accountGuid}',
'${data.moduleName}',
${data.moduleType},
${data.moduleSettingsType},
'${data.settingsJson}',
'${data.createdBy}',
'${data.createdAt}',
'${data.updatedBy}',
'${data.updatedAt}');`
    .replaceAll('\n', '')
    .replaceAll('\t', '');

//#endregion

//#region generic queries

export const getGenericGetQuery = (tableName: string, keyColumn: string, key: any): string => {
  return `SELECT FROM ${tableName} WHERE ${keyColumn} = ${key}`.replaceAll('\n', '').replaceAll('\t', '');
};

export const getGenericUpdateQuery = (tableName: string, setQueryPart: string, keyColumn: string, key: any): string => {
  return `UPDATE ${tableName} SET = ${setQueryPart} WHERE ${keyColumn} = ${key}`
    .replaceAll('\n', '')
    .replaceAll('\t', '');
};

//#endregion

export const getFirstFromTableByKeyQueryCallback = (table: string, columns: string, keyColumn: string, value: any) =>
  `SELECT ${columns} FROM ${table} WHERE ${keyColumn} = '${value}';`.replaceAll('\n', '').replaceAll('\t', '');
