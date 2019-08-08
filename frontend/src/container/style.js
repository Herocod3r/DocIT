import styled from "styled-components";

export const SignContainer = styled.div`
  width: 100%;
  display: flex;
  justify-content: center;

  .signIn_content {
    margin-top: 40px;

    h1 {
      text-align: center;
      margin-bottom: 2rem;
      font-weight: bold;
      font-size: 40px;
    }
    .footer {
      display: flex;
      justify-content: center;
      //   margin-top: 1rem;

      p {
        font-size: 12px;
      }
      .link-footer {
        margin-left: 0.5rem;
        text-decoration: underline;
        color: #006c75;
        cursor: pointer;
        font-size: 12px;
      }
    }
  }
`;

export const FormContainer = styled.div`
  width: ${props => props.width || "380px"};
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 2rem 1rem;
  background: #ffffff;
  border: 1.5px solid #dfe2e6;
  box-sizing: border-box;
  box-shadow: 0px 1px 10px rgba(0, 0, 0, 0.04);
  border-radius: 10px;
  margin-bottom: 1rem;

  p {
    font-size: 1rem;
    font-weight: 500;
  }
`;
