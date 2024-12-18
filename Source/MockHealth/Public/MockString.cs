/*
Copyright © 2024 JC Stevens

   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

   The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

   THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System.Globalization;

namespace Muthink.MockHealth;

public class MockString
{
    /// <summary>
    ///     Array of top 100 common city names - AI generated
    /// </summary>
    private static readonly string[] _cityNames =
    [
        "Springfield", "Riverside", "Franklin", "Greenville", "Fairview", "Madison", "Georgetown", "Arlington",
        "Salem", "Bristol", "Clinton", @"Centerville", "Mount Vernon", "Oakland", "Ashland", "Manchester",
        "Chester", "Milton", "Auburn", "Clayton", "Kingston", "Burlington", "Dayton", "Lexington",
        "Dover", "Hudson", "Newport", "Jackson", "Monroe", "Richmond", "Cleveland", "Wilson",
        "Carrollton", "Troy", "Farmington", "Lincoln", "Clay", "Lawrence", "Marion", "Union",
        "Florence", "Washington", "Fayetteville", "Hamilton", "Windsor", "Lancaster", "Plymouth", "Norfolk",
        "Harrison", "Somerset", "Rockville", "Charleston", "Newark", "Huntsville", "Henderson", @"Ashville",
        "Cambridge", "Oxford", "Gainesville", "Columbus", "Portland", "Knoxville", "Jefferson", "Winchester",
        "Fairfield", "Glendale", "Rochester", "Springdale", "Brooklyn", "Shelby", "Wilmington", "Hartford",
        "Alton", "Clifton", "Kingston", "Montgomery", "Parker", "Princeton", "Danville", "Midland",
        "Greensboro", "Aurora", "Elk Grove", "Rockport", "Peoria", "Canton", "Easton", "Monterey",
        "Cumberland", "Hampton", "Fulton", "Carson", "Medford", "Prescott", "Somerville", "Westfield",
        "Harrisburg", "Fort Worth", "Nashville", "Denver"
    ];

    /// <summary>
    ///     Array of top 100 common first names - AI generated
    /// </summary>
    private static readonly string[] _firstNames =
    [
        "James", "Mary", "Robert", "Patricia", "John", "Jennifer", "Michael", "Linda",
        "William", "Elizabeth", "David", "Barbara", "Richard", "Susan", "Joseph", "Jessica",
        "Thomas", "Sarah", "Charles", "Karen", "Christopher", "Nancy", "Daniel", "Lisa",
        "Matthew", "Margaret", "Anthony", "Betty", "Mark", "Sandra", "Donald", "Ashley",
        "Steven", "Dorothy", "Paul", "Kimberly", "Andrew", "Emily", "Joshua", "Donna",
        "Kenneth", "Michelle", "Kevin", "Carol", "Brian", "Amanda", "George", "Melissa",
        "Edward", "Deborah", "Ronald", "Stephanie", "Timothy", "Rebecca", "Jason", "Sharon",
        "Jeffrey", "Laura", "Ryan", "Cynthia", "Jacob", "Kathleen", "Gary", "Amy",
        "Nicholas", "Shirley", "Eric", "Angela", "Jonathan", "Helen", "Stephen", "Anna",
        "Larry", "Brenda", "Justin", "Pamela", "Scott", "Nicole", "Brandon", "Emma",
        "Frank", "Samantha", "Benjamin", "Katherine", "Gregory", "Christine", "Samuel", "Debra",
        "Raymond", "Rachel", "Patrick", "Catherine", "Alexander", "Carolyn", "Jack", "Janet",
        "Dennis", "Ruth", "Jerry", "Maria", "Tyler", "Heather"
    ];

    /// <summary>
    ///     Array of top 100 common last names - AI generated
    /// </summary>
    private static readonly string[] _lastNames =
    [
        "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis",
        "Rodriguez", "Martinez", "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas",
        "Taylor", "Moore", "Jackson", "Martin", "Lee", "Perez", "Thompson", "White",
        "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson", "Walker", "Young",
        "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores",
        "Green", "Adams", "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell",
        "Carter", "Roberts", "Gomez", "Phillips", "Evans", "Turner", "Diaz", "Parker",
        "Cruz", "Edwards", "Collins", "Reyes", "Stewart", "Morris", "Morales", "Murphy",
        "Cook", "Rogers", "Gutierrez", "Ortiz", "Morgan", "Cooper", "Peterson", "Bailey",
        "Reed", "Kelly", "Howard", "Ramos", "Kim", "Cox", "Ward", "Richardson",
        "Watson", "Brooks", "Chavez", "Wood", "James", "Bennett", "Gray", "Mendoza",
        "Ruiz", "Hughes", "Price", "Alvarez", "Castillo", "Sanders", "Patel", "Myers",
        "Long", "Ross", "Foster", "Jimenez"
    ];

    /// <summary>
    ///     Array of top 100 common middle names - AI generated
    /// </summary>
    private static readonly string[] _middleNames =
    [
        "James", "Marie", "Lee", "Ann", "John", "Lynn", "Michael", "Elizabeth",
        "William", "Jean", "Robert", "Louise", "Charles", "Rose", "Joseph", "Grace",
        "Edward", "May", "Thomas", "Jane", "George", "Elaine", "David", "Irene",
        "Paul", "Frances", "Richard", "Mae", "Anthony", "Victoria", "Daniel", "Alice",
        "Christopher", "Pearl", "Andrew", "Renee", "Matthew", "Claire", "Jonathan", "Faye",
        "Alexander", "Dawn", "Nicholas", "Faith", "Samuel", "Marie", "Patrick", "Hope",
        "Benjamin", "Ivy", "Henry", "Belle", "Timothy", "Eve", "Joshua", "Brooke",
        "Steven", "Joy", "Kenneth", "Paige", "Kevin", "Autumn", "Scott", "Jade",
        "Gregory", "Iris", "Peter", "Fern", "Raymond", "Sky", "Ryan", "Sage",
        "Justin", "Noel", "Mark", "Rae", "Jason", "Bliss", "Eric", "Snow",
        "Aaron", "Lark", "Adam", "Wren", "Shawn", "Dove", "Ethan", "Blue",
        "Keith", "Quinn", "Jeremy", "Reed", "Kyle", "Star", "Jacob", "Frost"
    ];

    /// <summary>
    ///     Array of street abbreviation types - AI generated
    /// </summary>
    private static readonly string[] _streetAbbreviationTypes =
    [
        "St", "Ave", "Rd", "Blvd", "Ln", "Dr", "Ct", "Cir",
        "Way", "Ter", "Pl", "Ally", @"Cres", "Pkwy", "Trl", "Sq",
        "Loop", "Hwy", "Esp", "Prom"
    ];

    /// <summary>
    ///     Array of top 100 common street names - AI generated
    /// </summary>
    private static readonly string[] _streetNames =
    [
        "Main", "High", "Oak", "Pine", "Maple", "Cedar", "Elm", "Washington",
        "Lake", "Hill", "Walnut", "Sunset", "2nd", "1st", "3rd", "4th",
        "Park", "5th", "Chestnut", "River", "6th", "7th", "8th", "9th",
        "Spruce", "Broadway", "Grove", "Central", "Madison", "Willow", "Forest", "Jackson",
        "Church", "Lincoln", "Adams", "Jefferson", "Franklin", "Roosevelt", "Cherry", "Grant",
        "Summit", "Harrison", "Monroe", "Taylor", "Clark", "School", "Cypress", "Valley",
        "Court", "North", "South", "East", "West", "Bridge", "Spring", "Washington",
        "King", "View", "Union", "Meadow", "College", "Prospect", "Laurel", "Bay",
        "Dogwood", "Pearl", "Magnolia", "Highland", "Fairview", "Sycamore", "Peachtree", "Ash",
        "Stone", "Terrace", "Holly", "Fairway", "Mill", "Ridge", "Shady", "Creek",
        "Broad", @"Riverbend", "Lakeside", "Locust", "Railroad", "Woodland", "Mountain", "Orchard",
        "Prairie", @"Stonebridge", "Birch", "Fifth", "Green", "Heritage", "Crossing", "Harvest"
    ];

    /// <summary>
    ///     Array of top 20 common street types - AI generated
    /// </summary>
    private static readonly string[] _streetTypes =
    [
        "Avenue", "Street", "Road", "Boulevard", "Lane", "Drive", "Court", "Circle",
        "Way", "Terrace", "Place", "Alley", "Crescent", "Parkway", "Trail", "Square",
        "Loop", "Highway", "Esplanade", "Promenade"
    ];

    /// <summary>
    ///     Array of 50 US States - AI generated
    /// </summary>
    private static readonly string[] _usStates =
    [
        "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware",
        "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas",
        "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi",
        "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York",
        "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island",
        "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington",
        "West Virginia", "Wisconsin", "Wyoming"
    ];

    private readonly Random _random;
    private readonly string _value;

    public MockString(int seed)
    {
        Seed = seed;
        _random = new Random(seed);
        _value = string.Empty;
    }

    public MockString(MockString mockString, string value)
    {
        Seed = mockString._random.Next();
        _random = mockString._random;
        if( mockString._value.Length == 0 )
        {
            _value = value;
        }
        else
        {
            _value += value;
        }
    }

    /// <summary>Appends a string value</summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public MockString Append(string value) => new(this, value);

    public MockString Append(string[] choices)
    {
        // Deterministically calculate the index from the seed
        var index = Math.Abs(Seed % choices.Length);

        // Return the selected name
        var name = choices[index];

        return new MockString(this, name);
    }

    /// <summary>
    ///     Picks a name out of all the possible city names
    /// </summary>
    /// <returns>A city name</returns>
    public MockString City() => Append(_cityNames);


    /// <summary>Appends a space</summary>
    /// <returns></returns>
    public MockString Comma() => Append(" ");

    /// <summary>
    ///     Formats a date, between a given range
    /// </summary>
    /// <param name="first">first date in the range</param>
    /// <param name="last">last date in the range</param>
    /// <param name="format">The formatting</param>
    /// <returns>Date, in the <paramref name="format" /></returns>
    public MockString Date(DateTime first, DateTime last, string format = "yyyy-MM-dd")
    {
        var lastTicks = Math.Max(first.Ticks, last.Ticks);
        var firstTicks = Math.Min(first.Ticks, last.Ticks);
        var ticks = _random.NextInt64(firstTicks, lastTicks);
        var dateTime = new DateTime(ticks);
        return Append(dateTime.ToString(format, CultureInfo.InvariantCulture));
    }

    /// <summary>
    ///     Picks a name out of all the first names
    /// </summary>
    /// <returns>The first name</returns>
    public MockString FirstName() => Append(_firstNames);

    /// <summary>
    ///     Creates a string, which contains a random value.
    /// </summary>
    /// <param name="min">The minimum value</param>
    /// <param name="max">The maximum value</param>
    /// <param name="padding">Optional zero padding to apply on left side of number</param>
    /// <returns>Appended, formatted integer</returns>
    public MockString Integer(int min, int max, int padding = -1)
    {
        var value = _random.Next(min, max).ToString(CultureInfo.InvariantCulture);

        return Append(padding <= 0 ? value : value.PadLeft(padding, '0'));
    }

    /// <summary>
    ///     Creates a string, which contains a random value.
    /// </summary>
    /// <param name="min">The minimum value</param>
    /// <param name="max">The maximum value</param>
    /// <param name="padding">Optional zero padding to apply on left side of number</param>
    /// <returns></returns>
    public MockString Integer(long min, long max, int padding = -1)
    {
        var value = _random.NextInt64(min, max).ToString(CultureInfo.InvariantCulture);

        return Append(padding <= 0 ? value : value.PadLeft(padding, '0'));
    }

    /// <summary>
    ///     Picks a name out of all the last names
    /// </summary>
    /// <returns>The last name</returns>
    public MockString LastName() => Append(_lastNames);

    /// <summary>
    ///     Picks a name out of all the middle names
    /// </summary>
    /// <returns>The middle name</returns>
    public MockString MiddleName() => Append(_middleNames);

    /// <summary>
    ///     Starts a new MockString
    /// </summary>
    /// <param name="seed">The seed to start with</param>
    /// <returns></returns>
    public static MockString New(int seed) => new(seed);

    /// <summary>
    ///     Starts a new MockString, using the seed from the previous
    /// </summary>
    /// <param name="mockString">The seed to start with</param>
    /// <returns></returns>
    public static MockString New(MockString mockString) => new(mockString.Seed);

    /// <summary>
    ///     Allows direct assignment of the build-up data to a string.
    /// </summary>
    /// <param name="mockString">Source of string</param>
    public static implicit operator string(MockString mockString) => mockString._value;

    /// <summary>
    ///     Generates a phone number
    /// </summary>
    /// <param name="noDelimiters">If true, then no hyphens</param>
    /// <returns>Returns invalid phone number, like 233-555-1234</returns>
    public MockString Phone(bool noDelimiters = false)
    {
        // Generate invalid area code
        var areaCode = _random.Next(100, 999);
        if( areaCode is 555 )
        {
            areaCode = 554;
        }

        // Generate line number
        var lineNumber = _random.Next(0, 9999); // Valid line numbers, but doesn't fix other invalid components

        // Format as a standard phone number
        var invalidPhoneNumber =
            noDelimiters ? $"{areaCode:000}555{lineNumber:0000}" : $"{areaCode:000}-555-{lineNumber:0000}";

        return Append(invalidPhoneNumber);
    }

    /// <summary>
    ///     Picks
    /// </summary>
    /// <param name="percentFirst">0-100 % chance that <paramref name="first" /> is picked</param>
    /// <param name="first">The first</param>
    /// <param name="second">The second</param>
    /// <returns>
    ///     <see cref="MockString" />
    /// </returns>
    public MockString Pick(double percentFirst,
        Func<MockString, MockString> first,
        Func<MockString, MockString> second)
    {
        var percent = Math.Abs(percentFirst % 100);

        var chance = _random.NextDouble() * 100;

        return chance <= percent ? first(this) : second(this);
    }


    /// <summary>
    ///     Current seed value.
    /// </summary>
    public int Seed { get; }

    /// <summary>Appends a space</summary>
    /// <returns></returns>
    public MockString Space() => Append(" ");

    /// <summary>
    ///     Generates an invalid social security number.
    /// </summary>
    /// <returns></returns>
    public MockString SSN()
    {
        // Generate invalid area number (000, 666, or 900-999)
        var area = _random.Next(0, 1000);
        if( area != 0 && area != 666 && area is < 900 or > 999 )
        {
            area = _random.Next(900, 1000); // Force invalid range if valid by chance
        }

        // Generate invalid group number (00)
        var group = _random.Next(0, 99); // Always invalid group number

        // Generate invalid serial number (0000)
        var serial = _random.Next(0, 9999); // Always invalid serial number

        // Format the SSN with leading zeros as needed
        var invalidSSN = $"{area:000}-{group:00}-{serial:0000}";

        return Append(invalidSSN);
    }

    /// <summary>
    ///     Picks a name out of all the street names
    /// </summary>
    /// <returns>A street name</returns>
    public MockString Street() => Append(_streetNames);

    /// <summary>
    ///     Picks a name out of all the street names
    /// </summary>
    /// <param name="isAbbreviation">If true, then an abbreviated street value is returned</param>
    /// <returns>A street name</returns>
    public MockString StreetType(bool isAbbreviation = false)
        => isAbbreviation ? Append(_streetAbbreviationTypes) : Append(_streetTypes);

    public override string ToString() => _value;

    /// <summary>
    ///     Appends a US state.
    /// </summary>
    /// <returns></returns>
    public MockString USState() => Append(_usStates);
}