import { ColorEnum } from '../enums/ColorEnum';

export type ApiResult<TModel> = {
  isLoading: boolean;
  data: TModel | null;
  get: (serviceUrl: string) => Promise<void>;
  post: (options: ApiOptions, model: TModel) => Promise<void>;
  sendPostRequest: (serviceUrl: string, requestModel: any) => Promise<Boolean>;
};

export type ApiOptions = {
  serviceUrl: string;
  model?: any;
  parameters: string;
};

export type ResponseMessage<TModel> = {
  success: boolean;
  statusCode: number;
  messageKey: string;
  data: TModel;
};

export type JwtData = {
  jwtToken: string;
  refreshToken: string;
};

export type ChartDataSet = {
  data: number[];
  color: (opacity: number) => string;
};

export type ChartData = {
  labels: string[];
  dataSets: ChartDataSet[];
};
