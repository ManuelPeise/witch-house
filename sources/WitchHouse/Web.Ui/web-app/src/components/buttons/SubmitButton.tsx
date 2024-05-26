import { Button } from '@mui/material';
import React from 'react';

interface IProps {
  title: string;
  disabled?: boolean;
  variant: 'outlined' | 'contained' | 'text';
  onClick: () => Promise<void>;
}

const SubmitButton: React.FC<IProps> = (props) => {
  const { title, disabled, variant, onClick } = props;

  return (
    <Button disabled={disabled} variant={variant} onClick={onClick}>
      {title}
    </Button>
  );
};

export default SubmitButton;
