import { Button } from '@mui/material';
import React from 'react';

interface IProps {
  title: string;
  onClick: () => Promise<void>;
}

const LinkButton: React.FC<IProps> = (props) => {
  const { title, onClick } = props;

  return (
    <Button variant="text" onClick={onClick}>
      {title}
    </Button>
  );
};

export default LinkButton;
