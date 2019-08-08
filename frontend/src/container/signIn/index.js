import React, { Component } from "react";
import { SignContainer, FormContainer } from "../style";
import { Link } from "react-router-dom";
import FormSignIn from "../../components/Forms/formSignIn";

class SignIn extends Component {
  render() {
    return (
      <SignContainer>
        <div className="signIn_content">
          <h1>DocIT</h1>
          <FormContainer>
            <p>Glad you're back!</p>

            <FormSignIn />
          </FormContainer>
          <div className="footer">
            <p>New to DocIT?</p>
            <Link to="/Create-account" className="link-footer">
              Create an account
            </Link>
          </div>

          <div className="footer">
            <p>Forgot your password?</p>
            <Link to="/Forget-Password" className="link-footer">
              Get another one
            </Link>
          </div>
        </div>
      </SignContainer>
    );
  }
}

export default SignIn;
