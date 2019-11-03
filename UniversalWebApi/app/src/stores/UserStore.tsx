import { action, observable, computed } from "mobx";
import { get } from "../api/api";
import { IUser } from "../contracts/IUser";

export interface IUserStore {
  user: IUser;
  isAuthenticated: boolean;
  authStateCheck: () => void;
  getUser: () => void;
}

export interface IToken {
  token: string | null;
  expirationTime: number;
}

export class UserStore implements IUserStore {
  @observable user: IUser = {
    id: "",
    name: "",
    email: "",
    phoneNumber: ""
  };

  @observable isAuthenticated: boolean = true;

  @action("authStateCheck")
  authStateCheck = () => {
    let token: IToken = {
      token: localStorage.getItem("token"),
      expirationTime: Number(localStorage.getItem("expTime"))
    };
    let authenticated: boolean =
      token.expirationTime <= Date.now() && token.token == null;

    if (!authenticated) {
      localStorage.removeItem("token");
    }

    this.isAuthenticated = authenticated;
  };

  @action("getUser")
  getUser = async () => {
    this.user = await get<IUser>("api/user/1");
  };
}
