import { createStackNavigator } from '@react-navigation/stack';
import React from 'react';
import { NavigationTypeEnum } from '../_lib/enums/NavigationTypeEnum';
import WelcomeScreen from '../_screens/public/WelcomeScreen';
import LoginScreen from '../_screens/public/LoginScreen';
import LoadingOverLay from '../_components/_loading/LoadingOverlay';
import { ColorEnum } from '../_lib/enums/ColorEnum';
4;
interface IProps {
  isLoading: boolean;
}

const Stack = createStackNavigator();

const AuthStack: React.FC<IProps> = (props) => {
  const { isLoading } = props;
  return (
    <>
      <Stack.Navigator initialRouteName={NavigationTypeEnum.Welcome} screenOptions={{ headerShown: false }}>
        <Stack.Screen name={NavigationTypeEnum.Welcome} component={WelcomeScreen} />
        <Stack.Screen name={NavigationTypeEnum.Login} component={LoginScreen} />
      </Stack.Navigator>
      {isLoading && <LoadingOverLay color={ColorEnum.Blue} size="large" scale={4} />}
    </>
  );
};

export default AuthStack;
