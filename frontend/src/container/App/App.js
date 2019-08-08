import React from "react";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import * as Pages from "./routes";
import "./App.css";
// import PrivateRoute from "./routes/protectedRoute";

function App() {
  return (
    <Router>
      <Switch>
        <Route exact path="/" component={Pages.signIn} />
        <Route path="/Forget-Password" component={Pages.forgetPassword} />
        <Route path="/Create-account" component={Pages.createAccount} />

        <Route path="" component={Pages.NotFoundPage} />
      </Switch>
    </Router>
  );
}

export default App;
