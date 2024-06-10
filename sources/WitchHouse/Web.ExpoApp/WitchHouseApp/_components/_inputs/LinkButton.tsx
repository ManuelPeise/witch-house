import React from 'react';
import { NavigationTypeEnum } from '../../_lib/enums/NavigationTypeEnum';
import { Pressable, Text, StyleSheet, View } from 'react-native';
import { ColorEnum } from '../../_lib/enums/ColorEnum';
import { BorderRadiusEnum } from '../../_lib/enums/BorderRadiusEnum';
import { FontSizeEnum } from '../../_lib/enums/FontSizeEnum';
import { useNavigation } from '@react-navigation/native';
import { TrainingRouteParam } from '../../_stacks/PrivateStack';

interface IProps {
  to: NavigationTypeEnum;
  label: string;
  backGround?: 'blue' | 'transparent';
  color?: ColorEnum;
  borderColor?: ColorEnum;
  borderRadius?: BorderRadiusEnum;
  fontSize: FontSizeEnum;
  padding?: number;
  fullWidth?: boolean;
  width?: number;
  param?: TrainingRouteParam;
}

const LinkButton: React.FC<IProps> = (props) => {
  const { to, label, backGround, fullWidth, width, borderColor, borderRadius, color, fontSize, param, padding } = props;

  const { navigate } = useNavigation();
  const backGroundStyle = styles[backGround];
  const onNavigate = React.useCallback(async () => {
    if (param) {
      const parameter = { name: to as never, params: param, merge: false } as never;

      navigate(parameter);
    } else {
      navigate(to as never);
    }
  }, [navigate, to, param]);

  return (
    <Pressable
      style={[
        backGroundStyle,
        styles.button,
        { width: fullWidth ? '100%' : width, borderColor: borderColor, borderRadius: borderRadius },
      ]}
      onPress={onNavigate}
    >
      <View style={[styles.spacer, { padding: padding }]}>
        <Text style={(styles.text, { color: color, fontSize: fontSize })}>{label}</Text>
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
