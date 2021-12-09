import React, { useState } from "react";
import {
  Button,
  Modal,
  Typography,
  MenuItem,
  Select,
  Box,
  Input,
  OutlinedInput,
} from "@mui/material";
import { fetchWrapper, get, post } from "../../utils/fetchWrapper";

const CreateSubcategory = ({ open, onHide }) => {
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [parentId, setParentId] = useState(0);
  const [image, setImage] = useState(null);
  const [insideImage, setInsideImage] = useState(null);

  const [selectedCategory, setSelectedCategory] = useState("");

  const addSubcategory = () => {
    post("api/Categories/CreateCategory", postCategoryResult, {
      name,
      description,
      parentId,
      image,
      insideImage,
    });
    setName("");
    setDescription("");
    setParentId(0);
    setImage(null);
    setInsideImage(null);

    setSelectedCategory("");
    onHide();
  };
  function postCategoryResult(res) {
    if (!res.success) {
      console.log(res.errorCodes + "POST CATEGORY ERROR");
    }
  }

  const categories = get("api/Categories/GetCategories", getCategoriesResult);
  function getCategoriesResult(res) {
    if (!res) console.log("GET CATEGORIES ERROR");
  }

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
          placeholder="Name"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
        <Input
          placeholder="Description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />

        <Select
          multiple
          input={
            <OutlinedInput label={selectedCategory || "Выберите категорию"} />
          }
        >
          {categories.map((category) => (
            <MenuItem
              key={category.id}
              onClick={() => (
                setParentId(category.id), setSelectedCategory(category.name)
              )}
            >
              {category.name}
            </MenuItem>
          ))}
        </Select>

        <Input
          placeholder="Image"
          type="file"
          value={image}
          onChange={(e) => setImage(e.target.value)}
        />
        <Input
          placeholder="Image"
          type="file"
          value={insideImage}
          onChange={(e) => setInsideImage(e.target.value)}
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
