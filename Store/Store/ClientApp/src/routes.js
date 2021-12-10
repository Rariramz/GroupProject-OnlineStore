import Registration from "./pages/Registration";
import React from "react";
import {
  ADMIN_ROUTE,
  CART_ROUTE,
  HOME_ROUTE,
  ITEM_ROUTE,
  LOGIN_ROUTE,
  REGISTRATION_ROUTE,
  CATEGORY_ROUTE,
} from "./utils/consts";
const Cart = React.lazy(() => import("./pages/Cart"));
const Home = React.lazy(() => import("./pages/Home"));
const ItemPage = React.lazy(() => import("./pages/ItemPage"));
const CategoryPage = React.lazy(() => import("./pages/CategoryPage"));
const Login = React.lazy(() => import("./pages/Login"));
const Admin = React.lazy(() => import("./pages/Admin"));

export const authRoutes = [
  /* к страницам админа и корзины имеет доступ
    только авторизованный пользователь */
  {
    path: ADMIN_ROUTE,
    component: <Admin />,
  },
  {
    path: CART_ROUTE,
    component: <Cart />,
  },
];

export const publicRoutes = [
  /* маршруты, доступные всем пользователям */
  {
    path: HOME_ROUTE,
    component: <Home />,
  },
  {
    path: ITEM_ROUTE + "/:id",
    component: <ItemPage />,
  },
  {
    path: CATEGORY_ROUTE + "/:id",
    component: <CategoryPage />,
  },
  {
    path: LOGIN_ROUTE,
    component: <Login />,
  },
  {
    path: REGISTRATION_ROUTE,
    component: <Registration />,
  },
];
