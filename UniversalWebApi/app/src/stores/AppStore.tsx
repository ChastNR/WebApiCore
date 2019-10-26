import { IUserStore, UserStore } from "./UserStore";

export interface IAppStore {
  UserStore: IUserStore;
}

export class AppStore implements IAppStore {
  constructor() {
    this.UserStore = new UserStore();
  }

  UserStore: IUserStore;
}
