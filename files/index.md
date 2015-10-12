---
layout: default
active: files
---
# Working with files
Files can be uploaded to Podio, but they don't stand on their own, they are always attached to another object. Files can be attached to Items, Tasks, Comments, and Status Messages. The maximum allowed file size is currently 100MB and there are a few types of files that you can't upload at all. The [documentation for the Files area](https://developers.nextpodio.dk/doc/files) has a list of all disallowed file types

## File uploads
When you want to attach a file to an object (e.g. a comment, status message etc.) you first need to upload the file to Podio and obtain a FileAttachment object. To upload a file use the `UploadFile` method on `FileService` class:

{% highlight csharp startinline %}
string filePath = Server.MapPath("\\files\\report.pdf");
FileAttachment file = podio.FileService.UploadFile(filePath, "report.pdf");

Response.Write("File uploaded. The file id is: " + file.FileId);
{% endhighlight %}

## File downloads
To download a file you must first procure a `FileAttachment` object. If you already have a `PodioItem`, `PodioComment` etc. object you probably already have the file object. Otherwise you have to get it manually. After that use the `DownloadFile` method in `FileService` class:

{% highlight csharp startinline %}
// Get the file object. Only necessary if you don't already have it!
FileAttachment file = podio.FileService.GetFile(fileId);

// Download the file. This might take a while...
FileResponse fileResponse = podio.FileService.DownloadFile(file);

// Store the file on local disk
string filePath = Server.MapPath("/Files/" + file.Name);
System.IO.File.WriteAllBytes(filePath, fileResponse.FileContents);
{% endhighlight %}

