import Admin from "./pages/Admin";
import Authorization from "./pages/Authorization";
import Cart from "./pages/Cart";
import Home from "./pages/Home";
import ItemPage from "./pages/ItemPage";
import {
  ADMIN_ROUTE,
  CART_ROUTE,
  HOME_ROUTE,
  ITEM_ROUTE,
  LOGIN_ROUTE,
  REGISTRATION_ROUTE,
} from "./utils/consts";

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
    path: LOGIN_ROUTE,
    component: <Authorization />,
  },
  {
    path: REGISTRATION_ROUTE,
    component: <Authorization />,
  },
];
