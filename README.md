# AiSRIPInterface
test project for Avicom


### Данный проект выполнен по следующему техническому заданию:

Компания Ромашка и Партнёры (РиП) решила перевести свою базу клиентов в электронный вид. Клиенты РиП – это другие компании, с каждой из которых заключается договор. У компаний-клиентов есть пользователи, которых РиП обслуживает. Необходимо разработать настольное WPF-приложение, которое поможет компании достичь поставленной цели.

### Технические требования к системе:

    Приложение: WPF/MVVM
    База данных: MSSQL
    ORM: Entity Framework

* Есть пользователь (User) со свойствами Id, Name, Login, Password;
* Есть компания (Company) со свойствами Id, Name, ContractStatus;
* ContractStatus - статус договора на услуги с компанией, принимает значения: Ещё не заключен, Заключен, Расторгнут 
* В компании может быть несколько пользователей 
* Пользователей без компании не существует

### Требования к функционалу: Приложение должно позволять:

    [X] Создавать/Редактировать/Удалять компании
    [X] Создавать/Редактировать/Удалять пользователей
    [X] Отображать списки компаний и пользователей компании
    
____

### This project was completed according to the following terms of reference:

Romashka & Partners (R&P) has decided to convert its customer base to electronic format. R&P's clients are other companies that have a contract with each Of them. Client companies have users, which Rip serves. It is necessary to develop a desktop WPF application that will help the company achieve this goal.

### Technical requirements for the system:

    Application: WPF/MVVM
    Database: MSSQL
    ORM: Entity Framework

* **User** - ID, Name, Login, Password;
* **Company** - ID, Name, ContractStatus;
* **ContractStatus** - the status of the service agreement with the company, takes the values: Not yet concluded, Concluded, Terminated.
* A company can have multiple users.
* There are no users without a company.

### Functional requirements: The app must allow you to:

    [X] Create/Edit/Delete companies
    [X] Create/Edit/Delete users
    [X] To display a list of companies and users throughout the company

