import { $authHost, $host } from "./index";

export const registration = async (formData) => {
  const user = await $host.post("api/account/registration", { formData });
  return user;
};

export const login = async (formData) => {
  const user = await $host.post("api/account/login", { formData });
  return user;
};

export const getUser = async () => {
  const user = await $host.get("api/account");
  return user;
};
