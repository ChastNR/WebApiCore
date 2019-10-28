import React from "react";
import { Switch, Route, Redirect } from "react-router";
import { Layout } from "./components/layout/Layout";
import { signInForm } from "./base/controls/SignInForm";
import { signUpForm } from "./base/controls/SignUpForm";
import { inject, observer } from "mobx-react";
import { IAppStore } from "./stores/AppStore";

/*
export const Router3: React.FC<{ appStore: IAppStore }> = inject("appStore")(
  observer(({ appStore }) => {
    const authentication: boolean = appStore.UserStore.authState;
    return (
      <Switch>
        <Route exact path="/" component={Layout} />
        <Route path="/about" />
        <Route path="/contacts" />
        <Route path="/signin" component={signInForm} />
        <PrivateRoute path="/signup" isAuthenticated={authentication}>
          {signUpForm}
        </PrivateRoute>
      </Switch>
    );
  })
);
*/

export const Router: React.FC<IAppStore> = (appStore: IAppStore) => {
  const authentication: boolean = appStore.UserStore.authState;
  return (
    <Switch>
      <Route exact path="/" component={Layout} />
      <Route path="/about" />
      <Route path="/contacts" />
      <Route path="/signin" component={signInForm} />
      <PrivateRoute path="/signup" isAuthenticated={authentication}>
        {signUpForm}
      </PrivateRoute>
    </Switch>
  );
};

function PrivateRoute({ children, isAuthenticated, ...rest }: any) {
  return (
    <Route
      {...rest}
      render={({ location }) =>
        isAuthenticated === true ? (
          children
        ) : (
          <Redirect
            to={{
              pathname: "/signin",
              state: { from: location }
            }}
          />
        )
      }
    />
  );
}
