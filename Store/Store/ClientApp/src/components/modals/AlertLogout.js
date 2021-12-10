import React from "react";
import {
  Dialog,
  DialogTitle,
  DialogActions,
  Button,
  Typography,
  Link,
} from "@mui/material";
import { fetchWrapper, get, post } from "../../utils/fetchWrapper";
import { useNavigate } from "react-router-dom";

const AlertLogout = ({ open, onHide }) => {
  const navigate = useNavigate();

  const handleYES = () => {
    get("api/Account/Logout");
    navigate("/");
    onHide();
  };

  return (
    <Dialog open={open} onClose={onHide}>
      <DialogTitle>
        <Typography variant="h3">Sign out of your account?</Typography>
      </DialogTitle>
      <DialogActions style={{ paddingInline: 25, paddingBottom: 20 }}>
        <Button onClick={onHide}>BACK</Button>
        <Button onClick={handleYES}>YES</Button>
      </DialogActions>
    </Dialog>
  );
};

export default AlertLogout;
