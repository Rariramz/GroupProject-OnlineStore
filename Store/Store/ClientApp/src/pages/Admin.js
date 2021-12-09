import React, { useState } from "react";
import { Button, Container } from "@mui/material";
import CreateSubcategory from "../components/modals/CreateSubcategory";
//import CreateItem from "../components/modals/CreateItem";

const Admin = () => {
  const [subcategoryVisible, setSubcategoryVisible] = useState(false);
  //const [itemVisible, setItemVisible] = useState(false);

  return (
    <Container>
      <Button variant="outlined" onClick={() => setSubcategoryVisible(true)}>
        Добавить подкатегорию
      </Button>
      {/*<Button variant="outlined" onClick={() => setItemVisible(true)}>*/}
      {/*  Добавить продукт*/}
      {/*</Button>*/}

      <CreateSubcategory
        open={subcategoryVisible}
        onHide={() => alert("EEE")}
      />
      {/*<CreateItem open={itemVisible} onHide={() => setItemVisible(false)} />*/}
    </Container>
  );
};

export default Admin;
