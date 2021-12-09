import { ThemeProvider, CircularProgress, Box } from "@mui/material";
import React, { useContext, useEffect, useState } from "react";
import { observer } from "mobx-react-lite";
import AppRouter from "./components/AppRouter";
import Footer from "./components/Footer";
import Header from "./components/Header";
import theme from "./theme";
import { Context } from "./index";
import { fetchWrapper, get } from "./utils/fetchWrapper";

const App = observer(() => {
  const { user } = useContext(Context);
  const [loading, setLoading] = useState(false);

  /*
  useEffect(() => {
    fetchWrapper("GET", "api/Account/Info", func, {});
  }, []);

  const func = (res) => {
        if (res.success) {
          user.setIsAuth(true);
          if (res.isAdmin) {
            user.setIsAdmin(true);
          }
          alert("HOORAY")
        } else {
            console.log(res.errorCodes);
            alert("NOT HORAY.")
        }
    };*/

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
      <AppRouter />
      <Footer />
    </ThemeProvider>
  );
});

export default App;
