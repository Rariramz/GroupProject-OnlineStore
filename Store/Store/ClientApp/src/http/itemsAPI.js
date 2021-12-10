import { $adminHost, $host } from "./index";

export const createCategory = async (category) => {
  const { data } = await $adminHost.post("api/category", category);
  return data;
};

export const fetchCategories = async () => {
  const { data } = await $host.get("api/category");
  return data;
};

export const createSubcategory = async (subcategory) => {
  const { data } = await $adminHost.post("api/subcategory", subcategory);
  return data;
};

export const fetchSubcategories = async () => {
  const { data } = await $host.get("api/subcategory");
  return data;
};

export const createItem = async (item) => {
  const { data } = await $adminHost.post("api/item", item);
  return data;
};

export const fetchItems = async (
  categoryId,
  subcategoryId,
  page,
  limit = 5
) => {
  const { data } = await $host.get("api/item", {
    params: {
      categoryId,
      subcategoryId,
      page,
      limit,
    },
  });
  return data;
};

export const fetchOneItem = async (id) => {
  const { data } = await $host.get("api/item/" + id);
  return data;
};
