Book-Order (Backend)
============

A complete system for managing an online library, integrating several key features for customers and system security.
The code is fully written in C#.

## Get started

You can download the .zip package directly, then unzip it and start the project by double-clicking on FinalProject.sln (need visual studio installed).

## Code explanation

A simple explanation to make it clear what the code does:
* Customers: Customers can register and log in using JSON Web Tokens (JWT). This authentication method ensures that only authorized users can access the system’s features.
* Books: Customers can view a catalog of books, search by title or author, and add new books. This allows for a dynamic and up-to-date management of the bookstore’s catalog.
* Order: Users can place orders and view their order history. This feature enables customers to keep track of their purchases and manage their orders efficiently.
* Security: I have implemented API protection with JWT authentication, ensuring that all communications between the client and server are secure and protected from unauthorized access.
