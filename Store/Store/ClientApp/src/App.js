import { Box, ThemeProvider, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import React, { useContext, useEffect, Suspense } from "react";
import AppRouter from "./components/AppRouter";
import Footer from "./components/Footer";
import Header from "./components/Header";
import theme from "./theme";
import { Context } from "./index";
import { fetchWrapper, get, post } from "./utils/fetchWrapper";
import ErrorBoundary from "./utils/ErrorBoundary";

const App = observer(() => {
  const { user } = useContext(Context);

  useEffect(() => {
    get("api/Account/Info", userInfoResult);
  }, []);
  function userInfoResult(res) {
    if (res.success) {
      user.setIsAuth(true);
      if (res.isAdmin) {
        user.setIsAdmin(true);
        console.log("ADMIN success");
      }
      console.log("LOGIN success");
    }
  }

  return (
    <ThemeProvider theme={theme}>
      <ErrorBoundary>
        <Header />
        <Suspense
          fallback={
            <Box padding="40vh">
              <Typography variant="h1" color="primary" textAlign="center">
                Loading...
              </Typography>
            </Box>
          }
        >
          <AppRouter />
        </Suspense>
        <Footer />
      </ErrorBoundary>
    </ThemeProvider>
  );
});

export default App;
