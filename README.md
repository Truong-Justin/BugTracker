# Bug Tracker App


## Project Description
This full-stack web application was made using C#, SQLite, HTML/CSS and ASP.NET Core Razor Pages. The user is able to create, view, update/edit or delete bugs from a SQLite database. ADO.NET was used to establish a connection between the application and the data source, the SQLite database, in order to better understand what an ORM frameworkd does and what is happening in the background. The Bug class in the Models folder is the data model used to represent the data attributes we will be working with when working with the Bug entities. The BugDataAccess class, from the same folder, holds the business logic that is used to manipulate the database data. The primary focus and goal of this project was to build and deploy a full-stack ASP.NET web application, with a focus on UI and UX design. 


## How to run the web application
1. The application is deployed and hosted on Microsoft Azure and can be accessed by visiting [https://bugtrackerapp.azurewebsites.net/](https://bugtrackerapp.azurewebsites.net/).


## How to use the project
1. Visit the application on any browser

2. To add a new bug record, click the "+ Add" button
    - Fill in the fields for the new bug record
    - Click the "Add Bug" button to add the new bug record
    
3. To View a bug record, click the "View" button 

4. To Edit/Update a bug record, click the "View" button of the bug record to edit/update
    - Fill in the new fields to update the bug record with
    - Click the "Save" button to update the selected record 
    
5. To Delete a bug record, click the "View" button of the bug record to delete
    - Click the "Delete" button to delete the selected record


## Technology used
1. .NET 7.0 & C#
2. HTML/CSS, Bootstrap 5 & ASP.NET Core Razor Pages for the UI
3. Microsoft Azure App Services to host and deploy the application
4. SQLite database 
