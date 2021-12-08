import React, { useContext } from "react";
import { Routes, Route } from "react-router-dom";
import PageNotFound from "../pages/PageNotFound";
import { authRoutes, publicRoutes } from "../routes";
import { Context } from "../index";

const AppRouter = () => {
  const { user } = useContext(Context);

  return (
    <Routes>
      {user.isAuth &&
        authRoutes.map(({ path, component }) => (
          <Route key={path} path={path} element={component} exact />
        ))}
      {publicRoutes.map(({ path, component }) => (
        <Route key={path} path={path} element={component} exact />
      ))}
      <Route path="*" element={<PageNotFound />} />
    </Routes>
  );
};

export default AppRouter;
