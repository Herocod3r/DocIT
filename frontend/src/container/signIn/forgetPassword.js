import React, { Component } from "react";
import { SignContainer, FormContainer } from "../style";
import { Link } from "react-router-dom";
import FormForgetPassword from "../../components/Forms/forgetPassword";

class SignIn extends Component {
  render() {
    return (
      <SignContainer>
        <div className="signIn_content">
          <h1>DocIT</h1>
          <FormContainer>
            <p>Get that password back!</p>

            <FormForgetPassword />
          </FormContainer>
          <div className="footer">
            <p>New to DocIT?</p>
            <Link to="/Create-account" className="link-footer">
              Create an account
            </Link>
          </div>

          <div className="footer">
            <p>Using DocIT already?</p>
            <Link to="/" className="link-footer">
              Sign in
            </Link>
          </div>
        </div>
      </SignContainer>
    );
  }
}

export default SignIn;
