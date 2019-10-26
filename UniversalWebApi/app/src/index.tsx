import React from "react";
import ReactDOM from "react-dom";
import App from "./App";
import serviceWorker from "./serviceWorker";
import { Provider } from "mobx-react";
import { BrowserRouter } from "react-router-dom";
import { AppStore } from "./stores/AppStore";

ReactDOM.render(
  <Provider appStore={new AppStore()}>
    <BrowserRouter>
      <App />
    </BrowserRouter>
  </Provider>,
  document.getElementById("root")
);

serviceWorker();
