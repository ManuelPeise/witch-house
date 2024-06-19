import { FormControl, FormLabel, Grid, ListItem } from '@mui/material';
import React from 'react';
import { RadioGroupProps } from '../../lib/types';
import CheckboxInput from './CheckboxInput';

interface IProps extends RadioGroupProps {
  marginTop?: number;
  marginBottom?: number;
  allowMultiple?: boolean;
}

const CheckBoxGroupListItem: React.FC<IProps> = (props) => {
  const { hasDivider, groupLabel, radioProps, value, marginBottom, marginTop, onChange } = props;

  const handleChange = React.useCallback(
    (key: string, value: boolean) => {
      onChange(key, value);
    },
    [onChange]
  );

  return (
    <ListItem
      divider={hasDivider}
      sx={{
        width: '100%',
        paddingLeft: '2rem',
        paddingRight: '2rem',
        marginTop: marginTop,
      }}
    >
      <FormControl
        sx={{
          width: '100%',
          display: 'flex',
          flexDirection: 'row',
          justifyContent: 'space-between',
          alignItems: 'center',
          marginBottom: marginBottom,
        }}
      >
        <FormLabel>{groupLabel}</FormLabel>
        <Grid item sx={{ display: 'flex', flexDirection: 'row' }}>
          {radioProps.map((prop, index) => {
            return (
              <CheckboxInput
                key={index}
                property={prop.value.toString()}
                checked={value.includes(prop.value)}
                label={prop.label}
                disabled={prop.disabled}
                onChange={handleChange}
              />
            );
          })}
        </Grid>
      </FormControl>
    </ListItem>
  );
};

export default CheckBoxGroupListItem;
