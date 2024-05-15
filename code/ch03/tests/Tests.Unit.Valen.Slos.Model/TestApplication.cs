using System.Runtime;
using Valen.Slos.Model;

namespace Tests.Unit.Valen.Slos.Model;

public class TestApplication
{
    [Test(Description = "Too many action steps")]
    public void Test1_Save_WhenPrincipalIsChanged_ExpectNewPrincipalValueInDatabase()
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

    [TestCase(Description = "One action step")]
    public void Test2_Save_WhenPrincipalIsChanged_ExpectNewPrincipalValueInDatabase()
    {
        // Arrange
        var expectedPrincipal = 1009.81m;
        TestApplicationContext.SetupTestDatabase("ApplicationDalTests_Scenario01.xml");

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

    [Test(Description = "Guard and secondary assertions")]
    public void Test_Save_WithScenario02_ExpectHighSchoolStateIsVirginia()
    {
        // Arrange
        TestApplicationContext.SetupTestDatabase("ApplicationDalTests_Scenario02.xml");
        var application = TestApplicationContext.CreateInstance(73);
        var classUnderTest = application.Student;

        Assert.That(classUnderTest, Is.Not.Null);

        // Act
        School highSchool = classUnderTest.HighSchool;

        // Assert
        Assert.That(highSchool, Is.Not.Null);
        Assert.That(highSchool.State, Is.EqualTo("Virginia"));
    }

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

    [Test]
    public void Test_Save_WithValidNewApplication_ExpectApplicationRepoCreateCalledOnce()
    {
        // Arrange
        var mockApplicationRepo =
            new Mock<IRepository<ApplicationEntity>>(MockBehavior.Strict);
        mockApplicationRepo
            .Setup(e => e.Create(It.IsAny<ApplicationEntity>()))
            .Returns(73)
            .Verifiable();
        var stubIndividualRepo =
            new Mock<IRepository<IndividualEntity>>(MockBehavior.Loose);
        var stubStudentRepo =
            new Mock<IRepository<StudentEntity>>(MockBehavior.Loose);
        var classUnderTest =
            new Application(
                stubIndividualRepo.Object,
                stubStudentRepo.Object,
                mockApplicationRepo.Object)
                {
                    Student =
                    {
                        LastName = "Smith",
                        FirstName = "John",
                        DateOfBirth = new DateTime(1993, 5, 7),
                        HighSchool =
                            {
                            Name = "Our Town High School",
                            City = "Anytown",
                            State = "QQ"
                            }
                        },
                    Principal = 7499,
                    AnnualPercentageRate = 1.79m,
                    TotalPayments = 113
                };

        // Act
        classUnderTest.Save();

        // Assert
        mockApplicationRepo
            .Verify(e => e.Create(It.IsAny<ApplicationEntity>()), Times.Once());
    }
}
