import React from 'react';
import { ColorEnum } from '../../_lib/enums/ColorEnum';
import { ActivityIndicator } from 'react-native';

interface IProps {
  color: ColorEnum;
  size: 'small' | 'large';
}

const Spinner: React.FC<IProps> = (props) => {
  const { size, color } = props;

  const scale = React.useMemo(() => {
    if (size === 'small') {
      return 2;
    } else {
      return 4;
    }
  }, [size]);

  return <ActivityIndicator size={size} style={{ transform: [{ scale: scale }] }} color={color} />;
};

export default Spinner;
