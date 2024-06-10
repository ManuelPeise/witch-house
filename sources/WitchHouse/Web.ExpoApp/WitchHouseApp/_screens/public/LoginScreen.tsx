import React from 'react';
import AuthPageWrapper from '../../_components/_wrappers/AuthPageWrapper';
import { View, StyleSheet } from 'react-native';
import { ColorEnum } from '../../_lib/enums/ColorEnum';
import { BorderRadiusEnum } from '../../_lib/enums/BorderRadiusEnum';
import TextField from '../../_components/_inputs/TextField';
import { LoginRequest } from '../../_lib/types';
import { useI18n } from '../../_hooks/useI18n';
import { Ionicons } from '@expo/vector-icons';
import { ScrollView } from 'react-native-gesture-handler';
import SubmitButton from '../../_components/_inputs/SubmitButton';
import { FontSizeEnum } from '../../_lib/enums/FontSizeEnum';
import { useAuth } from '../../_hooks/useAuth';
import LoadingOverLay from '../../_components/_loading/LoadingOverlay';

const loginModel: LoginRequest = {
  userName: '',
  password: '',
};

const LoginScreen: React.FC = () => {
  const { getResource } = useI18n();
  const { isLoading, onLogin } = useAuth();
  const [loginRequest, setLoginRequest] = React.useState<LoginRequest>(loginModel);

  React.useEffect(() => {
    setLoginRequest(loginModel);
  }, []);

  const onUserNameChanged = React.useCallback(
    (value: string) => {
      setLoginRequest({ ...loginRequest, userName: value });
    },
    [loginRequest]
  );

  const onPasswordChanged = React.useCallback(
    (value: string) => {
      setLoginRequest({ ...loginRequest, password: value });
    },
    [loginRequest]
  );

  const loginDisabled = React.useMemo(() => {
    return loginRequest.userName.length < 6 || loginRequest.password.length < 8;
  }, [loginRequest]);

  const handleLogin = React.useCallback(async () => {
    await onLogin(loginRequest);
  }, [onLogin, loginRequest]);

  return (
    <AuthPageWrapper>
      <ScrollView automaticallyAdjustKeyboardInsets contentContainerStyle={{ flex: 1 }}>
        <View style={styles.container}>
          <View style={styles.paper}>
            <View style={styles.item}>
              <View style={styles.iconContainer}>
                <Ionicons name="key" size={50}></Ionicons>
              </View>
            </View>
            <View style={styles.item}>
              <TextField
                value={loginRequest.userName}
                onChange={onUserNameChanged}
                placeholder={getResource('common:placeHolderUserName')}
              />
            </View>
            <View style={styles.item}>
              <TextField
                value={loginRequest.password}
                onChange={onPasswordChanged}
                isPassword
                placeholder={getResource('common:placeHolderPassword')}
              />
            </View>
            <View style={[styles.item, styles.buttonContainer]}>
              <SubmitButton
                label={getResource('common:labelLogin')}
                backGround="blue"
                fullWidth
                disabled={loginDisabled}
                borderRadius={BorderRadiusEnum.Medium}
                borderColor={ColorEnum.Blue}
                color={ColorEnum.White}
                fontSize={FontSizeEnum.xl}
                onPress={handleLogin}
              />
            </View>
          </View>
        </View>
      </ScrollView>
      {isLoading && <LoadingOverLay color={ColorEnum.BlackBlue} size="large" scale={4} />}
    </AuthPageWrapper>
  );
};

const styles = StyleSheet.create({
  container: {
    height: '100%',
    width: '100%',
    padding: 8,
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
  },
  paper: {
    borderRadius: BorderRadiusEnum.Medium,
    backgroundColor: ColorEnum.White,
    opacity: 0.9,
    height: 350,
    width: '80%',
    padding: 16,
  },
  item: {
    marginTop: 20,
    width: '100%',
    display: 'flex',
    alignItems: 'center',
  },
  iconContainer: {
    backgroundColor: ColorEnum.LightGray,
    padding: 10,
    borderRadius: 50,
  },
  buttonContainer: {
    padding: 10,
    width: '100%',
    display: 'flex',
    flexDirection: 'row',
    justifyContent: 'space-around',
  },
});

export default LoginScreen;
