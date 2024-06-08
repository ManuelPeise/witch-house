import React from 'react';
import { ColorEnum } from '../../_lib/enums/ColorEnum';
import { ActivityIndicator } from 'react-native';

interface IProps {
  color: ColorEnum;
  size: 'small' | 'large';
  scale: number;
}

const Spinner: React.FC<IProps> = (props) => {
  const { size, color, scale } = props;

  return <ActivityIndicator size={size} style={{ transform: [{ scale: scale }] }} color={color} />;
};

export default Spinner;
