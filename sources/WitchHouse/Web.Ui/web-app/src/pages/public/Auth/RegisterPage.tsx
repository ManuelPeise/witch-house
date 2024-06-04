import { Grid, Paper } from '@mui/material';
import React from 'react';
import FormItemWrapper from '../../../components/wrappers/FormItemWrapper';
import { AccountImportModel } from './types';
import TextInput from '../../../components/inputs/TextInput';
import { useI18n } from '../../../hooks/useI18n';
import HowToRegIcon from '@mui/icons-material/HowToReg';
import SubmitButton from '../../../components/buttons/SubmitButton';
import { useAuth } from '../../../lib/AuthContext';
import { useNavigate } from 'react-router-dom';
import { RouteTypes } from '../../../lib/Router';
import { useApi } from '../../../hooks/useApi';
import { endpoints } from '../../../lib/api/apiConfiguration';
import LinkButton from '../../../components/buttons/LinkButton';

const initialModel: AccountImportModel = {
  family: {
    familyName: '',
    city: '',
  },
  userAccount: {
    firstName: '',
    lastName: '',
    userName: '',
    secret: '',
  },
};

const RegisterPage: React.FC = () => {
  const { getResource } = useI18n();
  const { onRegister } = useAuth();
  const { data, get } = useApi<boolean>();
  const navigate = useNavigate();

  const [model, setModel] = React.useState<AccountImportModel>(initialModel);

  React.useEffect(() => {
    const checkUserName = async () => {
      if (model.userAccount.userName?.length && model.userAccount.userName.length > 5) {
        await get(endpoints.checkUserName.replace('{name}', model.userAccount.userName));
      }
    };

    checkUserName();
  }, [model.userAccount.userName, get]);

  const isValidUserName = React.useMemo(() => {
    if (data == null) {
      return false;
    }

    return data;
  }, [data]);

  const canSave = React.useMemo(() => {
    return (
      model.family.familyName?.length > 3 &&
      model.family.city?.length &&
      model?.family?.city.length > 3 &&
      model.userAccount.firstName?.length > 3 &&
      model.userAccount.lastName?.length > 3 &&
      isValidUserName &&
      model.userAccount.userName?.length > 5 &&
      model.userAccount.secret?.length > 7
    );
  }, [model, isValidUserName]);

  const handleFamilyChanged = React.useCallback(
    (key: string, value: string) => {
      setModel({ ...model, family: { ...model.family, [key]: value } });
    },
    [model]
  );

  const handleAccountChanged = React.useCallback(
    (key: string, value: string) => {
      setModel({
        ...model,
        userAccount: {
          ...model.userAccount,
          [key]: value,
        },
      });
    },
    [model]
  );

  const handleRegister = React.useCallback(async () => {
    const result = await onRegister(JSON.stringify(model));

    if (result) {
      navigate(RouteTypes.Login, { replace: true });
    }
  }, [model, navigate, onRegister]);

  const navigateToLogin = React.useCallback(async () => {
    await navigate(RouteTypes.Login, { replace: true });
  }, [navigate]);

  return (
    <Grid style={{ display: 'flex', justifyContent: 'center', alignContent: 'center' }}>
      <Paper elevation={12} style={{ maxWidth: '30%' }}>
        <Grid container style={{ padding: '4rem 2rem' }}>
          <FormItemWrapper marginVertical={10}>
            <Grid style={{ display: 'flex', justifyContent: 'center' }}>
              <HowToRegIcon style={{ margin: 0, padding: 0, height: 200, width: 200, color: 'lightgray' }} />
            </Grid>
          </FormItemWrapper>
          <FormItemWrapper marginVertical={10}>
            <TextInput
              fullWidth
              property="familyName"
              label={getResource('common:labelFamily')}
              value={model.family.familyName}
              onChange={handleFamilyChanged}
            />
          </FormItemWrapper>
          <FormItemWrapper marginVertical={10}>
            <TextInput
              fullWidth
              property="firstName"
              label={getResource('common:labelFirstName')}
              value={model.userAccount.firstName}
              onChange={handleAccountChanged}
            />
          </FormItemWrapper>
          <FormItemWrapper marginVertical={10}>
            <TextInput
              fullWidth
              property="lastName"
              label={getResource('common:labelLastName')}
              value={model.userAccount.lastName}
              onChange={handleAccountChanged}
            />
          </FormItemWrapper>
          <FormItemWrapper marginVertical={10}>
            <TextInput
              fullWidth
              property="city"
              label={getResource('common:labelCity')}
              value={model?.family?.city ?? ''}
              onChange={handleFamilyChanged}
            />
          </FormItemWrapper>
          <FormItemWrapper marginVertical={10}>
            <TextInput
              fullWidth
              property="userName"
              label={getResource('common:labelUserName')}
              value={model.userAccount.userName}
              onChange={handleAccountChanged}
            />
          </FormItemWrapper>
          <FormItemWrapper marginVertical={10}>
            <FormItemWrapper marginVertical={10}>
              <TextInput
                fullWidth
                property="secret"
                isPassword
                label={getResource('common:labelPassword')}
                value={model.userAccount.secret}
                onChange={handleAccountChanged}
              />
            </FormItemWrapper>
          </FormItemWrapper>
          <FormItemWrapper marginVertical={10}>
            <Grid>
              <Grid item xs={12} style={{ display: 'flex', justifyContent: 'flex-end', padding: 10, gap: 6 }}>
                <LinkButton title={getResource('common:labelNavigateToLogin')} onClick={navigateToLogin} />
                <SubmitButton
                  title={getResource('common:labelRegister')}
                  variant="outlined"
                  disabled={!canSave}
                  onClick={handleRegister}
                />
              </Grid>
            </Grid>
          </FormItemWrapper>
        </Grid>
      </Paper>
    </Grid>
  );
};

export default RegisterPage;
