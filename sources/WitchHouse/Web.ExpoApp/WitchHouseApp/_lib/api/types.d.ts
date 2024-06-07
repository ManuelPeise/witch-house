export type ApiResult<TModel> = {
  isLoading: boolean;
  data: TModel | null;
  get: (options: ApiOptions) => Promise<void>;
  post: (options: ApiOptions, model: TModel) => Promise<void>;
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
