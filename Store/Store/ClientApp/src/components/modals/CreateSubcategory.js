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
import { Context } from "../../index";
import { fetchWrapper, get, post } from "../../utils/fetchWrapper";

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

const errors = new Map();
errors.set(440, "parent category does not exist");
errors.set(441, "ignore this code");
errors.set(
  442,
  "parent category can't contain items and subcategories at the same time"
);
errors.set(443, "name is empty");
errors.set(444, "name validation fail");
errors.set(445, "name length is more than 50 characters");
errors.set(446, "description is empty");
errors.set(447, "description validation fail");
errors.set(448, "description length is more than 500 characters");
errors.set(449, "error in image");
errors.set(450, "error in insideImage");

const CreateSubcategory = ({ open, onHide }) => {
  const { items } = useContext(Context);
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [parentId, setParentId] = useState(0);
  const [image, setImage] = useState(null);
  const [insideImage, setInsideImage] = useState(null);

  useEffect(() => {
    get("api/Categories/GetMainCategories", (res) => {
      items.setCategories(res);
    });
  }, []);

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
    onHide();
  };
  function postCategoryResult(res) {
    if (res.success) {
      console.log("ADD SUBCATEGORY success");
    } else {
      res.errorCodes.foreach((err) => alert(errors.get(err)));
    }
  }

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
            {items.categories.map((category) => (
              <MenuItem
                key={category.id}
                value={category}
                onClick={() => setParentId(category.id)}
              >
                {category.name}
              </MenuItem>
            ))}
          </Select>
        </FormControl>

        <Button variant="outlined" component="label" sx={{ width: "100%" }}>
          <Typography variant="body2">Upload Image</Typography>
          <Input
            accept="image/*"
            type="file"
            onChange={selectImage}
            sx={{ display: "none" }}
          />
        </Button>

        <Button
          variant="outlined"
          component="label"
          sx={{ width: "100%", marginTop: 2 }}
        >
          <Typography variant="body2">Upload Inside Image</Typography>
          <Input
            accept="image/*"
            type="file"
            onChange={selectInsideImage}
            sx={{ display: "none" }}
          />
        </Button>
      </DialogContent>
      <DialogActions style={{ paddingInline: 25, paddingBottom: 20 }}>
        <Button onClick={onHide}>Cancel</Button>
        <Button onClick={addSubcategory}>Add</Button>
      </DialogActions>
    </Dialog>
  );
};

export default CreateSubcategory;
