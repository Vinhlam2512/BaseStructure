interface HttpResponse<TEntity> {
  value: TEntity;
  isSuccess: boolean;
  isFailure: boolean;
  error: Error;
}
interface Error {
  code: string;
  message: string;
}

interface HttpError {
  type: string;
  title: string;
  status: number;
  detail: string;
  errors?: any;
}
