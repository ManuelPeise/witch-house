import React from 'react';
import { Dimensions, RefreshControl, ScrollView, StyleSheet, Text, View } from 'react-native';
import { useApi } from '../../../_hooks/useApi';
import { ModuleConfiguration, UnitResultStatisticModel } from '../../../_lib/types';
import { endPoints } from '../../../_lib/api/apiConfiguration';
import { useAuth } from '../../../_hooks/useAuth';
import LineChartDiagram from '../../../_components/_charts/LineChartDiagram';
import { ChartDataSet } from '../../../_lib/api/types';
import { ColorEnum } from '../../../_lib/enums/ColorEnum';
import { UnitTypeEnum } from '../../../_lib/enums/UnitTypeEnum';
import { useI18n } from '../../../_hooks/useI18n';
import LoadingOverLay from '../../../_components/_loading/LoadingOverlay';
import * as SecureStore from 'expo-secure-store';
import { SecureStoreKeyEnum } from '../../../_lib/enums/SecureStoreKeyEnum';
import { ModuleTypeEnum } from '../../../_lib/enums/ModuleTypeEnum';
import UnitStatisticPlaceholder from './UnitStatisticPlaceholder';

const UnitStatisticScreen: React.FC = () => {
  const { data, get } = useApi<UnitResultStatisticModel[]>();
  const { getUserModule } = useAuth();
  const { getResource } = useI18n();
  const [isLoading, setIsLoading] = React.useState<boolean>(false);

  const module = getUserModule(ModuleTypeEnum.SchoolTrainingStatistics);
  const hasAccess = React.useMemo((): boolean => {
    return module.isActive;
  }, [module]);

  const onLoadStatisticData = React.useCallback(async () => {
    setIsLoading(true);
    await get(endPoints.training.getTrainingResultStatistics.replace('{userId}', module.accountGuid));
    setIsLoading(false);
  }, [module, get]);

  React.useEffect(() => {
    if (hasAccess) {
      setIsLoading(true);
      get(endPoints.training.getTrainingResultStatistics.replace('{userId}', module.accountGuid));
      setIsLoading(false);
    }
  }, [hasAccess, module]);

  const getLabel = React.useCallback((type: UnitTypeEnum): string => {
    switch (type) {
      case UnitTypeEnum.Addition:
        return getResource('common:labelAdditionStatistics');
      case UnitTypeEnum.Subtract:
        return getResource('common:labelSubtractStatistics');
      case UnitTypeEnum.Multiply:
        return getResource('common:labelMultiplyStatistics');
      case UnitTypeEnum.Divide:
        return getResource('common:labelDivideStatistics');
      case UnitTypeEnum.Doubling:
        return getResource('common:labelDoublingStatistics');
      case UnitTypeEnum.Letters:
        return getResource('common:labelLetterStatistics');
    }
  }, []);

  const getChartData = React.useCallback(
    (model: UnitResultStatisticModel, red: number, green: number, blue: number): ChartDataSet[] => {
      const sets: ChartDataSet[] = [];

      sets.push({
        data: model.entries.map((e) => e.success),
        color: (opacity: number) => `rgba(66,245,114, ${opacity})`,
      });
      sets.push({
        data: model.entries.map((e) => e.failed),
        color: (opacity: number) => `rgba(245,102,66, ${opacity})`,
      });
      return sets;
    },
    []
  );

  const sortedData = React.useMemo(() => {
    return data?.sort((x, y) => x.unitType - y.unitType) ?? [];
  }, [data]);

  if (!hasAccess || (hasAccess && sortedData?.length === 0)) {
    return <UnitStatisticPlaceholder hasAccess={hasAccess} statisticDataAvailable={sortedData?.length > 0} />;
  }

  return (
    <View>
      <View style={styles.container}>
        <ScrollView
          showsVerticalScrollIndicator={false}
          refreshControl={<RefreshControl refreshing={isLoading} onRefresh={onLoadStatisticData} />}
        >
          {sortedData?.length &&
            sortedData?.map((set, index) => {
              return (
                <LineChartDiagram
                  key={index}
                  label={getLabel(set.unitType)}
                  width={Dimensions.get('window').width - 50}
                  height={200}
                  yLabel="Points"
                  chartData={{
                    labels: set.entries.map((entity) => entity.timeStamp),
                    dataSets: getChartData(set, 100, 100, 30),
                  }}
                  config={{
                    backgroundColor: ColorEnum.White,
                    backgroundGradientFrom: ColorEnum.BlackBlue,
                    backgroundGradientTo: ColorEnum.Black,
                    decimalPlaces: 0, // optional, defaults to 2dp
                    color: (opacity = 0.3) => ColorEnum.White,
                    labelColor: (opacity = 1) => ColorEnum.White,
                    propsForDots: {
                      r: '4',
                      strokeWidth: '2',
                      stroke: ColorEnum.BlackBlue,
                    },
                  }}
                  style={{
                    marginVertical: 8,
                    borderRadius: 4,
                  }}
                  bezier={true}
                />
              );
            })}
        </ScrollView>
      </View>
      {isLoading && <LoadingOverLay color={ColorEnum.Blue} size="large" scale={4} />}
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    padding: 10,
    height: '100%',
  },
});
export default UnitStatisticScreen;
