# EvolentTest
This is the Test API Used for Contact Information

Below is the Swagger Endpoints That we have created to Manage Contact Information
![image](https://user-images.githubusercontent.com/81011299/111870735-6d88e380-89ac-11eb-8c66-315059bcbd9a.png)



Below are the Folder Structure we have 

1) ContactAPI 
Main Project that conatins controller and Filter Section of API.
ContactController :- All those API Endpoints used to manage contact information e.g Add/Update/Detele
Healthcontroller :- Will be used to check Health status of Service.
ErrorController :- will be used to handel Error at Global level.

Filter folder contains the HttpException filter those will be used to handle Error in the Application.

2) ContactAPI.Infrastructure

Common:
these project contains all the common Stuffs that will be used to Execute 
contract and interface are we used to Maintain contact information.

3) ContactAPI.Unittest
This project is used to get all the Unit test Cases using Moq anf Nunit Framework.

Note:

To run the Application please folloe below steps

1) Please copy all the DB schema from App Data folder and create all the Tables and procedure.
2) after that please change the connection string in Appconfig.josn.
3) and Try to run the application
