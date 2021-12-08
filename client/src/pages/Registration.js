import { TextField, Grid, Button, Typography, Paper } from "@mui/material";
import { styled } from "@mui/system";
import React, { useContext } from "react";
import { useState } from "react";
import { Box } from "@mui/system";
import { HOME_ROUTE, LOGIN_ROUTE } from "../utils/consts";
import { NavLink, useHistory } from "react-router-dom";
import { registration } from "../http/userAPI";
import { Context } from "../index.js";

const Registration = () => {
  const { user } = useContext(Context);
  //const history = useHistory();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [passwordConfirm, setPasswordConfirm] = useState("");
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");

  const handleClick = async () => {
    try {
      let formData = new FormData();
      formData.append("email", email);
      formData.append("password", password);
      formData.append("passwordConfirm", passwordConfirm);
      formData.append("lastName", lastName);
      formData.append("firstName", firstName);
      let userInfo = await registration(formData);
      user.setUser(userInfo); /* загружаем пользователя в контекст */
      user.setIsAuth(true);
      //history.push(HOME_ROUTE); /* переходим на главную страницу */
    } catch (e) {
      alert(e.response.data.message);
    }
  };

  const StyledPaper = styled(Paper)(({ theme }) => ({
    width: 700,
    height: 600,

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
                <Typography variant="h1">Register</Typography>
              </Grid>
              <Grid item>
                <Typography color="gray" variant="body1">
                  Register to make purchases
                </Typography>
              </Grid>
            </Grid>

            <Grid container item spacing={0}>
              <Grid item style={{ width: "100%" }}>
                <TextField
                  required
                  style={{ width: "100%" }}
                  label="Last Name"
                  type="name"
                  variant="outlined"
                  margin="dense"
                  value={lastName}
                  onChange={(e) => setLastName(e.target.value)}
                />
              </Grid>
              <Grid item style={{ width: "100%" }}>
                <TextField
                  required
                  style={{ width: "100%" }}
                  label="First Name"
                  type="name"
                  variant="outlined"
                  margin="dense"
                  value={firstName}
                  onChange={(e) => setFirstName(e.target.value)}
                />
              </Grid>
            </Grid>

            <Grid container item spacing={0}>
              <Grid item style={{ width: "100%" }}>
                <TextField
                  required
                  style={{ width: "100%" }}
                  label="Email"
                  type="email"
                  variant="outlined"
                  margin="dense"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                />
              </Grid>
              <Grid item style={{ width: "100%" }}>
                <TextField
                  required
                  style={{ width: "100%" }}
                  label="Password"
                  type="password"
                  variant="outlined"
                  margin="dense"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                />
              </Grid>
              <Grid item style={{ width: "100%" }}>
                <TextField
                  required
                  style={{ width: "100%" }}
                  label="Confirm Password"
                  type="password"
                  variant="outlined"
                  margin="dense"
                  value={passwordConfirm}
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
                  Register
                </Button>
              </Grid>
              <Grid item>
                <Typography variant="body2" color="primary" textAlign="right">
                  Already have an account?
                  <NavLink
                    to={LOGIN_ROUTE}
                    style={{ textDecoration: "none", color: "hotpink" }}
                  >
                    <Typography variant="body1">Log in</Typography>
                  </NavLink>
                </Typography>
              </Grid>
            </Grid>
          </Grid>
        </StyledPaper>
      </Grid>
    </ColoredBackground>
  );
};

export default Registration;
