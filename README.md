# Assignment: Library Management System

## Objective
The goal of this assignment is to design and implement a basic Library Management System using:
1. SQL Server for database design and management.
2. Console application that interacts with the database.

- Design a relational database with appropriate relationships.
- Perform basic CRUD (Create, Read, Update, Delete) operations.
- Integrate a C# application with a SQL Server database.

---

## Requirements

### Part 1: Database Design
1. **Database Structure**  
   Create a database named `LibraryManagement` with the following tables:
   - **Books**: Store information about books in the library. (Assume we have unlimited books in storage)
   - **Members**: Store details of library members.
   - **Loans**: Track which member has borrowed which book and when.

2. **Relationships**  
   - `Loans` table should reference `Books` and `Members` through foreign keys.

3. **Constraints**  
   - Primary keys for each table.
   - Appropriate data types and constraints for all fields (e.g., `NOT NULL` for required fields).

4. **Initial Data**  
   Populate the database with dummy data manually

---

### Part 2: Console Application
1. **Functionality**  
   Build a console application in C# that performs the following operations:
   - **Add New Book**: Add a new book to the library.
   - **Register Member**: Register a new library member.
   - **Borrow Book**: Record a book being borrowed by a member.  
   - **Return Book**:
   - **View Borrowed Books**: Display all currently borrowed books along with member details.
   - **Search Books**: Search for books by title or author.

2. **Dynamic Console Menu**  
   Implement a menu-based system in the application for user interaction.  
   Example:

Library Management System
1. Add New Book
2. Register Member
3. Borrow Book
4. Return Book
5. View Borrowed Books
6. Search Books by author or title
