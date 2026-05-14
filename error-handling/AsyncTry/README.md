# AsyncTry

Учебный проект по обработке ошибок в async-коде.

## Что делает
Выполняет асинхронный HTTP-запрос через HttpClient и обрабатывает возможные исключения.

## Что отрабатывалось
- `try/catch` вокруг `await`
- Ловля `HttpRequestException` (сетевые ошибки) и `TaskCanceledException` (таймаут)
- Свойство `ex.Message` для получения текста ошибки
- `client.Timeout = TimeSpan.FromSeconds(N)` — настройка таймаута HttpClient
- Поведение программы после поймнного исключения (продолжает работать)

## Стек
- .NET 10
- C#