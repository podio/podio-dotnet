---
layout: default
active: sessions
---
# Sessions management
An important part of any Podio API integration is managing your authentication tokens. You can avoid hitting rate limits and make your integration faster by storing authentication tokens and thus avoid having to re-authenticate every time your page runs.

## What is a session manager
When you initialize the Podio class you can choose to pass in an object of a class that implements IAuthStore interface. This IAuthStore handles storing and retrieving access tokens through a unified interface. For example if `SessionAuthStore` class in an implementation of `IAuthStore` Interface, you would initialize your Podio class as:

{% highlight csharp startinline %}
var sessionAuthStore = new SessionAuthStore();

var podio = new Podio(clientId, clientSecret, sessionAuthStore);

if(podio.IsAuthenticated())
{
    // An existing authentication is found by AuthStore
    // No need to re-authenticate
}
else
{
    // No authentication found in session manager.
    // You must re-authenticate here.
}
{% endhighlight %}

If you use an AuthStore your authentication tokens will automatically be stored/updated when ever authentication or refresh of token happends and automatically retrieved when you initialize `Podio` class. By default it takes the SessionStore Implementation (Store access token is session)

## Writing your own session manager
Writing a AuthStore is straight-forward. You need to create a new class that implements `IAuthStore` interface.

### The `Get` method
The `Get` method should retrieve an existing authentication when called. It should return a `PodioOAuth` object in all cases. Return an empty `PodioOAuth` object if no existing authentication could be found.

### The `Set` method
The `Set` method should store a `PodioOAuth` object when called. It has a parameters, `podioOAuth`, which holds the current `PodioOAuth` object.

### The `Clear` method
The `Set` method should clear `PodioOAuth` object form AuthStore when called.

## Example: Store access tokens in browser session cookie
This is a simple example meant for a web application. It stores the authentication data in a HttpContext.Current.Session variable. This avoid having to re-authenticate each time a page is refreshed.

{% highlight csharp startinline %}
using System.Web;

namespace PodioAPI.Utils.Authentication
{
    public class SessionAuthStore: IAuthStore
    {
        //Get PodioOAuth object from session, if present.
        public PodioOAuth Get()
        {
            // Check if we have a stored session
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["PodioOAuth"] != null)
                return HttpContext.Current.Session["PodioOAuth"] as PodioOAuth; // We have a session, return it
            else
                return new PodioOAuth(); // Else return an empty object
        }
        
        //Store the PodioOAuth object in the session
        public void Set(PodioOAuth podioOAuth)
        {
            HttpContext.Current.Session["PodioOAuth"] = podioOAuth;
        }

        //Clear PodioOAuth object from session
        public void Clear()
        {
            HttpContext.Current.Session["PodioOAuth"] = null;
        }
    }
}
{% endhighlight %}
The above class is included in the Podio-dotnet library out of box. You just need to initialize it and pass in when you initialze the Podio class. See below.
You can do your own implementation of IAuthStore interface if you need to store access tokens in database or elsewhere.

{% highlight csharp startinline %}
using PodioAPI;
using PodioAPI.Utils.Authentication;

var sessionAuthStore = new SessionAuthStore();
var podio = new Podio(ClientID, ClientID, sessionAuthStore);

if(!podio.IsAuthenticated())
{
    // No authentication found in AuthStore.
    // You must re-authenticate here.

    podio.AuthenticateWithApp(appId, appToken);
}

//Api calls here

{% endhighlight %}
