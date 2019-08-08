import styled from "styled-components";
import password from "../asset/images/password.svg";
import mail from "../asset/images/mail.svg";

export const FormInputContainer = styled.div`
  width: 330px;
  display: flex;
  justify-content: center;
  flex-direction: column;
  margin-top: 3rem;

  #password_img {
    background-image: url(${password});
    background-repeat: no-repeat;
    background-position: left;
    background-origin: content-box;
    text-indent: 20px;
  }
  #email_img {
    background-image: url(${mail});
    background-repeat: no-repeat;
    background-position: left;
    background-origin: content-box;
    text-indent: 20px;
  }

  .display_account_label {
    display: flex;
    justify-content: center;
    align-items: center;
    color: #a8a8a8;
    margin-bottom: 1.5rem;

    p {
      font-size: 12px;
      margin-bottom: 0;
    }
    hr {
      width: 81px;
      border: 0.7px solid #a8a8a8;
    }

    img {
      margin-right: 2rem;
    }
    img:last-child {
      margin-right: 0px;
    }
  }
`;

export const Label = styled.label`
  position: relative;
  left: 20px;
  margin-bottom: 0.5rem;
`;

export const FormInput = styled.input`
  height: 48px;
  background: #ffffff;
  border: 1px solid #dadada;
  box-sizing: border-box;
  border-radius: 6px;
  margin-bottom: 1.2rem;
  padding: 0.5rem;

  &:focus {
    border: 2px solid #006c75;
    outline: none;
  }
  &::placeholder {
    color: #838383;
    font-size: 13px;
  }
`;

export const Button = styled.button`
  height: 48px;
  background: #006c75;
  border-radius: 6px;
  color: #ffffff;
  border: none;
  cursor: pointer;
  margin-bottom: 2rem;

  &:focus {
    outline: none;
  }
`;
