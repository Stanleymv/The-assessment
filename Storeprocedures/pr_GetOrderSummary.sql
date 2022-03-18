USE NORTHWND
GO

IF EXISTS (SELECT * FROM sys.objects WHERE OBJECT_ID = OBJECT_ID(N'pr_GetOrderSummary')AND TYPE IN (N'P',N'PC'))
DROP PROCEDURE pr_GetOrderSummary
GO

CREATE PROCEDURE pr_GetOrderSummary
@StartDate DATETIME,
@EndDate  DATETIME,
@EmployeeID INT = NULL,
@CustomerID VARCHAR(15) = NULL

AS
BEGIN
SELECT CONCAT(E.TitleOfCourtesy,+' '+ + E.FirstName+'  '+ E.LastName) AS EmployeeFullName,
ShipName AS CompanyName,
C.CompanyName AS Customer_CompanyName,
Count(o.OrderID) AS NumberOfOrders,
OrderDate,
sum(Freight) AS TotalFreightCost,
count(od.ProductID) AS NumberOfDifferentProducts,
Sum(OD.UnitPrice) as TotalOrderValue
  FROM [NORTHWND].[dbo].[Orders] O
  INNER JOIN Employees E
  ON E.EmployeeID = O.EmployeeID
  INNER JOIN Customers C
  on  C.CustomerID = O.CustomerID
  INNER JOIN [Order Details] OD
  on OD.OrderID = O.OrderID
  WHERE OrderDate BETWEEN @StartDate AND @EndDate
  AND E.EmployeeID = @EmployeeID
  AND C.CustomerID = @CustomerID
  GROUP BY 
  E.TitleOfCourtesy,
  E.FirstName,
  E.LastName,
  OrderDate,
  ShipName,
  C.CompanyName 

  
  END
  GO

