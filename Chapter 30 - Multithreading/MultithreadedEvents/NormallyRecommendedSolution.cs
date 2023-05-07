//Still has some issues
public ThisEventHandler ThisEvent;

protected virtual void OnThisEvent (ThisEventArgs args)
{
   ThisEventHandler thisEvent=ThisEvent;  //assign the event to a local variable
   If (thisEvent!=null)
   {
      thisEvent (this,args);
   }
}
