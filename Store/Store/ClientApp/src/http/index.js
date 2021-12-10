import axios from "axios";
import { URL } from "../utils/consts";

export const $host = axios.create({
  baseURL: URL,
});

export const $adminHost = axios.create({
  baseURL: URL,
});
