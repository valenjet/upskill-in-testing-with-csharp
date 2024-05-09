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

For the rest of this chapter, let's adopt this quieter output.

#### Three Rules of TDD

We need to talk about the *Three Rules of TDD*. These three core rules are:

1. Only write production code to make a failing unit test pass.
2. Only write enough of a unit test to make it fail, including compilation errors.
3. Write only the necessary amount of production code required to pass the failing unit test.

To learn more: [Canon TDD](https://tidyfirst.substack.com/p/canon-tdd)


### Step 3: Code (green 🍏)

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
dotnet test -v q --nologo
```

Returns:
```bash
Test run for /Users/ ... /code/ch02/Tests.TemperatureConverter/bin/Debug/net8.0/Tests.TemperatureConverter.dll (.NETCoreApp,Version=v8.0)
Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:     1, Skipped:     0, Total:     1, Duration: 5 ms - Tests.TemperatureConverter.dll (net8.0)
```

### Huzzah!

There's the message in the output ... **Passed!**  ... **Passed:     1**

We have 1 passing test! 🎉

