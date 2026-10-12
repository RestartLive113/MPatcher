MPatcher — установщик Linux 0.1.2 Alpha
Для Arch Linux; графическое окно RU/EN/JP и командная строка.

1. Игра должна быть уже установлена. Поддерживается Windows-версия MachineCraft
   (Steam app 397100, McnCraft.exe), запускаемая через Steam/Proton или Wine.
   Сам установщик не требует Wine, .NET, root и не меняет Wine/Proton prefix.
   Нативная Linux-сборка игры с ELF/.so этим пакетом не поддерживается.
2. Для окна на Arch установите зависимости:
     sudo pacman -Syu python tk
   Требуется Python 3.10 или новее. Для командной строки tk не нужен.
3. Распакуйте архив, закройте игру и MPatcherCrashWatchdog, затем выполните:
     sh MPatcherInstaller.sh --lang ru
   Установщик найдёт библиотеки Steam, включая Flatpak и дополнительные диски.
   Если копий несколько, выберите нужную папку. Для Wine укажите папку вручную.
   Выберите Stable или Alpha и нажмите «Установить / обновить».
   Запускайте установщик от обычного пользователя; sudo нужен лишь для pacman.

CLI, если окно недоступно:
  sh MPatcherInstaller.sh --list-games
  sh MPatcherInstaller.sh --install --game-dir "/путь/к/MachineCraft" --lang ru
  sh MPatcherInstaller.sh --install --channel alpha --game-dir "/путь/к/MachineCraft" --lang ru
  sh MPatcherInstaller.sh --check --game-dir "/путь/к/MachineCraft"
  sh MPatcherInstaller.sh --uninstall --game-dir "/путь/к/MachineCraft" --lang ru
  sh MPatcherInstaller.sh --recover --game-dir "/путь/к/MachineCraft" --lang ru

Установщик загружает официальный пакет MPatcher из RestartLive113/MPatcher
на GitHub, сверяет размер и SHA-256, читает ресурсы пакета как данные и ставит
оригинальные __Internal.dll и MPatcherCrashWatchdog.exe. Windows EXE установщика
не запускается. Без интернета или при ошибке проверки игровые файлы не меняются;
ошибка записывается в журнал. Встроенного офлайн-пакета и офлайн-подмены нет.

Сохраняются UserData, машины, настройки и существующие резервные копии. Прежний
DLL сохраняется в McnCraft_Data/MPatcherForkBackup. Совместимый Format=2-манифест
поддерживает переходы с Windows-установщика. При удалении восстанавливается DLL,
существовавший до установки; изменённый извне loader не удаляется. Изменённый
watchdog сохраняется. Служебная MPatcherFork пересоздаётся при установке, как
в Windows-версии; туда не следует класть личные файлы.

Если установка оборвалась, закройте игру и нажмите «Восстановить после сбоя»
или выполните --recover для той же папки. Не удаляйте транзакцию вручную:
McnCraft_Data/.MPatcherLinux.transaction содержит файлы для отката.

Журнал: <папка игры>/logs/MPatcherInstallerLinux.log.
Ошибки до выбора игры: $XDG_STATE_HOME/MPatcher/logs/MPatcherInstallerLinux.log
(по умолчанию ~/.local/state/MPatcher/logs/MPatcherInstallerLinux.log).
Технические записи показаны и в окне установщика, без сообщений поверх игры.

Ограничения TEST:
- Это установщик для Windows-версии игры под Linux, а не перенос движка игры.
- Поддерживаемый McnCraft.exe закреплён по SHA-256; неизвестная версия отклоняется.
- Windows-служба Zapret/WinDivert не устанавливается. Не включайте Zapret - Legacy
  в MPatcher на Linux; Linux-адаптация этой службы сюда не входит.
- Обновляйте MPatcher этим Linux-установщиком. Встроенная кнопка обновления в игре
  запускает Windows-пакет; её Linux-совместимость не подтверждена.
- При обнаружении любой запущенной McnCraft.exe/MPatcherCrashWatchdog.exe операция
  отклоняется. Установщик сам игру не закрывает и не запускает.
- Файловые проверки выполнены на Windows. Запуск окна в Arch, загрузка MPatcher
  в Proton/Wine, watchdog и два клиента Unity Legacy ещё требуют живого теста.
- NTFS, сетевые FS, Flatpak-права и аварийное отключение питания не проверены.

Источники зависимостей/формата:
https://archlinux.org/packages/extra/x86_64/tk/
https://docs.python.org/3/library/tkinter.html
https://github.com/ValveSoftware/Proton
https://github.com/dotnet/runtime/blob/main/src/libraries/System.Reflection.Metadata/specs/PEReader.md

Исправление wrong DLL в 0.1.1:
Если сообщение появилось после Linux-установщика 0.1.0, закройте игру и
повторите установку из этого нового архива, выбрав тот же канал (например, Alpha):
  sh MPatcherInstaller.sh --install --channel alpha --game-dir "/путь/к/MachineCraft" --lang en
Текущая резервная копия с ошибочным именем __Internal-<SHA>.dll переименуется
в __Internal.original.<SHA>.dll с проверкой манифеста и SHA-256. Исторические
копии, подтверждённые манифестом или записями установщика и SHA-256, выносятся
в <game>/MPatcherBackupHistory, вне McnCraft_Data. Это сохраняет возможность
удаления и восстановления прежнего DLL. Ошибка откатывает переносы и имена.
Неизвестные или изменённые DLL не переносятся автоматически. Если сообщение
останется, нужен свежий logs/MPatcher_*.log со строками SCRIPT-DLL-COMPAT,
MPATCHER_REJECT и RESULT wrongDll. Рабочие DLL Mono/Managed вручную не удаляйте.
После успешной операции полностью перезапустите игру. Исправление проверено
на файловом стенде и исходной C#-политике MPatcher, но не в живом Arch/Proton.

0.1.2: внешний модуль Scale
Новая Alpha устанавливает MPatcher.ScaleRuntime.dll и
MPatcher.ScaleRuntime.manifest.ini в McnCraft_Data/MPatcher/Modules. До изменения
игры проверяются API, имя managed-сборки, версия и SHA-256 модуля. Загрузчик,
пара модуль/манифест и состояние установки входят в общую восстанавливаемую
транзакцию. --check показывает отсутствующие или изменённые компоненты.
Старые пакеты Stable/Alpha без модуля поддерживаются; переход на них удаляет
только принадлежащую установщику неизменённую пару. Другие модули сохраняются.
Неизвестный файл на пути этой пары или изменённый установленный файл останавливает
операцию до записи. Старый ручной монтаж принимается только при записанных
API1/хэше DLL и подтверждённом sidecar. Удаление также требует совпадения хэшей.
Живая проверка Arch/Proton/Wine этой версии ещё не выполнена.
