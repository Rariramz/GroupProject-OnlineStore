import React, { useContext, useEffect, useState } from "react";

import {
  Button,
  Modal,
  Typography,
  MenuItem,
  Select,
  Box,
  OutlinedInput,
  Input,
  Grid,
} from "@mui/material";
import {
  createItem,
  fetchCategories,
  fetchSubcategories,
} from "../../http/itemsAPI";
import { observer } from "mobx-react-lite";
import { Context } from "../../index";

const CreateItem = observer(({ open, onHide }) => {
  const { items } = useContext(Context);
  const [name, setName] = useState("");
  const [price, setPrice] = useState(0);
  const [file, setFile] = useState(null);
  const [info, setInfo] = useState([]);

  useEffect(() => {
    fetchCategories().then((data) => items.setCategories(data));
    fetchSubcategories().then((data) => items.setSubcategories(data));
  }, []);

  const addInfo = () => {
    setInfo([...info, { title: "", description: "", number: Date.now() }]);
  };
  const removeInfo = (number) => {
    setInfo(info.filter((i) => i.number !== number));
  };
  const changeInfo = (key, value, number) => {
    setInfo(
      info.map((i) => (i.number === number ? { ...i, [key]: value } : i))
    );
  };

  const selectFile = (e) => {
    setFile(e.target.files[0]);
  };

  const addItem = () => {
    const formData = new FormData();
    formData.append("name", name);
    formData.append("price", `${price}`);
    formData.append("img", file);
    formData.append("categoryId", items.selectedCategory.id);
    formData.append("subcategoryId", items.selectedSubcategory.id);
    formData.append("info", JSON.stringify(info));
    createItem(formData).then((data) => onHide());
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
      <Typography variant="h4">Добавить продукт</Typography>

      <Box>
        <Select
          multiple
          input={
            <OutlinedInput
              label={items.selectedCategory.name || "Выберите категорию"}
            />
          }
        >
          {items.categories.map((category) => (
            <MenuItem
              key={category.id}
              onClick={() => items.setSelectedCategory(category)}
            >
              {category.name}
            </MenuItem>
          ))}
        </Select>

        <Select
          multiple
          input={
            <OutlinedInput
              label={items.selectedSubcategory.name || "Выберите подкатегорию"}
            />
          }
        >
          {items.subcategories.map((subcategory) => (
            <MenuItem
              key={subcategory.id}
              onClick={() => items.setSelectedSubcategory(subcategory)}
            >
              {subcategory.name}
            </MenuItem>
          ))}
        </Select>

        <Input
          placeholder="Введите название устройства"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
        <Input
          placeholder="Введите стоимость устройства"
          value={price}
          onChange={(e) => setPrice(Number(e.target.value))}
          type="number"
        />
        <Input onChange={selectFile} type="file" />

        <hr />
        <Button variant={"outlined"} onClick={addInfo}>
          Добавить новое свойство
        </Button>
        {info.map((i) => (
          <Grid container key={i.number}>
            <Grid item md={4}>
              <Input
                placeholder="Введите название свойства"
                value={i.title}
                onChange={(e) => changeInfo("title", e.target.value, i.number)}
              />
            </Grid>
            <Grid item md={4}>
              <Input
                placeholder="Введите описание свойства"
                value={i.description}
                onChange={(e) =>
                  changeInfo("description", e.target.value, i.number)
                }
              />
            </Grid>
            <Grid item md={4}>
              <Button onClick={() => removeInfo(i.number)} variant={"outlined"}>
                Удалить
              </Button>
            </Grid>
          </Grid>
        ))}
      </Box>

      <Button variant="outlined" onClick={onHide}>
        Закрыть
      </Button>
      <Button variant="outlined" onClick={addItem}>
        Добавить
      </Button>
    </Modal>
  );
});

export default CreateItem;
