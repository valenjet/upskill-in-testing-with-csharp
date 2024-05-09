# Chapter 2: Code Kata and TDD

In this chapter we introduce the concept of a _code kata_ and the fundamentals of test-driven development (TDD).

## What is a _code kata_?

_Code kata_ is a term borrowed from martial arts, referring to a practice method in programming where developers engage in repetitive exercises to hone their skills. The idea is to perform these small, self-contained coding challenges repeatedly, each time trying to improve in some way, whether it be in code efficiency, design patterns, speed, or understanding of the language and its features. The term was popularized by Dave Thomas, co-author of "The Pragmatic Programmer," who likened the practice to karate exercises aimed at continuous improvement and mastery of programming techniques.

Code katas are designed to focus on the fundamentals of coding and software design, encouraging developers to explore new strategies, techniques, and technologies in a controlled, practice-based environment. They are not about finding a single correct answer to a problem, but rather about exploring different solutions and approaches, learning from each attempt, and gradually improving one's craft. By regularly practicing Code katas, developers can enhance their problem-solving skills, deepen their understanding of programming paradigms, and become more proficient in their chosen languages and tools.

## What is Test-Driven Development?

Test-Driven Development (TDD) is a software development methodology where tests are written before the actual code. The process follows a short, repeatable development cycle designed to ensure that code meets its requirements and is of high quality. The typical TDD cycle follows three basic steps, often described as red-green-refactor:

1. **Red**: Write a failing test. Before adding or modifying a feature, a developer writes a test that defines a desired improvement or new function. This test will naturally fail since the feature hasn't been implemented yet.

2. **Green**: Write the minimal amount of code required to make the test pass. This step involves writing just enough code to make the test succeed, thus meeting the requirements specified by the test.

3. **Refactor**: Clean up the new code, if necessary. After the test passes, the developer can refactor the code to improve its structure, performance, or readability while ensuring that the test still passes. This step helps maintain code quality and reduce technical debt.

The benefits of TDD include:
- **Improved Code Quality**: TDD leads to code that's more thoroughly tested and less prone to bugs.
- **Better Design**: The need to write tests first encourages better software design and more maintainable code.
- **Documentation**: Tests serve as documentation that explains what the code is supposed to do.
- **Confidence**: Developers can make changes or refactor code with confidence that existing functionality is preserved, as indicated by tests passing.

TDD is part of the Agile development methodologies and encourages a disciplined approach to programming, focusing on requirements and continuous feedback through testing.

### TDD and Thinking

In "The Art of Agile Development", James Shore explains that the first step in the TDD cycle is "Think". You want to be sure that the following steps (Red, Green, Refactor) are based on an understanding of the problem. Not in the larger sense of the word "problem" but in the simplest increment of behavior.

With the perspective of focusing on small increments of behavior during the "Think" step, the Test-Driven Development (TDD) cycle can be described as follows:

1. **Think (small)**: Before writing any code or tests, carefully consider only the next small increment of behavior that needs to be implemented. This step involves understanding the specific piece of functionality or improvement to be added, without getting lost in big upfront designs. The goal is to focus on what's immediately necessary, determining the simplest test that can drive the development of this new behavior.

2. **Red**: Write a failing test that defines the expected outcome of the small increment of behavior identified in the Think step. This test should initially fail because the functionality it describes does not yet exist. Writing this failing test first ensures that any new code written is directly tied to an improvement in functionality and adheres closely to the requirements.

3. **Green**: Implement just enough code to pass the failing test written in the Red step. The emphasis here is on simplicity and effectiveness, aiming to quickly achieve a passing state for the test with minimal code. This encourages writing code that is directly relevant to the feature or fix being developed, without over-engineering or introducing unnecessary complexity.

4. **Refactor**: Once the test passes, examine the code for opportunities to improve its structure, readability, or performance without altering its external behavior. This may involve cleaning up redundancies, applying design patterns where appropriate, or simplifying complex logic. Refactoring with the safety net of tests ensures that improvements do not break existing functionality.

By emphasizing thinking in small increments, the TDD cycle promotes a focused, efficient approach to software development. It encourages developers to maintain a narrow scope, reducing the risk of scope creep or becoming overwhelmed by the broader system's complexities. This iterative process leads to a well-designed, well-tested codebase that evolves incrementally with each cycle, ensuring steady progress and high-quality outcomes.

In [this blog post](https://www.kaizenko.com/what-is-test-driven-development-tdd/), Fadi Stephan illustrates the TDD Cycle like this:

**TODO:** insert "The TDD Cycle" image

## Before We Start Coding

Let's look at the lines in our `TestScaleConverter.cs` file.

```csharp
using Physics.Temperature;

namespace Tests.Unit.Physics.Temperature;

public class TestScaleConverter
{
    [Test]
    [Ignore("Not yet started")]
    public void Test_FahrenheitToCelsius_When32f_Expect0c()
    {
        // Arrange
        var classUnderTest = new ScaleConverter();

        // Act
        var actual = classUnderTest.FahrenheitToCelsius(32);

        // Assert
        Assert.That(actual, Is.EqualTo(0));
    }
}
```

The `Ignore` attribute tells the NUnit runner to skip the `Test_FahrenheitToCelsius_When32f_Expect0c` test method. The `reason` parameter is optional, but it’s usually helpful to provide a reason.

The `Ignore` attribute is a better mechanism than commenting out the test or renaming methods, since the tests will be compiled with the rest of the code and there is an indication at run time that a test is not being run.

Running this command:
```bash
dotnet test
```

Returns the following output:
```bash
  Determining projects to restore...
  All projects are up-to-date for restore.
  TemperatureConverter -> /Users/ ... /code/ch02/TemperatureConverter/bin/Debug/net8.0/TemperatureConverter.dll
  Tests.TemperatureConverter -> /Users/ ... /code/ch02/Tests.TemperatureConverter/bin/Debug/net8.0/Tests.TemperatureConverter.dll
Test run for /Users/ ... /code/ch02/Tests.TemperatureConverter/bin/Debug/net8.0/Tests.TemperatureConverter.dll (.NETCoreApp,Version=v8.0)
Microsoft (R) Test Execution Command Line Tool Version 17.9.0 (arm64)
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.
  Skipped Test_FahrenheitToCelsius_When32f_Expect0c [1 ms]

Skipped! - Failed:     0, Passed:     0, Skipped:     1, Total:     1, Duration: 1 ms - Tests.TemperatureConverter.dll (net8.0)
```

Notice the message ... **Skipped!** ... **Skipped:     1**

The one test was skipped because the `Ignore` attribute is used as a tag to indicate this method should be skipped.

Okay, assuming you have the one skipped test, you are all set to start this code kata.

## Fahrenheit to Celsius Code Kata

Allow me to explain *why* we might want to build a Fahrenheit to Celsius converter.

I have several cousins who live in Ireland. Like much of the world, the local temperature there is reported using the Celsius scale. However, I live in the U.S. and our local temperature is reported using the Fahrenheit scale.

Occasionally, while my cousins and I are messaging, I'd like to convert today's temperature in Fahrenheit to Celsius, so that I can chat with them about the weather.

Let's write this need as a use case:
```text
As a friendly cousin,
I want to convert temperature in degrees Fahrenheit to degrees Celsius,
So that I can chat about the weather with my Irish cousins.
```

In this chapter, we will use TDD to write a C# program to convert a temperature in degrees Fahrenheit to its equivalent in degrees Celsius.

## Iteration 1

### Step 1: Think (🤔)

Don't just think; _think small_.

*Question:* What is the absolutely smallest behavior you can think of?

For me, the smallest behavior is about the freezing temperature. I know that if it's 32°F outside then I say it's freezing. And I know that that's 0°C.

Let's make that our first test.

We want to test a temperature converter function named `FahrenheitToCelsius` and check that it returns 0.

### Step 2: Test (red 🔴)

In this step, you want to have one (and only one) failing test.

We will remove the `Ignore` attribute on the `Test_FahrenheitToCelsius_When32f_Expect0c` method.

Remove that line and save the `TestScaleConverter.cs` file, so it looks like this:

```csharp
using Physics.Temperature;

namespace Tests.Unit.Physics.Temperature;

public class TestScaleConverter
{
    [Test]
    public void Test_FahrenheitToCelsius_When32f_Expect0c()
    {
        // Arrange
        var classUnderTest = new ScaleConverter();

        // Act
        var actual = classUnderTest.FahrenheitToCelsius(32);

        // Assert
        Assert.That(actual, Is.EqualTo(0));
    }
}
```


Running this command:
```bash
dotnet test
```

Returns the following output:
```bash
  Determining projects to restore...
  All projects are up-to-date for restore.
  TemperatureConverter -> /Users/ ... /code/ch02/TemperatureConverter/bin/Debug/net8.0/TemperatureConverter.dll
  Tests.TemperatureConverter -> /Users/ ... /code/ch02/Tests.TemperatureConverter/bin/Debug/net8.0/Tests.TemperatureConverter.dll
Test run for /Users/ ... /code/ch02/Tests.TemperatureConverter/bin/Debug/net8.0/Tests.TemperatureConverter.dll (.NETCoreApp,Version=v8.0)
Microsoft (R) Test Execution Command Line Tool Version 17.9.0 (arm64)
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.
  Failed Test_FahrenheitToCelsius_When32f_Expect0c [5 ms]
  Error Message:
   System.NotImplementedException : Not yet started!
  Stack Trace:
     at Physics.Temperature.ScaleConverter.FahrenheitToCelsius(Int32 value) in /Users/ ... /code/ch02/TemperatureConverter/ScaleConverter.cs:line 6
   at Tests.Unit.Physics.Temperature.TestScaleConverter.Test_FahrenheitToCelsius_When32f_Expect0c() in /Users/ ... /code/ch02/Tests.TemperatureConverter/TestScaleConverter.cs:line 14
   at System.RuntimeMethodHandle.InvokeMethod(Object target, Void** arguments, Signature sig, Boolean isConstructor)
   at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)


Failed!  - Failed:     1, Passed:     0, Skipped:     0, Total:     1, Duration: 5 ms - Tests.TemperatureConverter.dll (net8.0)
```

Notice the message ... **Failed!  - Failed:     1**

Your one test failed because you haven't created the implementation yet. See the *Error Message:* line `System.NotImplementedException : Not yet started!`

But that's a good thing; the test failed because **we expected it to fail**.

#### Quieter Test Runner Output 

Up until this point, the examples run the command `dotnet test` without any parameters. Let's use some parameters to generate quieter test runner output. To learn more about .NET test driver used to execute unit tests, see the article: [dotnet test](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-test)

Running this command:
```bash
dotnet test -v q --nologo
```

Returns the following output:
```bash
Test run for /Users/ ... /code/ch02/Tests.TemperatureConverter/bin/Debug/net8.0/Tests.TemperatureConverter.dll (.NETCoreApp,Version=v8.0)
Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Failed!  - Failed:     1, Passed:     0, Skipped:     0, Total:     1, Duration: 11 ms - Tests.TemperatureConverter.dll (net8.0)
```

This is a bit too quiet. There's very little detail about what failed and why it failed. 

Running this command:
```bash
dotnet test -v m --nologo
```

Returns the following output:
```bash
  Determining projects to restore...
  All projects are up-to-date for restore.
  TemperatureConverter -> /Users/ ... /code/ch02/TemperatureConverter/bin/Debug/net8.0/TemperatureConverter.dll
  Tests.TemperatureConverter -> /Users/ ... /code/ch02/Tests.TemperatureConverter/bin/Debug/net8.0/Tests.TemperatureConverter.dll
Test run for /Users/ ... /code/ch02/Tests.TemperatureConverter/bin/Debug/net8.0/Tests.TemperatureConverter.dll (.NETCoreApp,Version=v8.0)
Starting test execution, please wait...
A total of 1 test files matched the specified pattern.
  Failed Test_FahrenheitToCelsius_When32f_Expect0c [7 ms]
  Error Message:
   System.NotImplementedException : Not yet started!
  Stack Trace:
     at Physics.Temperature.ScaleConverter.FahrenheitToCelsius(Int32 value) in /Users/ ... /code/ch02/TemperatureConverter/ScaleConverter.cs:line 6
   at Tests.Unit.Physics.Temperature.TestScaleConverter.Test_FahrenheitToCelsius_When32f_Expect0c() in /Users/ ... /code/ch02/Tests.TemperatureConverter/TestScaleConverter.cs:line 14
   at System.RuntimeMethodHandle.InvokeMethod(Object target, Void** arguments, Signature sig, Boolean isConstructor)
   at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)


Failed!  - Failed:     1, Passed:     0, Skipped:     0, Total:     1, Duration: 7 ms - Tests.TemperatureConverter.dll (net8.0)
```

There is a lot of information before `Starting test execution, please wait...` that isn't relevant to the concepts in this chapter.

For the rest of this chapter, let's adopt this `minimum` output option, but the output will be shortened to only provide the lines after the `Starting test execution, please wait...` line.

#### Three Rules of TDD

We need to talk about the *Three Rules of TDD*. These three core rules are:

1. Only write production code to make a failing unit test pass.
2. Only write enough of a unit test to make it fail, including compilation errors.
3. Write only the necessary amount of production code required to pass the failing unit test.

To learn more: [Canon TDD](https://tidyfirst.substack.com/p/canon-tdd)


### Step 3: Code (green ✅)

To follow the three rules of TDD, we should revise the `FahrenheitToCelsius()` method only enough to make the failing unit test pass, as follows:

```csharp
namespace Physics.Temperature;

public class ScaleConverter
{
    public int FahrenheitToCelsius(int value){
        return 0;
    }
}
```

Running:
```bash
dotnet test -v m --nologo
```

Returns:
```bash
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:     1, Skipped:     0, Total:     1, Duration: 4 ms - Tests.TemperatureConverter.dll (net8.0)
```

### Huzzah!

There's the message in the output ... **Passed!**  ... **Passed:     1**

We have 1 passing test! 🎉


### Step 4: Refactor (tidy 🧹)

In this step, we want to clean up or refactor our code.

However, at this stage, the code is pretty clean.

Let's go on to the next iteration and back to thinking.

## Iteration 2

### I2 - Step 1: Think (🤔)

Again, we have to _think small_.

*Question:* What is the next smallest behavior you can think of?

For me, the next smallest behavior is about the boiling temperature of water. I know from science class that water boils at 212°F and at 100°C.

Let's make that our second test.

We want to test our `FahrenheitToCelsius()` method and check that it returns 100°C when we pass it 212°F.

To follow the three rules of TDD, only write enough of a unit test to make it fail.

Let's focus on adding one failing test

### I2 - Step 2: Test (red 🔴)

Let's add this failing test as a method of the `TestScaleConverter` class, in the `TestScaleConverter.cs` file:
```csharp
using Physics.Temperature;

namespace Tests.Unit.Physics.Temperature;

public class TestScaleConverter
{
    [Test]
    public void Test_FahrenheitToCelsius_When32f_Expect0c()
    {
        // Arrange
        var classUnderTest = new ScaleConverter();

        // Act
        var actual = classUnderTest.FahrenheitToCelsius(32);

        // Assert
        Assert.That(actual, Is.EqualTo(0));
    }

    [Test]
    public void Test_FahrenheitToCelsius_When212f_Expect100c()
    {
        // Arrange
        var classUnderTest = new ScaleConverter();

        // Act
        var actual = classUnderTest.FahrenheitToCelsius(212);

        // Assert
        Assert.That(actual, Is.EqualTo(100));
    }
}
```

Save the `TestScaleConverter.cs` file.

Running:
```bash
dotnet test -v m --nologo
```

Returns:
```bash
A total of 1 test files matched the specified pattern.
  Failed Test_FahrenheitToCelsius_When212f_Expect100c [17 ms]
  Error Message:
     Expected: 100
  But was:  0

  Stack Trace:
     at Tests.Unit.Physics.Temperature.TestScaleConverter.Test_FahrenheitToCelsius_When212f_Expect100c() in /Users/ ... /code/ch02/Tests.TemperatureConverter/TestScaleConverter.cs:line 30

1)    at Tests.Unit.Physics.Temperature.TestScaleConverter.Test_FahrenheitToCelsius_When212f_Expect100c() in /Users/ ... /code/ch02/Tests.TemperatureConverter/TestScaleConverter.cs:line 30



Failed!  - Failed:     1, Passed:     1, Skipped:     0, Total:     2, Duration: 18 ms - Tests.TemperatureConverter.dll (net8.0)
```

We can see
* `Passed:     1`, as expected.
* `Failed:     1`, also as expected.

What failed is:
```bash
  Failed Test_FahrenheitToCelsius_When212f_Expect100c [17 ms]
  Error Message:
     Expected: 100
  But was:  0
```

This is a good thing! This is what was expected.

### I2 - Step 3: Code (green ✅)

To follow the three rules of TDD, we should revise the `FahrenheitToCelsius()` method only enough to make the failing unit test pass.

The simplest thing that could possibly work might be to look for 212 and return 100; otherwise return 0.

In the `ScaleConverter.cs` file, make that change as follows:

```csharp
namespace Physics.Temperature;

public class ScaleConverter
{
    public int FahrenheitToCelsius(int value){
        if(value == 212){
            return 100;
        }
        return 0;
    }
}
```

Save the `ScaleConverter.cs` file.

Now, running:
```bash
dotnet test -v m --nologo
```

Returns:
```bash
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:     2, Skipped:     0, Total:     2, Duration: 5 ms - Tests.TemperatureConverter.dll (net8.0)
```


### Huzzah!

There's the message in the output ... **Passed!**  ... **Passed:     2**

We have 2 passing tests! 🎉


### I2 - Step 4: Refactor (tidy 🧹)

Again, nothing says we have to refactor. If we're happy with everything as it stands, we can certainly move on to the next iteration and Step 1.


## Iteration 3

Let's start thinking again.

### I3 - Step 1: Think (🤔)

Although we alway want to _think small_, as we perform TDD our understanding grows. Our thinking can grow as our understanding grows.

The key concept with thinking: **keep the incremental changes small**

Based on some common knowledge and a bit of temperature conversion trivia, we may know these facts:
* 0°C = 32°F
* 100°C = 212°F
* -40°C = -40°F

With that information, we can create a "truth table".

| Fahrenheit   | Celsius     |
|-------------:|------------:|
|       32°F   |       0°C   |
|      212°F   |     100°C   |
|      -40°F   |     -40°C   |

The first two _truths_ are already tested. Let's test the third truth.

### I3 - Step 2: Test (red 🔴)

Let's add this failing test to the bottom of the `TestScaleConverter.cs` file:

```csharp
using Physics.Temperature;

namespace Tests.Unit.Physics.Temperature;

public class TestScaleConverter
{
...

    [Test]
    public void Test_FahrenheitToCelsius_WhenMinus40f_ExpectMinus40c()
    {
        // Arrange
        var classUnderTest = new ScaleConverter();

        // Act
        var actual = classUnderTest.FahrenheitToCelsius(-40);

        // Assert
        Assert.That(actual, Is.EqualTo(-40));
    }
}
```

Save the `TestScaleConverter.cs` file.

Running:
```bash
dotnet test -v m --nologo
```

Returns:
```bash
A total of 1 test files matched the specified pattern.
  Failed Test_FahrenheitToCelsius_WhenMinus40f_ExpectMinus40c [20 ms]
  Error Message:
     Expected: -40
  But was:  0

  Stack Trace:
     at Tests.Unit.Physics.Temperature.TestScaleConverter.Test_FahrenheitToCelsius_WhenMinus40f_ExpectMinus40c() in /Users/ ... /code/ch02/Tests.TemperatureConverter/TestScaleConverter.cs:line 43

1)    at Tests.Unit.Physics.Temperature.TestScaleConverter.Test_FahrenheitToCelsius_WhenMinus40f_ExpectMinus40c() in /Users/ ... /code/ch02/Tests.TemperatureConverter/TestScaleConverter.cs:line 43



Failed!  - Failed:     1, Passed:     2, Skipped:     0, Total:     3, Duration: 26 ms - Tests.TemperatureConverter.dll (net8.0)
```

As expected, only `Test_FahrenheitToCelsius_WhenMinus40f_ExpectMinus40c` failed.


### I3 - Step 3: Code (green ✅)

To follow the third rule of TDD (*Write only the necessary amount of production code required to pass the failing unit test*), let's write just enough code to make all the tests pass.

This code under test is in the `ScaleConverter.cs` file.

To pass this new test, the code might look like this:

```csharp
namespace Physics.Temperature;

public class ScaleConverter
{
    public int FahrenheitToCelsius(int value){
        if(value == 212){
            return 100;
        }
        if(value == -40){
            return -40;
        }
        return 0;
    }
}
```

Save the `ScaleConverter.cs` file.

Now, running:
```bash
dotnet test -v m --nologo
```

Returns:
```bash
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:     3, Skipped:     0, Total:     3, Duration: 6 ms - Tests.TemperatureConverter.dll (net8.0)
```


### Huzzah!

There's the message in the output ... **Passed!**  ... **Passed:     3**

We have 3 passing tests! 🎉




### I3 - Step 4: Refactor (tidy 🧹)

Let's skip the refactoring step and move to the next iteration.


## Iteration 4


### I4 - Step 1: Think (🤔)

From a conversion chart we find on the internet, we might learn another fact:
* -20°C = -4°F

Let's add that to our "truth table".

| Fahrenheit   | Celsius     |    |
|-------------:|------------:|---:|
|       32°F   |       0°C   | ✅ |
|      212°F   |     100°C   | ✅ |
|      -40°F   |     -40°C   | ✅ |
|       -4°F   |     -20°C   | 🤔 |

The first three _truths_ are already tested. Let's test that next truth.


### I3 - Step 2: Test (red 🔴)

Let's add this failing test to the `TestScaleConverter.cs` file.

NOTE: Each new test method is added to the `TestScaleConverter` class. By convention, the new test method is added as the last public methods in the class. For the sake of brevity, only the new test method will be listed.

That new test method might look like this:
```csharp
[Test]
public void Test_FahrenheitToCelsius_WhenMinus4f_ExpectMinus20c()
{
    // Arrange
    var classUnderTest = new ScaleConverter();

    // Act
    var actual = classUnderTest.FahrenheitToCelsius(-4);

    // Assert
    Assert.That(actual, Is.EqualTo(-20));
}
```

Save the `TestScaleConverter.cs` file.

Now, running:
```bash
dotnet test -v m --nologo
```

Returns:
```bash
A total of 1 test files matched the specified pattern.
  Failed Test_FahrenheitToCelsius_WhenMinus4f_ExpectMinus20c [13 ms]
  Error Message:
     Expected: -20
  But was:  0

  Stack Trace:
     at Tests.Unit.Physics.Temperature.TestScaleConverter.Test_FahrenheitToCelsius_WhenMinus4f_ExpectMinus20c() in /Users/sdr/training/valenjet/upskill-in-testing-with-csharp/sols/ch02/Tests.TemperatureConverter/TestScaleConverter.cs:line 56

1)    at Tests.Unit.Physics.Temperature.TestScaleConverter.Test_FahrenheitToCelsius_WhenMinus4f_ExpectMinus20c() in /Users/sdr/training/valenjet/upskill-in-testing-with-csharp/sols/ch02/Tests.TemperatureConverter/TestScaleConverter.cs:line 56



Failed!  - Failed:     1, Passed:     3, Skipped:     0, Total:     4, Duration: 19 ms - Tests.TemperatureConverter.dll (net8.0)
```


### I3 - Step 3: Code (green 🟩)

Let's write just enough code to make all the tests pass.

And that looks like this:
```csharp
namespace Physics.Temperature;

public class ScaleConverter
{
    public int FahrenheitToCelsius(int value){
        if(value == 212){
            return 100;
        }
        if(value == -40){
            return -40;
        }
        if(value == -4){
            return -20;
        }
        return 0;
    }
}
```

Running:
```bash
dotnet test -v m --nologo
```

Returns:
```bash
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:     4, Skipped:     0, Total:     4, Duration: 5 ms - Tests.TemperatureConverter.dll (net8.0)
```

All four tests are passing!

And the truth table looks like this:

| Fahrenheit   | Celsius     |    |
|-------------:|------------:|---:|
|       32°F   |       0°C   | ✅ |
|      212°F   |     100°C   | ✅ |
|      -40°F   |     -40°C   | ✅ |
|       -4°F   |     -20°C   | ✅ |


### I3 - Step 4: Refactor (tidy 🧹)

We could go on forever with the truth table concept, but we want to handle any arbitrary temperature in degrees Fahrenheit and convert it to the correct degrees Celsius.

Wiktionary defines [refactor](https://en.wiktionary.org/wiki/refactor) as:
> To rewrite existing source code in order to improve its readability, reusability or structure without affecting its meaning or behavior.

So, this is the right place to restructure the code-under-test so as to improve it without altering it's functionality. For this, we might like to have a mathematical formula.

Let's ask the team's Business Analyst to provide us with the formula.

Here is what we get:
```text
To convert temperatures in degrees Fahrenheit to Celsius, 
multiply by 9, divide by 5, and add 32.

celsius = ((fahrenheit * 9) / 5) + 32

Example: ((-40°F * 9) / 5) + 32 = -40°C
```

And we code it up as follows:
```csharp
namespace Physics.Temperature;

public class ScaleConverter
{
    public int FahrenheitToCelsius(int value){
        return ((value * 9) / 5) + 32;
    }
}
```

Save the `ScaleConverter.cs` file.

Running:
```bash
dotnet test -v m --nologo
```

Returns:
```bash
A total of 1 test files matched the specified pattern.
  Failed Test_FahrenheitToCelsius_When212f_Expect100c [17 ms]
  Error Message:
     Expected: 100
  But was:  413

  Stack Trace:
     at Tests.Unit.Physics.Temperature.TestScaleConverter.Test_FahrenheitToCelsius_When212f_Expect100c() in /Users/sdr/training/valenjet/upskill-in-testing-with-csharp/sols/ch02/Tests.TemperatureConverter/TestScaleConverter.cs:line 30

1)    at Tests.Unit.Physics.Temperature.TestScaleConverter.Test_FahrenheitToCelsius_When212f_Expect100c() in /Users/sdr/training/valenjet/upskill-in-testing-with-csharp/sols/ch02/Tests.TemperatureConverter/TestScaleConverter.cs:line 30


  Failed Test_FahrenheitToCelsius_When32f_Expect0c [< 1 ms]
  Error Message:
     Expected: 0
  But was:  89

  Stack Trace:
     at Tests.Unit.Physics.Temperature.TestScaleConverter.Test_FahrenheitToCelsius_When32f_Expect0c() in /Users/sdr/training/valenjet/upskill-in-testing-with-csharp/sols/ch02/Tests.TemperatureConverter/TestScaleConverter.cs:line 17

1)    at Tests.Unit.Physics.Temperature.TestScaleConverter.Test_FahrenheitToCelsius_When32f_Expect0c() in /Users/sdr/training/valenjet/upskill-in-testing-with-csharp/sols/ch02/Tests.TemperatureConverter/TestScaleConverter.cs:line 17


  Failed Test_FahrenheitToCelsius_WhenMinus4f_ExpectMinus20c [< 1 ms]
  Error Message:
     Expected: -20
  But was:  25

  Stack Trace:
     at Tests.Unit.Physics.Temperature.TestScaleConverter.Test_FahrenheitToCelsius_WhenMinus4f_ExpectMinus20c() in /Users/sdr/training/valenjet/upskill-in-testing-with-csharp/sols/ch02/Tests.TemperatureConverter/TestScaleConverter.cs:line 56

1)    at Tests.Unit.Physics.Temperature.TestScaleConverter.Test_FahrenheitToCelsius_WhenMinus4f_ExpectMinus20c() in /Users/sdr/training/valenjet/upskill-in-testing-with-csharp/sols/ch02/Tests.TemperatureConverter/TestScaleConverter.cs:line 56



Failed!  - Failed:     3, Passed:     1, Skipped:     0, Total:     4, Duration: 18 ms - Tests.TemperatureConverter.dll (net8.0)
```

Oh no! 🤯

We got 3 failed tests! What happened!?!


#### Requirement Defect

Here's what happened: _we uncovered a defect in the formula_ (a.k.a. a requirement defect).

The Business Analyst unwittingly (and we assume with the best of intentions) provided us the incorrect formula.

But the good news, and the beauty of TDD, is that our failing tests revealed the issue.

Our tests were built up from known truths. They helped to guard us from moving forward with a defective formula.

Now, let's go back to that Business Analyst.

We should be able to use a calculator to show that the formula does not work for every truth in our truth table.


#### Correct Formula

Now that that's all cleared up, we receive the correct formula.

The correct formula specification is provided as follows:
```text
To convert temperatures from Fahrenheit to Celsius

celsius = (5 * (fahrenheit - 32)) / 9

Example: ((50°F - 32) * 5) / 9 = 10°C
```

And we code it in the `ScaleConverter.cs` file, up as follows:
```csharp
namespace Physics.Temperature;

public class ScaleConverter
{
    public int FahrenheitToCelsius(int value){
        return (5 * (value -32)) / 9;
    }
}
```

Save the `ScaleConverter.cs` file.

Running:
```bash
dotnet test -v m --nologo
```

Returns:
```bash
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:     4, Skipped:     0, Total:     4, Duration: 5 ms - Tests.TemperatureConverter.dll (net8.0)
```


### Huzzah!

There's the message in the output ... **Passed!**  ... **Passed:     4**

We have 4 passing tests! 🎉

