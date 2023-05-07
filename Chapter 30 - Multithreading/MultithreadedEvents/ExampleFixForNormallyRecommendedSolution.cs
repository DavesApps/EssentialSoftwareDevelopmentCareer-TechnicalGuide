[ThreadStatic] volatile ThisEventHandler _localEvent = null; //need volatile to ensure we get the latest
public virtual void OnThisEvent (EventArgs args)
{
   _localEvent=ThisEvent; //assign the event to a local variable – notice is volatile class field
   if (_localEvent!=null)
   {
     _localEvent(this,args);
   }
}
