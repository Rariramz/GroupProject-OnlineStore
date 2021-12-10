import { Typography } from "@mui/material";
import React from "react";

const ErrorMesage = (message) => {
  return (
    <Typography variant="body2" style={{ width: "100%", color: "hotpink" }}>
      {message}
    </Typography>
  );
};

export default ErrorMesage;
