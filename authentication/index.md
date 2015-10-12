---
layout: default
active: auth
---
# Making your first API call
Making API calls is a three step process:

1. Setup the API client
2. Authenticate
3. Make your API calls

## Setting up the API client
Before you can do anything you must setup the API client using your Podio API key. [Head over to Podio to generate a client_id and client_secret](https://podio.com/settings/api) before continuing.

Podio-dotnet is a service based API client. Each service in this client library corresponds to an area in podio API and contains the methods exposed by this particular area. You'll be using more of these later, but for now you just need to use the main `Podio` class. Before you can make any API calls you must initialize the `Podio` class with your client ID and secret and authenticate with the Podio API. You only have to call it once before making any API calls.

{% highlight csharp startinline %}
using PodioAPI;
var podio = new Podio(clientId, clientSecret);
{% endhighlight %}

Now you're ready to authenticate.

## Authentication
Podio supports multiple forms of authentication depending on what you want to do:

1. Use the server-side flow for web apps where you need Podio users to access your apps.
2. Use the app authentication when you just need access to a single app without user interaction  
3. Use password authentication for testing or if you’re writing a batch job. 

[Read more about authentication in general at the Podio developer site](https://developers.podio.com/authentication).

### Server-side flow
The server-side flow requires you to redirect your users to a page on podio.com to authenticate. After they authenticate on podio.com they will be redirected back to your site. [Read about the flow on the developer site](https://developers.podio.com/authentication/server_side).

The example below handles three cases:

* The user has not authenticated and has not been redirected back to our page after authenticating.
* The user has already authenticated and they have a session stored using the [Session manager / AuthStore]({{site.baseurl}}/sessions).
* The user is being redirected back to our page after authenticating.

{% highlight csharp startinline %}
using PodioAPI;

// Set up the REDIRECT_URI -- which is just the URL for this page.
const string redirectURI = "http://example.com/exampleredirct.aspx";

var podio = new Podio(clientId, clientSecret);

if(string.IsNullOrEmpty(Request["code"]) && !podio.IsAuthenticated())
{
    // User is not being reidrected and does not have an active session
    // We just display a link to the authorization page on podio.com

    //You can get the podio authorization url calling this method
    string authUrl = podio.GetAuthorizeUrl(redirectURI);
}
else if(podio.IsAuthenticated())
{
    // User already has an active session. You can make API calls here:
    Response.Write("You were already authenticated and no authentication is needed.";)
}
else if(string.IsNullOrEmpty(Request["code"]))
{
    // User is being redirected back from podio.com after authenticating.
    // The authorization code is available in Request["code"]
    // We use it to finalize the authentication.

    // If there was a problem Request["error"] is set:
    if(!string.IsNullOrEmpty(Request["error"]))
    {
        Response.Write("There was a problem. The server said: " + Request["error_description"]);
    }
    else
    {
        // Finalize authentication. Note that we must pass the redirectURI again.
        await  podio.AuthenticateWithAuthorizationCode(Request["code"] , redirectURI);

        Response.Write("You have been authenticated. Wee!");
    }
}
{% endhighlight %}

### App authentication
App authentication doesn’t require any direct user authentication and therefore it is much simpler. This flow is suitable in situations where you only need data from a single app. You can simply pass the app id and app token directly to the AuthenticateWithApp method:

{% highlight csharp startinline %}
var podio = new Podio(clientId, clientSecret);
podio.AuthenticateWithApp(appId, appToken);
// You can now make API calls.
{% endhighlight %}

### Password authentication
Password authentication works the same way as app authentication, but you have full access to any data the user has access to. As it's bad practice to store your Podio password like this, you should only use password-based authentication for testing or if you cannot use any of the other options.

{% highlight csharp startinline %}
var podio = new Podio(clientId, clientSecret);
podio.AuthenticateWithPassword(userName, password);
// You can now make API calls.
{% endhighlight %}

<span class="note">If you get confused when you see something like this: `podio.SomeService.SomeMethod()` , I’m just using this authenticated instance of Podio class to show code examples throughout this documentation</span>

## Refreshing access tokens
Under the hood you receive two tokens upon authenticating. An access token is used to make API calls and a refresh token is used to get a new access/refresh token pair once the access token expires.

You should **avoid authenticating every time your page runs**. It's highly inefficient and you risk running into rate limits quickly. Instead [use a session manager to store access/refresh tokens between script runs]({{site.baseurl}}/sessions) to re-use your tokens.

Podio-dotnet will automatically refresh tokens for you.
