---
layout: default
active: debug
---
# Debugging and error handling

Podio-dotnet will throw exceptions when something goes predictably wrong. For example if you try to update something you don't have permission to update, if you don't include required attributes, if you hit the rate limit etc. All exceptions inherit from `PodioAPI.Exceptions.PodioException` and and it has an `Error ` property that represents the strongly typed version of response from the API:

{% highlight csharp startinline %}
try
{
    var podio = new Podio(clientId, clientSecret);
    podio.AuthenticateWithApp(appId, appToken);
    var uploadedFile = podio.FileService.UploadFile(filePath,"image.jpg")
}
catch (PodioException exception)
{
    Response.Write(exception.Status); // Status code of the response
    Response.Write(exception.Error.Error); // Error
    Response.Write(exception.Error.ErrorDescription); // You normally want this one, a human readable error description
    Response.Write(exception.Error.ErrorDetail); // Error detail
}
{% endhighlight %}