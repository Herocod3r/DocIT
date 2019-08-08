import React, { Component } from "react";
import { FormInputContainer, Label, FormInput, Button } from "../style";

class FormForgetPassword extends Component {
  state = {
    email: ""
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

        <Button>Reset Password</Button>
      </FormInputContainer>
    );
  }
}

export default FormForgetPassword;
