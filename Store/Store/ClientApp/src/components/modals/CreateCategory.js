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
import { createCategory } from "../../http/itemsAPI";

const CreateCategory = ({ open, onHide }) => {
  const [value, setValue] = useState("");

  const addCategory = () => {
    createCategory({ name: value }).then((data) => {
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
          placeholder="Введите название категории"
          value={value}
          onChange={(e) => setValue(e.target.value)}
        />
      </Box>

      <Button variant="outlined" onClick={onHide}>
        Закрыть
      </Button>
      <Button variant="outlined" onClick={addCategory}>
        Добавить
      </Button>
    </Modal>
  );
};

export default CreateCategory;
