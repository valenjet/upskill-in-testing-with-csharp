# Chapter 1: Getting Started

## Prerequisites

To follow along with the examples in this book, you need the following:
* A good working knowledge of the C# language.
  - *Neither C# nor .NET is explained in detail in this book.*
* This book was written using `zsh` on a Mac. The commands are mostly limited to `cd` to change director, `ls` to list files, and the `dotnet` command line program to run the tests.
* The IDE used is *Visual Studio Code* (VS Code) with the *C# Dev Kit* extension.

### Fundamentals of C#

Jeff Fritz has an innovative and comprehensive video training series designed to guide you through the fundamentals of the C# programming language. To learn more about the series, go to [C# in the Cards](https://csharpinthecards.com)

## Get C# Running Locally

You can get all of the things you need to build and test with C# locally on your Mac or PC.
* [.NET 8.0](https://dotnet.microsoft.com/en-us/download) or later
* [Visual Studio Code](https://code.visualstudio.com/) version 1.88 or later
* [C# Dev Kit](https://learn.microsoft.com/en-us/visualstudio/subscriptions/vs-c-sharp-dev-kit) extension

There are instructions at [Testing in C# in Visual Studio Code](https://code.visualstudio.com/docs/languages/dotnet)

## Unit testing C# with NUnit and .NET Core

This chapter walks you through the experience of building a sample solution step-by-step to learn get started with automated testing. If you prefer to follow the tutorial using a pre-built solution, view or download the sample code before you begin.

Before proceeding, let's ensure that you have .NET installed on your system.

## macOS Terminal and VS Code

This book was written using `zsh` on a Mac. The commands are mostly limited to _cd_ to change director, _ls_ to list files, and the _dotnet_ command to run the tests.

You can open the Terminal application by searching for it using Spotlight (press `Cmd + Space` and type "Terminal"), or by finding it in the Applications > Utilities folder.

### Windows and flavors of Unix

This book won't show you how to use Windows and the various flavors of Unix. The book sticks to the Mac and avoids the command names that are different, the forward slashes (/) instead of back slashes (\\), and the one file hierarchy vs many file hierarchies rooted in different drive letters.

If you know your Windows or Unix shell, all examples should be runnable on the terminal-like application that you choose to use.

### Another IDE

The IDE used is *Visual Studio Code* (VS Code) with the *C# Dev Kit* extension. This book won't show you how to use another integrated development environment (IDE). If you know your IDE, all examples should be runnable in the IDE that you choose to use.

## Creating the GettingStarted project

If you'd like to practice and follow the examples, create a directory locally for you to follow along. Then you can follow these steps.

### 1. Open Terminal

Open a Terminal window and navigate to the local directory that you created to follow along.

The code examples will use the following directory structure:
```zsh
.
└── code
    ├── ch01
    ├── ch02
    ├── ch03
    ├── ch04
    ├── ch05
    ├── ch06
    └── ch07
...
```

For now, make the directories and more
```zsh
$ mkdir code
$ cd code
$ mkdir ch01
$ cd code/ch01
```

NOTE: For the examples in the rest of the book, we’ll save a little clutter by dropping the `$` symbol the shows the command prompt.

### 3. Check the .NET version

Inside the `ch01` directory, run the following command to check the .NET version.

```zsh
dotnet --version
```

The output should show a value of `8.0` or greater. If not, double-check your .NET installation.

### 2. Create a new solution file

Inside the `ch01` directory, run the following command to create a new solution file for the class library and the test project:

```zsh
dotnet new sln
```

The output should show:
*The template "Solution File" was created successfully.*

Next, create a *GettingStarted* directory.

The following outline shows the directory and file structure so far:

```zsh
.
└── ch01
    ├── GettingStarted
    └── ch01.sln
```

Now, use the `cd` command to make *GettingStarted* the current directory.

Run the following command to create the source project:

```zsh
dotnet new classlib
```

Rename the *Class1.cs* file to *GettingStarted.cs*, using the following command:

```zsh
mv Class1.cs GettingStarted.cs
```

Change the directory back to the *ch01* directory. The following outline shows the directory and file structure so far:

```zsh
.
└── ch01
    ├── GettingStarted
    │   ├── GettingStarted.cs
    │   └── GettingStarted.csproj
    └── ch01.sln
```

Run the following command to add the class library project to the solution:

```zsh
dotnet sln add GettingStarted/GettingStarted.csproj
```

The output should show:
*Project `GettingStarted/GettingStarted.csproj` added to the solution.*


### 3. Open VS Code

For these examples, the IDE used is [Visual Studio Code on macOS](https://code.visualstudio.com/docs/setup/mac). The explanations run VS Code from the terminal by typing `code` because VS Code was added to the path.

If your terminal directory is in the right place, running the `pwd` command should return a path like this:

```zsh
$ pwd
/Users/sdr/upskill-in-testing-with-csharp/code/ch01
```

To open VS Code in the current directory, run the following command.

```zsh
code .
```

### 4. Revamp the *GettingStarted* class

Open the the *GettingStarted.cs* file and create a failing implementation of the *GettingStarted* class:

```csharp
using System;

namespace Getting.Started
{
    public class GettingStarted
    {
        public bool IsPassing()
        {
            throw new NotImplementedException("Not there yet!");
        }
    }
}
```

## Creating the test project

Next, return to the Terminal and create the *Tests.GettingStarted* directory with the `mkdir` command.

The following outline shows the directory and file structure so far:
```zsh
.
└── ch01
    ├── GettingStarted
    │   ├── GettingStarted.cs
    │   └── GettingStarted.csproj
    ├── Tests.GettingStarted
    └── ch01.sln
```

Make the *Tests.GettingStarted* directory the current directory

Create a new project using the following command:

```zsh
dotnet new nunit
```

The [dotnet new](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new) command creates a test project that uses *NUnit* as the test library.

The test project requires other packages to create and run unit tests. The `dotnet new` command in the previous step added the Microsoft test SDK, the NUnit test framework, and the NUnit test adapter.

The generated template configures the test runner in the *Tests.GettingStarted.csproj* file. You should see something like this:

```xml
<ItemGroup>
  <PackageReference Include="nunit" Version="4.1.0" />
  <PackageReference Include="NUnit3TestAdapter" Version="4.5.0" />
  <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.9.0" />
  <PackageReference Include="NUnit.Analyzers" Version="4.1.0">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
  </PackageReference>
</ItemGroup>
```

Now, add the *GettingStarted* class library as another dependency to the project. Use the `dotnet add reference` command, as follows:

```zsh
dotnet add reference ../GettingStarted/GettingStarted.csproj
```

That command should return:
```zsh
Reference `..\GettingStarted\GettingStarted.csproj` added to the project.
```

You can see the entire file under `code/ch01` in the book's [companion repository](https://github.com/valenjet/upskill-in-testing-with-csharp) on GitHub.

The following outline shows the final solution layout:

```zsh
.
└── ch01
    ├── GettingStarted
    │   ├── GettingStarted.cs
    │   └── GettingStarted.csproj
    ├── Tests.GettingStarted
    │   ├── Tests.GettingStarted.csproj
    │   └── UnitTest1.cs
    └── ch01.sln
```

Finally, to add the *Tests.GettingStarted* project to the solution, execute the following command in the *ch01* directory:

```zsh
dotnet sln add ./Tests.GettingStarted/Tests.GettingStarted.csproj
```

## Creating your first test

In the *Tests.GettingStarted* directory, rename the *UnitTest1.cs* file to *GettingStartedTests.cs* and replace its entire contents with the following code:

```csharp
using Getting.Started;

namespace Tests.Getting.Started;

public class GettingStartedTests
{
    [Test]
    public void IsPassing_WhenCalled_ExpectTrue()
    {
        // Arrange
        var classUnderTest = new GettingStarted();

        // Act
        var actual = classUnderTest.IsPassing();

        // Assert
        Assert.That(actual, Is.True);
    }
}
```

The `[Test]` attribute indicates that method of the class is a test method.

Save all files and execute the `dotnet test` command to build the tests and the class library and run the tests, as follows:

```zsh
dotnet test
```

You should output that includes these messages:

```zsh
...

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.
  Failed IsPassing_WhenCalled_ExpectTrue [4 ms]
  Error Message:
   System.NotImplementedException : Not there yet!

...

Failed!  - Failed:     1, Passed:     0, Skipped:     0, Total:     1, Duration: 4 ms - Tests.GettingStarted.dll (net8.0)
```

Your test fails. You haven't created the implementation yet.

So, the test failed because **we expected it to fail**.

## Why start with a failing test?

Test-Driven Development (TDD) is an approach where the developer writes an automated test before coding. TDD starts with a failing test. This first test must fail because no code-under-test exists.

Kent Beck, a pioneer of Extreme Programming (XP) and TDD, emphasizes the importance of small, iterative development steps. In _Extreme Programming Explained: Embrace Change_ he says, ["If you're having trouble succeeding, fail"](https://quotefancy.com/kent-beck-quotes). This philosophy underpins the TDD approach of writing a failing test before any functional code, promoting simplicity and continuous feedback.

Let's embrace the *fail our way to success* philosophy.


### Do you really _feel the need_ to make that test pass?

If you feel the need to make the `IsPassing_WhenCalled_ExpectTrue()` test pass ...

Make the test pass by writing the simplest code in the *GettingStarted* class that works:

```csharp
using System;

namespace Getting.Started
{
    public class GettingStarted
    {
        public bool IsPassing()
        {
            return true;
        }
    }
}
```

Save all files and execute the `dotnet test` command to build the tests and the class library and run the tests, as follows:

```zsh
dotnet test
```

You should output that includes these messages:

```zsh
...

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:     1, Skipped:     0, Total:     1, Duration: 1 ms - Tests.GettingStarted.dll (net8.0)
```

And, there it is ... "1 passed"
