---
layout: default
active: async
---

# Sync vs Async
Podio API Client for .NET comes in two flavors, [synchronous](https://www.nuget.org/packages/Podio/) and [asynchronous](https://www.nuget.org/packages/Podio.Async/)

## Which one to use?
If you're on .NET 4.5+, and finding that threads in your application are spending a significant percentage of CPU time waiting for API calls to complete, you should notice big improvements with asynchronous version.

Going only partially async is an [invitation for deadlocks](http://blog.stephencleary.com/2012/07/dont-block-on-async-code.html); you'll want to use async all the way up and down your call stack. 

## Is it faster than synchronous version?
No. But that's not the point of asynchronous code. The point is to free up threads while waiting on network or I/O bound work to complete, making desktop and mobile apps more responsive and web applications more scalable. The context switching magic wired up by the compiler when async/await are used actually adds a small amount of overhead to the running code.