# ABP Test assignment
Опис
Ми працюємо над новим дизайном і хочемо протестувати гіпотезу, використовуючи АБ -
тести. Для цього нам потрібна система, що є простою REST API, що складається з 2 ендпоітнів.
API та розподіл
Web-додаток (Клієнт) при запиті до вашого API генерує деякий унікальний ID клієнта, який
зберігається між сесіями, та запитує експеримент, додаючи GET параметр device-token. У відповідь
сервер дає експеримент.
Для кожного експерименту клієнт отримує:
• Ключ: назва експерименту (X-name). Передбачається, що в клієнті є код, який змінюватиме
якусь поведінку залежно від значення цього ключа
• Значення: рядок, одна з можливих опцій (див. нижче)
Важливо, щоб девайс попадав в одну групу і завжди залишався в ній на основі device-token.
 Query:
 Type: GET
 Parameters: "device-token"
 Return:
 {key: "X-name", value: "string"}
Експерименти
1. У нас є гіпотеза, що колір кнопки «купити» впливає на конверсію на покупку
 Ключ: button_color
 Опції:
 #FF0000 → 33.3%
 #00FF00 → 33.3%
 #0000FF → 33.3%
Так після 600 запитів до API з різними DeviceToken кожен колір повинні отримати по +-200
девайсів.
2. У нас є гіпотеза, що зміна вартості покупки в додатку може вплинути на наш маржинальний
прибуток. Але щоб не втрачати гроші у разі невдалого експерименту, 75% користувачів будуть
отримувати стару ціну і лише на малій частині аудиторії ми протестуємо зміну:
Ключ: price
 Опції:
 10 → 75%
 20 → 10%
 50 → 5%
 5 → 10%
Приклад 1:
Запит клієнта: GET: https://yourdomain.com/experiment/button-color?device-token=randomstring1
JSON відповідь сервера: {key: "button_color", value: "#FF0000”}
Приклад 2 (другий запит того самого клієнта):
Запит клієнта: GET: https://yourdomain.com/experiment/button-color?device-token=randomstring1
JSON відповідь сервера: {key: "button_color", value: "#FF0000"}
Приклад 3:
Запит клієнта: GET: https://yourdomain.com/experiment/button-color?device-token=randomstring2
JSON відповідь сервера: {key: "button_color", value: "#00FF00"}
Вимоги та обмеження
1. Якщо девайс одного разу отримав значення, то він завжди отримуватиме лише його
2. Експеримент проводиться тільки для нових девайсів: якщо експеримент створений після
першого запиту від девайсу, то девайс не повинен нічого знати про цей експеримент

# Завдання:
- [ ] Спроектуйте, опишіть та реалізуйте API. Воно має працювати через swagger чи Postman
- [ ] Створіть сторінку для статистики (на вибір):
a. проста таблиця зі списком експериментів, загальна кількість девайсів, що беруть
участь в експерименті та їх розподіл між опціями
b. Статистика у форматі JSON зі списком експериментів, загальна кількість девайсів,
що беруть участь в експерименті та їх розподіл між опціями
- [ ] Використовуйте MS SQL базу даних для зберігання інформації про експерименти та їх
результати
- [ ] Використовуйте прямі запити або процедури, що зберігаються для CRUD операцій з БД.
- [ ] Надайте структуру БД разом із результатом вашої роботи
- [X] Ваше рішення має бути розміщене на GitHub у відкритому репозиторії.
- [ ] Можна використовувати будь-які технології та бібліотеки в рамках .NET

## Плюсом буде:
- [ ] Наявність тестів (UNIT)
- [X] Заповнений GIT README
- [ ] Обробка винятків (try|catch)
- [ ] Коментарі в коді
- [ ] Оптимізація (Обґрунтування проведеної оптимізації у вигляді коментарів у коді)