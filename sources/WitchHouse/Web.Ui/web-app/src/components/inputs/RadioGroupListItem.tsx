import { FormControl, FormControlLabel, FormLabel, ListItem, Radio, RadioGroup } from '@mui/material';
import React from 'react';
import { RadioGroupProps } from '../../lib/types';

interface IProps extends RadioGroupProps {
  marginTop?: number;
  marginBottom?: number;
}

const RadioGroupListItem: React.FC<IProps> = (props) => {
  const { property, hasDivider, groupLabel, radioProps, value, marginBottom, marginTop, onChange } = props;

  const handleChange = React.useCallback(
    (event: React.ChangeEvent<HTMLInputElement>, value: string) => {
      onChange(property, value);
    },
    [property, onChange]
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
        <RadioGroup value={value} onChange={handleChange} sx={{ display: 'flex', flexDirection: 'row' }}>
          {radioProps.map((prop, index) => {
            return (
              <FormControlLabel
                key={index}
                value={prop.value}
                label={prop.label}
                control={<Radio disabled={prop?.disabled} />}
              />
            );
          })}
        </RadioGroup>
      </FormControl>
    </ListItem>
  );
};

export default RadioGroupListItem;
