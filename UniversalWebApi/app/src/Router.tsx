import React from "react";
import { Switch, Route } from "react-router";
import { Layout } from "./components/layout/Layout";
import { signInForm } from "./base/controls/SignInForm";
import { signUpForm } from "./base/controls/SignUpForm";
import { IAppStore } from "./stores/AppStore";
import { inject, observer } from "mobx-react";

@inject("appStore")
@observer
export class Router extends React.Component<IAppStore, {}> {
  isAuthenticated: boolean = this.props.UserStore.authState;

  render() {
    return this.isAuthenticated ? (
      <Switch>
        <Route exact path="/">
          <Layout text="yeah!!!!" />
        </Route>
        <Route path="/signup" component={signUpForm} />
      </Switch>
    ) : (
      <Switch>
        <Route exact path="/">
          <Layout text="Fuck" />
        </Route>
        <Route path="/signin" component={signInForm} />
      </Switch>
    );
  }
}
