export enum RequestType {
  Get = "GET",
  Post = "POST",
  Put = "PUT",
  Delete = "DELETE"
}

export interface IRequest {
  contractType: string;
  requestMethod?: RequestType;
  data?: any;
}

export const request = async <T>(params: IRequest): Promise<T> => {
  const response = await fetch(`/api/${params.contractType}`, {
    method: params.requestMethod === null ? "GET" : params.requestMethod,
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + localStorage.getItem("token")
    },
    body: params.data
  });
  if (!response.ok) {
    throw new Error(response.statusText);
  }
  return await response.json().then(data => data as T);
};
