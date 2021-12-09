import React, { useState, useEffect } from "react";
import {
  TextField,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  OutlinedInput,
} from "@mui/material";
import { styled } from "@mui/material/styles";
import { fetchWrapper, get, post } from "../../utils/fetchWrapper";

const CreateSubcategory = ({ open, onHide }) => {
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [parentId, setParentId] = useState(0);
  const [image, setImage] = useState(null);
  const [insideImage, setInsideImage] = useState(null);

  const [selectedCategory, setSelectedCategory] = useState("");

  const postCategoryResult = (res) => {
    if (!res.success) {
      console.log(res.errorCodes + "POST CATEGORY ERROR");
    }
  };
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

  const [categories, setCategories] = useState([]);
  useEffect(() => {
    get("api/Categories/GetCategories", console.log);
  }, []);

  const StyledInput = styled(TextField)(({ theme }) => ({
    margin: "dense",
    variant: "outlined",
    width: "100%",
  }));

  function handleSetImage(e) {
    setImage(e.target.value);
  }
  function handleSetInsideImage(e) {
    setInsideImage(e.target.value);
  }

  const ITEM_HEIGHT = 48;
  const ITEM_PADDING_TOP = 8;
  const MenuProps = {
    PaperProps: {
      style: {
        maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
        width: 250,
      },
    },
  };

  return (
    <Dialog open={open} onClose={onHide}>
      <DialogTitle>Add subcategory</DialogTitle>
      <DialogContent>
        <StyledInput
          label="Name"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
        <StyledInput
          label="Description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />

        <FormControl sx={{ m: 1, width: 300 }}>
          <InputLabel id="demo-multiple-name-label">Category</InputLabel>
          <Select
            labelId="demo-multiple-name-label"
            id="demo-multiple-name"
            input={<OutlinedInput label="Choose category" />}
            MenuProps={MenuProps}
          >
            {categories
              .filter((c) => c.parentId == null)
              .map((category) => (
                <MenuItem
                  key={category.id}
                  value={category}
                  onClick={(e) => (
                    setParentId(e.target.value.id),
                    setSelectedCategory(e.target.value.name)
                  )}
                >
                  {category.name}
                </MenuItem>
              ))}
          </Select>
        </FormControl>

        <StyledInput type="file" value={image} onChange={handleSetImage} />
        <StyledInput
          type="file"
          value={insideImage}
          onChange={handleSetInsideImage}
        />
      </DialogContent>
      <DialogActions>
        <Button variant="outlined" onClick={onHide}>
          Cancel
        </Button>
        <Button variant="outlined" onClick={addSubcategory}>
          Subscribe
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default CreateSubcategory;
