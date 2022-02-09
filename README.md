# Groophy.Batch-Man

[![NuGet version (Groophy.Batch-Man)](https://img.shields.io/nuget/v/Groophy.Batch-Man.svg?style=flat-square)](https://www.nuget.org/packages/Groophy.Batch-Man/)

[Source Code](https://github.com/Groophy-Inc/Groophy.Batch-Man/blob/main/Groophy.Batch-Man/API.cs)

## Usage
```c#
using Groophy.Batch-Man;
using System.Collections.Generic; //If you're going to use a list, you need to add it.
```

### Get article(s)
```c#
List<article> a = Batch_Man_API.Get();
```
or
```c#
article[] a = Batch_Man_API.Get().ToArray();
```

### Example
```c#
            Stopwatch watcher = new Stopwatch(); //create timer
            watcher.Start(); //start timer
            List<article> a = Batch_Man_API.Get();
            watcher.Stop(); //stop timer

            Console.WriteLine("Article Count: " + a.Count + Environment.NewLine +
                "Ms: " + watcher.Elapsed.TotalMilliseconds + Environment.NewLine+
                "Example: " + Environment.NewLine +
                    "    Title: " + a[2].articletitle + "\n" +
                    "    Author: " + a[2].by + "\n" +
                    "    Categori: " + a[2].categorize + "\n" +
                    "    Date: " + a[2].date + "\n" +
                    "    Url: " + a[2].url + "\n" +
                    "    Desc: " + a[2].desc + "\n" +
                    "    GitHub Link: " + a[2].gitlink + "\n----------------------------------------\n");
```
 - the slowness is due to my internet speed

![exa](https://user-images.githubusercontent.com/77299279/153057864-57fa5e22-6573-4f06-a1e3-1791bf4656ec.PNG)

### Let's look at the whole list another way

```c#
            foreach (article b in a)
            {
                Console.WriteLine("Title: " + b.articletitle + "\n" +
                    "Author: " + b.by + "\n" +
                    "Categori:" + b.categorize + "\n" +
                    "Date: " + b.date + "\n" +
                    "Url: " + b.url + "\n" +
                    "Desc: " + b.desc + "\n" +
                    "GitHub Link: " + b.gitlink + "\n----------------------------------------\n");
            }
```

[Console Output 1.0.1](https://github.com/Groophy-Inc/Groophy.Batch-Man/blob/main/console_out.txt)

[Console Output 1.0.2](https://github.com/Groophy-Inc/Groophy.Batch-Man/blob/main/1.0.2_console_out.txt)
-----
### Change Logs
#### 1.0.0
A basic start
#### 1.0.1
``` &#8211; ```, ``` &amp; ```, ``` k\"> ```, ```</a``` fixed.
#### 1.0.2
``` &#8217; ``` fixed.
Multi page render added.


~Groophy Lifefor ' https://batch-man.com/
