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
import { observer } from "mobx-react-lite";
import { fetchWrapper, get, post } from "../../utils/fetchWrapper";

const CreateItem = observer(({ open, onHide }) => {
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [price, setPrice] = useState(0);
  const [image, setImage] = useState(null);
  const [subcategoryId, setSubcategoryId] = useState(0);

  const [selectedCategoryId, setSelectedCategoryId] = useState(0);
  const [selectedCategory, setSelectedCategory] = useState("");
  const [selectedSubcategory, setSelectedSubcategory] = useState("");

    const categories = get("api/Categories/GetCategories", console.log);
    
  function getCategoriesResult (res) {
    if (!res) console.log("GET CATEGORIES ERROR");
  };

  const addItem = () => {
    post("api/Items/CreateItem", postItemResult, {
      name,
      description,
      price,
      image,
      categoryId: subcategoryId,
    });
    setName("");
    setDescription("");
    setPrice(0);
    setImage(null);
    setSubcategoryId(0);
  };
  const postItemResult = (res) => {
    if (!res.success) {
      console.log(res.errorCodes + "POST ITEM ERROR");
    }
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
      <Typography variant="h4">Add item</Typography>

      <Box>
        <Input
          placeholder="Name"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
        <Input
          placeholder="Description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />
        <Input
          placeholder="0 $"
          value={price}
          onChange={(e) => setPrice(e.target.value)}
        />
        <Input
          value={image}
          onChange={(e) => setImage(e.target.value)}
          type="file"
        />

        <Select
          multiple
          input={
            <OutlinedInput label={selectedCategory || "Choose category"} />
          }
        >
          {categories.map((category) => (
            <MenuItem
              key={category.id}
              onClick={() => setSelectedCategory(category.name)}
            >
              {category.name}
            </MenuItem>
          ))}
        </Select>

        <Select
          multiple
          input={
            <OutlinedInput
              label={selectedSubcategory || "Choose subcategory"}
            />
          }
        >
          {categories
            .filter((c) => c.parentId == selectedCategoryId)
            .map((subcategory) => (
              <MenuItem
                key={subcategory.id}
                onClick={() => (
                  setSubcategoryId(subcategory.id),
                  setSelectedSubcategory(subcategory.name)
                )}
              >
                {subcategory.name}
              </MenuItem>
            ))}
        </Select>
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
