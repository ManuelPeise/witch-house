import { DrawerContentComponentProps, DrawerContentScrollView } from '@react-navigation/drawer';
import React from 'react';
import { StyleSheet, Text, View } from 'react-native';
import { useAuth } from '../../_hooks/useAuth';
import { ColorEnum } from '../../_lib/enums/ColorEnum';
import { useI18n } from '../../_hooks/useI18n';
import AppDrawerItem from './AppDrawerItem';
import { NavigationTypeEnum } from '../../_lib/enums/NavigationTypeEnum';
import ProfileImageInput from '../../_components/_inputs/ProfileImageInput';
import { useApi } from '../../_hooks/useApi';
import { UserData } from '../../_lib/types';
import { ProfileImageUploadModel } from '../../_lib/api/types';
import { endPoints } from '../../_lib/api/apiConfiguration';

interface IProps {
  containerProps: DrawerContentComponentProps;
}
const base64Prefix = 'data:image/png;base64,';

const AppDrawer: React.FC<IProps> = (props) => {
  const { getUserDataReducerState } = useAuth();
  const { getResource } = useI18n();
  const { sendPostRequest } = useApi<UserData>();

  const routeNames = React.useMemo(() => {
    return props.containerProps.state.routeNames;
  }, []);

  const data = getUserDataReducerState();

  const getDisplayName = React.useCallback(
    (route: string) => {
      switch (route) {
        case NavigationTypeEnum.Home:
          return getResource('common:menuItemHome');
        case NavigationTypeEnum.TrainingOverview:
          return getResource('common:menuItemTraining');
        case NavigationTypeEnum.Settings:
          return getResource('common:menuItemSettings');
        case NavigationTypeEnum.UnitStatistics:
          return getResource('common:menuItemUnitStatistics');
      }
    },
    [getResource]
  );

  const routesToDisplay = React.useMemo(() => {
    return [
      NavigationTypeEnum.Home,
      NavigationTypeEnum.TrainingOverview,
      NavigationTypeEnum.UnitStatistics,
      NavigationTypeEnum.Settings,
    ];
  }, []);

  const onSaveImage = React.useCallback(
    async (img: string) => {
      const model: ProfileImageUploadModel = {
        userId: data?.userId,
        profileImage: `${base64Prefix}${img}`,
      };

      await sendPostRequest(endPoints.account.imageUpload, model);
    },
    [data, sendPostRequest]
  );

  return (
    <View style={styles.drawer}>
      <DrawerContentScrollView
        {...props.containerProps}
        contentContainerStyle={{ flex: 1, display: 'flex', justifyContent: 'space-between' }}
      >
        <View style={styles.background}>
          <View style={styles.userImgContainer}>
            <ProfileImageInput
              variant="round"
              size={100}
              imageSrc={data?.profileImage}
              disabled={false}
              quality={1}
              onSaveImage={onSaveImage}
            />
          </View>
          <Text style={styles.userName}>
            {getResource('common:labelLoggedInAs').replace('{UserName}', data?.userName)}
          </Text>
        </View>
        <View style={styles.drawerMenu}>
          {routeNames.map((route, index) => {
            if (routesToDisplay.includes(route as NavigationTypeEnum)) {
              return (
                <AppDrawerItem
                  key={index}
                  displayName={getDisplayName(route)}
                  to={route}
                  index={index}
                  selectedIndex={props.containerProps.state.index}
                />
              );
            }
          })}
        </View>
      </DrawerContentScrollView>
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
  background: {
    display: 'flex',
    alignContent: 'center',
    justifyContent: 'flex-end',
    marginTop: -30,
    height: 250,
    width: '100%',
  },
  userImgContainer: {
    width: '100%',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'flex-end',
    padding: 5,
    opacity: 0.7,
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
