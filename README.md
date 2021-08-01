# ShopBidze
ConsumerProducerApplication

2.	Implementation

Following are the implementation details:
1.	Add a new item to the inventory (The item should require a name, description, and price as basic fileds, think of additional information that would be useful):
The following screenshot show how to add new product to Inventory, fill up body in json format and send the post request.

2.	Modify an item in the inventory.
To modify the existing product send product id in query string and product details in body as shown in below, if product doesn’t exists in inventory then you will get Notfound result:
 

3.	Delete an item from the inventory.
To delete a product from inventory send product id in query string as shown below, if product doesn’t exists in inventory then you will get Notfound result.
 
4.	Lists the items in the inventory.
There two ways to get Products from inventory:
1.	Get all product from inventory:
To get all product from inventory send the get request as shown below:
 

2.	Get specific product from inventory:
To get specific product from inventory send the Get request and send the product id in query string as shown below, if product doesn’t exists in inventory then you will get Notfound result:

 

3. Nunit Testing and Code Coverage:
The application is created with Nunit implementation and following are the test case results.
  

Note: I’m using community version of visual studio so I can’t do the code coverage , I covered all corner of code and I assume the code coverage should be above 95 %.
4.	Deployment: 
1.	 Database deployment:
The shopBridge product is created with code first approach and it is configured with Sql Server just change the connection string in appsettings.json and call update-database from package manager console.
1.	Update the Dbservername and password as per your sql server in appsettings.json.

 

2.	Update-database from package manager console as shown below:

 

 Now we can run the application and test the APIS in any Api tester like postman etc.
