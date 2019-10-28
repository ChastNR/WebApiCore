const http = <T>(request: RequestInfo): Promise<T> => {
  return new Promise((resolve, reject) => {
    let res: Response;
    fetch(request)
      .then(response => {
        if (response.ok) {
          res = response;
          return response.json();
        } else {
          localStorage.removeItem("token");
          window.location.href = "/signin";
        }
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

// export interface IToken {
//   token: string | null;
//   expirationTime: number;
// }

// export const authCheck = (): boolean => {
//   let token: IToken = {
//     token: localStorage.getItem("token"),
//     expirationTime: Number(localStorage.getItem("expTime"))
//   };
//   let authenticated: boolean =
//     token.expirationTime <= Date.now() && token.token == null;

//   if (!authenticated) {
//     localStorage.removeItem("token");
//   }

//   return authenticated;
// };

export const get = async <T>(
  path: string,
  args: RequestInit = {
    method: RequestType.Get,
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${localStorage.getItem("token")}`
    }
  }
): Promise<T> => {
  return await http<T>(new Request(path, args));
};

export const post = async <T>(
  path: string,
  body: any,
  args: RequestInit = {
    method: RequestType.Post,
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${localStorage.getItem("token")}`
    },
    body: JSON.stringify(body)
  }
): Promise<T> => {
  return await http<T>(new Request(path, args));
};

export const put = async <T>(
  path: string,
  body: any,
  args: RequestInit = {
    method: RequestType.Put,
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${localStorage.getItem("token")}`
    },
    body: JSON.stringify(body)
  }
): Promise<T> => {
  return await http<T>(new Request(path, args));
};

export const del = async <T>(
  path: string,
  args: RequestInit = {
    method: RequestType.Delete,
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${localStorage.getItem("token")}`
    }
  }
): Promise<T> => {
  return await http<T>(new Request(path, args));
};

export interface SignInContract {
  login: string;
  password: string;
}

export const signIn = async (body: SignInContract): Promise<boolean> => {
  return await fetch("/api/auth/signin", {
    method: RequestType.Post,
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(body)
  })
    .then(response => (response.ok ? response.json() : false))
    .then((data: string) => {
      localStorage.setItem("token", data);
      return true;
    });
};

export interface SignUpContract {
  name: string;
  email: string;
  phoneNumber: string;
  password: string;
  passwordCompare: string;
}

export const signUp = async (body: SignUpContract): Promise<boolean> => {
  return await fetch("/api/auth/signup", {
    method: RequestType.Post,
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(body)
  }).then(response => {
    return response.ok;
  });
};
