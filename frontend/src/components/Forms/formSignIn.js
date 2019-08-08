import React, { Component } from "react";
import { FormInputContainer, Label, FormInput, Button } from "../style";

class FormSignIn extends Component {
  state = {
    email: "",
    password: ""
  };
  handleChange = e => {
    this.setState({
      [e.target.name]: e.target.value
    });
  };
  render() {
    return (
      <FormInputContainer>
        {this.state.email !== "" ? <Label>Email</Label> : null}
        <FormInput
          type="text"
          placeholder="Email"
          id="email_img"
          name="email"
          onChange={e => this.handleChange(e)}
        />
        {this.state.password !== "" ? <Label>Password</Label> : null}
        <FormInput
          type="password"
          placeholder="Password"
          id="password_img"
          name="password"
          onChange={e => this.handleChange(e)}
        />

        <Button>Sign in to your account</Button>

        {/* <div className="display_account_label">
          <hr />
          <p>Or create account with,</p>
          <hr />
        </div>
        <div className="display_account_label" style={{ marginBottom: "0" }}>
          <img src={require("../../asset/images/github.svg")} alt="github" />
          <img
            src={require("../../asset/images/bitbucket.svg")}
            alt="bitbucket"
          />
          <img src={require("../../asset/images/gitlab.svg")} alt="gitlab" />
        </div> */}
      </FormInputContainer>
    );
  }
}

export default FormSignIn;
