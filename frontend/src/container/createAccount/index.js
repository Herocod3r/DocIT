import React, { Component } from "react";
import { SignContainer, FormContainer } from "../style";
import { Link } from "react-router-dom";

class CreateAccount extends Component {
  render() {
    return (
      <SignContainer>
        <div className="signIn_content">
          <h1>DocIT</h1>
          <FormContainer width="800px">
            <p>Welcome to the DocIT family!</p>
          </FormContainer>
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

export default CreateAccount;
