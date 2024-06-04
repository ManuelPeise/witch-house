import React from 'react';
import { GermanSettings, MathSettings } from '../types';
import { ModuleTypeEnum } from '../../../../lib/enums/ModuleTypeEnum';
import { Grid, List, ListItem } from '@mui/material';
import CheckboxInput from '../../../../components/inputs/CheckboxInput';
import NumberInput from '../../../../components/inputs/NumberInput';
import { useI18n } from '../../../../hooks/useI18n';
import SubmitButton from '../../../../components/buttons/SubmitButton';

interface IProps {
  moduleType: ModuleTypeEnum;
  moduleSettings: MathSettings | GermanSettings;
  disabled: boolean;
}

const ModuleSettings: React.FC<IProps> = (props) => {
  const { moduleSettings, moduleType, disabled } = props;
  const { getResource } = useI18n();

  const [state, setState] = React.useState<MathSettings | GermanSettings>(moduleSettings);

  React.useEffect(() => {
    setState(moduleSettings);
  }, [moduleSettings]);

  const mathSettings = React.useMemo(() => {
    return state as MathSettings;
  }, [state]);

  // const configuration = getModuleSettingsConfiguration(moduleType);

  const handleChanged = React.useCallback(
    (key: string, value: any) => {
      setState({ ...state, [key]: value });
    },
    [state]
  );

  const cancelDisabled = React.useMemo(() => {
    return JSON.stringify(state) === JSON.stringify(moduleSettings);
  }, [state, moduleSettings]);

  const saveDisabled = React.useMemo(() => {
    return cancelDisabled;
  }, [cancelDisabled]);

  const handleCancel = React.useCallback(async () => {
    // setState(
    //   moduleType === ModuleTypeEnum.MathUnits ? (moduleSettings as MathSettings) : (moduleSettings as GermanSettings)
    // );
  }, [moduleSettings, moduleType]);

  console.log('STATE:', JSON.stringify(state));
  return (
    <ListItem divider>
      <Grid container>
        {/* <List disablePadding sx={{ width: '100%' }}>
          {configuration.hasCalculationRuleSettings && (
            <ListItem sx={{ marginTop: '1rem' }}>
              <Grid item xs={6}>
                <CheckboxInput
                  property="allowAddition"
                  label={getResource('common:labelAllowAddition')}
                  checked={mathSettings.allowAddition}
                  disabled={disabled}
                  onChange={handleChanged}
                />
              </Grid>
              <Grid item xs={6}>
                <CheckboxInput
                  property="allowSubtraction"
                  label={getResource('common:labelAllowSubtract')}
                  checked={mathSettings.allowSubtraction}
                  disabled={disabled}
                  onChange={handleChanged}
                />
              </Grid>
              <Grid item xs={6}>
                <CheckboxInput
                  property="allowMultiply"
                  label={getResource('common:labelAllowMultiply')}
                  checked={mathSettings.allowMultiply}
                  disabled={disabled}
                  onChange={handleChanged}
                />
              </Grid>
              <Grid item xs={6}>
                <CheckboxInput
                  property="allowDivide"
                  label={getResource('common:labelAllowDivide')}
                  checked={mathSettings.allowDivide}
                  disabled={disabled}
                  onChange={handleChanged}
                />
              </Grid>
            </ListItem>
          )}

          {configuration.hasMaxValue && (
            <ListItem sx={{ marginTop: '1rem' }}>
              <Grid item xs={12}>
                <NumberInput
                  label={getResource('common:labelMaxValue')}
                  property="maxValue"
                  fullWidth={true}
                  disabled={disabled}
                  value={mathSettings.maxValue}
                  onChange={handleChanged}
                />
              </Grid>
            </ListItem>
          )}
          {configuration.hasMaxWordLength && (
            <ListItem sx={{ marginTop: '1rem' }}>
              <Grid item xs={12}>
                <NumberInput
                  label={getResource('common:labelMaxWordLength')}
                  property="maxWordLength"
                  fullWidth={true}
                  disabled={disabled}
                  value={(state as GermanSettings).maxWordLength}
                  onChange={handleChanged}
                />
              </Grid>
            </ListItem>
          )}
          <ListItem>
            <Grid container sx={{ display: 'flex', justifyContent: 'flex-end', gap: 2, marginRight: '2rem' }}>
              <SubmitButton
                variant="text"
                title={getResource('common:labelCancel')}
                disabled={cancelDisabled}
                onClick={handleCancel}
              />
              <SubmitButton
                variant="text"
                title={getResource('common:labelSave')}
                disabled={saveDisabled}
                onClick={async () => {}}
              />
            </Grid>
          </ListItem>
        </List> */}
      </Grid>
    </ListItem>
  );
};

export default ModuleSettings;
