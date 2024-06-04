import { Grid, Switch, Typography } from '@mui/material';
import React from 'react';

interface IProps {
  property: string;
  label: string;
  disabled?: boolean;
  checked: boolean;
  marginBottom?: number;
  onChange: (key: string, value: any) => void;
}

export const SwitchWithLabel: React.FC<IProps> = (props) => {
  const { label, checked, property, disabled, marginBottom, onChange } = props;

  const handleChange = React.useCallback(
    (event: React.ChangeEvent<HTMLInputElement>, checked: boolean) => {
      onChange(property, checked);
    },
    [property, onChange]
  );

  return (
    <Grid
      item
      xs={12}
      sx={{
        width: '100%',
        display: 'flex',
        marginBottom: marginBottom,
        justifyContent: 'space-between',
        paddingLeft: '2rem',
        paddingRight: '2rem',
      }}
    >
      <Typography sx={{ fontSize: '1rem', fontWeight: '400', color: disabled ? 'gray' : '' }}>{label}</Typography>
      <Switch disabled={disabled} checked={checked} onChange={handleChange} color="success" />
    </Grid>
  );
};

export default SwitchWithLabel;
