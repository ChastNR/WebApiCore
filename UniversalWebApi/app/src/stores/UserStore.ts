import { action, observable, computed } from "mobx";
import { get, qlGet } from "../api/api";
import { IUser } from "../contracts/IUser";

export interface IUserStore {
  user: IUser;
  users: IUser[];
  isAuthenticated: boolean;
  authStateCheck: () => void;
  getUser: () => Promise<void>;
  getUsers: () => Promise<void>;
}

export interface IToken {
  token: string | null;
  expirationTime: number;
}

export class UserStore implements IUserStore {
  @observable public user: IUser = {
    id: "",
    name: "",
    email: "",
    phoneNumber: ""
  };

  @observable public users: IUser[] = [];

  @observable public isAuthenticated: boolean = true;

  @action("getUsers")
  getUsers = async () => {
    this.users = await qlGet({
      query: "{users {id, name, email, phoneNumber}}"
    }).then((users: IUser[]) => users);
  };

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
    this.user = await get<IUser>(`api/user/${this.user.id}`);
  };
}
