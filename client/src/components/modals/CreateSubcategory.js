import React, { useState } from "react";
import {
  Button,
  Modal,
  Typography,
  MenuItem,
  Select,
  Box,
  Input,
} from "@mui/material";
import { createSubcategory } from "../../http/itemsAPI";

const CreateSubcategory = ({ open, onHide }) => {
  const [value, setValue] = useState("");

  const addSubcategory = () => {
    createSubcategory({ name: value }).then((data) => {
      setValue("");
      onHide();
    });
  };

  return (
    <Modal
      open={open}
      onHide={onHide}
      sx={{
        position: "absolute",
        top: "50%",
        left: "50%",
        transform: "translate(-50%, -50%)",
      }}
    >
      <Typography variant="h4">Добавить категорию</Typography>

      <Box>
        <Input
          placeholder="Введите название подкатегории"
          value={value}
          onChange={(e) => setValue(e.target.value)}
        />
      </Box>

      <Button variant="outlined" onClick={onHide}>
        Закрыть
      </Button>
      <Button variant="outlined" onClick={addSubcategory}>
        Добавить
      </Button>
    </Modal>
  );
};

export default CreateSubcategory;
