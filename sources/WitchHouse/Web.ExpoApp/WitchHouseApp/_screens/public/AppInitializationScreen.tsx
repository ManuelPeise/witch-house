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

type InitializationModel = {
  userName: string;
  pin: string;
  pinReplication: string;
};

const initialization: InitializationModel = {
  userName: '',
  pin: '',
  pinReplication: '',
};

const AppInitializationScreen: React.FC = () => {
  const { getResource } = useI18n();
  const [model, setModel] = React.useState<InitializationModel>(initialization);

  React.useEffect(() => {
    setModel(initialization);
  }, []);

  const onUserNameChanged = React.useCallback(
    (value: string) => {
      setModel({ ...model, userName: value });
    },
    [model]
  );

  const onPinChanged = React.useCallback(
    (value: string) => {
      setModel({ ...model, pin: value });
    },
    [model]
  );

  const onPinReplicationChanged = React.useCallback(
    (value: string) => {
      setModel({ ...model, pinReplication: value });
    },
    [model]
  );

  const initializeDisabled = React.useMemo(() => {
    return model.userName.length < 6 || model.pin.length < 8 || model.pin !== model.pinReplication;
  }, [model]);

  return (
    <AuthPageWrapper>
      <ScrollView automaticallyAdjustKeyboardInsets contentContainerStyle={{ flex: 1 }}>
        <View style={styles.container}>
          <View style={styles.paper}>
            <View style={styles.item}>
              <View style={styles.iconContainer}>
                <Ionicons name="settings-outline" size={50}></Ionicons>
              </View>
            </View>
            <View style={styles.item}>
              <TextField
                value={model.userName}
                onChange={onUserNameChanged}
                placeholder={getResource('common:placeHolderUserName')}
              />
            </View>
            <View style={styles.item}>
              <TextField
                value={model.pin}
                onChange={onPinChanged}
                isPassword
                placeholder={getResource('common:placeHolderPin')}
              />
            </View>
            <View style={styles.item}>
              <TextField
                value={model.pinReplication}
                onChange={onPinReplicationChanged}
                isPassword
                placeholder={getResource('common:placeHolderPinReplication')}
              />
            </View>
            <View style={[styles.item, styles.buttonContainer]}>
              <SubmitButton
                label={getResource('common:labelInitializeApp')}
                backGround="blue"
                fullWidth
                disabled={initializeDisabled}
                color={ColorEnum.LightGray}
                fontSize={FontSizeEnum.md}
                onPress={async () => {}}
              />
            </View>
          </View>
        </View>
      </ScrollView>
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
    height: 400,
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
export default AppInitializationScreen;
