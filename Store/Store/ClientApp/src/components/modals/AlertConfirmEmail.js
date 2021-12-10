import React from "react";
import {
  Dialog,
  DialogTitle,
  DialogActions,
  Button,
  Typography,
  Link,
} from "@mui/material";

const AlertConfirmEmail = ({ open, onHide }) => {
  return (
    <Dialog open={open} onClose={onHide}>
      <DialogTitle>
        <Typography variant="h3">Please, confirm your email</Typography>
      </DialogTitle>
      <DialogActions style={{ paddingInline: 25, paddingBottom: 20 }}>
        <Link href="/">
          <Button onClick={onHide}>OK</Button>
        </Link>
      </DialogActions>
    </Dialog>
  );
};

export default AlertConfirmEmail;
