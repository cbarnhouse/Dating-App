# Dating-App
This is an app I've made to learn the newer versions of Angular and .Net (Angular 18, .Net 8). It follows a great course made by Neil Cummings called "A practical example of how to build an application with ASP.NET Core API and Angular from start to finish." I have no affiliation, I just think it's a great course and want to give due credit. 

# Database
The app uses a local SQLlite database with Entity Framework as the ORM. The repository pattern is used.

# Custom Middleware and Interceptors
A custom .Net middleware is made to catch exceptions and throw a custom ApiException type to give more control over any custom logic desired based on environment type (dev vs prod) and custom messages. A custon interceptor is used on the frontend to append the jwt to a request's header if the user is already logged in and their credentials stored in local storage. 

# Authentication
The app uses a custom authentication implementation with manual password hashing and salting, as well as JWT implementation.

# Bootstrap UI 
Bootstrap is used to build the UI. A custom theme from Bootswatch is used as well. The underlying column-based UI framework of Bootstrap is a quick and easy way to get a decent UI up and running. 
