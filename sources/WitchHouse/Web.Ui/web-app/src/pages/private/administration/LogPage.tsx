import {
  Checkbox,
  Grid,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Typography,
} from '@mui/material';
import React from 'react';
import { useApi } from '../../../hooks/useApi';
import { LogMessage } from './types';
import { endpoints } from '../../../lib/api/apiConfiguration';
import { Mapper } from '../../../lib/mapper';
import { Dayjs } from 'dayjs';
import { DropdownItem } from '../../../lib/types';
import DropdownInput from '../../../components/inputs/DropdownInput';
import { useI18n } from '../../../hooks/useI18n';
import SubmitButton from '../../../components/buttons/SubmitButton';

type LogTableRow = {
  delete: boolean;
  index: number;
  id: number;
  message: string;
  stackTrace: string;
  timeStamp: Dayjs;
  trigger: string;
};

type columnHeader = {
  key: string;
  width: number;
};

const tableHeaderKeys: columnHeader[] = [
  { key: 'labelTimeStamp', width: 10 },
  { key: 'trigger', width: 200 },
  { key: 'labelErrorMessage', width: 200 },
  { key: 'labelStacktrace', width: 300 },
];

const LogPage: React.FC = () => {
  const logMessageApi = useApi<LogMessage[]>();
  const { getResource } = useI18n();
  const [logMessageRows, setLogMessageRows] = React.useState<LogTableRow[]>([]);
  const [triggerFilter, setTriggerFilter] = React.useState<number>(0);
  const [selectedMessageIds, setSelectedMessageIds] = React.useState<number[]>([]);

  const onLoadLogMessages = React.useCallback(async () => {
    await logMessageApi.get(endpoints.administration.log);
  }, [logMessageApi]);
  React.useEffect(() => {
    onLoadLogMessages();
    // eslint-disable-next-line
  }, []);

  const triggerDropdownItems = React.useMemo((): DropdownItem[] => {
    const items: DropdownItem[] = [];
    items.push({
      id: 0,
      value: 'All',
      label: getResource('common:labelAllTriggers'),
    });

    logMessageApi?.data?.forEach((item, index) => {
      if (items.findIndex((x) => x.label === item.trigger) === -1) {
        items.push({
          id: index + 1,
          value: item.trigger,
          label: item.trigger,
        });
      }
    });

    return items;
  }, [logMessageApi?.data, getResource]);

  React.useEffect(() => {
    const rows =
      logMessageApi?.data?.map((msg, index) => {
        const mapper = new Mapper<LogMessage>(msg).mapBy('id', index);

        const model = {
          delete: false,
          index: index,
          id: mapper.propertyAccessor('id'),
          message: mapper.propertyAccessor('message'),
          stackTrace: mapper.propertyAccessor('stacktrace'),
          timeStamp: mapper.dateAccessor('timeStamp'),
          trigger: mapper.propertyAccessor('trigger'),
        };

        return model;
      }) ?? [];

    if (triggerFilter === 0) {
      setLogMessageRows(rows);
    } else {
      const dropdownItem = triggerDropdownItems.filter((x) => x.id === triggerFilter)[0] ?? null;
      if (dropdownItem == null) {
        setLogMessageRows(rows);
      } else {
        setLogMessageRows(rows.filter((x) => x.trigger === dropdownItem.value));
      }
    }
  }, [logMessageApi?.data, triggerDropdownItems, triggerFilter, setLogMessageRows]);

  const handleDeleteChanged = React.useCallback(
    (index: number) => {
      const rows: LogTableRow[] = [...logMessageRows];

      const messageIds = [...selectedMessageIds];

      if (selectedMessageIds.includes(rows[index].id)) {
        messageIds.splice(index);
      } else {
        messageIds.push(rows[index].id);
      }

      setSelectedMessageIds(messageIds);
    },
    [logMessageRows, selectedMessageIds]
  );

  const handleTriggerFilterChanged = React.useCallback((key: string, item: DropdownItem) => {
    setTriggerFilter(item.id);
  }, []);

  const handleCancel = React.useCallback(async () => {
    setSelectedMessageIds([]);
  }, []);

  const handleDelete = React.useCallback(async () => {
    await logMessageApi.post(endpoints.administration.delete, JSON.stringify(selectedMessageIds)).then(async () => {
      await onLoadLogMessages();
      setSelectedMessageIds([]);
    });
  }, [logMessageApi, selectedMessageIds, onLoadLogMessages]);

  if (logMessageApi.data == null) {
    return null;
  }

  return (
    <Grid container justifyContent="center" marginTop="4rem">
      <Grid item xs={11}>
        <Paper elevation={10}>
          {/* dense?? */}
          <TableContainer style={{ maxHeight: 700, minHeight: 700 }}>
            <Table sx={{ minWidth: 600, maxHeight: 50 }} size={'small'} stickyHeader>
              <TableHead>
                <TableRow style={{ height: 100 }}>
                  <TableCell padding="checkbox">
                    <Typography>{getResource('common:labelDelete')}</Typography>
                  </TableCell>
                  {tableHeaderKeys.map((key, index) => {
                    return (
                      <TableCell key={index} align="left" width={key.width}>
                        {key.key === 'trigger' ? (
                          <DropdownInput
                            property="trigger"
                            fullWidth
                            disabled={!logMessageRows.length}
                            items={triggerDropdownItems}
                            selectedValue={triggerFilter}
                            onChange={handleTriggerFilterChanged}
                          />
                        ) : (
                          <Typography>{getResource(`common:${key.key}`)}</Typography>
                        )}
                      </TableCell>
                    );
                  })}
                </TableRow>
              </TableHead>
              <TableBody>
                {logMessageRows.map((row, index) => {
                  return (
                    <TableRow key={index} hover style={{ height: 60 }}>
                      <TableCell padding="checkbox">
                        <Checkbox
                          color="primary"
                          checked={selectedMessageIds.includes(row.id)}
                          onChange={handleDeleteChanged.bind(null, index)}
                        />
                      </TableCell>
                      <TableCell>{row.timeStamp.format('DD/MM/YYYY hh:mm')}</TableCell>
                      <TableCell>{row.trigger}</TableCell>
                      <TableCell>{row.message}</TableCell>
                      <TableCell>{row.stackTrace}</TableCell>
                    </TableRow>
                  );
                })}
              </TableBody>
            </Table>
          </TableContainer>
        </Paper>
      </Grid>
      <Grid container item xs={11} marginTop={4} justifyContent="flex-end" paddingRight={10} gap={5}>
        <SubmitButton
          title={getResource('common:labelCancel')}
          variant="text"
          disabled={!selectedMessageIds.length}
          onClick={handleCancel}
        />
        <SubmitButton
          title={getResource('common:labelDelete')}
          variant="text"
          disabled={!selectedMessageIds.length}
          onClick={handleDelete}
        />
      </Grid>
    </Grid>
  );
};

export default LogPage;
