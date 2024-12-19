/*
Copyright © 2024 JC Stevens

   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

   The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

   THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

namespace Muthink.MockHealth;

internal class MockTimer : ITimer
{
    private readonly TimerCallback _callback;
    private readonly object? _state;
    private readonly MockTimeProvider _timeProvider;
    private DateTimeOffset _nextTime;
    private TimeSpan _period;

    public MockTimer(MockTimeProvider timeProvider, TimerCallback callback, object? state,
        TimeSpan dueTime, TimeSpan period)
    {
        _timeProvider = timeProvider;
        _callback = callback;
        _state = state;
        _nextTime = timeProvider.GetUtcNow() + dueTime;
        _period = period;
    }

    /// <summary>
    ///     Attempts to callback the method provided.
    /// </summary>
    /// <returns>True if the callback was called at least once</returns>
    internal bool TryCallback()
    {
        var isCalled = false;
        var utcNow = _timeProvider.GetUtcNow();
        while( _nextTime < utcNow )
        {
            isCalled = true;
            _callback(_state);
            if( _period > TimeSpan.Zero )
            {
                _nextTime += _period;
            }
            else
            {
                Dispose();
            }
        }

        return isCalled;
    }

    public ValueTask DisposeAsync()
    {
        Dispose();
        return ValueTask.CompletedTask;
    }

    public void Dispose() => _timeProvider.RemoveTimer(this);

    public bool Change(TimeSpan dueTime, TimeSpan period)
    {
        _period = period;
        _nextTime = _timeProvider.GetUtcNow() + dueTime;
        return true;
    }
}