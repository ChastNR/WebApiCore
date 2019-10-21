import React from "react";
import styled from "styled-components";

import SLInk from "../../base/controls/SLink";

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
          <SLInk to="/contacts" text="Contacts" />
        </StyledFlexColumn>
        <StyledFlexColumn>
          <SLInk to="/about" text="About" />
        </StyledFlexColumn>
      </StyledFlexContainer>
    </StyledFooter>
  );
};
