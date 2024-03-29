# Bug Tracker App
---

## Project Description
This responsive full-stack web application was made using C#, Microsoft SQL Server, HTML/CSS and ASP.NET Core Razor Pages. The user is able to create, view, update/edit or delete bugs from a Microsoft SQL Server database. ADO.NET was used to establish a connection between the application and the data source, the Microsoft SQL Server database, in order to better understand what an ORM framework does and what is happening in the background. The Bug class in the Models folder inherits from the Entity abstract class, and is the data model used to represent the data attributes we will be working with when working with the Bug entities. The BugDataAccess class, which is inherited from the EntityDataAccess abstract class, holds the database access logic that is used to manipulate/read from the database. The initial focus and goal of this project was to build and deploy a full-stack ASP.NET web application, with a focus on UI and UX design and <ins>includes a **UI for both desktop and mobile views**</ins>. 

The direction of this application will make it more of a `Project Management Application` that will <ins>include</ins>: `Project Managers`, `Projects that may or may not have bugs`, and `Employees that will be working on these projects`. The planned changes/additions are documented below within the ```Planned additions section``` of this document.


---
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


---
## Technology used
1. .NET 7.0 & C#
2. HTML/CSS, Bootstrap 5 & ASP.NET Core Razor Pages for the UI
3. Microsoft Azure App Services to host and deploy the application
4. Microsoft SQL Server and Azure SQL to implement and host the database


---
## Planned additions
1. I am currently in the process of expanding the number of tables/entities to include tables for: 
    - `Project Managers that would lead the project`
    - `Projects that the bugs would belong to`
    - `Employees that would be working on projects`

   These entities and their associated data access classes will be inherited from abstract classes to support code-reusability and method overloading. This design choice will also    be my attempt to include high extensibility, high cohesion, and low coupling. I want to implement these design features so that when new additions/features are added, major refactoring of the codebase or changes to the underlying architecture won't be necessary.  

2. As this project is also an application with a focus on UI/UX design, I would like to change the way records are shown to the user when a report is outputted. The information of each database record is placed within a container that is shaped like a long horizontal row. Instead of this design, I would like to implement an <ins>interface that will have three vertical sections</ins>:
    - `Projects that aren't being worked on yet || Projects that are currently being worked on || Projects that are finished.` 
    - `Bugs that aren't being worked on yet || Bugs that are currently being worked on || Bugs that have been finished.`
    
Finally, I would also like to place the information of each record within a `square container like an html card` to create a more visually pleasing interface and have each record be stacked on top of each other within the vertical sections they belong to.

