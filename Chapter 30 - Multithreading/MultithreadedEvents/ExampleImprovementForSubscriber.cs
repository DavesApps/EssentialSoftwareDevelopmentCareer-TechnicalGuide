private object _lock = new object();  
private volatile bool _unsubscribed=false;

private void MethodToUnsubscribe()
{
 	lock(_lock)
       {
       	_unsubscribed = true;
                
       	ThisEvent -= new ThisEventHandler(ThisEventChanged);
	   }
}

public void Subscribe()
{
       lock(_lock)
	  {
            _unsubscribed = false;
      
            ThisEvent += new ThisEventHandler(ThisEventChanged);
       }            
}

//Event handler
private void ThisEventChanged(object sender, ThisEventArgs e) 
{
   
	lock(_lock)
	{
       	if (!_unsubscribed)
       	{
			//Do work here
       	}
	}
	
}
