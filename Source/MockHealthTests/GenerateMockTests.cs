using Muthink.MockHealth;

namespace MockHealthTests;

public class GenerateMockTests
{
    [Fact]
    public void BuildNames()
    {
        var fullName = MockString.New(473).LastName().Append(", ")
            .FirstName().Space().MiddleName();
        Assert.NotEmpty(fullName.ToString());

        var city = MockString.New(234);
        Assert.Equal("San",city);
    }
}
