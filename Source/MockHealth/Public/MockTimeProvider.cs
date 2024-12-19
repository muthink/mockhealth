/*
Copyright © 2024 JC Stevens

   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

   The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

   THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

namespace Muthink.MockHealth;

/// <summary>
///     A mock timer, to get predictable results
/// </summary>
public class MockTimeProvider : TimeProvider
{
    private readonly List<MockTimer> _timers = new();
    private DateTimeOffset _time;

    /// <summary>
    ///     A mock timer, to get predictable results
    /// </summary>
    public MockTimeProvider(DateTimeOffset startTime)
    {
        _time = startTime;
    }

    /// <summary>
    ///     Advance the simulated now time
    /// </summary>
    /// <param name="duration">The time to advance the mock timer</param>
    /// <returns>The simulated new time</returns>
    public DateTimeOffset Advance(TimeSpan duration)
    {
        _time += duration;
        MockTimer[] timers;
        lock(_timers)
        {
            if( _timers.Count == 0 )
            {
                return _time;
            }

            timers = _timers.ToArray();
        }

        foreach(var timer in timers)
        {
            timer.TryCallback();
        }

        return _time;
    }

    /// <inheritdoc cref="TimeProvider.CreateTimer"/>
    public override ITimer CreateTimer(TimerCallback callback, object? state, TimeSpan dueTime, TimeSpan period)
    {
        var timer = new MockTimer(this, callback, state, dueTime, period);
        _timers.Add(timer);
        return timer;
    }

    public override TimeZoneInfo LocalTimeZone => TimeZoneInfo.Local;

    /// <inheritdoc cref="TimeProvider.GetUtcNow()"/>
    public override DateTimeOffset GetUtcNow() => _time.ToUniversalTime();

    /// <summary>
    /// Internal call to remove a timer
    /// </summary>
    /// <param name="mockTimer"></param>
    internal void RemoveTimer(MockTimer mockTimer) => _timers.Remove(mockTimer);
}