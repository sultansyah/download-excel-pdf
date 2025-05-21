Contoh proses download pdf dan excel di asp net core


<!-- ddl -->
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Email NVARCHAR(100),
    JoinDate DATE
);

<!-- dml -->
INSERT INTO Customers (Name, Email, JoinDate) VALUES
('Budi', 'budi@example.com', '2023-01-10'),
('Siti', 'siti@example.com', '2023-02-12'),
('Rudi', 'rudi@example.com', '2023-03-05');


<!-- stored procedure to get all data from table customers -->
CREATE PROCEDURE GetCustomerList
AS
BEGIN
    WITH CTE_Customers AS (
        SELECT 
            CustomerID,
            Name,
            Email,
            JoinDate
        FROM Customers
    )
    SELECT * FROM CTE_Customers;
END
