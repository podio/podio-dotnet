---
layout: default
active: items
---
# Working with Podio items

Apps and app items form the core of Podio. With this client library we have made it easy as possible to read and manipulate app items.

## Individual items

### Get an item

There are multiple ways to get a single item from the API. All result in a `PodioAPI.Models.Item` object. Which one you should use depends on what data you have available and how much data you need returned.

If you have the `item_id` you can use either `GetItem()` or `GetItemBasic()` method in `ItemService` . The first will return all auxiliary data about the item but is slower, the latter returns no auxiliary data.

{% highlight csharp startinline %}
// Get only data about the item
var item = podio.ItemService.GetItemBasic(123); // Get item with item_id = 123
{% endhighlight %}

{% highlight csharp startinline %}
// Get item and auxiliary data such as comments
var item = podio.ItemService.GetItem(123); // Get item with item_id=123
{% endhighlight %}

If you have assigned an external_id to an item you can get the item by providing that external_id. Since external_ids are not unique you need to provide the app_id as well.

{% highlight csharp startinline %}
// Get item by external_id

var appId = 456;
var externalId = "my_sample_external_id";

var item = podio.ItemService.GetItemByExternalId(appId, externalId);
{% endhighlight %}

In a similar fashion each item in an app has a short numeric id called `app_item_id`. These are unique within the app, but not globally unique. It's the numeric id you can see in the URL when viewing an item on Podio. You can also get an item by providing an app_id and the app_item_id.

{% highlight csharp startinline %}
// Get item by app_item_id

var appId = 456;
var appItemId = 1;

var item = podio.ItemService.GetItemByAppItemId(appId, appItemId);
{% endhighlight %}

The final option is to [resolve an item URL](https://developers.podio.com/doc/reference/resolve-url-66839423) and create an item that way. This is useful in cases where you only have the browser's URL to work with (e.g. when creating browser extensions or when you are asking users to copy and paste URLs into your app).

{% highlight csharp startinline %}
// Get item by resolving its URL

string url = "http://podio.com/myorganization/myspace/apps/myapp/items/1";

// Resolve URL to a reference
var reference = podio.ReferenceService.ResolveURL(url);

// Create item from reference
var item = reference.Data.ToObject<PodioAPI.Models.Item>();
{% endhighlight %}

### Item fields
The most interesting part of an item is the `Fields` property. Here the values for the item are found. If you're doing any work with items it's likely that you're modifying fields in one way or another.

#### Iterating over all fields
If you just want see all fields you can iterate over them.

{% highlight csharp startinline %}
// Get an item to work on
var item = podio.ItemService.GetItemBasic(123); // Get item with item_id = 123

// Iterate over the field collection
foreach (ItemField field in item.Fields)
{
    Response.Write("This field has the id: " + field.FieldId);
    Response.Write("This field has the external_id: " + field.ExternalId);
}
{% endhighlight %}

#### Get field values
You can access individual fields either by `field_id` or more likely by the human-readable `external_id`. The `Values` property of ItemField object is dynamic. We have created an abstraction to access and set values as strongly typed objects.

<span class="note">Here’s how to get and set values of different type of item fields [Item field examples]({{site.baseurl}}/fields).</span>


### Create item
To create a new item from scratch you create a new `Item` without an `item_id`, add the field values and use 'ItemService.AddNewItem' method with the created item and app_id as parameter. Here is an example:

{% highlight csharp startinline %}

Item myNewItem = new Item();

//A Text field with external_id 'title'
var textfield = myNewItem.Field<TextItemField>("title");
textfield.Value = "This is a text field";

//A Date field with external_id 'deadline-date'
var dateField = myNewItem.Field<DateItemField>("deadline-date");
dateField.Start = DateTime.Now;
dateField.End = DateTime.Now.AddMonths(2);

int itemId = podio.ItemService.AddNewItem(appId, myNewItem);
{% endhighlight %}

### Modifying items
Updating items are handled exactly the same way as creating items. The only difference is you need to set the `ItemId` property of `Item` object. Values will only be updated for fields included. If you need to remove the value from a field, just initialize the field and dont set the value. This will update the value of textfield ‘title’ and empty the value of date field ‘deadline-date’. Here is an example:

{% highlight csharp startinline %}
Item itemToUpdate = new Item();

itemToUpdate.ItemId = 12345; // The item_id of the item you need to update.

//A Text field with external_id 'title'
var textfield = itemToUpdate.Field<TextItemField>("title");
textfield.Value = "This is a an updated value";

//A Date field with external_id 'deadline-date'
var dateField = itemToUpdate.Field<DateItemField>("deadline-date");

podio.ItemService.UpdateItem(myNewItem);
{% endhighlight %}

To update the item values for a specific field you can use `ItemService.UpdateItemFieldValues` method.
Example Usage: Updating a text field

{% highlight csharp startinline %}               
Item item = new Item();
item.ItemId = 12345;
var textfield = item.Field<TextItemField>("title");
textfield.Value = "My updated title";
var newRevisionId = ItemService.UpdateItemFieldValues(item);
{% endhighlight %}

## Item collections
One of the most common operations is getting a collection of items from an app, potentially with a filter applied. For this you can use [ItemService.FilterItems()](https://developers.podio.com/doc/items/filter-items-4496747). It returns a `PodioCollection<Item>` which has two additional properties: filtered (total amount of items with the filter applied) and total (total amount of items in the app).

{% highlight csharp startinline %}
var filteredItems = podio.ItemService.FilterItems(123);

Response.Write("The collection contains" + filteredItems.Items.Count() + " items");
Response.Write("There are " + filteredItems.Total + " items in the app in total");
Response.Write("There are " + filteredItems.Filtered + " items with the current filter applied");

// Output the title of each item
foreach (var item in filteredItems.Items)
{
    Response.Write(item.Title);
}
{% endhighlight %}

### Sorting items
You can sort items by various properties. [See a full list in the API reference](https://developers.podio.com/doc/filters).

{% highlight csharp startinline %}
// Sort by last edit date for the items, descending
var filteredItems = podio.ItemService.FilterItems(appId:123, sortBy: "last_edit_on", sortDesc: true);

{% endhighlight %}

### Filters

<span class="note">**Important:** You can use both `field_id` and `external_id` when filtering items. The examples below all use `field_id` for brevity.</span>

You can filter on most fields. Take a look at the [API reference for a full list of filter options](https://developers.podio.com/doc/filters). When filtering on app fields use the `field_id` or `external_id` as the key for your filter. Some examples below:

{% highlight csharp startinline %}
// Category: Only items with "FooBar" in category field value
string categoryFieldId = "1";
var filter = new Dictionary<string, object>
{
    {categoryFieldId, new string[]{"FooBar"} }
};
var filteredItems = podio.ItemService.FilterItems(appId: 123, filters: filter);
{% endhighlight %}

{% highlight csharp startinline %}
// Number: Only items within a certain range
// Same concept for calculation, progress, duration & money fields
string numberFieldId = "2";
var filter = new Dictionary<string, object>
{
    {numberFieldId, new { from = 100, to = 200} }
};
var filteredItems = podio.ItemService.FilterItems(appId: 123, filters: filter);
{% endhighlight %}

{% highlight csharp startinline %}
// App reference: Only items that has a specific reference
string appReferenceFieldId = "3";

// Item id to filter against
int filterTargetItemId = 1234567;

var filter = new Dictionary<string, object>
{
    {appReferenceFieldId, new int[]{filterTargetItemId} }
};
var filteredItems = podio.ItemService.FilterItems(appId: 123, filters: filter);
{% endhighlight %}

{% highlight csharp startinline %}
// Contact: Only items that has a specific contact set
string contactFieldId = "4";

// Item id to filter against
int filterTargetProfileId = 123456789;

var filter = new Dictionary<string, object>
{
    {contactFieldId, new int[]{filterTargetProfileId} }
};
var filteredItems = podio.ItemService.FilterItems(appId: 123, filters: filter);
{% endhighlight %}

{% highlight csharp startinline %}
// Date: Date within a certain range
string dateFieldId = "5";

// Item id to filter against
int filterTargetProfileId = 123456789;

var filter = new Dictionary<string, object>
{
    {dateFieldId, from = new DateTime(2013, 9, 1), to = new DateTime(2013, 9, 30) }
};
var filteredItems = podio.ItemService.FilterItems(appId: 123, filters: filter);
{% endhighlight %}

{% highlight csharp startinline %}
// Combining multiple filters
string numberFieldId = "2";
string textFieldId = "7";
var filter = new Dictionary<string, object>
{
    {numberFieldId, new { from = 100, to = 200} },
    {textFieldId, "Support" }
};
var filteredItems = podio.ItemService.FilterItems(appId: 123, filters: filter);
{% endhighlight %}

