import React, { useState } from "react";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";

const AddressDialog = (props) => {
  const [address, setAddress] = useState("");
  return (
    <Dialog open={props.open} onClose={props.handleClose}>
      <DialogTitle>Order confirmation</DialogTitle>
      <DialogContent style={{ padding: 20 }}>
        <DialogContentText>
          To finish with the check-out for yor order, please enter your address
          here. We will send an email with order information to you.
        </DialogContentText>
        <TextField
          autoFocus
          margin="dense"
          label="Address"
          fullWidth
          variant="standard"
          value={address}
          onChange={(e) => setAddress(e.target.value)}
        />
      </DialogContent>
      <DialogActions style={{ paddingBottom: 20, paddingRight: 20 }}>
        <Button onClick={props.handleClose}>Cancel</Button>
        <Button onClick={() => props.onCheckoutConfirm(address)}>
          Confirm
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default AddressDialog;
