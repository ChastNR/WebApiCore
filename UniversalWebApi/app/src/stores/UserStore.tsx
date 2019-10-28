import { action, observable, computed } from "mobx";
import { get } from "../api/api";
import { IUser } from "../contracts/IUser";

export interface IUserStore {
  userState: IUser;
  authState: boolean;
  getUser: () => void;
}

export class UserStore implements IUserStore {
  @observable private user: IUser = {
    id: "",
    name: "",
    email: "",
    phoneNumber: ""
  };

  @observable private isAuthenticated: boolean = false;

  constructor() {
    //this.getUser();
  }

  @computed
  get authState() {
    return this.isAuthenticated;
  }

  @computed
  get userState() {
    return this.user;
  }

  @action("getUser")
  getUser = async () => {
    this.user = await get<IUser>("api/user/1");
  };
}
