export type LogMessage = {
  id: number;
  message: string;
  stacktrace: string;
  timeStamp: string;
  trigger: string;
};

export type LogMessageTableModel = {
  id: number;
  message: string;
  stacktrace: string;
};
