import {
  TextField,
  Grid,
  Button,
  Typography,
  Paper,
  Stack,
  Alert,
} from "@mui/material";
import { styled } from "@mui/system";
import React, { useContext, useRef } from "react";
import { useState } from "react";
import { Box } from "@mui/system";
import { HOME_ROUTE, REGISTRATION_ROUTE } from "../utils/consts";
import { NavLink, useHistory } from "react-router-dom";
import { Context } from "../index.js";
import { observer } from "mobx-react-lite";
import { fetchWrapper, get, post } from "../utils/fetchWrapper";
import { useNavigate } from "react-router-dom";

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

const errors = new Map();
errors.set(440, "empty email");
errors.set(441, "email validation failed");
errors.set(442, "no user registered with this email");
errors.set(443, "email not confirmed");
errors.set(444, "empty password");
errors.set(445, "wrong password");

const Login = observer(() => {
  const { user } = useContext(Context);
  //const history = useHistory();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorCodes, setErrorCodes] = useState([]);
  const navigate = useNavigate();

  const handleClick = () => {
    post("api/Account/Login", loginResult, { email, password });
    //history.push(HOME_ROUTE);
  };
  function loginResult(res) {
    if (res.success) {
      console.log("LOGIN success");
      user.setIsAuth(true);
      get("api/Account/Info", userInfoResult);
      navigate("/");
    } else {
      setErrorCodes(res.errorCodes);
    }
  }
  function userInfoResult(res) {
    user.setIsAdmin(res.isAdmin);
  }

  return (
    <>
      {errorCodes.length > 0 ? (
        <Stack>
          {errorCodes.map((err) => (
            <Alert severity="error">{errors.get(err)}</Alert>
          ))}
        </Stack>
      ) : (
        <></>
      )}
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
                  <Button color="primary" onClick={handleClick}>
                    <Typography variant="h3" sx={{ padding: 1 }}>
                      Log In
                    </Typography>
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
    </>
  );
});

export default Login;
