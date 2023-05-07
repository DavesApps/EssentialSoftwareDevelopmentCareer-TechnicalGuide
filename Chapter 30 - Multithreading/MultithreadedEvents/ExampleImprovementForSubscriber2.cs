Private ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();  

private volatile bool _unsubscribed=false;

private void MethodToUnsubscribe()
{
 	_rwLock.EnterWriteLock();
       _unsubscribed = true;
                
       ThisEvent -= new ThisEventHandler(ThisEventChanged);
       _rwLock.ExitWriteLock();
}

public void Subscribe()
{
            
            _rwLock.EnterWriteLock();
            _unsubscribed = false;
                
            ThisEvent += new ThisEventHandler(ThisEventChanged);
            _rwLock.ExitWriteLock();
            
}

//Event handler – due to read lock could get called out of sequence by multiple threads
private void ThisEventChanged(object sender, ThisEventArgs e) 
{
   
_rwLock.EnterReadLock();
	try
	{
       	if (!_unsubscribed)
       	{
			//Do work here
       	}
	}
	finally
	{
       	_rwLock.ExitReadLock();         
      }
}
