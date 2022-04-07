This is a simple web application that allows you to keep track of inventory in various places. You can create storage levels and sub-storage levels, create items and
assign items to locations. Along with storage levels you can add a picture of that item.

Datasource is Postgres database. You have to provide server address, port, username and password in Storage-Inventory/Storage Inventory/WebApp/appsettings.json under
DefaultConnection. Next under DataInitialization set all values to true for first time run.

By default there is an administrator account with username 'admin@kkilum.com' and password 'Kala.maja1'. You can change the values in 
Storage-Inventory/Storage Inventory/WebApp/Helpers/AppDataHelper file. Only administrator has access to the statistics data. 

API paths:
User must be logged in and user can only see user's data

GET hostname/api/items - shows all of users items
POST hostname/api/items - add item
GET hostname/api/items/id - shows single item
PUT hostname/api/items/id - modify existing item
DELETE hostname/api/items/id - delete existing item
GET hostname/api/items/keyword - finds items that match keyword in any field

GET hostname/api/image - shows all of users items
POST hostname/api/image - add item
GET hostname/api/image/id - shows single item
PUT hostname/api/image/id - modify existing item
DELETE hostname/api/image/id - delete existing item

GET hostname/api/storagelevel - shows all of users items
POST hostname/api/storagelevel - add item
GET hostname/api/storagelevel/id - shows single item
PUT hostname/api/storagelevel/id - modify existing item
DELETE hostname/api/storagelevel/id - delete existing item

POST hostname/api/account/register - create account
POST hostname/api/account/login - login

GET localhost/api/statistics - get data about existing users, visible only to the admin
