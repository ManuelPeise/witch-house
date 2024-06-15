import React from 'react';
import { ModuleSettings, SchoolSettings } from '../types';
import { Grid, ListItem, Typography } from '@mui/material';
import { getModuleSettingsConfiguration } from '../moduleSettingsConfiguration';
import CheckboxInput from '../../../../components/inputs/CheckboxInput';
import { useI18n } from '../../../../hooks/useI18n';
import NumberInput from '../../../../components/inputs/NumberInput';
import { ModuleSettingsTypeEnum } from '../../../../lib/enums/ModuleSettingsTypeEnum';
import SubmitButton from '../../../../components/buttons/SubmitButton';

interface IProps {
  model: ModuleSettings;
  disabled: boolean;
  onSave: (settings: ModuleSettings) => Promise<void>;
}

const SchoolModuleSettings: React.FC<IProps> = (props) => {
  const { model, disabled, onSave } = props;
  const { getResource } = useI18n();

  const fallbackSettings: SchoolSettings = React.useMemo((): SchoolSettings => {
    return {
      moduleType: model.moduleType,
      settingsType: model.moduleSettingsType,
      allowAddition: false,
      allowSubtraction: false,
      allowMultiply: false,
      allowDivide: false,
      allowDoubling: false,
      minValue: 0,
      maxValue: 0,
      maxWordLength: 0,
    };
  }, [model]);

  const [settings, setSettings] = React.useState<SchoolSettings>(model.settings ?? fallbackSettings);

  React.useEffect(() => {
    setSettings(model.settings ?? fallbackSettings);
  }, [model, fallbackSettings]);

  const configuration = getModuleSettingsConfiguration(model.moduleSettingsType);

  const title = React.useMemo(() => {
    switch (model.moduleSettingsType) {
      case ModuleSettingsTypeEnum.MathUnits:
        return getResource('administration:captionMathSettings');
      case ModuleSettingsTypeEnum.GermanUnits:
        return getResource('administration:captionGermanSettings');
    }
  }, [model, getResource]);

  const cancelDisabled = React.useMemo(() => {
    return JSON.stringify(model.settings ?? fallbackSettings) === JSON.stringify(settings);
  }, [model, settings, fallbackSettings]);

  const saveDisabled = React.useMemo(() => {
    if (model.moduleSettingsType === ModuleSettingsTypeEnum.GermanUnits) {
      return JSON.stringify(model.settings) === JSON.stringify(settings) || settings.maxWordLength === 0;
    }

    if (model.moduleSettingsType === ModuleSettingsTypeEnum.MathUnits) {
      return (
        JSON.stringify(model.settings) === JSON.stringify(settings) ||
        settings.minValue === 0 ||
        settings.maxValue === 0
      );
    }
    return false;
  }, [model, settings]);

  const handleChange = React.useCallback(
    async (key: string, value: any) => {
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
    setSettings(model.settings ?? fallbackSettings);
  }, [model, fallbackSettings]);

  const handleSave = React.useCallback(async () => {
    const update = { ...model, settings: settings };

    await onSave(update);
  }, [model, settings, onSave]);

  return (
    <ListItem divider>
      <Grid container padding={1}>
        <Grid container padding={1}>
          <Typography sx={{ marginBottom: '.5rem', fontSize: '1.4rem', fontStyle: 'italic', color: 'lightgray' }}>
            {title}
          </Typography>
        </Grid>
        {configuration.hasCalculationRuleSettings && (
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
        )}
        {configuration.hasMaxValue && (
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
        )}
        {configuration.hasMaxWordLength && (
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
        )}
        <Grid container justifyContent="flex-end" paddingRight="1.5rem" marginTop="2rem">
          <SubmitButton title="Cancel" variant="text" disabled={cancelDisabled} onClick={handleCancel} />
          <SubmitButton title="Save" variant="text" disabled={saveDisabled} onClick={handleSave} />
        </Grid>
      </Grid>
    </ListItem>
  );
};

export default SchoolModuleSettings;
