import axios, { AxiosError } from "axios";
import { ApiException, OrderServiceClient } from "../api/order-service-api";

export function GetOSApiClient(accessToken: string): OrderServiceClient {
  var instance = axios.create();
  instance.defaults.headers.common["Authorization"] = "Bearer ".concat(
    accessToken
  );
  return new OrderServiceClient(process.env.REACT_APP_API_URL, instance);
}

export function GetMessageErrorApiException(
  err: ApiException | AxiosError
): string {
  var message = "";
  if (err instanceof ApiException) {
    switch (err.status) {
      case 400:
        var data400 = Array.isArray(err.response)
          ? err.response[0]
          : err.response;
        data400 = data400.description ? data400.description : data400;
        message = data400;
        break;
      case 500:
        var data500 = err.response as any;

        data500 = data500.title ? data500.title : data500;
        message = data500;
        break;
    }
  }

  if (err instanceof AxiosError) {
    return err.message === "Network Error"
      ? "Sem conexão com a API"
      : err.message;
  }

  return message;
}

export function CallApi<T>(promise: Promise<T>) {
  return new Promise<T>(async function (resolve, reject) {
    try {
      const data = await promise;
      return resolve(data);
    } catch (err: any) {
      return reject(GetMessageErrorApiException(err));
    }
  });
}
