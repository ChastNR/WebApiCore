import React from "react";
import ReactDOM from "react-dom";
import { BrowserRouter } from "react-router-dom";
import { Provider } from "mobx-react";
import { Router } from "./Router";
import { appStore } from "./stores/AppStore";
import serviceWorker from "./serviceWorker";

ReactDOM.render(
  <Provider appStore={appStore}>
    <BrowserRouter>
      <Router />
    </BrowserRouter>
  </Provider>,
  document.body
);

serviceWorker();
