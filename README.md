# HamiDiabet
WebSite to provide services to diabetic patients (Website & API Project With DB & JWT Authentication)

## Overview

This repository(HamiDiabet) includes two projects:

_HamiDiabet Website project as a client in root of repository(hamidiabetSite Folder)
    The project “HamiDiabet Website” is a responsive website made with Asp.Net MVC Technology using C#, Bootstrap, JQuery, Ajax, HTML, CSS, JavaScript, SQL, and EntityFramework.
    User registration and login using JWT.

_Rest Api project in root of repository(hamidiabetWebApi Folder)
    hamidiabetWebApi is a Rest Api shows how to implement JSON Web Token authentication with ASP.NET MVC 5, Web Api 2,.Net Framework 4.5

![alt text](https://github.com/soheilasadeghian/HamiDiabet/blob/main/image/rest.png)

## Steps:
1. Restore DB in SQL Server from the DB file in root of repository <br/>
2. Open hamidiabetWebApi Solution in Visual Studio and build the project <br/>
3. Execute (F5) to run. Browser will throw error page which is fine as this is only WEB-API implementation <br/>
4. Open hamidiabetSite Solution in Visual Studio and build the project <br/>
3. Execute (F5) to run. Browser will show Homepage of website (the picture of homepage is end of this readme)<br/>
4. you can Register and Login to website and see the userTbl Table in database fields how to jwt authentication work



## About Implement JWT Authentication:
JWT authentication is a self-contained authentication protocol where the token is a base64 representation of a object which contains 3 parts seperated by a period:
- Header
- Payload (Claims)
    set claims for user:setClaimsIdentity function in [`code`](https://github.com/soheilasadeghian/HamiDiabet/blob/main/hamidiabetWebApi/testwebapi/SimpleAuthorizationServerProvider.cs)
- Signature

The API has 1 controller:<br/>
AuthController Contains the SignUp, and SignOut.<br>
SignIn is here [`signIn`](https://github.com/soheilasadeghian/HamiDiabet/blob/main/hamidiabetWebApi/testwebapi/App_Start/Startup.cs)<br>
i use this NuGet: [`Microsoft.Owin.Security.Jwt`](https://www.nuget.org/packages/Microsoft.Owin.Security.Jwt)

Hashing:<br>
For hashing we can implement it using various algorithms.This project implements hashing using SHA256.<br>
function of set Sha256Hash is in: [`here`](https://github.com/soheilasadeghian/HamiDiabet/blob/main/hamidiabetWebApi/testwebapi/SimpleRefreshTokenProvider.cs)


## The Auth server exposes the following endpoints:
•	http://host/user/SignUp to register the user
•	http://host/user/signIn to login the user and generate the initial set of access token and refresh token
•	http://host/user/SignOut to register the user
•	http://host/user/signIn to refresh the access token using the refresh token sent

SignUp Help:
WebRequest:http://host/user/SignUp
Method:POST
ContentType: application/x-www-form-urlencoded
requestBody:
•	{name} : name
•	{family} : family
•	{cityId}: city Id (default:1)
•	{mobile}: mobile num
•	{password}: password
•	{subscribeNewsletter}: subscribe newsletter (true or false)


signIn Help:
WebRequest:http://host/user/signIn
Method:POST
ContentType: application/x-www-form-urlencoded
requestBody:
•	username: mobile num
•	password: password
•	grant_type: password

sample json output:
    json: 
    {
        "access_token":"eyJ0eXAiOiJKV1QiLCJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRza",
        "token_type": "bearer",
        "expires_in": 86399,
        "refresh_token": "d4572fbf0763403083448b6c82a0fa0e"
    }


signOut Help:
WebRequest:http://host/user/SignOut
Method:POST
requestHeader:
•	Authorization: “Bearer”+” “+Token


refreshToken Help:
WebRequest:http://host/user/signIn
Method:POST
ContentType: application/x-www-form-urlencoded
requestBody:
•	refresh_token: refreshToken  (sample:” 5687654271344265a04d1d8644a9c151”)
•	grant_type: refresh_token

sample json output:
    json: 
    {
        "access_token":"eyJ0eXAiOiJKV1QiLCJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRza",
        "token_type": "bearer",
        "expires_in": 86399,
        "refresh_token": "d4572fbf0763403083448b6c82a0fa0e"
    }

implement of this requests is here: [`code`](https://github.com/soheilasadeghian/HamiDiabet/blob/main/hamidiabetSite/HamiDiabet/ClassCollection/User.cs)
 
__NOTE: You can also test the API using a tool such as [`Postman`](https://www.getpostman.com/).__


HamiDiabet Website:
![alt text](https://github.com/soheilasadeghian/HamiDiabet/blob/main/HamiDiabet/image/screenshot_hamidiabet.png?raw=true)

Considerations:
If you have doubts about the implementation details or if you find a bug, please, open an issue. If you have ideas on how to improve the API or if you want to add a new functionality or fix a bug, please, contact me.

If you have doubts about the implementation details or if you find a bug, please, open an issue. If you have ideas on how to improve the API or if you want to add a new functionality or fix a bug, please, contact me.

