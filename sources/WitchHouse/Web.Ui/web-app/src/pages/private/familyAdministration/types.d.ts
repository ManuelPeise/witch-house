import { UserRoleEnum } from '../../../lib/enums/UserRoleEnum';

export type UserDataModel = {
  userId: string;
  familyGuid: string;
  firstName: string;
  lastName: string;
  userName: string;
  isActive: boolean;
  role: UserRoleEnum;
};
