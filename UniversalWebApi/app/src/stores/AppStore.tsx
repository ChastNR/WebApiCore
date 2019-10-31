import { IUserStore, UserStore } from "./UserStore";

export interface IAppStore {
  UserStore: IUserStore;
}

export interface IAppStoreInject {
  AppStore?: IAppStore | undefined;
}

export class AppStore implements IAppStore {
  constructor() {
    this.UserStore = new UserStore();
  }

  UserStore: IUserStore;
}
