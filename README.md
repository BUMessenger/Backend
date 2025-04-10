# BUMessenger

## Настройка

Переменные для настройки сервера расположены в файле **settings.conf**

## Сборка

Для запуска требуется установка docker

---

- Ubuntu:

```
sudo apt-get install docker
```

- ArchLinux

```
sudo pacman -S docker
```

---

Сборка и запуск сервера осуществляется запуском команды

```
sudo docker-compose up
```

## Запуск

Панель управления базой данных PgAdmin доступна после запуска
сервера по адресу http://server_address:1234/pgadmin
