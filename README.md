# _Dillon's Salon Management Program_

#### C# and SQL Exercise for Epicodus, 3.4.2018

#### By _**Dillon Titcomb**_

## Description

_This webpage presents the owner of a salon with a list of their stylists and the stylists' clients. The owner can add new stylists to their list of employees and also assign clients to stylists._

## User Stories
* _As a salon employee, I need to be able to see a list of all our stylists._
* _As an employee, I need to be able to select a stylist, see their details, and see a list of all clients that belong to that stylist._
* _As an employee, I need to add new stylists to our system when they are hired._
* _As an employee, I need to be able to add new clients to a specific stylist. I should not be able to add a client if no stylists have been added._
* _As an employee, I need to be able to delete stylists (all and single)._
* _As an employee, I need to be able to delete clients (all and single)._
* _As an employee, I need to be able to view clients (all and single)._
* _As an employee, I need to be able to edit JUST the name of a stylist. (You can choose to allow employees to edit additional properties but it is not required.)_
* _As an employee, I need to be able to edit ALL of the information for a client._
* _As an employee, I need to be able to add a specialty and view all specialties that have been added._
* _As an employee, I need to be able to add a specialty to a stylist._
* _As an employee, I need to be able to click on a specialty and see all of the stylists that have that specialty._
* _As an employee, I need to see the stylist's specialties on the stylist's details page._
* _As an employee, I need to be able to add a stylist to a specialty._

## Specifications

* _Program displays list of employees/stylists:_
	* _Example input: 'View Stylists'_
	* _Example output: 'John, Jane, Jim, etc.'_

* _Program displays list of clients with their associated stylist assignments:_
	* _Example input: 'View Clients'_
	* _Example output: 'Joseph assigned to John, Tom assigned to Jane, Alex assigned to Jim'_

* _Program displays information pages for individual stylists:_
	* _Example input: 'John'_
	* _Example output: 'Name: John, Clients: Joseph, Tom, Alex'_

* _Program allows user to add new stylists to stylists list:_
	* _Example input: 'Add Sally'_
	* _Example output: 'Stylist List: John, Jane, Jim, Sally'_

* _Program allows user to add and assign clients to individual stylists:_
	* _Example input: 'Assign to Sally'_
	* _Example output: 'Sally's Client List: Beverly, Timothy, Sam'_

* _Program does not allow user to add client without attached stylist:_
	* _Example input: 'Add Client'_
	* _Example output: 'Cannot add client without assigning to stylist'_

## Setup/Installation Requirements

* _Clone this repository_
* _Create SQL databases by using the following commands:_
	* _CREATE DATABASE firstname_lastname;_
	* _CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255));_
	* _CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255));_
	* _CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255));_
	* _CREATE TABLE stylists_specialists (id serial PRIMARY KEY, stylist_id int(11), specialist_id int(11));_
	* _CREATE TABLE stylists_clients (id serial PRIMARY KEY, stylist_id int(11), client_id int(11));_
*	_Create test database named 'firstname_lastname_test' by navigating to phpMyAdmin and clicking on your firstname_lastname database_
* _On a Mac, run the following commands in terminal:_
* _dotnet add package MySqlConnector_
* _dotnet restore_
* _dotnet build_
* _dotnet run_
* _Navigate to "localhost:5000" in browser_
* _Add stylists, clients, and specialties to your heart's desire._

## Known Bugs

_No known bugs._

## Support and contact details

_Please contact me at dillontitcomb@gmail.com with inquiries._

## Technologies Used

* _CSHTML_
* _CSS_
* _Bootstrap_
* _C#_
* _SQL_

## Link to page

https://github.com/dillontitcomb/hair-salon

### License

*The software is licensed under the MIT license.*

Copyright (c) 2018 **_Dillon Titcomb_**
