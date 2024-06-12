import React from 'react';
import { createDrawerNavigator } from '@react-navigation/drawer';
import { NavigationTypeEnum } from '../_lib/enums/NavigationTypeEnum';
import HomeScreen from '../_screens/private/HomeScreen';
import { View, Text } from 'react-native';
import IconButton from '../_components/_inputs/IconButton';
import { useAuth } from '../_hooks/useAuth';
import { useI18n } from '../_hooks/useI18n';
import AppDrawer from './_drawer/AppDrawer';
import TrainingScreen from '../_screens/private/_training/TrainingScreen';
import SettingsScreen from '../_screens/private/_settings/SettingsScreen';
import TrainingPage from '../_screens/private/_training/TrainingPage';
import { UnitTypeEnum } from '../_lib/enums/UnitTypeEnum';

export type TrainingRouteParam = {
  rule: UnitTypeEnum;
};

export type RootParamList = {
  home: undefined;
  trainingOverview: undefined;
  settings: undefined;
  schoolTraining: { rule: UnitTypeEnum };
};

const Drawer = createDrawerNavigator<RootParamList>();

const PrivateStack: React.FC = () => {
  const { onLogout } = useAuth();
  const { getResource } = useI18n();
  return (
    <Drawer.Navigator
      initialRouteName={NavigationTypeEnum.Home}
      drawerContent={(props) => <AppDrawer containerProps={props} />}
      screenOptions={{
        title: getResource('common:labelHome'),
        headerShown: true,
        headerRight: () => {
          return (
            <View style={{ marginRight: 10 }}>
              <IconButton size={30} padding={1} icon="log-out" onPress={onLogout} />
            </View>
          );
        },
      }}
    >
      <Drawer.Screen
        name={NavigationTypeEnum.Home}
        component={HomeScreen}
        options={{ headerShown: true, title: getResource('common:headerHome') }}
      />
      <Drawer.Screen
        name={NavigationTypeEnum.TrainingOverview}
        component={TrainingScreen}
        options={{ headerShown: true, title: getResource('common:headerTraining') }}
      />
      <Drawer.Screen
        name={NavigationTypeEnum.Settings}
        component={SettingsScreen}
        options={{ headerShown: true, title: getResource('common:headerSettings') }}
      />
      <Drawer.Screen
        name={NavigationTypeEnum.SchoolTraining}
        component={TrainingPage}
        options={{ headerShown: true, title: getResource('common:headerSchoolTraining') }}
      />
    </Drawer.Navigator>
  );
};

export default PrivateStack;
