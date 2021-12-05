import { createTheme } from "@mui/material/styles";

const theme = createTheme({
  spacing: 5,
  palette: {
    primary: { main: "#56b280", contrastText: "#fff" },
    text: {
      primary: "#000",
      secondary: "#849a8e",
    },
  },
  typography: {
    fontFamily: "Poppins",
    button: {
      color: "#fff",
      fontWeight: 400,
      fontSize: 20,
      lineHeight: "24px",
      textTransform: "none",
    },
    h1: {
      fontSize: 40,
      fontWeight: 500,
    },
    h2: {
      fontSize: 26,
      fontWeight: 500,
    },
    h3: {
      fontSize: 20,
    },
    body1: {
      fontSize: 18,
    },
    body2: {
      fontSize: 16,
    },
  },
  components: {
    MuiButton: {
      defaultProps: {
        variant: "contained",
        color: "primary",
      },
      styleOverrides: {
        contained: { fontcolor: "#fff" },
      },
    },
  },
});

export default theme;
