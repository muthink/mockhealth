/*
Copyright © 2024 JC Stevens

   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

   The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

   THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using Muthink.MockHealth;

namespace MockHealthTests;

public class MockBaseTests
{
    [Fact]
    public void NameTests()
    {
        var fullName = MockString.New(473).LastName().Comma(true)
            .FirstName().Space().MiddleName();
        Assert.Equal("Harris, Rowen Paul", fullName);

        var city = MockString.New(234).City();
        Assert.Equal("Gainesville", city);

        var state = MockString.New(665).USState();
        Assert.Equal("Montana", state);
    }

    [Fact]
    public void DateTests()
    {
        var timeProvider =
            new MockTimeProvider(new DateTimeOffset(2024, 12, 18, 12, 0, 0, TimeZoneInfo.Local.BaseUtcOffset));

        var mock = new MockBase(2399, timeProvider);

        var birthDate = mock.BirthDate(1, 100);

        Assert.Equal(new DateOnly(1926,6,3), birthDate);

        var time = mock.Next(TimeSpan.FromHours(8), TimeSpan.FromHours(18));

        Assert.Equal(new TimeSpan(17,16,51),
            new TimeSpan(time.Hours,time.Minutes,time.Seconds));
    }
}