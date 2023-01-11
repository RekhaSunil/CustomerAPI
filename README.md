# CustomerAPI

The Customer API is created with .Net Core Version 3.1

Added Swagger in API. Using swagger methods, we can get the result of each methods clearly and easily.

Added Authorization with UserName and Password in WebAPI.  We need to authorize the methods by giving Username as 'admin' and password as 'admin'

The result of the methods cannot be displaced without authorization.

In the API, a datatable object is created with fields CustomerID, Transaction ID, Price, Purchase date.

It will consists of different set of records with different Transaction IDs for different customers for an interval of 3 months.

In the constructor of web API class, the methods for adding columns to the datatable and inserting values are given.

While running the Web API, it will trigger the constructor and the datatables with different records for each customers are created.

There are mainly 3 get methods used in Web API for different operations.

The return type of each methods are RetDataTable which consists of a DataTable, Status value and ErrorDescription.

1.  RetDataTable GetTransactions()
    
    In this method no need to pass any parameter.
    
    While calling this method, it will list all the transactions for different customers.
  
    This will display all the transactions with fields TransactionID, PurchaseDate, CustomerID and Price
    
    If any error occurs during the process, it will diplay the ErrorDescription  and the Status=false. 
    
    Otherwise it will diplay the list of datas with Status=true and ErrorDescription=""

2.  RetDataTable GetRewards()

    No need to pass any parameters for this method also.

    This will calculate the reward points for each transactions with fields TransactionID, PurchaseDate, CustomerID, Price and Rewards.
    
    The rewards will be calculated with the conditions given in the question.
    
    If any error occurs during the process, it will diplay the ErrorDescription and the Status=false and the Datatable will be null. 
    
    Otherwise it will diplay the list of datas with Status=true and ErrorDescription=""

3.  RetDataTable GetFinalRewards(string CustomerID)

    In this method we need to pass a string variable CustoerID for calculating the rewards for a particular customer.
    
    If no records exists in the datatable for the given CustomerID, it will display "No customer records exist".

    This will calculate the monthwise rewards for a particular customer and total rewards with fields Month, CustomerID, Rewards and TotalRewards.
    
    If any error occurs during the process, it will diplay the ErrorDescription and the Status=false and the Datatable will be null. 
    
    Otherwise it will diplay the list of datas with Status=true and ErrorDescription="". 
    
    The method will calculate the rewards for a particular month and then calculate the grand total for the selected Customer.
    
    
