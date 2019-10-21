import React from "react";
import ReactDOM from "react-dom";
import App from "./App";
import serviceWorker from "./serviceWorker";
import { Provider } from "mobx-react";
import { AppStore } from "./stores/AppStore";
import { BrowserRouter } from "react-router-dom";

ReactDOM.render(
  <Provider appStore={new AppStore()}>
    <BrowserRouter>
      <App />
    </BrowserRouter>
  </Provider>,
  document.getElementById("root")
);

serviceWorker();
