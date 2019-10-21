import React from "react";
import { Link } from "react-router-dom";
import styled from "styled-components";

interface ILinkProps {
  to: string;
  text: JSX.Element | string;
}

const StyledLink = styled(Link)`
  text-decoration: none;
  color: #1089ff;

  :hover {
    text-decoration: underline;
    text-decoration-style: solid;
    color: #00d1cd;
  }

  transition: 0.5s;
`;

const SLink = (props: ILinkProps) => (
  <StyledLink to={props.to}>{props.text}</StyledLink>
);

export default SLink;
