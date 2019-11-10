import { IUserStore, UserStore } from "./UserStore";

export const App_Store: string = "appStore";
export const User_Store: string = "userStore";

export interface IAppStore {
  [key: string]: IUserStore;
}

export const appStore: IAppStore = {
  [User_Store]: new UserStore()
};
