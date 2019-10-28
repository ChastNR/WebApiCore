import React from "react";
import ReactDOM from "react-dom";
import { Router } from "./Router";
import serviceWorker from "./serviceWorker";
import { Provider } from "mobx-react";
import { BrowserRouter } from "react-router-dom";
import { AppStore } from "./stores/AppStore";

const appStore = new AppStore();

ReactDOM.render(
  <Provider appStore={appStore}>
    <BrowserRouter>
      <Router {...appStore} />
    </BrowserRouter>
  </Provider>,
  document.getElementById("root")
);

serviceWorker();
