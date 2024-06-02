import { Grid, List, ListItem } from '@mui/material';
import React from 'react';
import { UserDataModel } from '../types';
import { useI18n } from '../../../../hooks/useI18n';
import TextInputListItem from '../../../../components/inputs/TextInputListItem';
import ListItemSwitch from '../../../../components/inputs/ListItemSwitch';
import RadioGroupListItem from '../../../../components/inputs/RadioGroupListItem';
import { UserRoleEnum } from '../../../../lib/enums/UserRoleEnum';
import SubmitButton from '../../../../components/buttons/SubmitButton';

interface IProps {
  userData: UserDataModel;
  disabled: boolean;
  onSave: (data: UserDataModel) => Promise<void>;
}

const UserDetailsForm: React.FC<IProps> = (props) => {
  const { userData, disabled, onSave } = props;

  const { getResource } = useI18n();

  const [data, setData] = React.useState<UserDataModel>(userData);

  React.useEffect(() => {
    setData(userData);
  }, [userData]);

  const handleChange = React.useCallback(
    (key: string, value: any) => {
      setData({ ...data, [key]: value });
    },
    [data]
  );

  const handleCancel = React.useCallback(async () => {
    setData(userData);
  }, [userData]);

  const handleSave = React.useCallback(async () => {
    await onSave(data);
  }, [data, onSave]);

  const isModified = React.useMemo(() => {
    return JSON.stringify(userData) !== JSON.stringify(data);
  }, [userData, data]);

  return (
    <Grid container padding={5} justifyContent="center">
      <Grid item xs={10}>
        <List>
          <ListItemSwitch
            hasDivider
            marginBottom={5}
            property="isActive"
            disabled={disabled}
            checked={data.isActive}
            label={getResource('common:labelIsActive')}
            onChange={handleChange}
          />

          <TextInputListItem
            textFieldProps={{
              property: 'userId',
              fullWidth: true,
              disabled: true,
              label: getResource('common:labelUserId'),
              value: data.userId,
              onChange: handleChange,
            }}
          />
          {data.familyGuid && (
            <TextInputListItem
              textFieldProps={{
                property: 'familyGuid',
                fullWidth: true,
                disabled: true,
                label: getResource('common:labelFamilyId'),
                value: data.familyGuid,
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
              value: data.firstName,
              onChange: handleChange,
            }}
          />
          <TextInputListItem
            textFieldProps={{
              property: 'lastName',
              fullWidth: true,
              disabled: true,
              label: getResource('common:labelLastName'),
              value: data.lastName,
              onChange: handleChange,
            }}
          />
          <TextInputListItem
            hasDivider
            textFieldProps={{
              property: 'userName',
              marginBottom: 5,
              fullWidth: true,
              disabled: true,
              label: getResource('common:labelUserName'),
              value: data.userName,
              onChange: handleChange,
            }}
          />
          <RadioGroupListItem
            hasDivider
            marginTop={1}
            marginBottom={1}
            groupLabel="UserRole"
            value={data.role}
            property="role"
            radioProps={[
              { label: getResource('common:labelAdmin'), value: UserRoleEnum.Admin.toString(), disabled: true },
              {
                label: getResource('common:labelLocalAdmin'),
                value: UserRoleEnum.LocalAdmin.toString(),
                disabled: disabled,
              },
              {
                label: getResource('common:labelUser'),
                value: UserRoleEnum.User.toString(),
                disabled: disabled,
              },
            ]}
            onChange={handleChange}
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
      </Grid>
    </Grid>
  );
};

export default UserDetailsForm;
