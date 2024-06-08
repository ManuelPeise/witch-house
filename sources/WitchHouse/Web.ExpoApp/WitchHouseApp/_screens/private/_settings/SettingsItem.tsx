import React, { PropsWithChildren } from 'react';
import { StyleSheet, View, Text } from 'react-native';
import { ColorEnum } from '../../../_lib/enums/ColorEnum';
import { BorderRadiusEnum } from '../../../_lib/enums/BorderRadiusEnum';

interface IProps extends PropsWithChildren {
  title: string;
}

const SettingsItem: React.FC<IProps> = (props) => {
  const { title, children } = props;
  return (
    <View style={styles.container}>
      <View style={styles.titleContainer}>
        <Text style={styles.title}>{title}</Text>
        <View style={styles.divider}></View>
      </View>
      <View style={styles.childContainer}>{children}</View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    margin: 0,
    width: '100%',
    height: 'auto',
    padding: 16,
    borderRadius: BorderRadiusEnum.Small,
    backgroundColor: ColorEnum.White,
    opacity: 0.9,
    display: 'flex',
    flexDirection: 'column',
  },
  titleContainer: {
    display: 'flex',
    flexDirection: 'column',
    width: '100%',
  },
  title: {
    color: ColorEnum.Gray,
    fontSize: 18,
    paddingLeft: 20,
  },
  divider: {
    width: '100%',
    height: 1,
    backgroundColor: ColorEnum.Gray,
  },
  childContainer: {
    width: '100%',
  },
});

export default SettingsItem;
