/*
Copyright © 2024 JC Stevens

   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

   The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

   THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using Hl7.Fhir.Model;

namespace Muthink.MockHealth.FHIR.R4.Internal;
public class MockFHIRGeneratorBase
{
    protected MockString Mocker;

    protected MockFHIRGeneratorBase(int seed = 0)
    {
        Mocker = new MockString(seed);
    }

    public Address GenerateAddress()
    {
        return new Address()
        {
            City = Mocker.City(),
            Country = "USA",
            State = Mocker.USState(),
            Line = new[] { Mocker.Integer(1, 999).Space().Street().StreetType().ToString() },
            Type = Address.AddressType.Physical,
            Use = Address.AddressUse.Home
        };
    }

    public HumanName GenerateHumanName()
    {
        return new HumanName()
        {
            Family = Mocker.LastName(),
            Given = new string[] { Mocker.FirstName(), Mocker.MiddleName() },
            Use = HumanName.NameUse.Official
        };
    }
}
