---
layout: default
active: webhooks
---
# Webhooks
Webhooks provide realtime notifications when changes occur on your apps and spaces. Before continuing you should read the [general introduction to Podio webhooks](https://developers.podio.com/examples/webhooks) and [review the list of webhook events](https://developers.podio.com/doc/hooks).

## Creating webhooks
The easiest way to create a new webhook is to do it manually in the developer section for the app. There are you can create most webhooks through a point and click interface. Not all webhooks can be created here. The rest you most create programmatically. E.g. if you only want to receive updates about a single field in an app:

{% highlight csharp startinline %}
int appFieldId = 1234;
string eventType = "item.update";
string externalUrl  = "http://example.com/my/hook/url";

int hookId = podio.HookService.CreateHook("app_field", appFieldId, externalUrl, eventType);
{% endhighlight %}

Immediately after you create a webhook you must verify it. Verifying just means the URL you provided must respond to a special type of webhook event called `hook.verify`. See the full example below for how to verify a webhook.

If you create your webhook ahead of the URL being available you must manually request a webhook verification:

{% highlight csharp startinline %}
podio.HookService.Verify(hookId);
{% endhighlight %}

## Checking webhooks status
If you are unsure of the status of your hooks you can get a list of all hooks for a reference:

{% highlight csharp startinline %}
List<Hook> hooks = podio.HookService.GetHooks(refType, refId);

foreach (var hook in hooks)
{
    int hookId = hook.HookId;
    string hookType = hook.Type;
    string hookUrl = hook.Url;
}
{% endhighlight %}

## Troubleshooting webhooks
When webhooks fail to show up it's typically for one of the following reasons:

* The URL is not public. Webhooks must be available on the public internet. You can test them locally using tools like [ngrok](https://ngrok.com/)
* The URL is not on a standard port. Webhooks must be served on port 80 for http and 443 for https
* Incoming requests are being blocked by firewall/hosting provider. Your IT department or hosting provider may be blocking webhooks

Query string parameters will be converted to POST parameters, because webhooks are POST requests and any query string parameters will be converted to a POST parameter. If your URL is 'http://example.com/hook?foo=bar' you will not be able to use 'Request.QueryString["foo"]' - use 'Request["foo"]' instead


## Full webhooks example
This is a standalone Generic Handler (.ashx) that will verify all webhook verification requests and show how to handle item events. It uses app authentication. For more events see https://developers.podio.com/doc/hooks

{% highlight csharp startinline %}
<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using PodioAPI;

public class Handler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        // API key setup
        string clientId = "YOUR_CLIENT_ID";
        string clientSecret = "YOUR_CLIENT_SECRET";
        
        // Authentication setup
        int appId = 123456;
        string appToken = "YOUR_APP_TOKEN";

        // Setup client and authenticate
        var podio = new Podio(clientId, clientSecret);
        podio.AuthenicateWithApp(appId, appToken);

        // Big switch statement to handle the different events

        var request = context.Request;

        switch (request["type"])
        {
            case "hook.verify":
                podio.HookService.ValidateHookVerification(int.Parse(request["hook_id"]), request["code"]);
                break;
            // An item was created
            case "item.create":
                // For item events you will get "item_id", "item_revision_id" and "external_id". in post params
                int itemIdOfCreatedItem = int.Parse(request["item_id"]);
                // Fetch the item and do what ever you want
                break;
                
            // An item was updated
            case "item.update":
                // For item events you will get "item_id", "item_revision_id" and "external_id". in post params
                int itemIdOfUpdatedItem = int.Parse(request["item_id"]);
                // Fetch the item and do what ever you want
                break;

            // An item was deleted    
            case "item.delete":
                // For item events you will get "item_id", "item_revision_id" and "external_id". in post params
                int deletedItemId = int.Parse(request["item_id"]);
                break;
        }
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}

{% endhighlight %}
