import { Button, ThemeProvider, Typography } from "@mui/material";
import React from "react";
import { BrowserRouter } from "react-router-dom";
import AppRouter from "./components/AppRouter";
import Footer from "./components/Footer";
import Header from "./components/Header";
import theme from "./theme";

function App() {
  return (
    <ThemeProvider theme={theme}>
      <Header />
      <BrowserRouter>
        <AppRouter />
      </BrowserRouter>
      <Footer />
    </ThemeProvider>
  );
}

export default App;
