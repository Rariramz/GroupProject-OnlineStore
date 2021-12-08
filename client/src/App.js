import {
  Container,
  Button,
  ThemeProvider,
  Typography,
  CircularProgress,
  Grid,
  Box,
} from "@mui/material";
import React, { useContext, useEffect, useState } from "react";
import { BrowserRouter } from "react-router-dom";
import { observer } from "mobx-react-lite";
import AppRouter from "./components/AppRouter";
import Footer from "./components/Footer";
import Header from "./components/Header";
import theme from "./theme";
import { Context } from "./index";
import { getUser } from "./http/userAPI";

const App = observer(() => {
  const { user } = useContext(Context);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getUser()
      .then((data) => {
        user.setUser(data);
        user.setIsAuth(true);
      })
      .finally(() => setLoading(false));
  }, []);

  if (loading) {
    return (
      <Box
        height="100vh"
        alignItems="center"
        justifyContent="center"
        sx={{ display: "flex" }}
      >
        <CircularProgress />
      </Box>
    );
  }

  return (
    <ThemeProvider theme={theme}>
      <Header />
      <BrowserRouter>
        <AppRouter />
      </BrowserRouter>
      <Footer />
    </ThemeProvider>
  );
});

export default App;
