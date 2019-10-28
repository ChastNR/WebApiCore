import { action, observable, computed } from "mobx";
import { get } from "../api/api";
import { IUser } from "../contracts/IUser";

export interface IUserStore {
  userState: IUser;
  authState: boolean;
  authStateCheck: () => void;
  getUser: () => void;
}

export interface IToken {
  token: string | null;
  expirationTime: number;
}

export class UserStore implements IUserStore {
  @observable private user: IUser = {
    id: "",
    name: "",
    email: "",
    phoneNumber: ""
  };

  @observable private isAuthenticated: boolean = true;

  @computed
  get authState() {
    return this.isAuthenticated;
  }

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

  @computed
  get userState() {
    return this.user;
  }

  @action("getUser")
  getUser = async () => {
    this.user = await get<IUser>("api/user/1");
  };
}
