import { Component } from "react";

enum HttpMethod {
  Get = "GET",
  Post = "POST",
  Put = "PUT",
  Delete = "DELETE"
}

export interface IApi {
  get: <T>(path: string, args?: RequestInit) => Promise<T>;
  post: <T>(path: string, body?: any) => Promise<T>;
  put: <T>(path: string, body?: any, args?: RequestInit) => Promise<T>;
  del: (path: string, id?: string | number | undefined) => Promise<boolean>;

  qlGet: (typeName: string, body?: any) => Promise<any>;
}

export class Api extends Component {
  public get = async <T>(
    path: string,
    args: RequestInit = {
      method: HttpMethod.Get
    }
  ): Promise<T> => {
    const response = await fetch(path, args);

    if (!response.ok) {
      throw new Error(response.statusText);
    }

    const data: T = await response.json();
    return data;
  };
}

export const api: IApi = {
  get: async <T>(
    path: string,
    args: RequestInit = {
      method: HttpMethod.Get
    }
  ): Promise<T> => {
    const response = await fetch(path, args);

    if (!response.ok) {
      throw new Error(response.statusText);
    }

    const data: T = await response.json();
    return data;
  },
  post: async <T>(path: string, body?: any): Promise<T> => {
    const response = await fetch(path, {
      method: HttpMethod.Post,
      headers: { "Content-Type": "application/json" },
      body: body ? JSON.stringify(body) : undefined
    });

    if (!response.ok) {
      throw new Error(response.statusText);
    }

    const data: T = await response.json();
    return data;
  },
  put: async <T>(
    path: string,
    body?: any,
    args: RequestInit = {
      method: HttpMethod.Put,
      headers: { "Content-Type": "application/json" },
      body: body ? JSON.stringify(body) : undefined
    }
  ): Promise<T> => {
    const response = await fetch(path, args);

    if (!response.ok) {
      throw new Error(response.statusText);
    }

    const data: T = await response.json();
    return data;
  },
  del: async (path: string, id?: number | string): Promise<boolean> => {
    const response = await fetch(`${path}/${id}`, {
      method: HttpMethod.Delete
    });

    return response.ok;
  },
  qlGet: async (typeName: string, body?: any) => {
    const response = await fetch("/graphql", {
      method: HttpMethod.Post,
      headers: { "Content-Type": "application/json" },
      body: body ? JSON.stringify(body) : undefined
    });

    if (!response.ok) {
      throw new Error(response.statusText);
    }

    const jsonResponse = await response.json();
    return jsonResponse.data[typeName];
  }
};

export interface SignInContract {
  login: string;
  password: string;
}

export interface SignInResponse {
  token: string;
  expirationTime: Date;
  userId: string;
}

export const signIn = async (body: SignInContract) => await post<SignInResponse>("/api/auth/signin", body);

export interface SignUpContract {
  name: string;
  email: string;
  phoneNumber: string;
  password: string;
  passwordCompare: string;
}

export const signUp = async (body?: SignUpContract) =>
  await fetch("/api/auth/signup", {
    method: HttpMethod.Post,
    headers: { "Content-Type": "application/json" },
    body: body ? JSON.stringify(body) : undefined
  }).then(response => {
    return response.ok;
  });
