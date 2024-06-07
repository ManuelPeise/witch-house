import React from 'react';

import { createDrawerNavigator } from '@react-navigation/drawer';
import { NavigationTypeEnum } from '../_lib/enums/NavigationTypeEnum';
import HomeScreen from '../_screens/private/HomeScreen';

const Drawer = createDrawerNavigator();

const PrivateStack: React.FC = () => {
  return (
    <Drawer.Navigator initialRouteName={NavigationTypeEnum.Home}>
      <Drawer.Screen name={NavigationTypeEnum.Home} component={HomeScreen} />
    </Drawer.Navigator>
  );
};

export default PrivateStack;
