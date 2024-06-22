export const syncTableColumnDefinition =
  'syncId TEXT PRIMARY KEY NOT NULL, userGuid TEXT, lastSync TEXT, createdBy TEXT, createdAt TEXT, updatedBy TEXT, updatedAt TEXT';

export const syncTableColumns = 'syncId, userGuid, lastSync, createdBy, createdAt, updatedBy, updatedAt';

export const userTableColumnDefinition =
  'userId TEXT PRIMARY KEY NOT NULL, familyId TEXT, firstName TEXT, lastName TEXT, userName TEXT, dateOfBirth TEXT, isActive INT, culture TEXT, profileImage BLOB, createdBy TEXT, createdAt TEXT, updatedBy TEXT, updatedAt TEXT';

export const userTableColumns =
  'userId, familyId, firstName, lastName, userName, dateOfBirth, isActive, culture, profileImage, createdBy, createdAt, updatedBy, updatedAt';

export const credentialColumnDefinition =
  'credentialsId TEXT PRIMARY KEY NOT NULL, mobilePin INT, jwtToken BLOB, refreshToken BLOB, createdBy TEXT, createdAt TEXT, updatedBy TEXT, updatedAt TEXT';

export const credentialColumns =
  'credentialsId, mobilePin, jwtToken, refreshToken, createdBy, createdAt, updatedBy, updatedAt';

export const moduleTableColumnDefinition =
  'moduleId TEXT PRIMARY KEY NOT NULL, accountGuid TEXT, moduleName TEXT, moduleType INT, moduleSettingsType INT, settingsJson BLOB, createdBy TEXT, createdAt TEXT, updatedBy TEXT, updatedAt TEXT';

export const moduleTableColumns =
  'moduleId, accountGuid, moduleName, moduleType, moduleSettingsType, settingsJson, createdBy, createdAt, updatedBy, updatedAt';
