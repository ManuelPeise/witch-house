import { UserRoleEnum } from './enums/UserRoleEnum';

export type ApiResult<T> = {
  isLoading: boolean;
  error: string;
  data: T | null;
  get: (url: string) => Promise<void>;
  post: (url: string, json: any) => Promise<void>;
};

export type ProfileData = {
  userId: string;
  familyGuid: string;
  firstName: string;
  lastName: string;
  userName: string;
  dateOfBirth: string | null;
  culture?: 'en' | 'de';
};

export type ProfileRequestModel = {
  id: string;
};

export type ListItemModel = {
  id: number;
  isSelected: boolean;
  value: string;
};

export type DropdownItem = {
  id: number;
  value: string;
  label: string;
};

export type NavigationListItemProps = {
  key: string;
  userRole: UserRoleEnum;
  component: React.ComponentType;
};

export type CheckboxProps = {
  property: string;
  label: string;
  disabled?: boolean;
  checked: boolean;
  onChange: (key: string, value: any) => void;
};

export type RadioInputProps = {
  disabled: boolean;
  label: string;
  value: string;
};
export type RadioGroupProps = {
  property: string;
  hasDivider?: boolean;
  groupLabel: string;
  value: any;
  radioProps: RadioInputProps[];
  onChange: (key: string, value: any) => void;
};
