import React from 'react';
import { ColorEnum } from '../../_lib/enums/ColorEnum';
import { StyleSheet, View } from 'react-native';
import Spinner from './Spinner';

interface IProps {
  color: ColorEnum;
  size: 'small' | 'large';
  scale: number;
}

const LoadingOverLay: React.FC<IProps> = (props) => {
  const { color, size, scale } = props;

  return (
    <View style={styles.overlay}>
      <Spinner size={size} color={color} scale={scale} />
    </View>
  );
};

const styles = StyleSheet.create({
  overlay: {
    height: '100%',
    width: '100%',
    position: 'absolute',
    left: 0,
    top: 0,
    opacity: 0.8,
    backgroundColor: 'black',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
  },
});

export default LoadingOverLay;
