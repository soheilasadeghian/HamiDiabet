# HamiDiabet 🔥
WebSite to provide services to diabetic patients (Website & web service Project With DB & JWT Authentication)

:star: Star me on GitHub — it helps!

[![Ask Me Anything !](https://img.shields.io/badge/ask%20me-linkedin-1abc9c.svg)](https://www.linkedin.com/in/SoheilaSadeghian/)
[![Maintenance](https://img.shields.io/badge/maintained-yes-green.svg)](https://github.com/SoheilaSadeghian/SoheilaSadeghian.github.io)
[![Ask Me Anything !](https://img.shields.io/badge/production%20year-2017-1abc9c.svg)]()

## Overview
This repository (HamiDiabet) includes two projects:

✔️ HamiDiabet Website project as a client in root of repository(hamidiabetSite Folder)
    The project “HamiDiabet Website” is a responsive website made with Asp.Net MVC Technology using C#, Bootstrap, JQuery, Ajax, HTML, CSS, JavaScript, SQL, and EntityFramework.
    User registration and login using JWT.

✔️ web service project in root of repository(hamidiabetWebApi Folder) 
    hamidiabetWebApi is a Web Service (via SOAP with C#) shows how to implement JSON Web Token authentication with ASP.NET MVC 5, .Net Framework 4.5


![alt text](https://github.com/soheilasadeghian/HamiDiabet/blob/main/image/rest.png)


## Tools Used 🛠️
*  Visual studio app,Sql server app
*  ASP.NET MVC 5,.Net Framework 4.5, C#, SQL, HTML, CSS, JavaScript, Bootstrap, JQuery, Ajax
*  i use this NuGet: [`Microsoft.Owin.Security.Jwt`](https://www.nuget.org/packages/Microsoft.Owin.Security.Jwt)

## Installation Steps 📦 
1. Restore DB in SQL Server from the DB file in root of repository <br/>
2. Open hamidiabetWebApi Solution in Visual Studio and build the project <br/>
3. Execute (F5) to run. Browser will throw error page which is fine as this is only WEB-API implementation <br/>
4. Open hamidiabetSite Solution in Visual Studio and build the project <br/>
3. Execute (F5) to run. Browser will show Homepage of website (the picture of homepage is end of this readme)<br/>
4. you can Register and Login to website and see the userTbl Table in database fields how to jwt authentication work

## Contributing implementation JWT Authentication 💡
JSON Web Token is a self-contained authentication protocol where the token is a base64 representation of a object which contains 3 parts seperated by a period:\
✔️ Header\
✔️ Payload (Claims): set claims for user:setClaimsIdentity function in [`code`](https://github.com/soheilasadeghian/HamiDiabet/blob/main/hamidiabetWebApi/testwebapi/SimpleAuthorizationServerProvider.cs)\
✔️ Signature 

- **encryption by HMAC Algorithm**

- **The project has 1 controller:**\
AuthController Contains the SignUp, and SignOut.\
SignIn is here [`signIn`](https://github.com/soheilasadeghian/HamiDiabet/blob/main/hamidiabetWebApi/testwebapi/App_Start/Startup.cs)<br>


- **Hashing:**\
For hashing we can implement it using various algorithms.This project implements hashing using SHA256.<br>
function of set Sha256Hash is in: [`here`](https://github.com/soheilasadeghian/HamiDiabet/blob/main/hamidiabetWebApi/testwebapi/SimpleRefreshTokenProvider.cs)


- **create Signature[`code`](https://github.com/soheilasadeghian/HamiDiabet/blob/main/hamidiabetWebApi/testwebapi/CustomJwtFormat.cs)**

```c#
private static readonly byte[] _secret = TextEncodings.Base64Url.Decode("QkU0QUMwNUNBODEyRDlGNTY0QTc3RUQ1MkE1NTY4RTQ4QzlDMDA3MTE1QTE2NEYyRUFFM0QzRjQzREQzNDVFMA==");
var signingKey = new HmacSigningCredentials(_secret);
return new JwtSecurityTokenHandler()
.WriteToken(
new JwtSecurityToken(_issuer, "Any", data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey)
);
```

- **after SignIn we have accessToken, so in request we [`add to header Authorization`](https://github.com/soheilasadeghian/HamiDiabet/blob/main/hamidiabetSite/HamiDiabet/ClassCollection/WebService.cs):**

```c#
if (Authorization)
{
    request.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["access_token"]);
}
```

## The Auth server exposes the following endpoints:
- http://host/user/SignUp to register the user
- http://host/user/SignIn to login the user and generate the initial set of access token and refresh token
- http://host/user/SignOut to register the user
- http://host/user/SignIn to refresh the access token using the refresh token sent


SignUp:

```SignUp Help:
WebRequest:http://host/user/SignUp
Method:POST
ContentType: application/x-www-form-urlencoded
requestBody:
    {name} : name
	{family} : family
	{cityId}: city Id (default:1)
	{mobile}: mobile num
	{password}: password
	{subscribeNewsletter}: subscribe newsletter (true or false)
```
signIn:

```signIn Help:
WebRequest:http://host/user/signIn
Method:POST
ContentType: application/x-www-form-urlencoded
requestBody:
	username: mobile num
	password: password
	grant_type: password

sample json output:
    json: 
    {
        "access_token":"eyJ0eXAiOiJKV1QiLCJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRza",
        "token_type": "bearer",
        "expires_in": 86399,
        "refresh_token": "d4572fbf0763403083448b6c82a0fa0e"
    }
```
SignOut:

```signOut Help:
WebRequest:http://host/user/SignOut
Method:POST
requestHeader:
	Authorization: “Bearer”+” “+Token
```
refreshtoken:

```refreshToken Help:
WebRequest:http://host/user/signIn
Method:POST
ContentType: application/x-www-form-urlencoded
requestBody:
	refresh_token: refreshToken  (sample:” 5687654271344265a04d1d8644a9c151”)
	grant_type: refresh_token

sample json output:
    json: 
    {
        "access_token":"eyJ0eXAiOiJKV1QiLCJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRza",
        "token_type": "bearer",
        "expires_in": 86399,
        "refresh_token": "d4572fbf0763403083448b6c82a0fa0e"
    }
```

## implement of this requests is here: [`code`](https://github.com/soheilasadeghian/HamiDiabet/blob/main/hamidiabetSite/HamiDiabet/ClassCollection/User.cs)<br>
 
✔️ __NOTE: You can also test the API using a tool such as [`Postman`](https://www.getpostman.com/).__

[`persian help link`](https://zerotohero.ir/%D8%AF%D8%B3%D8%AA%D9%87%E2%80%8C%D8%A8%D9%86%D8%AF%DB%8C-%D9%86%D8%B4%D8%AF%D9%87/%D8%A7%D8%AD%D8%B2%D8%A7%D8%B1-%D9%87%D9%88%DB%8C%D8%AA-%D8%AA%D9%88%D8%B3%D8%B7-jwt/)

HamiDiabet Website:\\
![alt text](https://github.com/soheilasadeghian/HamiDiabet/blob/main/image/screenshot_hamidiabet.png?raw=true)


## Support
For support, [click here](https://github.com/soheilasadeghian).

## Give a star ⭐️ !!!
If you liked the project, please give a star :)



