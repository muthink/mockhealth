/*
Copyright © 2024 JC Stevens

   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

   The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

   THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

namespace Muthink.MockHealth;

/// <summary>
///     The base mocker, which extends the <see cref="Random" /> class
/// </summary>
public class MockBase
{
    protected readonly TimeProvider TimeProvider;

    /// <summary>
    ///     The base mocker, which extends the <see cref="Random" /> class
    /// </summary>
    public MockBase(int seed, TimeProvider? timeProvider = null)
        : this(new Random(seed), timeProvider)
    {
    }

    /// <summary>
    ///     The base mocker, which extends the <see cref="Random" /> class
    /// </summary>
    public MockBase(Random random, TimeProvider? timeProvider = null)
    {
        Rand = random;
        TimeProvider = timeProvider ?? TimeProvider.System;
    }

    /// <summary>
    ///     Generates a birthdate, given a minimum and maximum age
    /// </summary>
    /// <param name="minAge">Minimum age, in years</param>
    /// <param name="maxAge">Maximum age, in years</param>
    /// <returns>The age</returns>
    /// <exception cref="ArgumentException"></exception>
    public DateOnly BirthDate(int minAge, int maxAge)
    {
        // Validate input
        if( minAge < 0 || maxAge < minAge )
        {
            throw new ArgumentException(
                "Invalid age range. Minimum age must be non-negative and less than or equal to maximum age.");
        }

        // Calculate the date range for the birthdate
        var today = Now;
        var maxBirthdate = today.AddYears(-minAge).Date; // Youngest person
        var minBirthdate = today.AddYears(-maxAge - 1).AddDays(1).Date; // Oldest person

        // Generate a random birthdate within the range
        var rangeInDays = (maxBirthdate - minBirthdate).Days;
        var birthDatetime = minBirthdate.AddDays(Rand.Next(rangeInDays + 1));

        return new DateOnly(birthDatetime.Year, birthDatetime.Month, birthDatetime.Day);
    }

    /// <summary>
    ///     Picks a <see cref="TimeSpan" /> between <paramref name="minimum" /> and <paramref name="maximum" />.
    /// </summary>
    /// <param name="minimum">The minimum <see cref="TimeSpan" /></param>
    /// <param name="maximum">The maximum <see cref="TimeSpan" /> to go back</param>
    /// <returns>a value between <paramref name="minimum" /> and </returns>
    public TimeSpan Next(TimeSpan minimum, TimeSpan maximum)
    {
        if( minimum > maximum )
        {
            throw new ArgumentException(
                $"Your {nameof(minimum)} timespan of {minimum} is bigger than {nameof(maximum)} {maximum}");
        }

        var ticks = Rand.NextInt64(minimum.Ticks, maximum.Ticks);
        return new TimeSpan(ticks);
    }

    /// <summary>Picks a <see cref="DateTime" /> between a range. </summary>
    /// <param name="oldest"></param>
    /// <param name="newest"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public DateTime Next(DateTime oldest, DateTime newest)
    {
        if( newest > oldest )
        {
            throw new ArgumentException(
                $"Your {nameof(oldest)} date of {oldest} is newer than {nameof(newest)} {newest}");
        }

        var ticks = Rand.NextInt64(oldest.Ticks, newest.Ticks);
        return new DateTime(ticks);
    }

    /// <summary>Picks a <see cref="DateTime" /> between a range. </summary>
    /// <param name="oldest"></param>
    /// <param name="newest"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public DateTimeOffset Next(DateTimeOffset oldest, DateTimeOffset newest)
    {
        if( newest < oldest )
        {
            throw new ArgumentException(
                $"Your {nameof(oldest)} date of {oldest} is newer than {nameof(newest)} {newest}");
        }

        var ticks = Rand.NextInt64(newest.UtcTicks - oldest.UtcTicks);

        return oldest + new TimeSpan(ticks);
    }

    /// <summary>Generate a random value</summary>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    public int Next(int minValue, int maxValue) => Rand.Next(minValue, maxValue);


    /// <summary>Generate a random value</summary>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    public double Next(double minValue, double maxValue)
    {
        if( maxValue < minValue )
        {
            throw new ArgumentException($"minimum {minValue} is greater than maximum {maxValue}");
        }

        var difference = maxValue - minValue;
        var value = Rand.NextDouble() * difference + minValue;
        return value;
    }

    /// <summary>Generate a random value</summary>
    /// <param name="minValue">Minimum integer value</param>
    /// <param name="maxValue">Maximum integer value</param>
    /// <returns></returns>
    public long NextInt64(long minValue, long maxValue) => Rand.NextInt64(minValue, maxValue);

    /// <summary>
    ///     Local Date Time
    /// </summary>
    public DateTimeOffset Now => TimeProvider.GetLocalNow().DateTime;

    /// <summary>
    ///     The underlying random number generator
    /// </summary>
    protected Random Rand { get; }

    /// <summary>
    ///     UTC Date time.
    /// </summary>
    public DateTimeOffset UtcNow => TimeProvider.GetUtcNow().UtcDateTime;
}