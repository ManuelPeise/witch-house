import dayjs, { Dayjs } from 'dayjs';

export type MapperResult<TModel> = {
  index?: number | null;
  key: any;
  propertyAccessor: (key: keyof TModel) => any;
  dateAccessor: (key: keyof TModel) => Dayjs;
  modelAccessor: () => TModel;
};

export class Mapper<TModel> {
  private _model: TModel;

  constructor(model: TModel) {
    this._model = model;
  }

  public mapBy = (keyAccessor: keyof TModel, indexAccessor?: number): MapperResult<TModel> => {
    return {
      index: indexAccessor ?? null,
      key: this._model[keyAccessor],
      propertyAccessor: (key: keyof TModel) => this._model[key],
      dateAccessor: (key: keyof TModel) => dayjs(this._model[key] as string),
      modelAccessor: () => this._model,
    };
  };
}
