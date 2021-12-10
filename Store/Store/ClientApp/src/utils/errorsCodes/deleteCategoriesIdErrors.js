export const errors = new Map();

/* DELETE api/categories/{int id} */
errors.set(
  440,
  "невозможно удалить рут категорию (которая ссылается сама на себя))"
);
