---
layout: default
active: fields
---
# Field reference
You can access individual fields either by `field_id` or more likely by the human-readable `external_id`. The `Values` property of ItemField object is dynamic. We have created an abstraction to access and set values as strongly typed objects.

Below you'll find examples for getting and setting field values for each of the fields available in Podio.

* [App reference field](#app-reference-field)
* [Calculation field](#calculation-field)
* [Category & Question fields](#category-field--question-field)
* [Contact field](#contact-field)
* [Date field](#date-field)
* [Duration field](#duration-field)
* [Email field](#email-field)
* [Image field](#image-field)
* [Link/embed field](#linkembed-field)
* [Location/Google Maps field](#locationgoogle-maps-field)
* [Money field](#money-field)
* [Number field](#number-field)
* [Phone field](#phone-field)
* [Progress field](#progress-field)
* [Text field](#text-field)

## App reference field

#### Getting values
Values are returned as a list of `PodioAPI.Models.Item` object:

{% highlight csharp startinline %}
var item = podio.ItemService.GetItemBasic(123);

// A App reference field with external_id 'related-items'
AppItemField appField = item.Field<AppItemField>("related-items");
IEnumerable<Item> relatedItems = appField.Items;
{% endhighlight %}

#### Setting values
For setting values for field type App reference, you need assign the item_id's:

{% highlight csharp startinline %}
Item myNewItem = new Item();

//An App reference field with external_id 'app-reference'
var appReferenceField = myNewItem.Field<AppItemField>("app-reference");
//Set Item id's to reference
appReferenceField.ItemIds = new List<int>
{
    1234, 4568
};
{% endhighlight %}


------------------------------------------------------------------------------

## Calculation field

#### Getting values
Value is provided as type `float?`.

{% highlight csharp startinline %}
var item = podio.ItemService.GetItemBasic(123);

// A Calculation field with external_id 'calculation'
CalculationItemField calculationField = item.Field<CalculationItemField>("calculation");
float? number = calculationField.Value;
{% endhighlight %}

Calculation fields are read-only. It's not possible to modify the value.


------------------------------------------------------------------------------

## Category field & Question field

#### Getting values
Category and Question fields function in the same manner. Values are provided as an list of options.

{% highlight csharp startinline %}
var item = podio.ItemService.GetItemBasic(123);

// A Category field with external_id 'categories'
CategoryItemField categoryField = item.Field<CategoryItemField>("categories");
IEnumerable<CategoryItemField.Option> categories = categoryField.Options;

// A Question field with external_id 'question'
QuestionItemField questionField = item.Field<QuestionItemField>("question");
IEnumerable<CategoryItemField.Option> answers = questionField.Options;

{% endhighlight %}

#### Setting values
Set a single value by using the OptionId.

{% highlight csharp startinline %}
Item myNewItem = new Item();
var categoryField = myNewItem.Field<CategoryItemField>("categories");

// Set value to a single option
categoryField.OptionId =  2;  //option_id: 2
{% endhighlight %}

Use OptionIds to set multiple values
{% highlight csharp startinline %}
Item myNewItem = new Item();

var categoryField = myNewItem.Field<CategoryItemField>("categories");
categoryField.OptionIds = new List<int> { 4, 5, 6 }; // option_ids: 4, 5 and 6
{% endhighlight %}


------------------------------------------------------------------------------

## Contact field

#### Getting values

Values are returned as a List of `PodioAPI.Models.Contact` objects:

{% highlight csharp startinline %}
var item = podio.ItemService.GetItemBasic(123);

// A Contact field with external_id 'client'
ContactItemField contactField = item.Field<ContactItemField>("client");
IEnumerable<Contact> contacts = contactField.Contacts;
{% endhighlight %}

#### Setting values
Setting value to field type Contact can be done by setting profile_id's

{% highlight csharp startinline %}
Item myNewItem = new Item();

var contactField = myNewItem.Field<ContactItemField>("contacts");

// Set the profile id's of the contacts
contactField.ContactIds = new List<int> { 3254, 89745 }; 
{% endhighlight %}


------------------------------------------------------------------------------

## Date field

#### Getting values
Date field values have two components: The start date and the end date. You can also access date and time sections individually. This is often preferred as the time component will be null for events without time.

{% highlight csharp startinline %}
var item = podio.ItemService.GetItemBasic(123);

// A Date field with external_id 'deadline-date'
DateItemField dateField = item.Field<DateItemField>("deadline-date");
DateTime? date = dateField.Start; //Nullable DateTime
DateTime? endDate = dateField.End; //Nullable DateTime

string startTime =  dateField.StartTime; // Time as string else null
string endTime =  dateField.EndTime; // Time as string else null
{% endhighlight %}

#### Setting values
To set values you can assign value to `Start` and `End` properties.

{% highlight csharp startinline %}
Item myNewItem = new Item();

//A Date field with external_id 'deadline-date'
var dateField = myNewItem.Field<DateItemField>("deadline-date");
dateField.Start = DateTime.Now;
dateField.End = DateTime.Now.AddMonths(2);
{% endhighlight %}


------------------------------------------------------------------------------

## Duration field

#### Getting values
Progress fields return TimeSpan representing the duration in seconds.

{% highlight csharp startinline %}
var item = podio.ItemService.GetItemBasic(123);

// A Duration field with external_id 'duration'
DurationItemField durationField = item.Field<DurationItemField>("duration");
TimeSpan? timeSpan = durationField.Value;
{% endhighlight %}

#### Setting values
Simply assign Value property of type TimeSpan to set the value

{% highlight csharp startinline %}
Item myNewItem = new Item();

// A Duration field with external_id 'duration'
DurationItemField durationField = item.Field<DurationItemField>("duration");
durationField.Value = new TimeSpan(1, 30, 0);
{% endhighlight %}


------------------------------------------------------------------------------

## Email field

#### Getting values
Values are returned as a List of `PodioAPI.Utils.ItemFields.EmailPhoneFieldResult` object:

{% highlight csharp startinline %}
var item = podio.ItemService.GetItemBasic(123);

// An Email field with external_id 'email'
EmailItemField emailField = item.Field<EmailItemField>("email");
IEnumerable<EmailPhoneFieldResult> emails = emailField.Value;
{% endhighlight %}

#### Setting values
Simply assign Value property with a List of `PodioAPI.Utils.ItemFields.EmailPhoneFieldResult`

{% highlight csharp startinline %}
Item myNewItem = new Item();

// An Email field with external_id 'email'
EmailItemField emailField = item.Field<EmailItemField>("email");
emailField.Value = new List<EmailPhoneFieldResult>
{
    new EmailPhoneFieldResult { Type = "work", Value = "john@examples.dk" },
    new EmailPhoneFieldResult { Type = "home", Value = "test@example.com" },
};
{% endhighlight %}


------------------------------------------------------------------------------

## Image field

#### Getting values

Values are returned as a List of `PodioAPI.Models.FileAttachment` objects:

{% highlight csharp startinline %}
var item = podio.ItemService.GetItemBasic(123);

// A Image field with external_id 'image'
ImageItemField imageField = item.Field<ImageItemField>("image");
IEnumerable<FileAttachment> images = imageField.Images;

foreach (var file in images)
{
    int fileId = file.FileId;
    string fileUrl = file.Link;

    // You can download and save to a file
    FileResponse fileResponse = podio.FileService.DownloadFile(file);
    string filePath = Server.MapPath("/Images/" + file.Name);

    System.IO.File.WriteAllBytes(filePath, fileResponse.FileContents);
   
}
{% endhighlight %}

#### Setting values
Setting value can be done by setting a list of file_id's to `FileIds` property. You have to upload a file to get a file_id to use.

{% highlight csharp startinline %}
// Upload file
var filePath = Server.MapPath("\\files\\report.pdf");
var uploadedFile = podio.FileService.UploadFile(filePath, "report.pdf");

// Set FileIds
Item myNewItem = new Item();
ImageItemField imageField = myNewItem.Field<ImageItemField>("image");
imageField.FileIds = new List<int>{uploadedFile.FileId};
{% endhighlight %}


------------------------------------------------------------------------------

## Link/Embed field

#### Getting values

Values are returned as a List of `PodioAPI.Models.Embed` objects:

{% highlight csharp startinline %}
var item = podio.ItemService.GetItemBasic(123);

// A Link field with external_id 'link'
EmbedItemField embedField = item.Field<EmbedItemField>("link");
IEnumerable<Embed> embeds = embedField.Embeds;

//Get url of the first link
string url = embeds.First().OriginalUrl;
{% endhighlight %}

#### Setting values
Setting value to EmbedItemField can be done by calling AddEmbed method and passing in the embed_id. You will need to create the embed first.

{% highlight csharp startinline %}
Item myNewItem = new Item();

// Creating an embed
var embed = podio.EmbedService.AddAnEmbed("https://www.google.com/");

// Embed/Link field with with external_id 'link'
var embedField = myNewItem.Field<EmbedItemField>("link");
embedField.AddEmbed(embed.EmbedId);
{% endhighlight %}


------------------------------------------------------------------------------

## Location/Google Maps field

#### Getting values
Location fields return an list of strings (addresses)

{% highlight csharp startinline %}
var item = podio.ItemService.GetItemBasic(123);

// A Location field with external_id 'location'
LocationItemField locationField = item.Field<LocationItemField>("location");
IEnumerable<string> locations = locationField.Locations;
{% endhighlight %}

#### Setting values
Set values using an array of locations

{% highlight csharp startinline %}
Item myNewItem = new Item();

//A Location field with external_id 'location'
 var locationField = myNewItem.Field<LocationItemField>("location");
locationField.Locations = new List<string> 
{ 
  "650 Townsend St., San Francisco, CA 94103",
  "Vesterbrogade 34, 1620 Copenhagen, Denmark"
};
{% endhighlight %}


------------------------------------------------------------------------------

## Money field

#### Getting values
Money field values have two components: The amount and the currency. You can access these through properties.

{% highlight csharp startinline %}
var item = podio.ItemService.GetItemBasic(123);

// A Money field with external_id 'price'
MoneyItemField moneyField = item.Field<MoneyItemField>("price");
decimal? number = moneyField.Value;
string currency = moneyField.Currency; // E.g. "USD"
{% endhighlight %}

#### Setting values
You can simply assign values to `Currency` and `Value` properties to set the value.

{% highlight csharp startinline %}
Item myNewItem = new Item();

//A Money field with external_id 'money'
var moneyField = myNewItem.Field<MoneyItemField>("money");
moneyField.Currency = "EUR";
moneyField.Value = 250.50M;
{% endhighlight %}


------------------------------------------------------------------------------

## Number field

#### Getting values
The value of a number is of type `double?`.

{% highlight csharp startinline %}
var item = podio.ItemService.GetItemBasic(123);

// A Number field with external_id 'number'
NumericItemField numberField = item.Field<NumericItemField>("number");
double? number = numberField.Value;
{% endhighlight %}

#### Setting values
Simply assign Value property to set the value.

{% highlight csharp startinline %}
Item myNewItem = new Item();

NumericItemField numberField = myNewItem.Field<NumericItemField>("number");
numberField.Value = 567.89;
{% endhighlight %}


------------------------------------------------------------------------------

## Phone field

#### Getting values
Values are returned as a List of `PodioAPI.Utils.ItemFields.EmailPhoneFieldResult` object:

{% highlight csharp startinline %}
var item = podio.ItemService.GetItemBasic(123);

// A Phone field with external_id 'phone'
PhoneItemField phoneField = item.Field<PhoneItemField>("phone");
IEnumerable<EmailPhoneFieldResult> emails = phoneField.Value;
{% endhighlight %}

#### Setting values
Simply assign Value property with a List of `PodioAPI.Utils.ItemFields.EmailPhoneFieldResult`

{% highlight csharp startinline %}
Item myNewItem = new Item();

// A Phone field with external_id 'phone'
PhoneItemField phoneField = item.Field<PhoneItemField>("phone");
phoneField.Value = new List<EmailPhoneFieldResult>
{
    new EmailPhoneFieldResult { Type = "mobile", Value = "9847071777" },
    new EmailPhoneFieldResult { Type = "home", Value = "8954621419" },
};
{% endhighlight %}


------------------------------------------------------------------------------

## Progress field

#### Getting values
Progress fields return a integer between 0 and 100.

{% highlight csharp startinline %}
var item = podio.ItemService.GetItemBasic(123);

// A Progress field with external_id 'progress'
ProgressItemField progressField = item.Field<ProgressItemField>("progress");
int? progress = progressField.Value;
{% endhighlight %}

#### Setting values
Simply assign a new integer to set the value

{% highlight csharp startinline %}
Item myNewItem = new Item();

// A Progress field with external_id 'progress'
ProgressItemField progressField = myNewItem.Field<ProgressItemField>("progress");
progressField.Value = 70;
{% endhighlight %}


------------------------------------------------------------------------------

## Text field

#### Getting values
Text fields return a regular string

{% highlight csharp startinline %}
var item = podio.ItemService.GetItemBasic(123);

// Text field with external_id 'title'.
TextItemField titleField = item.Field<TextItemField>("title");
string text = titleField.Value;
{% endhighlight %}

#### Setting values
Simply assign the new string to set the value

{% highlight csharp startinline %}
Item myNewItem = new Item();

//A Text field with external_id 'title'
var textfield = myNewItem.Field<TextItemField>("title");
textfield.Value = "This is a text field";
{% endhighlight %}
