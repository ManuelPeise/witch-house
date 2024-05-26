import { Grid, Paper } from '@mui/material';
import React from 'react';
import TextInput from '../../../components/inputs/TextInput';
import FormItemWrapper from '../../../components/wrappers/FormItemWrapper';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import SubmitButton from '../../../components/buttons/SubmitButton';
import { useAuth } from '../../../lib/AuthContext';
import { LoginImportModel } from './types';
import { useNavigate } from 'react-router-dom';
import { RouteTypes } from '../../../lib/Router';
import LinkButton from '../../../components/buttons/LinkButton';
import { useI18n } from '../../../hooks/useI18n';

const LoginPage: React.FC = () => {
  const { getResource } = useI18n();
  const { onLogin } = useAuth();
  const navigate = useNavigate();
  const [model, setModel] = React.useState<LoginImportModel>({
    userName: '',
    secret: '',
  });

  const handleChanged = React.useCallback(
    (key: string, value: string) => {
      setModel({ ...model, [key]: value });
    },
    [model]
  );

  const loginDisabled = React.useMemo(() => {
    return model.userName === '' || model.userName?.length < 5 || model.secret === '' || model.secret.length < 5;
  }, [model]);

  const handleLogin = React.useCallback(async () => {
    onLogin({ userName: model.userName, secret: model.secret });
  }, [model, onLogin]);

  const navigateToRegister = React.useCallback(async () => {
    await navigate(RouteTypes.Register, { replace: true });
  }, [navigate]);

  return (
    <Grid container style={{ display: 'flex', justifyContent: 'center', alignContent: 'center' }}>
      <Paper elevation={12} style={{ maxWidth: '50%' }}>
        <Grid container style={{ padding: '2rem 2rem' }}>
          <FormItemWrapper marginVertical={10}>
            <Grid style={{ display: 'flex', justifyContent: 'center' }}>
              <AccountCircleIcon style={{ margin: 0, padding: 0, height: 200, width: 200, color: 'lightgray' }} />
            </Grid>
          </FormItemWrapper>
          <FormItemWrapper marginVertical={10}>
            <TextInput
              property="userName"
              fullWidth
              label={getResource('common:labelUserName')}
              value={model.userName}
              onChange={handleChanged}
            />
          </FormItemWrapper>
          <FormItemWrapper marginVertical={10}>
            <TextInput
              property="secret"
              fullWidth
              isPassword
              label={getResource('common:labelPassword')}
              value={model.secret}
              onChange={handleChanged}
            />
          </FormItemWrapper>
          <FormItemWrapper marginVertical={10}>
            <Grid>
              <Grid item xs={12} style={{ display: 'flex', justifyContent: 'flex-end', padding: 10, gap: 6 }}>
                <LinkButton title={getResource('common:labelNavigateToRegister')} onClick={navigateToRegister} />
                <SubmitButton
                  title={getResource('common:labelLogin')}
                  variant="outlined"
                  disabled={loginDisabled}
                  onClick={handleLogin}
                />
              </Grid>
            </Grid>
          </FormItemWrapper>
        </Grid>
      </Paper>
    </Grid>
  );
};

export default LoginPage;
