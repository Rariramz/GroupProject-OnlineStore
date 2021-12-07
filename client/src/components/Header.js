import { Box, Grid, Paper, CardMedia, Typography, Button } from "@mui/material";
import CheckCircleOutlineIcon from "@mui/icons-material/CheckCircleOutline";
import { styled } from "@mui/styles";
import React from "react";

import logo from "../images/svg/logo.svg";
import cart from "../images/svg/Cart.svg";
import profile from "../images/svg/Profile.svg";

const HeaderDiv = styled(Box)(({ theme }) => ({
  display: "flex",
  flexDirection: "row",
  justifyContent: "space-evenly",
  alignItems: "center",
  padding: 20,
}));

const Header = () => {
  return (
    <HeaderDiv>
      <CardMedia component="img" title="Candleaf" image={logo} />
      <HeaderDiv width="40vw">
        <Typography variant="body1" color="initial" textDecoration="none">
          <a href="#">Home</a>
        </Typography>
        <Typography variant="body1" color="initial" textDecoration="none">
          <a href="#">About</a>
        </Typography>
        <Typography variant="body1" color="initial" textDecoration="none">
          <a href="#">Contact us</a>
        </Typography>
      </HeaderDiv>

      <HeaderDiv>
        <img class="cart" src={cart} alt="cart" paddingRight="20" />
        <img class="profile" src={profile} alt="profile" />
      </HeaderDiv>
    </HeaderDiv>
  );
};

export default Header;
