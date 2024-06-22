import React from 'react';
import { Image, StyleSheet, View, Text } from 'react-native';
import noData from '../../../img/noData.png';
import { useI18n } from '../../../_hooks/useI18n';

interface IProps {
  hasAccess: boolean;
  statisticDataAvailable: boolean;
}

const UnitStatisticPlaceholder: React.FC<IProps> = (props) => {
  const { hasAccess, statisticDataAvailable } = props;
  const { getResource } = useI18n();

  const textElement = React.useMemo(() => {
    if (!hasAccess) {
      return <Text style={[styles.text, styles.accessDenied]}>{getResource('common:labelAccessDenied')}</Text>;
    }

    if (hasAccess && !statisticDataAvailable) {
      return <Text style={styles.text}>{getResource('common:labelNoStatisticData')}</Text>;
    }
  }, [hasAccess, statisticDataAvailable]);

  return (
    <View style={styles.container}>
      <Image style={styles.image} source={noData} resizeMode="center" />
      <View style={styles.textContainer}>{textElement}</View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    width: '100%',
    height: '100%',
    display: 'flex',
    alignItems: 'center',
  },
  image: {
    display: 'flex',
    alignItems: 'center',
  },
  textContainer: {
    marginTop: -100,
    display: 'flex',
  },
  text: {
    textAlign: 'center',
    fontSize: 30,
  },
  accessDenied: {
    color: 'red',
  },
});
export default UnitStatisticPlaceholder;
