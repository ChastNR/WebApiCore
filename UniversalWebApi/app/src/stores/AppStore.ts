import { IUserStore, UserStore } from "./UserStore";

export const App_Store: string = "appStore";
export const User_Store: string = "userStore";

export interface IAppStoreInject {
  appStore: IAppStore;
}

export interface IAppStore {
  UserStore: IUserStore;
}

export const AppStore: IAppStore = {
  UserStore: new UserStore()
};
