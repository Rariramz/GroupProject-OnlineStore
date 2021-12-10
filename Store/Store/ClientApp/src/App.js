import { Box, ThemeProvider, Typography } from "@mui/material";
import React, { Suspense } from "react";
import { BrowserRouter } from "react-router-dom";
import AppRouter from "./components/AppRouter";
import Footer from "./components/Footer";
import Header from "./components/Header";
import theme from "./theme";

function App() {
  return (
    <ThemeProvider theme={theme}>
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
    </ThemeProvider>
  );
}

export default App;
