import React, { useState } from "react";
import { Button, Container } from "@mui/material";
import CreateSubcategory from "../components/modals/CreateSubcategory";
import CreateItem from "../components/modals/CreateItem";
import CreateCategory from "../components/modals/CreateCategory";

const Admin = () => {
  const [categoryVisible, setCategoryVisible] = useState(false);
  const [subcategoryVisible, setSubcategoryVisible] = useState(false);
  const [itemVisible, setItemVisible] = useState(false);

  return (
    <Container>
      <Button variant="outlined" onClick={() => setCategoryVisible(true)}>
        Добавить категорию
      </Button>
      <Button variant="outlined" onClick={() => setSubcategoryVisible(true)}>
        Добавить подкатегорию
      </Button>
      <Button variant="outlined" onClick={() => setItemVisible(true)}>
        Добавить продукт
      </Button>

      <CreateCategory
        open={categoryVisible}
        onHide={() => setCategoryVisible(false)}
      />
      <CreateSubcategory
        open={subcategoryVisible}
        onHide={() => setSubcategoryVisible(false)}
      />
      <CreateItem open={itemVisible} onHide={() => setItemVisible(false)} />
    </Container>
  );
};

export default Admin;
