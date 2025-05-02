CREATE DATABASE LibraryManagement;
GO

USE LibraryManagement;
GO

CREATE TABLE Books (
    BookId INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(100) NOT NULL,
    Author NVARCHAR(100) NOT NULL,
    ISBN NVARCHAR(20) UNIQUE NOT NULL,
    PublicationYear INT NOT NULL
);

CREATE TABLE Members (
    MemberId INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    PhoneNumber NVARCHAR(20),
    RegistrationDate DATE DEFAULT GETDATE()
);

CREATE TABLE Loans (
    LoanId INT PRIMARY KEY IDENTITY(1,1),
    BookId INT NOT NULL,
    MemberId INT NOT NULL,
    BorrowDate DATE NOT NULL DEFAULT GETDATE(),
    DueDate DATE NOT NULL,
    ReturnDate DATE NULL,
    FOREIGN KEY (BookId) REFERENCES Books(BookId),
    FOREIGN KEY (MemberId) REFERENCES Members(MemberId)
);

INSERT INTO Books (Title, Author, ISBN, PublicationYear)
VALUES
    ('The Count of Monte Cristo', 'Alexandre Dumas', '9780140440026', 1844),
    ('War and Peace', 'Leo Tolstoy', '9780140067281', 1869),
    ('Crime and Punishment', 'Fyodor Dostoevsky', '9780140171460', 1866),
    ('The Brothers Karamazov', 'Fyodor Dostoevsky', '9780140440019', 1880),
    ('Ulysses', 'James Joyce', '9780140034773', 1922),
    ('The Sound and the Fury', 'William Faulkner', '9780140185910', 1929),
    ('One Hundred Years of Solitude', 'Gabriel García Márquez', '9780060883270', 1967),
    ('The Catcher in the Rye', 'J.D. Salinger', '9780316769488', 1951),
    ('The Adventures of Huckleberry Finn', 'Mark Twain', '9780099505671', 1885),
    ('Moby Dick', 'Herman Melville', '9780140091191', 1851),
    ('The Odyssey', 'Homer', '9780140275344', -750), -- Estimated publication year
    ('The Iliad', 'Homer', '9780140275360', -760), -- Estimated publication year
    ('Hamlet', 'William Shakespeare', '9780140706444', 1600),
    ('Romeo and Juliet', 'William Shakespeare', '9780140706428', 1597),
    ('A Midsummer Night''s Dream', 'William Shakespeare', '9780140706404', 1595),
    ('Macbeth', 'William Shakespeare', '9780140706460', 1606),
    ('Othello', 'William Shakespeare', '9780140706487', 1603),
    ('King Lear', 'William Shakespeare', '9780140706509', 1605),
    ('Don Quixote', 'Miguel de Cervantes', '9780140440001', 1605),
    ('The Divine Comedy', 'Dante Alighieri', '9780140440033', 1320),
    ('The Prince', 'Niccolò Machiavelli', '9780140440040', 1532),
    ('The Art of War', 'Sun Tzu', '9780140440057', 500), -- Estimated publication year
    ('The Metamorphosis', 'Franz Kafka', '9780140171484', 1915),
    ('The Stranger', 'Albert Camus', '9780307351435', 1942),
    ('The Plague', 'Albert Camus', '9780307351442', 1947),
    ('Siddhartha', 'Hermann Hesse', '9780140171491', 1922),
    ('The Alchemist', 'Paulo Coelho', '9780060928696', 1988),
    ('The Little Prince', 'Antoine de Saint-Exupéry', '9780140067526', 1943),
    ('The Picture of Dorian Gray', 'Oscar Wilde', '9780140440064', 1890),
    ('Frankenstein', 'Mary Shelley', '9780140171505', 1818),
    ('Dracula', 'Bram Stoker', '9780140440071', 1897),
    ('Wuthering Heights', 'Emily Brontë', '9780140440088', 1847),
    ('Jane Eyre', 'Charlotte Brontë', '9780140440096', 1847),
    ('Great Expectations', 'Charles Dickens', '9780140440102', 1861),
    ('Oliver Twist', 'Charles Dickens', '9780140440110', 1838),
    ('David Copperfield', 'Charles Dickens', '9780140440128', 1850),
    ('Alice''s Adventures in Wonderland', 'Lewis Carroll', '9780140440135', 1865),
    ('Through the Looking-Glass', 'Lewis Carroll', '9780140440142', 1871),
    ('The Adventures of Sherlock Holmes', 'Arthur Conan Doyle', '9780140440159', 1892),
    ('The Hound of the Baskervilles', 'Arthur Conan Doyle', '9780140440166', 1902),
    ('The Call of the Wild', 'Jack London', '9780140091177', 1903),
    ('White Fang', 'Jack London', '9780140091185', 1906),
    ('The Jungle', 'Upton Sinclair', '9780140171512', 1906),
    ('The Grapes of Wrath', 'John Steinbeck', '9780140171520', 1939),
    ('Of Mice and Men', 'John Steinbeck', '9780140171539', 1937);

