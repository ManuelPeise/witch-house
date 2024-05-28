import { Accordion, AccordionDetails, AccordionSummary, List, Typography } from '@mui/material';
import React from 'react';

interface IProps {
  expanded: string | false;
  menuKey: string;
  label: string;
  onExpandedChanged: (key: string) => void;
}

const CollapsibleMenuItem: React.FC<IProps> = (props) => {
  const { expanded, menuKey, label, onExpandedChanged } = props;

  return (
    <Accordion
      style={{ margin: 0, padding: 0, backgroundColor: '#000' }}
      expanded={expanded === menuKey}
      onChange={onExpandedChanged.bind(null, menuKey)}
    >
      <AccordionSummary>
        <Typography style={{ color: '#fff', fontStyle: 'italic' }} variant="h5">
          {label}
        </Typography>
      </AccordionSummary>
      <AccordionDetails>
        <List>{/* todo add related items */}</List>
      </AccordionDetails>
    </Accordion>
  );
};

export default CollapsibleMenuItem;
