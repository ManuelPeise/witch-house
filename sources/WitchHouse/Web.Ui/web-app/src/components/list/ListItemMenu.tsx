import { List, ListItemButton, Typography } from '@mui/material';
import React from 'react';
import { ListItemModel } from '../../lib/types';

interface IProps {
  selectedItem: number;
  listItems: ListItemModel[];
  onSelect: (id: number) => void;
}

const ListItemMenu: React.FC<IProps> = (props) => {
  const { selectedItem, listItems, onSelect } = props;

  return (
    <List disablePadding>
      {listItems &&
        listItems.map((item, index) => {
          return (
            <ListItemButton
              key={index}
              style={{ padding: '1rem' }}
              selected={item.id === selectedItem}
              onClick={onSelect.bind(null, item.id)}
            >
              <Typography style={{ paddingLeft: '1rem' }} variant="h5">
                {item.value}
              </Typography>
            </ListItemButton>
          );
        })}
    </List>
  );
};

export default ListItemMenu;
