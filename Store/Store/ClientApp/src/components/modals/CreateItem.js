import React, { useState, useEffect, useContext } from "react";
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
  Input,
  Typography,
} from "@mui/material";
import { styled } from "@mui/material/styles";
import { fetchWrapper, get, post } from "../../utils/fetchWrapper";
import { Context } from "../../index";

const CreateItem = ({ open, onHide }) => {
  const items = useContext(Context);
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [price, setPrice] = useState(0);
  const [image, setImage] = useState(null);
  const [subcategoryId, setSubcategoryId] = useState(0);

  const [selectedCategoryId, setSelectedCategoryId] = useState(0);
  const [selectedCategory, setSelectedCategory] = useState("");
  const [selectedSubcategory, setSelectedSubcategory] = useState("");

  const [categories, setCategories] = useState([]);
  const [subcategories, setSubcategories] = useState([]);

  // useEffect(() => {
  //   get("api/Categories/GetCategories", setCategories);
  // }, []);

  // useEffect(() => {
  //   setSubcategories(
  //     categories.filter((c) => c.parentID == selectedCategoryId)
  //   );
  // }, [selectedCategoryId, selectedCategory]);

  useEffect(() => {
    get("api/Categories/GetCategories", setCategories).then((res) => {
      items.setCategories(res);
      items.setSubcategories(res.filter((c) => c.parentId == items.parentId));
    });
  }, [items.parentId]);

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

    setCategories([]);
    setSelectedCategoryId(0);
    setSelectedCategory("");
    setSelectedSubcategory("");
    onHide();
  };
  function postItemResult(res) {
    if (res.success) {
      console.log("ADD ITEM success");
    } else {
      res.errorCodes.foreach((err) => alert(errors.get(err)));
    }
  }

  const StyledInput = styled(TextField)(({ theme }) => ({
    margin: "dense",
    variant: "outlined",
    width: "100%",
    margin: theme.spacing(2, 0),
  }));

  const ITEM_HEIGHT = 48;
  const ITEM_PADDING_TOP = 8;
  const MenuProps = {
    PaperProps: {
      style: {
        maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
        width: 300,
      },
    },
  };

  const selectImage = (e) => {
    setImage(e.target.files[0]);
  };

  const errors = new Map();
  errors.set(440, "empty name");
  errors.set(441, "name length is more than 50 characters");
  errors.set(442, "firstname validation failed");
  errors.set(443, "empty description");
  errors.set(444, "description length is more than 500 characters");
  errors.set(445, "description validation failed");
  errors.set(446, "image error");
  errors.set(447, "category does not exist");
  errors.set(448, "category already contains subcategories");
  errors.set(449, "price not valid");

  return (
    <Dialog open={open} onClose={onHide}>
      <DialogTitle>Add item</DialogTitle>
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
        <StyledInput
          label="Price"
          value={price}
          onChange={(e) => setPrice(e.target.value)}
        />

        <Button variant="outlined" component="label" sx={{ width: "100%" }}>
          <Typography variant="body2">Upload Image</Typography>
          <Input
            accept="image/*"
            type="file"
            onChange={selectImage}
            sx={{ display: "none" }}
          />
        </Button>

        <FormControl sx={{ width: 400, marginTop: 5 }}>
          <InputLabel id="demo-multiple-name-label">Category</InputLabel>
          <Select
            labelId="demo-multiple-name-label"
            id="demo-multiple-name"
            input={<OutlinedInput label="Choose category" />}
            MenuProps={MenuProps}
          >
            {categories
              .filter((c) => c.parentID == 1)
              .map((category) => (
                <MenuItem
                  key={category.id}
                  value={category}
                  onClick={() => (
                    setSelectedCategory(category.name),
                    items.setParentId(category.id)
                  )}
                >
                  {category.name}
                </MenuItem>
              ))}
          </Select>
        </FormControl>

        <FormControl sx={{ width: 400, marginTop: 5 }}>
          <InputLabel id="demo-multiple-name-label">Subcategory</InputLabel>
          <Select
            labelId="demo-multiple-name-label"
            id="demo-multiple-name"
            input={<OutlinedInput label="Choose subcategory" />}
            MenuProps={MenuProps}
          >
            {/* {categories
              .filter((c) => c.parentID == selectedCategoryId) */}
            {subcategories.map((subcategory) => (
              <MenuItem
                key={subcategory.id}
                value={subcategory}
                onClick={() => (
                  setSubcategoryId(subcategory.id),
                  setSelectedSubcategory(subcategory.name)
                )}
              >
                {subcategory.name}
              </MenuItem>
            ))}
          </Select>
        </FormControl>
      </DialogContent>
      <DialogActions style={{ paddingInline: 25, paddingBottom: 20 }}>
        <Button onClick={onHide}>Cancel</Button>
        <Button onClick={addItem}>Add</Button>
      </DialogActions>
    </Dialog>
  );
};

export default CreateItem;
