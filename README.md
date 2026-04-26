# UI Dialog System

Чистая, расширяемая система диалогов для **Unity** с разделением ответственности, внедрением зависимостей (DI) и `async/await`.

---

## Что внутри

| Компонент | Ответственность |
| :--- | :--- |
| **View** | Рисует UI, ловит клики, публикует события. Никакой логики. |
| **Presenter** | Принимает решения: можно ли нажать кнопку, хватает ли валюты, что вернуть в результате. |
| **Model** | Хранит текущее состояние диалога (выбранная сложность, доступность кнопок). |
| **Dialog** | Склеивает View и Presenter, управляет жизненным циклом. |
| **Services** | Внешние зависимости (кошелёк, реклама, запуск уровней). Внедряются через DI. |

---

## Технологический стек
* **MVP** — классическое разделение Model-View-Presenter.
* **VContainer** — лёгкий DI-контейнер для Unity без рефлексии в рантайме.
* **UniTask** — zero-allocation async/await вместо корутин.
* **TextMeshPro** — для текста (встроен в Unity).

---

## Структура проекта

```text
Assets/Scripts/
├── Core/UI/               # Базовые классы: IDialog, BaseDialog, UIRoot, UIService, DialogFactory
├── Core/Bootstrap/        # Корневой DI-контейнер: GameLifetimeScope
├── Services/Wallet/       # IWalletService, FakeWalletService
├── Services/Ads/          # IAdsService, FakeAdsService
├── Services/Puzzle/       # IPuzzleStartService, FakePuzzleStartService
├── Features/StartPuzzleDialog/ # Конкретный диалог: Context, Result, Model, View, Presenter
└── Demo/                  # DemoSceneController — пример открытия диалога из сцены
```

## Ограничения и последующие шаги

* **Анимация**: Методы BaseDialog.ShowAsync/HideAsync выполняются мгновенно. Для реализации появляющихся или исчезающих анимаций (fade/slide), необходимо переписать данные методы с помощью DOTween или Animator.
* **Стек диалогов**: Активная версия позволяет показывать только одно окно одновременно. Для возможности показывать диалоги друг поверх друга, следует реализовать очередь (queue) в UIService.
* **Отмена действия** (Cancellation): Данный компонент не принимает токен отмены (CancellationToken) в Presenter. Необходимо дополнить параметр токена в метод Bind для очистки ресурсов при закрытии окна.
* **Реальные сервисы**: Classes FakeWalletService, FakeAdsService, FakePuzzleStartService являются stubами и требуется их заменить на реальные сервисы, связь с сервером и игры.
* **Обработка ошибок**: При получении ответа от диалогов нет событий

![Image alt](https://github.com/Ficksik/PuzzleDemo/blob/main/Screenshot1.png)
![Image alt](https://github.com/Ficksik/PuzzleDemo/blob/main/Screenshot2.png)