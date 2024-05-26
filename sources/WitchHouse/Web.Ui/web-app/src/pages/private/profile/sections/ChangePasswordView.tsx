import { Grid } from '@mui/material';
import React from 'react';
import FormItemWrapper from '../../../../components/wrappers/FormItemWrapper';
import TextInput from '../../../../components/inputs/TextInput';
import { useI18n } from '../../../../hooks/useI18n';
import SubmitButton from '../../../../components/buttons/SubmitButton';
import { useApi } from '../../../../hooks/useApi';
import { endpoints } from '../../../../lib/api/apiConfiguration';
import { useAuth } from '../../../../lib/AuthContext';

type PasswordSettings = {
  currentPassword: string;
  newPassword: string;
  passwordReplication: string;
};
interface IProps {}

const ChangePasswordView: React.FC<IProps> = (props) => {
  const { getResource } = useI18n();
  const { loginResult } = useAuth();
  const passwordApi = useApi<boolean>();
  const passwordUpdateApi = useApi<boolean>();

  const [passwordSettings, setPasswordSettings] = React.useState<PasswordSettings>({
    currentPassword: '',
    newPassword: '',
    passwordReplication: '',
  });

  const handleChange = React.useCallback(
    async (key: string, value: string) => {
      setPasswordSettings({ ...passwordSettings, [key]: value });
    },
    [passwordSettings]
  );

  const handleCheckCurrentPassword = React.useCallback(async () => {
    if (loginResult != null) {
      await passwordApi.post(
        endpoints.account.passwordCheck,
        JSON.stringify({ userId: loginResult.userId, password: passwordSettings.currentPassword })
      );
    }
  }, [passwordSettings, passwordApi, loginResult]);

  const cancelDisabled = React.useMemo(() => {
    return (
      passwordSettings.currentPassword === '' &&
      passwordSettings.newPassword === '' &&
      passwordSettings.passwordReplication === ''
    );
  }, [passwordSettings]);

  const saveDisabled = React.useMemo(() => {
    return (
      passwordSettings.currentPassword.length < 5 ||
      passwordSettings.newPassword.length < 8 ||
      passwordSettings.newPassword !== passwordSettings.passwordReplication ||
      passwordSettings.currentPassword === passwordSettings.newPassword
    );
  }, [passwordSettings]);

  const handleCancel = React.useCallback(async () => {
    setPasswordSettings({ currentPassword: '', newPassword: '', passwordReplication: '' });
  }, []);

  const handleChangePassword = React.useCallback(async () => {
    if (loginResult != null) {
      await passwordUpdateApi.post(
        endpoints.account.updatePassword,
        JSON.stringify({ userId: loginResult.userId, password: passwordSettings.newPassword })
      );
    }
  }, [loginResult, passwordSettings, passwordUpdateApi]);

  React.useEffect(() => {
    if (passwordUpdateApi.data) {
      handleCancel();
    }
  }, [passwordUpdateApi, handleCancel]);

  return (
    <Grid container style={{ width: '100%', padding: '2rem' }}>
      <Grid item xs={12}>
        <FormItemWrapper marginVertical={20}>
          <TextInput
            property="currentPassword"
            label={getResource('common:labelCurrentPassword')}
            fullWidth
            disabled={false}
            isPassword
            value={passwordSettings.currentPassword}
            onChange={handleChange}
          />
        </FormItemWrapper>
        <FormItemWrapper marginVertical={25}>
          <Grid>
            <Grid item xs={12} style={{ display: 'flex', justifyContent: 'flex-end', padding: 10, gap: 6 }}>
              <SubmitButton
                title={getResource('common:labelCheckPassword')}
                variant="outlined"
                disabled={passwordSettings.currentPassword.length < 6}
                onClick={handleCheckCurrentPassword}
              />
            </Grid>
          </Grid>
        </FormItemWrapper>
        <FormItemWrapper marginVertical={20}>
          <TextInput
            property="newPassword"
            label={getResource('common:labelNewPassword')}
            fullWidth
            disabled={passwordApi.data == null && !passwordApi.data}
            isPassword={true}
            value={passwordSettings.newPassword}
            onChange={handleChange}
          />
        </FormItemWrapper>
        <FormItemWrapper marginVertical={20}>
          <TextInput
            property="passwordReplication"
            label={getResource('common:labelPasswordReplication')}
            fullWidth
            disabled={passwordApi.data == null && !passwordApi.data}
            isPassword={true}
            value={passwordSettings.passwordReplication}
            onChange={handleChange}
          />
        </FormItemWrapper>
        <FormItemWrapper marginVertical={25}>
          <Grid>
            <Grid item xs={12} style={{ display: 'flex', justifyContent: 'flex-end', padding: 10, gap: 6 }}>
              <SubmitButton
                title={getResource('common:labelCancel')}
                variant="outlined"
                disabled={cancelDisabled}
                onClick={handleCancel}
              />
              <SubmitButton
                title={getResource('common:labelSave')}
                variant="outlined"
                disabled={saveDisabled}
                onClick={handleChangePassword}
              />
            </Grid>
          </Grid>
        </FormItemWrapper>
      </Grid>
    </Grid>
  );
};

export default ChangePasswordView;
