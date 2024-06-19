import React from 'react';
import { ModuleSettings, SchoolModule, SchoolSettings } from '../types';
import { Grid, ListItem, Typography } from '@mui/material';
import CheckboxInput from '../../../../components/inputs/CheckboxInput';
import { useI18n } from '../../../../hooks/useI18n';
import NumberInput from '../../../../components/inputs/NumberInput';
import SubmitButton from '../../../../components/buttons/SubmitButton';

interface IProps {
  module: SchoolModule;
  disabled: boolean;
  onSave: (settings: ModuleSettings) => Promise<void>;
}

const SchoolModuleSettings: React.FC<IProps> = (props) => {
  const { module, disabled, onSave } = props;
  const { getResource } = useI18n();

  const fallbackSettings: SchoolSettings = React.useMemo((): SchoolSettings => {
    return (
      module?.settings ?? {
        moduleType: module.moduleType,
        allowAddition: false,
        allowSubtraction: false,
        allowMultiply: false,
        allowDivide: false,
        allowDoubling: false,
        minValue: 0,
        maxValue: 0,
        maxWordLength: 0,
      }
    );
  }, [module]);

  const [settings, setSettings] = React.useState<SchoolSettings>(fallbackSettings);

  React.useEffect(() => {
    setSettings(module.settings ?? fallbackSettings);
  }, [module, fallbackSettings]);

  const cancelDisabled = React.useMemo(() => {
    return JSON.stringify(module.settings ?? fallbackSettings) === JSON.stringify(settings);
  }, [module, settings, fallbackSettings]);

  const saveDisabled = React.useMemo(() => {
    return JSON.stringify(module.settings) === JSON.stringify(settings) || settings.maxWordLength === 0;
  }, [module, settings]);

  const handleChange = React.useCallback(
    async (key: string, value: any) => {
      console.log(settings);
      if (key === 'minValue' || key === 'maxValue' || key === 'maxWordLength') {
        setSettings({ ...settings, [key]: parseInt(value as string) });

        return;
      } else if (
        key === 'allowAddition' ||
        key === 'allowSubtraction' ||
        key === 'allowMultiply' ||
        key === 'allowDivide' ||
        key === 'allowDoubling'
      )
        setSettings({ ...settings, [key]: value as boolean });
      return;
    },

    [settings]
  );

  const handleCancel = React.useCallback(async () => {
    setSettings(module.settings ?? fallbackSettings);
  }, [module, fallbackSettings]);

  const handleSave = React.useCallback(async () => {
    const update: ModuleSettings = {
      userId: module.userId,
      moduleGuid: module.moduleId,
      moduleType: module.moduleType,
      settings: settings,
    };

    await onSave(update);
  }, [module, settings, onSave]);

  console.log(settings);
  return (
    <ListItem>
      <Grid container padding={1}>
        <Grid item xs={12} style={{ padding: 10 }}>
          <Typography variant="h6">{getResource('administration:captionMathSettings')}</Typography>
        </Grid>
        <Grid container padding={1}>
          <Grid item xs={3}>
            <CheckboxInput
              property="allowAddition"
              label={getResource('administration:labelAddition')}
              checked={settings.allowAddition}
              disabled={disabled}
              onChange={handleChange}
            />
          </Grid>
          <Grid item xs={3}>
            <CheckboxInput
              property="allowSubtraction"
              label={getResource('administration:labelSubtraction')}
              checked={settings.allowSubtraction}
              disabled={disabled}
              onChange={handleChange}
            />
          </Grid>
          <Grid item xs={3}>
            <CheckboxInput
              property="allowMultiply"
              label={getResource('administration:labelMultiply')}
              checked={settings.allowMultiply}
              disabled={disabled}
              onChange={handleChange}
            />
          </Grid>
          <Grid item xs={3}>
            <CheckboxInput
              property="allowDivide"
              label={getResource('administration:labelDivide')}
              checked={settings.allowDivide}
              disabled={disabled}
              onChange={handleChange}
            />
          </Grid>
          <Grid item xs={3}>
            <CheckboxInput
              property="allowDoubling"
              label={getResource('administration:labelDoubling')}
              checked={settings.allowDoubling}
              disabled={disabled}
              onChange={handleChange}
            />
          </Grid>
        </Grid>
        <Grid container padding={1} justifyContent="space-between">
          <Grid item xs={5}>
            <NumberInput
              property="minValue"
              fullWidth
              label={getResource('administration:labelMinValue')}
              value={settings.minValue}
              disabled={disabled}
              onChange={handleChange}
            />
          </Grid>
          <Grid item xs={5}>
            <NumberInput
              property="maxValue"
              fullWidth
              label={getResource('administration:labelMaxValue')}
              value={settings.maxValue}
              disabled={disabled}
              onChange={handleChange}
            />
          </Grid>
        </Grid>
        <Grid item xs={12} style={{ padding: 10 }}>
          <Typography variant="h6">{getResource('administration:captionGermanSettings')}</Typography>
        </Grid>
        <Grid container padding={1} justifyContent="space-between" gap={2}>
          <Grid item xs={12}>
            <NumberInput
              property="maxWordLength"
              fullWidth
              label={getResource('administration:labelMaxLetterCount')}
              value={settings.maxWordLength}
              disabled={disabled}
              onChange={handleChange}
            />
          </Grid>
        </Grid>
        <Grid container justifyContent="flex-end" paddingRight="1.5rem" marginTop="2rem">
          <SubmitButton title="Cancel" variant="text" disabled={cancelDisabled} onClick={handleCancel} />
          <SubmitButton title="Save" variant="text" disabled={saveDisabled} onClick={handleSave} />
        </Grid>
      </Grid>
    </ListItem>
  );
};

export default SchoolModuleSettings;
