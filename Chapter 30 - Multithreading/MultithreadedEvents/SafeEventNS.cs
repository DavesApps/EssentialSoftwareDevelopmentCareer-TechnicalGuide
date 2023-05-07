/// <summary>
/// SafeHandlerInfo - used for storing handler info, plus contains a flag
/// indicating if subscribed or not and the reader writer lock
/// for when the subscription is read or updated.
/// </summary>
/// <typeparam name="TArgs">Event args</typeparam>
internal class SafeHandlerInfo< TArgs> where TArgs : EventArgs
{
    public EventHandler<TArgs> Handler;
    public bool Subscribed = true;
    //public object LockObj = new object();

    public ReaderWriterLockSlim Lock = new ReaderWriterLockslim();

    public SafeHandlerInfo(EventHandler<TArgs> handler)
    {
        Handler = handler;
    }

}

/// <summary>
/// SafeEvent class provides safety for unsubscribing from events
/// so even with multiple threads after unsubscribing, it will not get called.
/// Also makes sure that a null exception won't happen due to the removing of events
/// </summary>
/// <typeparam name="TArgs">The type of the event args</typeparam>
public class SafeEventNS<TArgs>
    where Targs : EventArgs

{

    Dictionary<EventHandler<TArgs>,
        SafeHandlerInfo<TArgs>> _handlers = new Dictionary<EventHandler<TArgs>,
        SafeHandlerInfo<TArgs>>();
    private ReaderWriterLockSlim _rwLock = new ReaderwriterLockSlim();

    public event EventHandler<TArgs> Event {
        add
        {
            Subscribe(value);
        }

        	   remove
        {
            Unsubscribe(value);
        }

    }

    /// <summary>
    /// Used to fire this event from within the class using the SafeEvent
    /// This reads a copy of the list and calls all the subscribers in it that are still subscribed.
    /// Anything that was unsubscribed gets removed at the end of the event call.  It was handler here since
    /// The copy of the list might be held by multiple threads unsubscribe flags a handler unsubscribed and removes it.
    /// that way if it is still in the list it will not be called
    ///
    /// </summary>
    /// <param name="args">The event args</param>
    public virtual void FireEvent(object sender,TArgs args)
    {
        _rwLock.EnterReadLock();
        List<SafeHandlerInfo<TArgs>> localHandlerInfos = _handlers.Values.ToList();
        _rwLock.ExitReadLock();

        foreach (SafeHandlerInfo<TArgs> info in localHandlerInfos)
        {
            info.Lock.EnterReadLock();
            try
            {
                if (info.Subscribed)
                {
                    EventHandler<TArgs> handler = info.Handler;
                    try
                    {
                        handler(sender, args);
                    }
                    catch { };
                }

           	 	}
            finally
            {
                info.Lock.ExitReadLock();
            }
        }

    }


    /// <summary>
    /// Unsubscribe - internally used to unsubscribe a handler from the event
    /// </summary>
    /// <param name="unsubscribeHandler">The handler being unsubscribed</param>
    protected void Unsubscribe(EventHandler<TArgs> unsubscribeHandler)
    {

        	_rwLock.EnterWriteLock();
        try
        {

            SafeHandlerInfo<TArgs> handler = null;

            if (_handlers.TryGetValue(unsubscribeHandler, out handler))
            {
                handler.Lock.EnterWriteLock();
                try
                {

                    handler.Subscribed = false;

                    _handlers.Remove(handler.Handler);

               	 }
                finally
                {
                    handler.Lock.ExitWriteLock();
                }
            }
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            _rwLock.ExitWriteLock();
        }

    }

    /// <summary>
    /// Subscribe - Called to subscribe the handler
    /// </summary>
    /// <param name="eventHandler">The handler to subscribe</param>
    protected void Subscribe(EventHandler<TArgs> eventHandler)
    {

       	 _rwLock.EnterWriteLock();
        try
        {

            SafeHandlerInfo<TArgs> handlerInfo = null;

            if (!_handlers.TryGetValue(eventHandler, out handlerInfo))
            {
                handlerInfo = new SafeHandlerInfo<TArgs>(eventHandler);


                handlerInfo.Lock.EnterWriteLock();
                try
                {

                    _handlers.Add(eventHandler, handlerInfo);
                }
                finally
                {
                    handlerInfo.Lock.ExitWriteLock();
                }
            }
            else
            {
                handlerInfo.Lock.EnterWriteLock();
                try
                {

                    handlerInfo.Subscribed = true;
                }
                finally
                {
                    handlerInfo.Lock.ExitWriteLock();
                }

            }
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            _rwLock.ExitWriteLock();
        }

    }

}
Let’s show an example of how to use this, which is the same way as the previous SafeEvent class.
C#
Shrink ▲   Copy Code
/// <summary>
    /// TestEventUser
    /// Test the SafeEvent class for implementing an event using SafeEvent
    /// </summary>
    public class TestSafeEventNS
    {
        //declare our private event 
        private SafeEventNS<EventArgs> _myEvent = new SafeEventNS<EventArgs>();

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
