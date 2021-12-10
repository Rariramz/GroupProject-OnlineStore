const errors = new Map();
/* post user - вход(логин) */
errors.set(440, "Поле Почта должно быть не пустым");
errors.set(441, "Недопустимые символы в Почте");
errors.set(442, "Почта не зарегистрирована");
errors.set(443, "Почта не подтверждена");
errors.set(444, "Поле Пароль должно быть не пустым");
errors.set(445, "Пароль неверный");

export default errors;
