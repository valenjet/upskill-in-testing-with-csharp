# Chapter 3: Automated Testing

This chapter is *not* about the usual QA-style automated testing.

The automated testing presented is about ensuring that the developer's code works as the developer expects. Think of "testing" as us verify and validating the intentions behind our code. Many experts refer to this as "checking" instead of testing.

In the last chapter, we took a dive into test-driven development (TDD) and the red-green-refactor cycle. There is a wealth of information out there in books, blogs, and other online resources. Turn to Appendix A for lists of resources, including TDD and test-writing guides.

This chapter focuses on improving your testing skills with an emphasis on:
- **Clarity:** Writing test code that is self-explanatory
- **Durability:** Ensuring your tests remain effective and useful over time
- **Hands-off:** Developing tests that run consistently well and without intervention

Automated testing is invaluable for catching issues early, preventing future changes from causing problems, ensuring code components work well together, and validating code quality from the start.

We'll explore various types of testing, including:
- **Unit testing:** The foundation of automated tests
- **Seam testing:** A technique we'll touch upon here and delve into later
- **Integration testing:** Automating the integration process
- **Smoke testing:** Quick checks for basic functionality
- **Stability and performance tests:** Keeping things stable and efficient
- **Database testing:** Ensuring that the persistence layer has integrity

The realm of automated testing is vast, and while we can't explore every technique, understanding the core principles will enable you to apply them effectively.

The key points to focus on:
- Prioritize maintainable test code
- Choose descriptive names for your tests
- Follow the Arrange-Act-Assert pattern for clarity
- Keep tests short and focused
- Limit to one action per test
- Test one primary thing (assertion) per test
- Avoid type hints in test code
- Use a `TestsContext` class to organize tests
- Utilize `TestHelper` classes for reusable test code
- Implement data-driven tests to cover more scenarios

Let's get to it and make our test code as robust as our Production code.


## Maintainable Test Code

Let's discuss an often-overlooked aspect of software development: ensuring our test code is not only functional but also readable and maintainable. It’s a common scenario: after investing significant effort into writing tests, they become neglected because they’re overly complex or prone to frequent failures. This can be incredibly frustrating and may lead to questioning the value of the effort.

### Test Code Maintainability

> Focus on Writing Maintainable Test Code

One of the primary reasons test code falls by the wayside is its difficulty to maintain. We've all encountered test code that feels hastily assembled and is challenging to decipher or repair. However, test code, like any other code, requires careful attention to remain effective. Testing a single method may necessitate multiple test cases, and if each case is burdensome to maintain, the process becomes tedious.

The solution lies in maintaining clean and manageable test code. When test code is straightforward to work with, it remains valuable and avoids becoming a drain on resources. This section focuses on strategies to keep your test code well-organized and efficient, ensuring it supports rather than hinders your projects. By following these guidelines, we can prevent our test code from becoming obsolete and keep it as functional and relevant as when it was first written.

Here are several practices to ensure maintainable test code:
- **Adopt an effective naming convention** for test namespaces, classes, and methods.
- **Use a consistent pattern** for the test method body.
- **Keep test methods concise;** aim for fewer than ten lines of code.
- **Limit test actions** to one or two lines of code.
- **Make a single primary assertion** in each test method.
- **Create a context class** to encapsulate repetitive arrangement code.
- **Develop helper classes** to hold common code used across the test suite.
- **Implement data-driven testing** by passing data as arguments into the test methods.

By adhering to these practices, we can ensure that our test code remains maintainable and continues to provide value over time.

### Naming Convention

> Adopt an Effective Naming Convention

Diving into the topic of naming standards for unit tests, you'll find common patterns described by Roy Osherove in his book [The Art of Unit Testing](https://www.manning.com/books/the-art-of-unit-testing-third-edition). Microsoft provides their take on the topic in [Unit testing best practices with .NET Core and .NET Standard](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices). I fully endorse what is described in these two resource.

However, while learning how to use `pytest` (a Python testing framework), I read about the [Conventions for Python test discovery](https://docs.pytest.org/en/latest/explanation/goodpractices.html#conventions-for-python-test-discovery) that `pytest` uses.

Based on the `pytest` naming standards, I've augmented my approach to the .NET test code naming standards.

I recommend that:
* The name of the test namespace and assembly should be:
  - Saved under a `tests/` folder that parallels the code-under-test `src/` folder
  - A class library project prefixed with `Tests.` and should parallel the project of the code-under-test
  - Namespaces prefixed with `Tests.` that parallel the namespaces of the code-under-test
  - Projects that build assemblies with names prefixed with `Tests.`
* The name of the test class should be `Test` prefixed test classes in `Test*.cs` files.
* The name of the test method should be `Test_` prefixed test methods inside `Test` prefixed test classes.

I call this the "Test, Test, Test" approach. It enhances the recommendations of others by ensuring that "Test" is always in front.


#### Test Methods

`NUnit` collects test items from within .NET assemblies references or "discovered". Within each assembly, the testing framework looks for methods that are marked with either the `[Test]`, `[TestCase]`, or `[TestCaseSource]` attribute. This is how `NUnit` looks at the test code.

For the person looking at the test code, three important facts need to be clear from the test method's name:
1. The name of the method being tested (i.e., the method-under-test)
2. Conditions (or scenario) under which the test is performed
3. Expected result (or behavior) needed to pass the test

The test method naming convention fits a readability pattern and clearly communicates the intention of the test. The following are examples of test methods that follow the naming convention:

* `Test_ComputePayment_WithProvidedLoanData_ExpectProperAmount`
* `Test_ApproveLoan_WhenLoanDataIsValid_ExpectLoanSaveCalledExactlyOnce`
* `Test_ComputeBalance_ForNegativeBalanceScenarios_ExpectOutOfRangeException`

This test method naming convention is used by the code samples and throughout the book.

The important principle is to establish a naming convention that clearly states how the system-under-test is expected to behave under the conditions arranged by the test.

#### Namespaces

The naming convention starts by using the same namespace as the class-under-test and adds a prefix. The prefix has two terms; the word `Tests` and a category for the tests. Both terms are separated by a period. Categories that you might use include names like `Unit`, `Surface`, or `Stability`. An example of the name of an assembly with integration tests is `Tests.Integration.Lender.Slos.Dal.dll`.

Adding the word `Tests` to the beginning of every namespace may seem redundant; however, it is important for several key reasons. Test assemblies that begin with `Tests.*` ... 
* Are clearly understood as being for testing use only.
* Can easily be sorted from production assemblies, allowing you to prevent test assemblies from going into production.
* Can be discovered by convention in build scripts and other CI techniques.
* Can be excluded, through the use of wild-carding, from code coverage tools and other program analysis.

The category name is also important because various testing categories have different setup and configuration requirements. The namespace lets everyone know what to expect:
* `Tests.Unit.*` require absolutely no setup or configuration and must execute quickly. Unit tests must always run as automated tests.
* `Tests.Integration.*` require significant setup and configuration. These tests should be run manually.
* `Tests.Seam.*` are specialized automated integration tests, called seam tests. These require a one-time setup and configuration, but can be run by developers as automated integration tests.
* `Tests.Stability.*` require setup and configuration and are normally performed after deployment on an integration server. These tests should run automatically by the CI server.

Choose a test project and assembly name to be consistent with these namespaces to reap the benefits of this convention.

#### Test Classes

When maintaining test code, you will need to quickly locate all tests for a specific class. In order to do this, take the name of the class you want to write tests for and, in the test project, create a *Test Class* with the same name prefixed with `Test`. This naming convention uses a `Test+<ClassUnderTest>` format.

For the class-under-test named `StudentDal`, create a test class in your test project named `TestStudentDal`. This test class should be saved in a file named `TestStudentDal.cs`.

The one-test-class-per-class pattern mentioned in [xUnit Test Patterns: Refactoring Test Code](http://xunitpatterns.com/Testcase%20Class%20per%20Class.html) by Gerard Meszaros "is a good starting point when we don't have very many Test Methods or we are just starting to write tests for our [system under test]." There are other patterns, but let's stick to this one.

Put all the tests for all methods of the class-under-test in one test class.

By using a *Test Class* approach, `NUnit` allows us to group tests together using classes. The can be helpful.

#### Directory and file structure

- For C# class files, we'll prefix them with `Test`, like `TestScaleConverter.cs`
- For .NET class library project files, we'll prefix them with `Tests.`, like `Tests.Unit.Physics.Temperature.csproj`
- Putting all the tests into an extra directory outside the system-under-test code is useful.

Here is an example of the `tests/` file structure:
```
.
├── src
│   └── Valen.Slos.Model
│       ├── Valen.Slos.Model.csproj
│       └── Loan.cs
├── tests
│   └── Tests.Unit.Valen.Slos.Model
│       ├── Tests.Unit.Valen.Slos.Model.csproj
│       └── TestLoan.cs
```



#### Naming Is Important

Since test code is a lot different than production code and serves a completely different purpose that the code-under-test, we name test methods differently.

We use a naming convention for our test code that's clear and makes our lives easier when test fail.

We want to pinpoint why the test failed without needing to decipher the output. We keep the function names distinct, structured, and clear. This makes our test code as maintenance-friendly as possible.

The naming convention presented here is helpful in these ways:
- The test directory and filename identifies the modules being tested.
- The test method name describes three key aspects of the test:
  - The method under test
  - Conditions of the test
  - Expected results of the test
- The test class name identifies the class-under-test, which is the class that the tests are testing.



### Test Method Structure

It is very helpful for the test function structure to adhere to a consistent convention. This uniformity ensures that all developers within a project can easily locate and comprehend the test code. Adherence to a common convention accelerates familiarity and efficiency, especially when extended across the entire organization. Such consistency facilitates smoother transitions for developers joining new projects or initiating new endeavors.

### Arrange-Act-Assert Framework

> Use the Arrange-Act-Assert (3-As) Pattern

While various methodologies exist for structuring test functions, the Arrange-Act-Assert (triple-A or 3As) framework stands out for its effectiveness and widespread adoption. This framework segments the test function into three distinct sections:

- **Arrange**: This section of the test function is dedicated to setting up preconditions and dependencies. It involves initializing the class or object under test and assigning any necessary values required for the test scenario.
  
- **Act**: This segment of the test function executes the specific actions or operations that are being tested. It is the core phase where the test's main functionality is performed.
  
- **Assert**: The final section focuses on validating the outcomes of the test. It verifies that the results meet the expected conditions, and it will trigger a failure if the test does not conform to the anticipated outcomes.

Incorporating comments such as `// Arrange`, `// Act`," and `// Assert` within the test code is a recommended practice. These comments serve as clear markers delineating each section, thereby enhancing the readability and maintainability of the test code.

Here is an example that uses the `TestLoan` class to group tests that test the `ComputePayment` method of the `Loan` class.

#### Listing 3-1: Example Using the Test Code Naming Convention

```csharp
using Valen.Slos.Model;

namespace Tests.Unit.Valen.Slos.Model;

public class TestLoan
{
    [Test]
    public void Test_ComputePayment_WhenTermIs300_ExpectPaymentIs126pt39()
    {
        // Arrange
        var classUnderTest = 
            new Loan
            {
                Principal = 12000m,
                AnnualPercentageRate = 12m,
            };

        // Act
        decimal actual = classUnderTest.ComputePayment(300);

        // Assert
        Assert.That(actual, Is.EqualTo(126.39m));
    }
}
```

### Keep Tests Short

> Prefer Many Short Tests Over Few Long Tests

#### Simplifying Test Method Complexity

Maintaining brevity within test methods is crucial for readability and comprehension. Short test methods simplify the management of test complexity, proving particularly beneficial when diagnosing and resolving issues with tests developed by others. A variety of factors, such as new requirements, inconsistent behaviors, or obsolete test cases, can cause a test to fail following modifications to the codebase. Each test method should clearly delineate the functionality of the code under test and its expected behavior.

#### Principle of Singular Focus in Test Methods

Adhering to the principle that each test method should verify a single specific intention is essential for enhancing the longevity and utility of test code. This approach facilitates rapid identification and resolution of failures by any developer, reducing the likelihood of tests being overlooked or ignored. The simplicity and focus of short, well-defined test methods increase the willingness and ability of developers to address and rectify issues promptly.

### Limit Test Actions

> Avoid More Than One Line of Action

#### Streamline Test Actions for Clarity

In the context of constructing a test, particularly within the "Act" section, it's important to adhere to the principle of minimalism. A common pitfall is the incorporation of multiple actions within a single test, which not only complicates the test but also might require extensive setup code. The objective is to refine the "Act" section to a singular, clear line of code whenever possible. The primary rationale behind this approach is to simplify debugging when a test fails. Multiple actions within a test can hide the root cause of failure, requiring additional effort to pinpoint the exact line responsible.

Consider a scenario in a `NUnit` framework where the "Act" section comprises five lines of code. Should an error arise within the `Get` method, distinguishing whether the first or second invocation of `Get` is at fault becomes less straightforward. Moreover, in the event of a test failure, identifying the specific action line that triggered the issue becomes a challenge. This underscores the importance of limiting actions within the "Act" phase to enhance test clarity and facilitate easier troubleshooting.

#### Listing 3-2: Too many actions in the Act step
```csharp
[Test(Description = "Too many action steps")]
public void Test_Save_WhenPrincipalIsChanged_ExpectNewPrincipalValueInDatabase()
{
    // Arrange
    Application classUnderTest = new Application();
    classUnderTest.Principal = 999.91m;
    classUnderTest.Save();
    int id = classUnderTest.Id;

    // Act
    classUnderTest.Get(id);
    classUnderTest.Principal = 1009.81m;
    classUnderTest.Save();
    classUnderTest.Get(id);
    decimal actual = classUnderTest.Principal;

    // Assert
    Assert.That(actual, Is.EqualTo(1009.81m));
}
```

### Simplification of Action Steps in Test Functions

In the example provided as Listing 3-4, the test method is structured to contain a single action within the _Act_ section, specifically invoking the `save` method of the `Application` class. This illustrates the principle of focusing each test on a specific action, which is stated in the test function's name. Notice that the _Arrange_ section is intentionally more involved. We'll explore that topic in a later discussion.

For the purpose of maintaining high readability and clarity within test code, it's recommended that you limit the _Act_ section to a single line of action. This practice ensures that the test's intent remains focused and straightforward.

Additionally, something worth noting is the use of the hard-coded value `97` at multiple points within the test. Subsequent sections will discuss techniques to parameterize such values, allowing them to be injected into the test method as arguments, thereby enhancing the test's flexibility and reducing redundancy.

#### Listing 3-4: Only one action in the Act step
```csharp
[TestCase(Description = "One action step")]
public void Test_Save_WhenPrincipalIsChanged_ExpectNewPrincipalValueInDatabase()
{
    // Arrange
    var expectedPrincipal = 1009.81m;
    TestApplicationContext.SetupTestDatabase("TestApplicationDal_Scenario01.xml");

    var classUnderTest = TestApplicationContext.CreateInstance(97);
    classUnderTest.Principal = expectedPrincipal;

    // Act
    classUnderTest.Save();

    // Assert
    var actual = TestApplicationContext.Retrieve<decimal>(
        "Principal",
        "Application",
        string.Format("[Id] = {0}", 97));
    Assert.That(actual, Is.EqualTo(expectedPrincipal));
}
```

### Primary Assertion

> Avoid More Than One Primary Assertion

When designing test methods, it's crucial to maintain a narrow focus by limiting each function to one primary assertion.

This principle ensures that:
- tests' _Assert_ section are straightforward
- tests directly verify the functionality described in the test function's name

#### Avoiding Multiple Primary Assertions

For clarity and precision, tests should avoid including more than one primary assertion.

For example, if the expectation in a test method name is *expect the correct payment amount* then the primary assertion statement ought to be:
```csharp
    // Assert
    var expectedPaymentAmount = 126.39m;
    Assert.That(actual, Is.Equal(expectedPaymentAmount));
```

This approach minimizes ambiguity, ensuring that if the test fails, the reason is clearly related to the test's primary focus.

#### Types of Assertions

Assertions can be categorized based on their verification methods, including:

| Category              | Description |
|:----------------------|:------------|
| Comparison            | Validating the equivalence, difference, or relational comparison between expected and actual values. |
| Expected Interaction  | Ensuring a dependency is called a specific number of times, with certain arguments, or validating that it is not called with invalid arguments. |
| Exception Handling    | Confirming an expected exception is thrown under specific conditions. |
| Collection Assertions | Verifying the presence or absence of elements within a collection. |
| Instance Verification | Checking whether an object is null, not null, or an instance of a specific type. |
| Boolean Checks        | Assessing the truthfulness of an expression. |
| Constraint-Based      | Evaluating whether an instance meets a set of defined constraints. |

The constraint-based syntax (called using the `Assert.That` method in NUnit) is very powerful. In the NUnit framework many legacy assert methods implement their behavior by wrapping a call to `Assert.That`. The constraint-based syntax also allows you to implement your own custom constraints.

#### Primary Versus Secondary Assertions

The concept of a primary assertion raises the question: What about *secondary* assertions?

Secondary assertions are additional checks that, while useful, are less critical than the primary assertion. They often _precede_ the primary assertion to clarify any _secondary_ reasons why a test may fail. Secondary assertions are helpful if they make explicit any implicit assumptions by turning them into direct assertions. This often makes for clearer understanding.

#### Guard and Secondary Assertions

In addition to secondary assertions within the _Assert_ section, "guard assertions" in the _Arrange_ section help clarify the conditions necessary for the _Act_ section to proceed correctly. These assertions explicitly state assumptions critical to the test's setup, providing clarity and avoiding ambiguous failures.

#### Listing 3-5: Guard and Secondary Assertion
```csharp
[Test(Description = "Guard and secondary assertions")]
public void Test_Save_WithScenario02_ExpectHighSchoolStateIsVirginia()
{
    // Arrange
    TestApplicationContext.SetupTestDatabase("TestApplicationDal_Scenario02.xml");
    var application = TestApplicationContext.CreateInstance(73);
    var classUnderTest = application.Student;

    Assert.That(classUnderTest, Is.Not.Null);  // This is a guard assert

    // Act
    School highSchool = classUnderTest.HighSchool;

    // Assert
    Assert.That(highSchool, Is.Not.Null);  // This is secondary assert
    Assert.That(highSchool.State, Is.EqualTo("Virginia"));
}
```

#### Caution Against Over-Specification

While guard and secondary assertions are useful, it's important to avoid over-specifying test expectations.

Secondary assertions _should only support_ reaching the primary assertion, without introducing unnecessary complexity or maintenance overhead.

#### Listing 3-6: Over-Specified Expectations
```csharp
[Test(Description = "Over-specified expectations")]
public void Test_ComputePayment_WhenInvalidTermInMonthsIsZero_ExpectArgumentOutOfRangeException()
{
    // Arrange
    Loan loan =
        new Loan
        {
            Principal = 7499,
            AnnualPercentageRate = 1.79m,
        };
    Assert.That(loan, Is.Not.Null);

    // Act
    TestDelegate act = () => loan.ComputePayment(0);

    // Assert
    ArgumentOutOfRangeException exception =
        Assert.Throws<ArgumentOutOfRangeException>(act);
    Assert.That(exception.ParamName, Is.EqualTo("termInPeriods"));
    Assert.That(exception.Message, Is.EqualTo(
        "Specified argument was out of the range of valid values.\r\n" +
        "Parameter name: termInPeriods"));
}
```

Take a look at Listing 3-6. At the end of the *Arrange* section there is a guard assertion that asserts that loan is not null. There really is no reason to do that because .NET takes care of constructing the new Loan; the test code is in effect testing that the C# compiler works. You can safely remove that line.

Now take a look at the *Assert* section in Listing 3-6; there are three assertions. The last two assertions over-specify the requirements of the code-under-test:
- Is there really a requirement that the `ComputePayment` method’s parameter be named `TermInMonths`?
- Will the message in the exception change over time? Isn't that okay?

If we really need to test the exception message, it should be in a separate test method.

By looking at the test method’s name, a better implementation of Listing 3-6 is suggested as follows:
```csharp
[Test]
public void ComputePayment_WhenInvalidTermInMonthsIsZero_ExpectArgumentOutOfRangeException ()
{
  // Arrange
  Loan loan =
    new Loan
    {
      Principal = 7499,
      AnnualPercentageRate = 1.79m,
    };

  // Act
  TestDelegate act = () => loan.ComputePayment(0);

  // Assert
  Assert.Throws<ArgumentOutOfRangeException>(act);
}
```

### Test Context Classes

> Use a Tests Context Class to Reduce Repetition and Manage Complexity

### Test Context Classes Enhance Maintainability

As tests evolve to cover more complex scenarios, the code within the _Arrange_ section of the test method can become very lengthy, leading to repetition across test methods. To streamline this process and manage the complexity, using a "Test Context Class" is recommended.

#### The Test Context Class

The introduction of a "Test Context Class", following the Library Class pattern described by Kent Beck in his book [Implementation Patterns](https://www.amazon.com/Implementation-Patterns-Kent-Beck/dp/0321413091).

The Library Class pattern offers a structured solution to minimize redundant setup code in tests. This strategy involves creating a class equipped with static methods that encapsulate common arrangement tasks tailored to specific tests. This approach not only reduces code duplication but also enhances the readability and maintainability of test code.

#### Implementing a Test Context

To illustrate this concept in the `NUnit` framework, consider transitioning duplicate test code into a dedicated context class. This class will contain static methods responsible for creating instances and setting up mocks or stubs as required. Listing 3-7 provides an example.

```csharp
// To Do: Listing 3-7: Test Context for Testing Application
// test context class
```

```csharp
// To Do: Listing 3-7: Test Context for Testing Application
// test method
```

##### Key Considerations

- **Maintain Test Isolation**: The test context class should be used exclusively within the scope of its corresponding test class to prevent unintended dependencies between tests.
- **Avoid Test Class Inheritance**: While tempting, using inheritance to share code between test classes can introduce maintenance challenges. A notable exception is leveraging a base test class for shared testing infrastructure, which does not compromise test isolation. This is an advanced topic we will cover later.

By adopting a *Test Context Class*, developers significantly reduce the redundancy of setup code across tests, making the tests easier to read, write, and maintain. This practice aligns with the goal of keeping test methods focused and manageable, especially as the complexity of the scenarios they cover increases.

### Reusable Test Helper Classes

> Build Test Helper Classes to Promote Reuse

To enhance code reuse across test suites, implementing "Test Helper Classes" is a good approach, analogous to employing a "Test Context Class".

These _helper classes_ leverage the Library Pattern to consolidate commonly used test code. This facilitate building up a repository for shared test functions and code that's applicable across various tests.

#### Strategy for Reuse Through Test Helpers

Test Helper Classes provide functions that address general needs across multiple testing scenarios. This approach ensures that reusable code is accessible, maintaining a focus on broad applicability rather than being tailored to specific test cases.

#### Implementing Test Helper Functions

Consider translating the concept of generating random strings for test data. Such a utility can be particularly useful for generating dynamic values like names, addresses, or any string-based data, with the flexibility to specify length or complexity through optional parameters. Listing 3-8 provides the `BuildNameString()` function as an example of how to generate random names. The test code uses `BuildNameString()` to generate a large, random name.

```csharp
// To Do: ##### Listing 3-8: Test Data Helper
```

```csharp
// To Do: ##### Listing 3-8: Test Data Helper
// test method
```

#### Guidelines for Test Helper Utilization

- **Generalization over Specialization**: Test helpers should be designed to provide utility across the testing spectrum without embedding logic specific to the system under test. This ensures their relevance and applicability across different test scenarios.
  
- **Domain-Specific Logic**: Avoid incorporating application-specific logic within test helpers. That risks coupling test suites too closely, potentially leading to widespread changes if domain-specific assumptions are altered. Helpers should remain as agnostic as possible to the domain of the application being tested.

By adhering to these practices, developers can create a suite of Test Helper Classes that significantly reduce redundancy and enhance the efficiency of the testing process. This approach promotes a more organized and maintainable testing framework, enabling developers to focus on the specifics of each test case without duplicating common setup or utility code.

### Data Scenarios

> Data-Drive Test Methods for Multiple Test Cases

#### Leverage Data-Driven Testing

In the development of comprehensive test suites, it becomes obvious that many test methods can be structurally identical, differing only in the data that each test uses. To address this, `NUnit` offers the `TestCase` attribute to mark a test method with varying sets of input data. This can enhance test coverage without unnecessarily multiplying the test code.

#### Data-Drive Test Functions

Data-driven testing allows for the efficient checking of multiple scenarios using a single test function, by feeding it different sets of input values and expected values. This approach significantly reduces the test code maintenance burden by consolidating what would otherwise require multiple individual test methods.

#### Implementing Data-Driven Tests

The `NUnit` framework supports parameterized testing. Listing 3-9 shows how to use the `TestCase` attribute for data-driven testing.

#### Listing 3-9: Data-Driven Test Cases
```csharp
[TestCase(7499, 1.79, 0, 72.16)]
[TestCase(7499, 1.79, -1, 72.16)]
[TestCase(7499, 1.79, -73, 72.16)]
[TestCase(7499, 1.79, int.MinValue, 72.16)]
[TestCase(7499, 1.79, 361, 72.16)]
[TestCase(7499, 1.79, 2039, 72.16)]
[TestCase(7499, 1.79, int.MaxValue, 72.16)]
public void Test_ComputePayment_WithInvalidTermInMonths_ExpectArgumentOutOfRangeException(
    decimal principal,
    decimal annualPercentageRate,
    int termInMonths,
    decimal expectedPaymentPerPeriod)
{
    // Arrange
    var loan =
        new Loan
        {
            Principal = principal,
            AnnualPercentageRate = annualPercentageRate,
        };

    // Act
    TestDelegate act = () => loan.ComputePayment(termInMonths);

    // Assert
    Assert.Throws<ArgumentOutOfRangeException>(act);
}
```

#### Practical Tips for Data-Driven Testing

- **Use of Prime Numbers**: To reduce the risk of coincidental arithmetic, using prime numbers in test data is recommended. This helps ensure that test validations are genuine and not accidentally passing due to arithmetic coincidences.

A common coincidental arithmetic problem occurs when a test uses the number 2. These three expressions: `2 + 2`, `2 * 2`, and `2**2` all equal to 4. When using the number 2 as a test value, there are many ways the test falsely passes. Arithmetic errors are less likely to yield an improper result when the test values are different prime numbers. Consider a loan that has a principal of $12,000.00, a term of 360 payments, an annual interest rate of 12%, and, of course, there are 12 months in a year. Because there is a coincidental factor of 12 in all these numbers, that data scenario yields a lot of very bad test cases.

For this reason, prefer large prime numbers like 73 or 7499. Pick decimals like 1.79 or 15.43 to avoid these arithmetic coincidences. Consider bookmarking this list of [The First 1,000 Primes](https://t5k.org/lists/small/1000.txt).
  
- **Avoiding Hardcoded Strings**: For dynamic test data, especially where uniqueness is crucial, avoid hardcoded strings. Use a Test Helper Class function to generate something unique and variable. These helpers can make the input consistently large and random.

Through data-driven testing, developers can achieve thorough coverage with fewer test methods, streamlining the testing process and maintaining code simplicity. This approach not only enhances the efficiency of writing and maintaining tests but also contributes to the reliability and robustness of the software testing strategy.


## Conclusion

Here’s a cheat sheet to make your test code better:
- **Naming is important**: Pick the right naming convention for your test methods, classes, namespaces, assemblies, and folders.
- **Use a consistent pattern for the test method body**: Consistency is key.
- **Short and sweet**: Aim to keep your test methods under ten lines.
- **Limit the test actions to one or two lines of code**: Don’t make your tests run a marathon.
- **One primary assertion rule**: Focus to one primary thing you’re checking per test.
- **Context classes are your friends**: Test code that sets up your tests goes in a context class.
- **Helper classes to the rescue**: Common code across your tests? Offload it into helper classes.
- **Data-drive your tests**: Feeding different data into your tests? Pass them as parameters.

### Comprehensive Test Method Implementation

The following C# code exemplifies the application of best practices in unit testing as outlined. This test method incorporates:

- A clear naming convention
- The Arrange-Act-Assert (AAA) pattern for structured testing
- Concise arrangement of test data
- A singular action within the test
- One assertion per test case to validate the expected outcome
- Data-driven approach to evaluate multiple scenarios within a single test method
- Utilization of prime numbers to enhance the reliability of test outcomes and facilitate debugging

Listing 3-10 shows test code that follows the recommended approach. It applies a data-driven approach to validate the functionality of a loan payment computation across several scenarios. By adhering to these practices, you can be sure that your tests are both efficient and effective, facilitating easier maintenance and debugging.

#### Listing 3-10: Putting it all together
```csharp
using Valen.Slos.Model;

namespace Tests.Unit.Valen.Slos.Model;

public class TestLoan
{
    [TestCase(7499, 1.79, 113, 72.16)]
    [TestCase(8753, 6.53, 139, 89.92)]
    [TestCase(61331, 7.09, 367, 409.5)]
    public void ComputePayment_WithProvidedLoanData_ExpectProperMonthlyPayment(
        decimal principal,
        decimal annualPercentageRate,
        int termInMonths,
        decimal expectedPaymentPerPeriod)
    {
        // Arrange
        var loan =
            new Loan
            {
                Principal = principal,
                AnnualPercentageRate = annualPercentageRate,
            };

        // Act
        var actual = loan.ComputePayment(termInMonths);

        // Assert
        Assert.That(actual, Is.EqualTo(expectedPaymentPerPeriod));
    }
}
```
