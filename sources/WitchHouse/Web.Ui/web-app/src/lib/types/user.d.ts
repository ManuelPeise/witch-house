export type UserAdministrationDataModel = {
  userId: string;
  familyGuid: string;
  firstName: string;
  lastName: string;
  userName: string;
  dateOfBirth: string;
  isActive: boolean;
  culture: string;
  userRoles: number[];
};

export type UserAdministrationUpdateImportModel = {
  userId: string;
  isActive: boolean;
  roles: number[];
};
