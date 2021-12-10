import React, { useState } from "react";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";

const OkDialog = (props) => {
  return (
    <Dialog open={props.open} onClose={props.handleClose}>
      <DialogTitle>Order confirmation</DialogTitle>
      <DialogContent style={{ padding: 20 }}>
        <DialogContentText>
          Order confirmed. Message with order information was sent to your
          email.
        </DialogContentText>
      </DialogContent>
      <DialogActions style={{ paddingBottom: 20, paddingRight: 20 }}>
        <Button onClick={props.handleClose}>Ok</Button>
      </DialogActions>
    </Dialog>
  );
};

export default OkDialog;
