const http = <T>(request: RequestInfo): Promise<T> => {
  return new Promise((resolve, reject) => {
    let res: Response;
    fetch(request)
      .then(response => {
        res = response;
        return response.json();
      })
      .then((data: T) => (res.ok ? resolve(data) : reject(data)))
      .catch(err => reject(err));
  });
};

enum RequestType {
  Get = "GET",
  Post = "POST",
  Put = "PUT",
  Delete = "DELETE"
}

export const get = async <T>(
  path: string,
  args: RequestInit = { method: RequestType.Get }
): Promise<T> => {
  return await http<T>(new Request(path, args));
};

export const post = async <T>(
  path: string,
  body: any,
  args: RequestInit = { method: RequestType.Post, body: JSON.stringify(body) }
): Promise<T> => {
  return await http<T>(new Request(path, args));
};

export const put = async <T>(
  path: string,
  body: any,
  args: RequestInit = { method: RequestType.Put, body: JSON.stringify(body) }
): Promise<T> => {
  return await http<T>(new Request(path, args));
};

export const del = async <T>(
  path: string,
  args: RequestInit = { method: RequestType.Delete }
): Promise<T> => {
  return await http<T>(new Request(path, args));
};
