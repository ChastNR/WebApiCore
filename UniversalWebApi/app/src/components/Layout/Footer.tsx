import React from "react";
import styled from "styled-components";

import { SLink } from "../../base/controls/SLink";

const StyledFooter = styled.div`
  background-color: black;
  color: white;
`;

const StyledFlexContainer = styled.div`
  display: flex;
  justify-content: center;
`;

const StyledFlexColumn = styled.div`
  padding: 1em;
`;

export const Footer: React.FC = () => {
  return (
    <StyledFooter>
      <StyledFlexContainer>
        <StyledFlexColumn>
          <SLink to="/contacts" text="Contacts" />
        </StyledFlexColumn>
        <StyledFlexColumn>
          <SLink to="/about" text="About" />
        </StyledFlexColumn>
      </StyledFlexContainer>
    </StyledFooter>
  );
};
