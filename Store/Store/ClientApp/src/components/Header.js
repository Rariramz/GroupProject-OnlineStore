import { Box, Grid, Paper, Typography, Button } from "@mui/material";
import CheckCircleOutlineIcon from "@mui/icons-material/CheckCircleOutline";
import { styled } from "@mui/styles";
import React from "react";

import logo from "../images/svg/logo.svg";
import cart from "../images/svg/Cart.svg";
import profile from "../images/svg/Profile.svg";
import { Link } from "react-router-dom";

const HeaderDiv = styled(Box)(({ theme }) => ({
  display: "flex",
  flexDirection: "row",
  justifyContent: "space-around",
  alignItems: "center",
  padding: 10,
}));

const Header = () => {
  return (
    <HeaderDiv>
      <Link to="">
        <img class="logo" src={logo} alt="Candleaf" />
      </Link>
      <HeaderDiv width="40vw">
        <Typography variant="body1" color="initial">
          Home
        </Typography>
        <Typography variant="body1" color="initial">
          About
        </Typography>
        <Typography variant="body1" color="initial">
          Contact us
        </Typography>
      </HeaderDiv>

      <HeaderDiv width="7vw">
        <Link to="/login">
          <img class="profile" src={profile} alt="profile" />
        </Link>
        <Link to="cart">
          <img class="cart" src={cart} alt="cart" paddingRight="20" />
        </Link>
      </HeaderDiv>
    </HeaderDiv>
  );
};

export default Header;
