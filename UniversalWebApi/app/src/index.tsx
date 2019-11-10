import React from "react";
import ReactDOM from "react-dom";
import { Router } from "react-router-dom";
import { Provider } from "mobx-react";
import history from "./history";
import serviceWorker from "./serviceWorker";

import { App } from "./App";
import { AppStore } from "./stores/AppStore";

ReactDOM.render(
  <Provider appStore={AppStore}>
    <Router history={history}>
      <App />
    </Router>
  </Provider>,
  document.getElementById("root")
);

serviceWorker();
