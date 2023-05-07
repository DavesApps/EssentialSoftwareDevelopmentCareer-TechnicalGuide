/// <summary>
/// TestEventUseSolveProblemSeven
/// Test the SafeEvent class for implementing an event using
/// SafeEvent – we want StartEvent to be fired before EndEvent
/// This shows how to solve problem #7.  If FireEvent() is that
/// only thing that could be used to fire events then
/// that will guarantee a StartEvent is called before an EndEvent()
/// </summary>
public class TestEventUseSolveProblemSeven
{
    //declare our private event
    private SafeEvent<EventArgs> _startEvent = new SafeEvent<EventArgs>();
    private SafeEvent<EventArgs> _endEvent = new SafeEvent<EventArgs>();
    private object _lock = new object();
    bool _started = false;

    /// <summary>
    /// StartEvent - proxy the add/remove calls to the private event
    /// </summary>
    public event EventHandler<EventArgs> StartEvent
    {
        add
        {
            _startEvent.Event += value;
        }

        remove
        {
            _startEvent.Event -= value;

        }

    }

    public event EventHandler<EventArgs> EndEvent
    {
        add
        {
            _endEvent.Event += value;
        }

        remove
        {
            _endEvent.Event -= value;

        }

    }

    /// <summary>
    /// OnStartEvent - standard example idiom of how to fire an event
    /// </summary>
    /// <param name="args"></param>
    protected void OnStartEvent(EventArgs args)
    {

        _startEvent.FireEvent(this, args); //call our private event to fire it
    }

    /// <summary>
    /// OnEndEvent - standard example idiom of how to fire an event
    /// </summary>
    /// <param name="args"></param>
    protected void OnEndEvent(EventArgs args)
    {

        _endEvent.FireEvent(this, args); //call our private event to fire it
    }

    /// <summary>
    /// FireEvent - This we provided on our test class as a quick way to fire
    /// the event from another class.
    /// </summary>
    public void FireEvent()
    {
        // by using a lock to prevent other events from being fired and managing our
        // state we can guarantee event ordering.
        lock (_lock)
        {
            if (_started)
                OnEndEvent(new EventArgs());
            else
                OnStartEvent(new EventArgs());
            _started = !_started;

        }
    }

}
