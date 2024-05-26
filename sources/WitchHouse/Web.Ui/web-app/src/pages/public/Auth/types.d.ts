export type LoginImportModel = {
  userName: string;
  secret: string;
};

export type FamilyAccountImportModel = {
  familyName: string;
  city: string;
};

export type UserAccountImportModel = {
  firstName: string;
  lastName: string;
  userName: string;
  secret: string;
};

export type AccountImportModel = {
  family: FamilyAccountImportModel;
  userAccount: UserAccountImportModel;
};
