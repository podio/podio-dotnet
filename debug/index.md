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
    podio.AuthenicateWithApp(appId, appToken);
    podio.FileService.UploadFile(filePath,"image.jpg")
}
catch (PodioException exception)
{
    Response.Write(exception.Status); // Status code of the response
    Response.Write(exception.Error.Error); // Error
    Response.Write(exception.Error.ErrorDescription); // You normally want this one, a human readable error description
    Response.Write(exception.Error.ErrorDetail); // Error detail
}
{% endhighlight %}

If you get unexpected results, but you are not seeing exceptions, you can see the data that's being sent between your script and the Podio API using HTTP debugging proxy server application [Fiddler](http://www.telerik.com/fiddler). Route the traffic through Fiddler proxy by setting `Proxy` property of Podio class from the constroctor:


{% highlight csharp startinline %}
try
{
	WebProxy fiddlerProxy = new WebProxy("127.0.0.1", 8888);

	var podio = new Podio(clientId, clientSecret, proxy: fiddlerProxy);
	podio.AuthenicateWithApp(appId, appToken);

    var filteredItems = podio.ItemService.FilterItems(appId: 123);
}
{% endhighlight %}

Now open up the fiddler and run the page. You can see the HTTP traffic between Podio-dotnet client and [Podio API endpoint](https://api.podio.com/). Since the traffic is HTTPS you need need check 'Capture HTTPS Traffic' and 'Decrypt HTTPS traffic' in Fiddler options.