import { TextField, Grid, Button, Typography, Paper } from "@mui/material";
import { styled } from "@mui/system";
import React, { useContext, useRef } from "react";
import { useState } from "react";
import { Box } from "@mui/system";
import { HOME_ROUTE, REGISTRATION_ROUTE } from "../utils/consts";
import { NavLink, useHistory } from "react-router-dom";
import { login } from "../http/userAPI";
import { Context } from "../index.js";
import { observer } from "mobx-react-lite";

const Login = observer(() => {
  const { user } = useContext(Context);
  //const history = useHistory();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const emailRef = useRef();
  const passwordRef = useRef();

  const focus = (value) => {
    setEmail(value);
    emailRef.current.focus();
  };

  const handleClick = async () => {
    try {
      let formData = new FormData();
      formData.append("email", email);
      formData.append("password", password);
      let userInfo = await login(formData);
      /* сохраняем пользователя в контекст */
      user.setUser(userInfo);
      user.setIsAuth(true);
      //history.push(HOME_ROUTE); /* переходим на главную страницу */
    } catch (e) {
      alert(e.response.data.message);
    }
  };

  const StyledPaper = styled(Paper)(({ theme }) => ({
    width: 700,
    height: 400,
    padding: theme.spacing(5, 30),
    boxSizing: "border-box",
    background: "#fff", //"rgba(247, 248, 250, 0.8)",
    borderRadius: 6,
  }));
  const ColoredBackground = styled(Box)(({ theme }) => ({
    width: "100%",
    height: "100vh",
    backgroundColor: "rgb(238, 247, 242)",
    backgroundSize: "cover",
  }));

  return (
    <ColoredBackground>
      <Grid
        container
        spacing={0}
        direction="column"
        alignItems="center"
        justifyContent="center"
        style={{ minHeight: "100vh" }}
      >
        <StyledPaper>
          <Grid container item spacing={4}>
            <Grid
              container
              item
              spacing={2}
              direction="column"
              alignItems="center"
              justifyContent="center"
            >
              <Grid item>
                <Typography variant="h1">Log In</Typography>
              </Grid>
              <Grid item>
                <Typography color="gray" variant="body1">
                  Log in to make purchases
                </Typography>
              </Grid>
            </Grid>

            <Grid container item spacing={1}>
              <Grid item style={{ width: "100%" }}>
                <TextField
                  ref={emailRef}
                  style={{ width: "100%" }}
                  label="Email"
                  type="email"
                  variant="outlined"
                  margin="dense"
                  value={email}
                  onChange={(e) => focus(e.target.value)}
                />
              </Grid>
              <Grid item style={{ width: "100%" }}>
                <TextField
                  style={{ width: "100%" }}
                  label="Password"
                  type="password"
                  variant="outlined"
                  margin="dense"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                />
              </Grid>
            </Grid>

            <Grid
              container
              item
              spacing={1}
              direction="row"
              alignItems="center"
              justifyContent="space-between"
            >
              <Grid item>
                <Button color="primary" onClick={handleClick} size="large">
                  Log in
                </Button>
              </Grid>
              <Grid item>
                <Typography variant="body2" color="primary" textAlign="right">
                  Don't have an account?
                  <NavLink
                    to={REGISTRATION_ROUTE}
                    style={{ textDecoration: "none", color: "hotpink" }}
                  >
                    <Typography variant="body1">Register</Typography>
                  </NavLink>
                </Typography>
              </Grid>
            </Grid>
          </Grid>
        </StyledPaper>
      </Grid>
    </ColoredBackground>
  );
});

export default Login;
