import { SignInContract } from "../contracts/SignInContract";
import { SignInResponse } from "../contracts/SignInResponse";
import { SignUpContract } from "../contracts/SignUpContract";

enum HttpMethod {
  Get = "GET",
  Post = "POST",
  Put = "PUT",
  Delete = "DELETE"
}

const defaultAuthHeaders:
  | Headers
  | string[][]
  | Record<string, string>
  | undefined = {
  "Content-Type": "application/json",
  Authorization: `Bearer ${localStorage.getItem("token")}`
};

const defaultHeaders: Record<string, string> = {
  "Content-Type": "application/json"
};

const deleteToken = () => {
  localStorage.removeItem("token");
};

export interface IApi {
  get: <T>(path: string, args?: RequestInit) => Promise<T>;
  post: <T>(path: string, body?: any) => Promise<T>;
  put: <T>(path: string, body?: any, args?: RequestInit) => Promise<T>;
  del: (path: string, id?: string | number) => Promise<boolean>;
  qlGet: (typeName: string, body?: any) => Promise<any>;
  signIn: (body: SignInContract) => Promise<SignInResponse>;
  signUp: (body: SignUpContract) => Promise<boolean>;
}

export const api: IApi = {
  get: async <T>(path: string): Promise<T> => {
    const response = await fetch(path, {
      method: HttpMethod.Get,
      headers: defaultAuthHeaders
    });

    if (!response.ok) {
      deleteToken();
      throw new Error(response.statusText);
    }

    return response.json();
  },
  post: async <T>(path: string, body?: any) => {
    const response = await fetch(path, {
      method: HttpMethod.Post,
      headers: defaultAuthHeaders,
      body: body ? JSON.stringify(body) : undefined
    });

    if (!response.ok) {
      deleteToken();
      throw new Error(response.statusText);
    }

    const data: T = await response.json();
    return data;
  },
  put: async <T>(path: string, body?: any) => {
    const response = await fetch(path, {
      method: HttpMethod.Put,
      headers: defaultAuthHeaders,
      body: body ? JSON.stringify(body) : undefined
    });

    if (!response.ok) {
      deleteToken();
      throw new Error(response.statusText);
    }

    const data: T = await response.json();
    return data;
  },
  del: async (path: string, id?: number | string) => {
    const response = await fetch(`${path}/${id}`, {
      method: HttpMethod.Delete,
      headers: defaultAuthHeaders
    });

    if (!response.ok) {
      deleteToken();
      throw new Error(response.statusText);
    }

    return true;
  },
  qlGet: async (typeName: string, body?: any) => {
    const response = await fetch("/graphql", {
      method: HttpMethod.Post,
      headers: defaultAuthHeaders,
      body: body ? JSON.stringify(body) : undefined
    });

    if (!response.ok) {
      deleteToken();
      throw new Error(response.statusText);
    }

    const jsonResponse = await response.json();
    return jsonResponse.data[typeName];
  },
  signIn: async (body: SignInContract) => {
    const response = await fetch("/api/auth/signin", {
      method: HttpMethod.Post,
      headers: defaultHeaders,
      body: body ? JSON.stringify(body) : undefined
    });

    if (!response.ok) {
      throw new Error(response.statusText);
    }

    const data: SignInResponse = await response.json();
    return data;
  },
  signUp: async (body: SignUpContract) => {
    const response = await fetch("/api/auth/signup", {
      method: HttpMethod.Post,
      headers: defaultHeaders,
      body: body ? JSON.stringify(body) : undefined
    });
    return response.ok;
  }
};
