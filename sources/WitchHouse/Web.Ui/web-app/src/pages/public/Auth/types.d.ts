import { UserRoleEnum } from '../../../lib/enums/UserRoleEnum';

export type LoginImportModel = {
  userName: string;
  secret: string;
};

export type FamilyAccountImportModel = {
  familyGuid?: string;
  familyName: string;
  familyFullName: string;
};

export type UserAccountImportModel = {
  firstName: string;
  lastName: string;
  userName: string;
  secret: string;
  userRole?: UserRoleEnum;
};

export type AccountImportModel = {
  family: FamilyAccountImportModel;
  userAccount: UserAccountImportModel;
};

export type FamilyMemberModel = {
  familyGuid?: string;
  firstName: string;
  lastName: string;
  userName: string;
};

export type FamilyMemberUpdate = {
  userId: string;
  isActive: boolean;
  role: number;
};
