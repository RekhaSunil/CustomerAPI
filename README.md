# CustomerAPI

The Customer API is created with .Net Core Version 3.1

Added Swagger in API

A datatable is created for the transactions for different customer for an interval of 3 months

In constructor, the methods for adding datatable and inserting values are given.

There are mainly 3 get methods used.

   Each will return a DataTable, Status and ErrorDescription.

1.  RetDataTable GetTransactions()
  
    This will display all the transactions with fields TransactionID, PurchaseDate, CustomerID and Price

2.  RetDataTable GetRewards()

    This will calculate the reward points for each transactions with fields TransactionID, PurchaseDate, CustomerID, Price and Rewards

3.  RetDataTable GetFinalRewards(string CustomerID)

    This will calculate the monthwise rewards for a particular customer and total rewards with fields Month, CustomerID, Rewards and TotalRewards
