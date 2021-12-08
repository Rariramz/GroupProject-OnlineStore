import React from "react";
import { Button, Link } from "@mui/material";
import { LOGIN_ROUTE } from "../utils/consts";

const Header = () => {
  return (
    <>
      <Link href={LOGIN_ROUTE}>
        <Button variant="outlined">Войти</Button>
      </Link>
    </>
  );
};

export default Header;
