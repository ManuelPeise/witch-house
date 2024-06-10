import React from 'react';
import { Pressable, Text, StyleSheet, View } from 'react-native';
import { ColorEnum } from '../../_lib/enums/ColorEnum';
import { BorderRadiusEnum } from '../../_lib/enums/BorderRadiusEnum';
import { FontSizeEnum } from '../../_lib/enums/FontSizeEnum';

interface IProps {
  label: string;
  disabled?: boolean;
  backGround?: 'blue' | 'transparent';
  borderColor?: ColorEnum;
  borderRadius?: BorderRadiusEnum;
  fontSize: FontSizeEnum;
  fullWidth?: boolean;
  color?: ColorEnum;
  width?: number;
  onPress: () => Promise<void>;
}

const SubmitButton: React.FC<IProps> = (props) => {
  const { disabled, label, fontSize, fullWidth, backGround, borderColor, color, borderRadius, width, onPress } = props;

  const backGroundStyle = disabled ? styles['disabled'] : styles[backGround];

  return (
    <Pressable
      disabled={disabled}
      style={[
        backGroundStyle,
        styles.button,
        { width: fullWidth ? '100%' : width, borderColor: borderColor, borderRadius: borderRadius },
      ]}
      onPress={onPress}
    >
      <View style={styles.spacer}>
        <Text style={[styles.text, { fontSize: fontSize, color: color }]}>{label}</Text>
      </View>
    </Pressable>
  );
};

const styles = StyleSheet.create({
  button: {
    borderWidth: 2,
    paddingHorizontal: 8,
    paddingVertical: 4,
  },
  spacer: {
    display: 'flex',
    alignItems: 'center',
  },
  text: {
    textAlign: 'center',
  },
  disabled: {
    color: ColorEnum.Gray,
    backgroundColor: 'transparent',
    borderWidth: 0,
    borderRadius: BorderRadiusEnum.Small,
  },
  blue: {
    color: ColorEnum.White,
    backgroundColor: ColorEnum.Blue,
    borderWidth: 0,
    borderRadius: BorderRadiusEnum.Small,
  },
  transparent: {
    color: ColorEnum.Black,
    backgroundColor: 'transparent',
  },
});
export default SubmitButton;
