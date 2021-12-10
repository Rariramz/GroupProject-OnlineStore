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
import React, { useContext } from "react";
import { useState } from "react";
import { Box } from "@mui/system";
import { HOME_ROUTE, LOGIN_ROUTE } from "../utils/consts";
import { NavLink, useHistory } from "react-router-dom";
import { Context } from "../index.js";
import { fetchWrapper, get, post } from "../utils/fetchWrapper";
import AlertConfirmEmail from "../components/modals/AlertConfirmEmail";

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

const errors = new Map();
errors.set(440, "empty firstname");
errors.set(441, "firstname length is more than 20 characters");
errors.set(442, "firstname validation failed");
errors.set(443, "empty lastname");
errors.set(444, "lastname length is more than 20 characters");
errors.set(445, "lastname validation failed");
errors.set(446, "empty email");
errors.set(447, "user with this email is already registered");
errors.set(448, "email validation failed");
errors.set(449, "password is too weak (length is less than 8 characters)");
errors.set(450, "password is empty");
errors.set(451, "passwordConfirm is empty");
errors.set(452, "password and passwordConfirm do not match");

const Registration = () => {
  const { user } = useContext(Context);
  //const history = useHistory();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [passwordConfirm, setPasswordConfirm] = useState("");
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [errorCodes, setErrorCodes] = useState([]);
  const [alertVisible, setAlertVisible] = useState(false);

  const handleClick = () => {
    post("api/Account/Register", registerResult, {
      firstName,
      lastName,
      email,
      password,
      passwordConfirm,
    });
  };
  function registerResult(res) {
    if (res.success) {
      console.log("REGISTER success");
      user.setIsAuth(true);
      //history.push(HOME_ROUTE);
      setAlertVisible(true);
    } else {
      setErrorCodes(res.errorCodes);
    }
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
                    label="First Name"
                    type="name"
                    variant="outlined"
                    margin="dense"
                    value={firstName}
                    onChange={(e) => setFirstName(e.target.value)}
                  />
                </Grid>
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
                    onChange={(e) => setPasswordConfirm(e.target.value)}
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
              <AlertConfirmEmail
                open={alertVisible}
                onHide={() => setAlertVisible(false)}
              />
            </Grid>
          </StyledPaper>
        </Grid>
      </ColoredBackground>
    </>
  );
};

export default Registration;
