import { Grid, List, ListItem } from '@mui/material';
import React from 'react';
import { UserDataModel } from '../types';
import { useI18n } from '../../../../hooks/useI18n';
import TextInputListItem from '../../../../components/inputs/TextInputListItem';
import { UserRoleEnum } from '../../../../lib/enums/UserRoleEnum';
import SubmitButton from '../../../../components/buttons/SubmitButton';
import { useAuth } from '../../../../lib/AuthContext';

interface IProps {
  onSave: (data: UserDataModel) => Promise<void>;
}

const AddUserForm: React.FC<IProps> = (props) => {
  const { onSave } = props;
  const { loginResult } = useAuth();
  const { getResource } = useI18n();

  const initialState: UserDataModel = React.useMemo(() => {
    return {
      userId: '',
      familyGuid: loginResult?.familyGuid ?? '',
      firstName: '',
      lastName: '',
      userName: '',
      role: UserRoleEnum.User,
      isActive: false,
      moduleSettings: [],
    };
  }, [loginResult]);

  const [data, setData] = React.useState<UserDataModel>(initialState);

  const handleChange = React.useCallback(
    (key: string, value: any) => {
      setData({ ...data, [key]: value });
    },
    [data]
  );

  const handleCancel = React.useCallback(async () => {
    setData(initialState);
  }, [initialState]);

  const handleSave = React.useCallback(async () => {
    await onSave(data);

    setData(initialState);
  }, [data, initialState, onSave]);

  const isModified = React.useMemo(() => {
    return JSON.stringify(data) !== JSON.stringify(initialState);
  }, [initialState, data]);

  const canSave = React.useMemo(() => {
    return data.firstName !== '' && data.lastName !== '' && data.userName !== '' && isModified;
  }, [data, isModified]);

  return (
    <Grid container padding={5} justifyContent="center">
      <Grid item xs={10}>
        <List>
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
              disabled: false,
              label: getResource('common:labelFirstName'),
              value: data.firstName,
              onChange: handleChange,
            }}
          />
          <TextInputListItem
            textFieldProps={{
              property: 'lastName',
              fullWidth: true,
              disabled: false,
              label: getResource('common:labelLastName'),
              value: data.lastName,
              onChange: handleChange,
            }}
          />
          <TextInputListItem
            textFieldProps={{
              property: 'userName',
              marginBottom: 5,
              fullWidth: true,
              disabled: false,
              label: getResource('common:labelUserName'),
              value: data.userName,
              onChange: handleChange,
            }}
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
              disabled={!canSave}
              variant="text"
              onClick={handleSave}
            />
          </ListItem>
        </List>
      </Grid>
    </Grid>
  );
};

export default AddUserForm;
