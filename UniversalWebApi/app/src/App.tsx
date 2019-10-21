import React from "react";
import { Switch, Route } from "react-router";
import { Layout } from "./components/Layout/Layout";

const App: React.FC = () => {
  return (
    <Switch>
      <Route exact path="/" component={Layout} />
      <Route path="/about" />
      <Route path="/contacts" />
    </Switch>
  );
};

export default App;
