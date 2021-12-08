import Admin from "./pages/Admin";
import Cart from "./pages/Cart";
import Home from "./pages/Home";
import ItemPage from "./pages/ItemPage";
import CategoryPage from "./pages/CategoryPage";
import {
  ADMIN_ROUTE,
  CART_ROUTE,
  HOME_ROUTE,
  ITEM_ROUTE,
  LOGIN_ROUTE,
  REGISTRATION_ROUTE,
  CATEGORY_ROUTE,
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
    path: CATEGORY_ROUTE + "/:name",
    component: <CategoryPage />,
  },
  {
    path: LOGIN_ROUTE,
    component: <CategoryPage />,
  },
  {
    path: REGISTRATION_ROUTE,
    component: <CategoryPage />,
  },
];
