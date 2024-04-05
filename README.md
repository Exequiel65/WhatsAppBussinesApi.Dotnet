# WhatsAppBussinesApi.Dotnet Package
This NuGet package provides classes and functionalities to integrate the WhatsApp Business API into .NET 8 and higher projects. In this initial version, it supports building message objects and sending them in the following formats:

#### _Text Messages_:
- Text messages
- Messages with images (supports image/jpeg and image/png)
- Messages with location
- Document messages
- Interactive messages

#### _Templates_:
- Template with text
- Template with text and header
- Template with text, header, and buttons 

### Configuration
1. Package installation:
```
dotnet add package WhatsAppBussinesApi.Dotnet
```
3. Import the necessary classes into your project:
```
using WhatsAppBusinessApi.Dotnet;
```
5. Add the service in Program.cs:
```
builder.Services.AddWhatsAppBusinessApi();
```
7. Add your credentials in appSettings.json:
    ```
    "WhatsAppBusiness": {
        "PhoneNumber": "", // WhatsApp phone number identifier
        "BearerToken": "", // Authentication Bearer Token
        "Version": "" // API version to use
    }
    ```
    

Make sure to replace PhoneNumber, BearerToken, and Version with the corresponding values from your WhatsApp Business account.

### Example
[Example Link](https://github.com/Exequiel65/WhatsAppBussinesApi.Dotnet/blob/main/Readme_Example.md)

### Note:
This package will be continuously updated to include more methods supported by the WhatsApp Business API. In the following days, additional examples and explanations for constructing and utilizing classes will be provided.

Thank you for using WhatsApp Business API .NET Package!
