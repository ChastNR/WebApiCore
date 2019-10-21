import { action, observable, computed } from "mobx";
import { request } from "../api/api";
import { IUser } from "../interfaces/IUser";

export class UserStore {
  @observable private user?: IUser;

  @computed
  get userState() {
    return this.user;
  }

  @action("getUser")
  getUser = async () => {
    this.user = await request("user");
  };
}
