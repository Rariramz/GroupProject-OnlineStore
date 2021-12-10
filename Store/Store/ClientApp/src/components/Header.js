import { Box, Grid, Paper, Typography, Button } from "@mui/material";
import * as material from "@mui/material";
import CheckCircleOutlineIcon from "@mui/icons-material/CheckCircleOutline";
import { styled } from "@mui/styles";
import React, { useContext, useEffect, useState } from "react";
import { fetchWrapper, get, post } from "../utils/fetchWrapper";
import { Context } from "../index";

import logo from "../images/svg/logo.svg";
import cart from "../images/svg/Cart.svg";
import profile from "../images/svg/Profile.svg";
import setting from "../images/svg/setting.png";
import { Link } from "react-router-dom";
import { LOGIN_ROUTE, ADMIN_ROUTE } from "../utils/consts";
import { observer } from "mobx-react-lite";
import AlertLogout from "./modals/AlertLogout";

const HeaderDiv = styled(Box)(({ theme }) => ({
  display: "flex",
  flexDirection: "row",
  justifyContent: "space-around",
  alignItems: "center",
  padding: 10,
}));

const Header = observer(() => {
  const { user } = useContext(Context);
  const [adminVisible, setAdminVisible] = useState(false);
  const [alertLogoutVisible, setAlertLogoutVisible] = useState(false);
  useEffect(() => {
    get("api/Account/Info", (res) => setAdminVisible(res.isAdmin));
  }, []);

  const handleLogout = () => {
    setAlertLogoutVisible(true);
  };

  return (
    <HeaderDiv>
      <Link to="">
        <img src={logo} alt="Candleaf" />
      </Link>
      <HeaderDiv width="40vw">
        <Link
          to=""
          style={{
            textDecoration: "none",
          }}
        >
          <Typography variant="body1" color="initial">
            Home
          </Typography>
        </Link>
        <material.Link
          href="mailto:onlinestore953501@gmail.com"
          style={{
            textDecoration: "none",
          }}
          target="_blank"
        >
          <Typography variant="body1" color="initial">
            Contact us
          </Typography>
        </material.Link>
      </HeaderDiv>

      <HeaderDiv width={adminVisible ? "11vw" : "7wh"}>
        {adminVisible && (
          <Link to={ADMIN_ROUTE}>
            <img src={setting} alt="setting" style={{ width: 26 }} />
          </Link>
        )}
        {user.isAuth ? (
          <>
            <Button
              variant="outlined"
              onClick={handleLogout}
              sx={{ marginInline: 2 }}
            >
              <Typography variant="body1">Logout</Typography>
            </Button>
            <Link to="cart">
              <img src={cart} alt="cart" paddingRight="20" />
            </Link>
          </>
        ) : (
          <Link to={LOGIN_ROUTE}>
            <img src={profile} alt="profile" />
          </Link>
        )}
      </HeaderDiv>
      <AlertLogout
        open={alertLogoutVisible}
        onHide={() => setAlertLogoutVisible(false)}
      />
    </HeaderDiv>
  );
});

export default Header;
