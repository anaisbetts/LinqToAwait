## LinqToAwait

In a world where many methods return `Task<T>` or other Awaitable types
(especially in WinRT), LINQ can't be used nearly as easily. Enter LinqToAwait:

```
var inputs = new[] { 
    "http://www.google.com", 
    "http://www.yahoo.com", 
    "http://www.aol.com", 
};

IEnumerable<string> results = await inputs.AsAsync()
    .WhereAsync(async x => await IsPageInTop10WebSitesByTrafficAsync(x))
    .SelectAsync(async x => await DownloadPageAsync(x))
    .GetResults();

>>> ["<html>...."]
```

## Where does this work?

Currently, you're going to need Visual Studio 11 - you can use LinqToAwait with both C# Metro-based applications, as well as standard .NET 4.5 applications.

## Hey, isn't this kind of like Rx?

It **is** Rx! However, it is a simplification of the API used for a more
specific use-case. Instead of choosing async/await *or* Rx, LinqToAwait helps
you use both at the same time, applying the most straightforward technique for
the particular problem.

## How do I get started?

It's .NET, how else? [Use NuGet!](http://nuget.org/packages/linqtoawait). There's a trick though: since Rx 2.0 is currently in beta, you need to install the package from the Powershell Console:

```
Install-Package LinqToAwait -pre
```
