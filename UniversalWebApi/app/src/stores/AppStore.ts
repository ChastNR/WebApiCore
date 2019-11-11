import { IUserStore, UserStore } from "./UserStore";
import { IShowLoader, ShowLoader } from "./ShowLoader";

export const App_Store: string = "appStore";
export const User_Store: string = "userStore";

export interface IAppStoreInject {
  appStore: IAppStore;
}

export interface IAppStore {
  UserStore: IUserStore;
  ShowLoader: IShowLoader;
}

export const AppStore: IAppStore = {
  UserStore: new UserStore(),
  ShowLoader: new ShowLoader()
};
