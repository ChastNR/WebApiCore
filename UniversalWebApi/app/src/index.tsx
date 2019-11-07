import React from "react";
import ReactDOM from "react-dom";
import { BrowserRouter } from "react-router-dom";
import { Provider } from "mobx-react";
import { Router } from "./Router";
import { AppStore, IAppStore } from "./stores/AppStore";
import serviceWorker from "./serviceWorker";

const appStore: IAppStore = new AppStore();

ReactDOM.render(
  <Provider appStore={appStore}>
    <BrowserRouter>
      <Router />
    </BrowserRouter>
  </Provider>,
  document.getElementById("root")
);

serviceWorker();
