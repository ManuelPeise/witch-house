import React, { PropsWithChildren } from 'react';
import { ImageBackground, ImageSourcePropType, SafeAreaView, StyleSheet, View } from 'react-native';

interface IProps extends PropsWithChildren {
  image: ImageSourcePropType;
}

const PrivatePageWrapper: React.FC<IProps> = (props) => {
  const { children, image } = props;

  return (
    <SafeAreaView>
      <View style={styles.wrapper}>
        <ImageBackground style={styles.image} source={image}>
          {children}
        </ImageBackground>
      </View>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  wrapper: {
    height: '100%',
    width: '100%',
  },
  image: {
    height: '100%',
    width: '100%',
  },
});

export default PrivatePageWrapper;
