import React from 'react';
import { Pressable, StyleSheet, Text, View } from 'react-native';
import { ColorEnum } from '../../_lib/enums/ColorEnum';
import { useNavigation } from '@react-navigation/native';

interface IProps {
  displayName: string;
  to: string;
  index: number;
  selectedIndex: number;
}

const AppDrawerItem: React.FC<IProps> = (props) => {
  const { displayName, to, index, selectedIndex } = props;
  const navigate = useNavigation();
  const buttonStyle = [styles.item, selectedIndex === index ? styles.selected : null];

  const handleNavigate = React.useCallback(() => {
    navigate.navigate(to as never);
  }, [navigate, to]);

  return (
    <Pressable style={buttonStyle} onPress={handleNavigate}>
      <Text style={styles.text}>{displayName}</Text>
      <View style={styles.divider}></View>
    </Pressable>
  );
};

const styles = StyleSheet.create({
  item: {
    width: '100%',
  },
  text: {
    fontSize: 24,
    paddingLeft: 30,
    padding: 10,
  },
  divider: {
    minHeight: 1,
    width: '100%',
    backgroundColor: ColorEnum.Black,
  },
  selected: {
    backgroundColor: ColorEnum.LightGray,
  },
});
export default AppDrawerItem;
