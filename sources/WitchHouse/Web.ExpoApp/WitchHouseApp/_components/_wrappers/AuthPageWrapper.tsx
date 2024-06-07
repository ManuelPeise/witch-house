import React, { PropsWithChildren } from 'react';
import { ImageBackground, SafeAreaView, StyleSheet, View } from 'react-native';
import background from '../../img/startScreen.png';

interface IProps extends PropsWithChildren {}

const AuthPageWrapper: React.FC<IProps> = (props) => {
  const { children } = props;
  return (
    <SafeAreaView>
      <View style={styles.wrapper}>
        <ImageBackground style={styles.image} source={background} resizeMode="cover">
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
export default AuthPageWrapper;
