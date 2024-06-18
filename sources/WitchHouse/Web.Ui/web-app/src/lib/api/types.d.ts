import { ModuleTypeEnum } from '../enums/ModuleTypeEnum';
import { UserRoleEnum } from '../enums/UserRoleEnum';

export type AuthResult = {
  loginResult: LoginResult | null;
  onLogin: (loginData: LoginData) => Promise<void>;
  onLogout: () => void;
  onRegister: (json: string) => Promise<boolean>;
};

export type ResponseMessage<TModel> = {
  success: boolean;
  statusCode: number;
  messageKey: string;
  data: TModel;
};

export type LoginResult = {
  userData: UserData;
  jwtData: JwtData;
  modules: UserModule[];
};

export type UserData = {
  userId: string;
  familyGuid: string;
  userRoles: UserRoleEnum[];
  language: 'en' | 'de';
  userName: string;
  profileImage: string;
};

export type JwtData = {
  jwtToken: string;
  refreshToken: string;
};

export type UserModule = {
  userId: string;
  moduleId: string;
  moduleType: ModuleTypeEnum;
  moduleSettings: string;
  isActive: boolean;
};

export type LoginData = {
  userName: string;
  secret: string;
};
