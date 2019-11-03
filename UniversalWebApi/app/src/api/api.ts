enum HttpMethod {
  Get = "GET",
  Post = "POST",
  Put = "PUT",
  Delete = "DELETE"
}

const apiDefaultHeaders: HeadersInit = {
  Accept: "application/json",
  "Content-Type": "application/json",
  pragma: "no-cache",
  "cache-control": "no-cache"
};

export const get = async <T>(
  path: string,
  args: RequestInit = {
    method: HttpMethod.Get,
    headers: apiDefaultHeaders
  }
): Promise<T> => {
  const response = await fetch(path, args);

  if (!response.ok) {
    throw new Error(response.statusText);
  }

  return (await response.json()) as T;
};

export const post = async <T>(
  path: string,
  body?: any,
  args: RequestInit = {
    method: HttpMethod.Post,
    headers: apiDefaultHeaders,
    body: body ? JSON.stringify(body) : undefined
  }
): Promise<T> => {
  const response = await fetch(path, args);

  if (!response.ok) {
    throw new Error(response.statusText);
  }

  return (await response.json()) as T;
};

export const put = async <T>(
  path: string,
  body?: any,
  args: RequestInit = {
    method: HttpMethod.Put,
    headers: apiDefaultHeaders,
    body: body ? JSON.stringify(body) : undefined
  }
): Promise<T> => {
  const response = await fetch(path, args);

  if (!response.ok) {
    throw new Error(response.statusText);
  }

  return (await response.json()) as T;
};

export const del = async <T>(
  path: string,
  body?: any,
  args: RequestInit = {
    method: HttpMethod.Delete,
    headers: apiDefaultHeaders,
    body: body ? JSON.stringify(body) : undefined
  }
): Promise<T> => {
  const response = await fetch(path, args);

  if (!response.ok) {
    throw new Error(response.statusText);
  }

  return (await response.json()) as T;
};

export interface SignInContract {
  login: string;
  password: string;
}

export interface SignInResponse {
  token: string;
  userId: string;
}

export const signIn = async (
  body?: SignInContract
): Promise<SignInResponse> => {
  return await post<SignInResponse>("/api/auth/signin", {
    method: HttpMethod.Delete,
    headers: apiDefaultHeaders,
    body: body ? JSON.stringify(body) : undefined
  });
};

export interface SignUpContract {
  name: string;
  email: string;
  phoneNumber: string;
  password: string;
  passwordCompare: string;
}

export const signUp = async (body?: SignUpContract): Promise<boolean> => {
  return await fetch("/api/auth/signup", {
    method: HttpMethod.Post,
    headers: apiDefaultHeaders,
    body: body ? JSON.stringify(body) : undefined
  }).then(response => {
    return response.ok;
  });
};
