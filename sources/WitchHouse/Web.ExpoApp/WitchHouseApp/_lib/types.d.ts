import { ModuleSettingsTypeEnum } from './enums/ModuleSettingsTypeEnum';
import { ModuleTypeEnum } from './enums/ModuleTypeEnum';

export type LoginResult = {
  userId: string;
  familyGuid: string;
  userRole: UserRoleEnum;
  language: 'de' | 'en';
  userName: string;
  jwt: string;
  refreshToken: string;
};

export type AuthState = {
  isLoading: boolean;
  isAuthenticated: boolean;
  apiIsAvailable: boolean;
  loginResult: LoginResult;
  onLogin: (data: LoginRequest) => Promise<void>;
  onLogout: () => Promise<void>;
};

export type LoginRequest = {
  userName: string;
  password: string;
};
