import React, { useState } from "react";
import { TextField, Button, Typography } from "@mui/material";

const Authorization = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const sendData = () => {
    fetch("api/Account/Login", {
      method: "POST",
      body: JSON.stringify({ email, password }),
    })
      .then((res) => res.json())
      .then((result) => {
        console.log(result);
      });
  };
  return (
    <>
          AUTHORIZATION
          <TextField label="email" value={email} onChange={e => setEmail(e.target.value)} />
          <TextField label="password" value={password} onChange={e => setPassword(e.target.value)} />
      <Button variant="outlined" onClick={sendData}>
        <Typography variant="h1" color="initial">
          Send Info
        </Typography>
      </Button>
    </>
  );
};

export default Authorization;
