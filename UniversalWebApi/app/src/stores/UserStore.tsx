import { action, observable, computed } from "mobx";
import { get } from "../api/api";
import { IUser } from "../contracts/IUser";

export interface IUserStore {
  user: IUser;
  userState: IUser;
  getUser: () => void;
}

export class UserStore implements IUserStore {
  @observable user: IUser = {
    id: "",
    name: "",
    email: "",
    phoneNumber: ""
  };

  constructor() {
    this.getUser();
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
