import { observable, action } from "mobx";

export interface IShowLoader {
  isLoading: boolean;
  setLoaderState: (state: boolean) => void;
}

export class ShowLoader implements IShowLoader {
  @observable isLoading: boolean = false;

  @action("setLoaderState")
  setLoaderState = (state: boolean) => (this.isLoading = state);
}
