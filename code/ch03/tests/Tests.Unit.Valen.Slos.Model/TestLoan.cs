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