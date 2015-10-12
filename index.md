---
layout: default
active: index
---
# About Podio-dotnet
Podio-dotnet is a C# client for interacting with the Podio API. All core areas are covered in the library.

## Requirements
You need .NET Framework 4.5 or higher and [Json.NET](http://www.nuget.org/packages/Newtonsoft.Json/) as its dependency.

## Installation
This package is available on NuGet Gallery. It comes in two flavors [synchronous](https://www.nuget.org/packages/Podio/) and [asynchronous](https://www.nuget.org/packages/Podio.Async/), To install the Podio package run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console)

For synchronous version:

    PM> Install-Package Podio


For asynchronous version:

    PM> Install-Package Podio.Async
    
This will install the client library and the required dependency.

## Hello world
To get started right away, use app authentication to work on a single Podio app. To find your app id and token: 

* go to your app
* click the wrench in the top right corner of the sidebar
* click the Developer option

{% highlight csharp startinline %}
using PodioAPI;

var podio = new Podio(clientId, clientSecret);
podio.AuthenticateWithApp(appId, appSecret);

var items = podio.ItemService.FilterItems(appId);
Response.Write("My app has " + items.Total + " items");
{% endhighlight %}
