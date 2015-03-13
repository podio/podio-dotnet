About
=====

This is the .NET Client for accessing the Podio API.

Installation
-------

The client library requires .NET Framework 4.0 or higher and [Json.NET](http://www.nuget.org/packages/Newtonsoft.Json/) as its dependency.

This package is available on NuGet Gallery. To install the [Podio package](http://www.nuget.org/packages/podio) run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console)

    PM> Install-Package Podio

This will install the client library and the required dependency.

Constructing API client instance
-------------

Before you can make any API calls you must initialize the `Podio` class with your client ID and secret and authenticate with the Podio API.

    using PodioAPI;
    var podio = new Podio(clientId, clientSecret);

We will use this initialized podio object for all further operations.

Authentication
--------------

The client supports three ways of authentication:

### Web Server Flow

The default OAuth flow to be used when you authenticate Podio users from your web application.

```csharp
// Redirect the user to the authorize url, You can get the authorize url by calling 'GetAuthorizeUrl' method in podio class.
string authUrl = podio.GetAuthorizeUrl(redirectUri);

// In the callback you get the authorization_code 
// which you use to get the access token
podio.AuthenticateWithAuthorizationCode(Request["code"], redirectUri);
```

### Username and Password Flow

If you're writing a batch job or are just playing around with the API, this is the easiest to get started. Do not use this for authenticating users other than yourself, the web server flow is meant for that.

```csharp
podio.AuthenticateWithPassword("USERNAME", "PASSWORD");
```

### App authentication flow

The app authentication flow is suitable in situations where you only need data from a single app and do not wish authenticate as a specific user.

```csharp
podio.AuthenticateWithApp("APP_ID", "APP_SECRET")
```

Basic Usage
-----------

After constructing `Podio` object  you can use all of the wrapper functions to make API requests. The functions are organized into services, each service corresponds to an Area in official [API documentation](https://developers.podio.com/doc). You can access the services right from podio class. For example:

```csharp

// Getting an item
var item = podio.ItemService.GetItem(123456);

//Filtering items with a date field with external_id 'deadline' and limit the results by 10
  var filters = new Dictionary<string,object>
  {
    {"deadline",new  { from = new DateTime(2013, 10, 1), to = DateTime.Now }}
  };

podio.ItemService.FilterItems(AppId, 10, null, filters);
```

All the wrapped methods either return a strongly typed model, or a collection of models.

Error Handling
--------------

All unsuccessful responses returned by the API (everything that has a 4xx or 5xx HTTP status code) will throw exceptions. All exceptions inherit from `PodioAPI.Exceptions.PodioException` and and it has an `Error` property that represents the strongly typed version of response from the API:

```csharp
try
{
    podio.FileService.UploadFile(filePath,"image.jpg")
}
catch (PodioException exception)
{
    Response.Write(exception.Status); // Status code of the response
    Response.Write(exception.Error.Error); // Error
    Response.Write(exception.Error.ErrorDescription); // Error description -> You need this in most cases
    Response.Write(exception.Error.ErrorDetail); // Error detail
}
```

Full Example
------------
Adding a new item with a file on an application with id 5678.

```csharp
using PodioAPI;
using PodioAPI.Models;
using PodioAPI.Utils.ItemFields;
using PodioAPI.Exceptions;

podio.AuthenticateWithPassword("YOUR_PODIO_USERNAME", "YOUR_PODIO_PASSWORD");

Item myNewItem = new Item();

//A Text field with external_id 'title'
var textfield = myNewItem.Field<TextItemField>("title");
textfield.Value = "This is a text field";

//A Date field with external_id 'deadline-date'
var dateField = myNewItem.Field<DateItemField>("deadline-date");
dateField.Start = DateTime.Now;
dateField.End = DateTime.Now.AddMonths(2);

//A Location field with external_id 'location'
var locationField = myNewItem.Field<LocationItemField>("location");
locationField.Locations = new List<string> 
{ 
 "Copenhagen, Denmark"
};

//A Money field with external_id 'money'
var moneyField = myNewItem.Field<MoneyItemField>("money");
moneyField.Currency = "EUR";
moneyField.Value = 250;

//An App reference field with external_id 'app-reference'
var appReferenceField = myNewItem.Field<AppItemField>("app-reference");
//Item id's to reference
appReferenceField.ItemIds = new List<int>
{
    1234, 4568
};

// Embed/Link field with with external_id 'link'
var embedField = myNewItem.Field<EmbedItemField>("link");
var embed = podio.EmbedService.AddAnEmbed("https://www.google.com/"); // Creating an embed
embedField.AddEmbed(embed.EmbedId);

//Uploading a file and attaching it to new item
var filePath = Server.MapPath("\\files\\report.pdf");
var uploadedFile = podio.FileService.UploadFile(filePath, "report.pdf");
myNewItem.FileIds = new List<int> { uploadedFile.FileId }; //Attach the uploaded file's id to item

podio.ItemService.AddNewItem(5678, myNewItem);
```

For more documentation and examples see [Documentation](http://podio.github.io/podio-dotnet/)

Contribution guideline
-----------------

Your contribution to Podio .NET client would be very welcome. If you find a bug, please raise it as an issue. Even better fix it and send us a pull request. If you want to contribute, fork the repo, fix an issue and send a pull request.

Pull requests are code reviewed. Here is what we look for in your pull request:

- Clean implementation
- Comment the code if you are writing anything complex.
- Xml documentation for new methods or updating the existing documentation when you make a change.
- Adherence to the existing coding styles.
- Also please link to the issue(s) you're fixing from your PR description.
