public class TestEventUse
{
    //declare our private event
    private SafeEvent<EventArgs> _myEvent = new SafeEvent<EventArgs>();

    /// <summary>
    /// MyEvent - proxy the add/remove calls to the private event
    /// </summary>
    public event EventHandler<EventArgs> MyEvent
    {
        add
        {
            _myEvent.Event += value;
        }

        remove
        {
            _myEvent.Event -= value;

       	    }

    	}

    /// <summary>
    /// OnMyEvent - standard example idiom of how to fire an event
    /// </summary>
    /// <param name="args"></param>
    protected void OnMyEvent(EventArgs args)
    {

        		_myEvent.FireEvent(this,args); //call our private event to fire it
    }

    /// <summary>
    /// FireEvent - This we provided on our test class as a quick way to fire the event from another class.
    /// </summary>
    public void FireEvent()
    {
        OnMyEvent(new EventArgs());
    }

}
