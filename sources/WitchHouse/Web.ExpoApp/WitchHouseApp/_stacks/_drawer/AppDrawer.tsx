import {
  DrawerContentComponentProps,
  DrawerContentScrollView,
  DrawerItem,
  DrawerItemList,
} from '@react-navigation/drawer';
import React from 'react';
import { ImageBackground, Pressable, StyleSheet, Text, View } from 'react-native';
import witchHouse from '../../img/witchHouse.jpg';
import { useAuth } from '../../_hooks/useAuth';
import { ColorEnum } from '../../_lib/enums/ColorEnum';
import { useI18n } from '../../_hooks/useI18n';
import AppDrawerItem from './AppDrawerItem';
import LoadingOverLay from '../../_components/_loading/LoadingOverlay';
import { useDataSync } from '../../_hooks/useDataSync';

interface IProps {
  containerProps: DrawerContentComponentProps;
}

const AppDrawer: React.FC<IProps> = (props) => {
  const { loginResult, onLogout } = useAuth();
  const { getResource } = useI18n();
  const { data, isLoading, executeDataSync } = useDataSync();

  const routeNames = React.useMemo(() => {
    return props.containerProps.state.routeNames;
  }, []);

  const getDisplayName = React.useCallback(
    (route: string) => {
      switch (route) {
        case 'home':
          return getResource('common:menuItemHome');
        case 'training':
          return getResource('common:menuItemTraining');
        case 'settings':
          return getResource('common:menuItemSettings');
      }
    },
    [getResource]
  );

  return (
    <View style={styles.drawer}>
      <DrawerContentScrollView
        {...props.containerProps}
        contentContainerStyle={{ flex: 1, display: 'flex', justifyContent: 'space-between' }}
      >
        <ImageBackground source={witchHouse} style={styles.imgBackground}>
          <Text style={styles.userName}>
            {getResource('common:labelLoggedInAs').replace('{UserName}', loginResult?.userName)}
          </Text>
        </ImageBackground>
        <View style={styles.drawerMenu}>
          {routeNames.map((route, index) => {
            return (
              <AppDrawerItem
                key={index}
                displayName={getDisplayName(route)}
                to={route}
                index={index}
                selectedIndex={props.containerProps.state.index}
              />
            );
          })}
        </View>
        <View style={styles.footer}>
          <Pressable onPress={onLogout}>
            <Text style={{ textAlign: 'center' }}>{getResource('common:labelLogout')}</Text>
          </Pressable>
        </View>
      </DrawerContentScrollView>
      {isLoading && <LoadingOverLay scale={4} size="large" color={ColorEnum.Blue} />}
    </View>
  );
};

const styles = StyleSheet.create({
  drawer: {
    padding: 0,
    margin: 0,
    marginTop: 0,
    width: '100%',
    height: '100%',
    display: 'flex',
    justifyContent: 'space-around',
  },
  imgBackground: {
    display: 'flex',
    alignContent: 'center',
    justifyContent: 'flex-end',
    marginTop: -30,
    height: 250,
    width: '100%',
  },
  userName: {
    padding: 20,
    color: ColorEnum.LightGray,
    fontSize: 18,
    textAlign: 'center',
  },
  drawerMenu: {
    flex: 1,
    display: 'flex',
    alignItems: 'flex-start',
    alignContent: 'flex-start',
    justifyContent: 'flex-start',
  },
  footer: {
    display: 'flex',
    justifyContent: 'center',
    height: 40,
    backgroundColor: ColorEnum.Lightblue,
  },
});
export default AppDrawer;
