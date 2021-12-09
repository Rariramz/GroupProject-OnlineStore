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
  Grid,
  Input,
  Typography,
} from "@mui/material";
import { styled } from "@mui/material/styles";
import { fetchWrapper, get, post } from "../../utils/fetchWrapper";

const CreateSubcategory = ({ open, onHide }) => {
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [parentID, setParentId] = useState(0);
  const [image, setImage] = useState(null);
  const [insideImage, setInsideImage] = useState(null);

  const [selectedCategory, setSelectedCategory] = useState("");

  const postCategoryResult = (res) => {
    console.log(res);
    if (res.success) {
      console.log("OKEY");
    }
    if (!res.success) {
      console.log(res.errorCodes + "POST CATEGORY ERROR");
    }
  };
  const addSubcategory = () => {
    console.log(name);
    console.log(description);
    console.log(parentId);
    console.log(image);
    console.log(insideImage);
    const res = post("api/Categories/CreateCategory", postCategoryResult, {
      name,
      description,
      parentID,
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
    get("api/Categories/GetCategories", setCategories);
  }, []);

  const StyledInput = styled(TextField)(({ theme }) => ({
    margin: "dense",
    variant: "outlined",
    width: "100%",
    margin: theme.spacing(2, 0),
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
        width: 300,
      },
    },
  };

  const selectImage = (e) => {
    setImage(e.target.files[0]);
  };
  const selectInsideImage = (e) => {
    setInsideImage(e.target.files[0]);
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

        <FormControl sx={{ width: 400, marginBlock: 5 }}>
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
                    setParentId(category.id), setSelectedCategory(category.name)
                  )}
                >
                  {category.name}
                </MenuItem>
              ))}
          </Select>
        </FormControl>

        <Button variant="outlined" component="label" sx={{ width: "100%" }}>
          <Typography variant="body2">Upload Image</Typography>
          <Input type="file" onChange={selectImage} sx={{ display: "none" }} />
        </Button>

        <Button
          variant="outlined"
          component="label"
          sx={{ width: "100%", marginTop: 2 }}
        >
          <Typography variant="body2">Upload Inside Image</Typography>
          <Input
            type="file"
            onChange={selectInsideImage}
            sx={{ display: "none" }}
          />
        </Button>
      </DialogContent>
      <DialogActions style={{ paddingInline: 25, paddingBottom: 20 }}>
        <Button onClick={onHide}>Cancel</Button>
        <Button onClick={addSubcategory}>Subscribe</Button>
      </DialogActions>
    </Dialog>
  );
};

export default CreateSubcategory;
