import React from 'react';
import { NavigationTypeEnum } from '../../_lib/enums/NavigationTypeEnum';
import { Pressable, Text, StyleSheet, View } from 'react-native';
import { ColorEnum } from '../../_lib/enums/ColorEnum';
import { BorderRadiusEnum } from '../../_lib/enums/BorderRadiusEnum';
import { FontSizeEnum } from '../../_lib/enums/FontSizeEnum';
import { useNavigation } from '@react-navigation/native';

interface IProps {
  to: NavigationTypeEnum;
  label: string;
}

const LinkButton: React.FC<IProps> = (props) => {
  const { to, label } = props;

  const navigation = useNavigation();

  const onNavigate = React.useCallback(async () => {
    navigation.navigate(to as never);
  }, [to]);

  return (
    <Pressable style={styles.button} onPress={onNavigate}>
      <View style={styles.spacer}>
        <Text style={styles.text}>{label}</Text>
      </View>
    </Pressable>
  );
};

const styles = StyleSheet.create({
  button: {
    borderColor: ColorEnum.LightGray,
    borderWidth: 2,
    borderRadius: BorderRadiusEnum.Medium,
  },
  spacer: {
    display: 'flex',
    alignItems: 'center',
  },
  text: {
    textAlign: 'center',
    fontSize: FontSizeEnum.xl,
    color: ColorEnum.White,
  },
});
export default LinkButton;
