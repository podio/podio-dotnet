Podio ASP.NET MVC sample application
====================================

This app demonstrates how to authenticate against Podio, and CRUD operations with item.

Preparations on Podio
---------------------
To run this sample you must have the Leads app installed on Podio. You can find this app in the  App Store here: https://podio.com/store/app/11236-leads. You can use another app, but then this example won't work "out of the box".

Create an API key:
1) Log into Podio 
2) Go to Account Settings in the My Account dropdown.  
3) Go to the API Keys tab and write a name and a domain for this app. 
It’s recommended you set the domain to localhost:3000 or similar while you are developing.

Configuration
-------------

Put your API key, secret, App ID in the appSettings section of Web.config. The App ID should be the ID of your instance of the Leads app.

How to find the App ID:
1) Go to your installed Leads app on Podio
2) Click the wrench icon and select "Developers" 
or  
1) Going to an item in that app
2) Click the small cloud icon on the right side of the gray action bar at the top of the item.


Tips
----

It’s also possible to work with Podio apps with a completely different structure than the Leads app used in this example. The easiest way to see the structure and the all-important external ids of your app is to go to the aforementioned "Developers" page for your app.
You can also go to https://developers.podio.com/doc/applications/get-app-22349 and enter the app id with "full" as type in the in the sandbox.

You can also see exactly what fields and values you get from a Get Items call by using the sandbox here: https://developers.podio.com/doc/items/get-items-27803