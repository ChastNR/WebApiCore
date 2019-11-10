import { action, observable } from "mobx";
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

export class UserStore implements IUserStore {
  @observable public user: IUser = {
    id: "",
    name: "",
    email: "",
    phoneNumber: ""
  };

  @observable public users: IUser[] = [];

  @observable public isAuthenticated: boolean = false;

  @action("getUsers")
  getUsers = async () => {
    this.users = await qlGet("users", {
      query: "{ users {id, name, email, phoneNumber}}"
    });
  };

  @action("authStateCheck")
  authStateCheck = () => {
    let token = localStorage.getItem("token");
    this.isAuthenticated = token !== undefined;
  };

  @action("getUser")
  getUser = async () => {
    this.user = await get<IUser>(`api/user/${this.user.id}`);
  };
}
