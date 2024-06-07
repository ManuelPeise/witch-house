import { createStackNavigator } from '@react-navigation/stack';
import React from 'react';
import { NavigationTypeEnum } from '../_lib/enums/NavigationTypeEnum';
import WelcomeScreen from '../_screens/public/WelcomeScreen';
import LoginScreen from '../_screens/public/LoginScreen';

const Stack = createStackNavigator();

const AuthStack: React.FC = () => {
  return (
    <Stack.Navigator initialRouteName={NavigationTypeEnum.Welcome} screenOptions={{ headerShown: false }}>
      <Stack.Screen name={NavigationTypeEnum.Welcome} component={WelcomeScreen} />
      <Stack.Screen name={NavigationTypeEnum.Login} component={LoginScreen} />
    </Stack.Navigator>
  );
};

export default AuthStack;
