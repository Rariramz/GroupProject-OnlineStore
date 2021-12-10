export const errors = new Map();

/* POST api/categories/{int id} */
errors.set(
  440,
  "рут категория (которая ссылаться сама на себя) уже существует"
);
errors.set(441, "не указана родительская категория");
errors.set(
  442,
  "категория не может одновременно содержать itemы и подкатегории"
);
