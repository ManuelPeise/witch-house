import React from 'react';
import { LineChart } from 'react-native-chart-kit';
import { ChartData } from '../../_lib/api/types';
import { Dataset } from 'react-native-chart-kit/dist/HelperTypes';
import { AbstractChartConfig } from 'react-native-chart-kit/dist/AbstractChart';
import { View, Text, ViewStyle, StyleSheet } from 'react-native';
import { ColorEnum } from '../../_lib/enums/ColorEnum';

interface IProps {
  label: string;
  yLabel: string;
  height: number;
  width: number;
  chartData: ChartData;
  config: AbstractChartConfig;
  bezier?: boolean;
  style: Partial<ViewStyle>;
}

const LineChartDiagram: React.FC<IProps> = (props) => {
  const { width, height, chartData, config, bezier, style, label, yLabel } = props;

  const dataSets = React.useMemo((): Dataset[] => {
    return chartData.dataSets.map((data) => {
      const set: Dataset = {
        data: data.data,
        color: data.color,
      };

      return set;
    });
  }, [chartData]);

  return (
    // <View style={styles.shadow}>
    <View style={[styles.container, styles.shadow]}>
      <View style={styles.titleContainer}>
        <Text style={styles.title}>{label}</Text>
      </View>
      <View style={styles.chartContainer}>
        <LineChart
          width={width}
          height={height}
          // yAxisLabel={yLabel}
          //   xAxisLabel="Test"
          //   yLabelsOffset={5}
          data={{
            labels: chartData.labels,
            datasets: dataSets,
          }}
          chartConfig={config}
          bezier={bezier}
          style={style}
        />
      </View>
    </View>
    // </View>
  );
};

const styles = StyleSheet.create({
  container: {
    width: '100%',
    display: 'flex',
    alignItems: 'center',
    padding: 20,
    backgroundColor: '#ffffff',
    marginBottom: 7,
    marginTop: 7,
    shadowColor: 'red',
    shadowOffset: { width: 50, height: 50 },
    shadowRadius: 25,
    shadowOpacity: 1,
    borderRadius: 2,
  },
  shadow: {
    shadowColor: ColorEnum.Black,
    shadowOffset: { width: 5, height: 5 },
    shadowRadius: 0,
    shadowOpacity: 0.8,
    elevation: 5,
  },
  titleContainer: {
    width: '100%',
    display: 'flex',
    alignItems: 'flex-start',
  },
  title: {
    paddingLeft: 20,
    fontSize: 18,
    fontWeight: '600',
  },
  chartContainer: {
    padding: 5,
  },
});
export default LineChartDiagram;
