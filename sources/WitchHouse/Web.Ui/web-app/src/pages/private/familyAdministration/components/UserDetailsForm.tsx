import { List, ListItem } from '@mui/material';
import React from 'react';
import { useI18n } from '../../../../hooks/useI18n';
import TextInputListItem from '../../../../components/inputs/TextInputListItem';
import ListItemSwitch from '../../../../components/inputs/ListItemSwitch';
import CheckBoxGroupListItem from '../../../../components/inputs/CheckBoxGroupListItem';
import { UserRoleEnum } from '../../../../lib/enums/UserRoleEnum';
import SubmitButton from '../../../../components/buttons/SubmitButton';
import { useApi } from '../../../../hooks/useApi';
import { UserAdministrationDataModel, UserAdministrationUpdateImportModel } from '../../../../lib/types/user';
import { endpoints } from '../../../../lib/api/apiConfiguration';
import { ResponseMessage } from '../../../../lib/api/types';

interface IProps {
  userId: string;
  disabled: boolean;
}

const UserDetailsForm: React.FC<IProps> = (props) => {
  const { userId, disabled } = props;
  const { getResource } = useI18n();
  const { data, isLoading, get, post } = useApi<ResponseMessage<UserAdministrationDataModel>>();
  const [userData, setUserData] = React.useState<UserAdministrationDataModel | null>(null);

  const loadUserData = React.useCallback(async () => {
    await get(endpoints.administration.loadFamilyUserData.replace('{guid}', userId));
  }, [userId, get]);

  React.useEffect(() => {
    loadUserData();
  }, [loadUserData]);

  React.useEffect(() => {
    if (data != null) {
      setUserData(data.data);
    }
  }, [data]);

  const handleRoleChange = React.useCallback(
    (key: string, value: boolean) => {
      if (userData != null) {
        const roles = userData?.userRoles.slice() ?? [];
        const role = parseInt(key);
        if (value === true) {
          roles.push(role);
        } else {
          roles.splice(
            roles.findIndex((x) => x === role),
            1
          );
        }

        setUserData({ ...userData, userRoles: roles });
      }
    },
    [userData]
  );

  const handleChange = React.useCallback(
    (key: string, value: any) => {
      if (userData != null) {
        setUserData({ ...userData, [key]: value });
      }
    },
    [userData]
  );

  const handleCancel = React.useCallback(async () => {
    if (data?.data != null) {
      setUserData(data?.data);
    }
  }, [data]);

  const handleSave = React.useCallback(async () => {
    if (userData != null) {
      const model: UserAdministrationUpdateImportModel = {
        userId: userData.userId,
        roles: userData.userRoles,
        isActive: userData.isActive,
      };

      await post(endpoints.administration.updateFamilyMember, JSON.stringify(model)).then(async () => {
        await loadUserData();
      });
    }
  }, [userData, post, loadUserData]);

  const userRolesEqual = React.useMemo(() => {
    const isEqual = JSON.stringify(data?.data.userRoles) === JSON.stringify(userData?.userRoles);

    return isEqual;
  }, [data, userData]);

  const isModified = React.useMemo(() => {
    const modified = JSON.stringify(userData) !== JSON.stringify(data?.data) || !userRolesEqual;

    return modified;
  }, [userData, data, userRolesEqual]);

  if (isLoading || userData == null) {
    return null;
  }

  return (
    <List disablePadding>
      <ListItemSwitch
        marginBottom={0}
        property="isActive"
        disabled={disabled}
        checked={userData.isActive}
        label={getResource('common:labelIsActive')}
        onChange={handleChange}
      />
      <TextInputListItem
        textFieldProps={{
          property: 'userId',
          fullWidth: true,
          disabled: true,
          label: getResource('common:labelUserId'),
          value: userData.userId,
          onChange: handleChange,
        }}
      />
      {userData.familyGuid && (
        <TextInputListItem
          textFieldProps={{
            property: 'familyGuid',
            fullWidth: true,
            disabled: true,
            label: getResource('common:labelFamilyId'),
            value: userData.familyGuid,
            onChange: handleChange,
          }}
        />
      )}
      <TextInputListItem
        textFieldProps={{
          property: 'firstName',
          fullWidth: true,
          disabled: true,
          label: getResource('common:labelFirstName'),
          value: userData.firstName,
          onChange: handleChange,
        }}
      />
      <TextInputListItem
        textFieldProps={{
          property: 'lastName',
          fullWidth: true,
          disabled: true,
          label: getResource('common:labelLastName'),
          value: userData.lastName,
          onChange: handleChange,
        }}
      />
      <TextInputListItem
        textFieldProps={{
          property: 'userName',
          marginBottom: 1,
          fullWidth: true,
          disabled: true,
          label: getResource('common:labelUserName'),
          value: userData.userName,
          onChange: handleChange,
        }}
      />
      <CheckBoxGroupListItem
        marginTop={1}
        marginBottom={1}
        groupLabel="UserRole"
        value={userData.userRoles}
        property="role"
        radioProps={[
          { label: getResource('common:labelAdmin'), value: UserRoleEnum.Admin, disabled: true },
          {
            label: getResource('common:labelLocalAdmin'),
            value: UserRoleEnum.LocalAdmin,
            disabled: disabled,
          },
          {
            label: getResource('common:labelUser'),
            value: UserRoleEnum.User,
            disabled: disabled,
          },
        ]}
        onChange={handleRoleChange}
      />
      <ListItem sx={{ display: 'flex', justifyContent: 'flex-end', paddingRight: '2rem', marginTop: '3rem' }}>
        <SubmitButton
          title={getResource('common:labelCancel')}
          disabled={!isModified}
          variant="text"
          onClick={handleCancel}
        />
        <SubmitButton
          title={getResource('common:labelSave')}
          disabled={!isModified}
          variant="text"
          onClick={handleSave}
        />
      </ListItem>
    </List>
  );
};

export default UserDetailsForm;
