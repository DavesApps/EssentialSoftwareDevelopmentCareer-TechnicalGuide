//Code has issue
public ThisEventHandler ThisEvent;

protected virtual OnThisEvent (ThisEventArgs args)
{  
      If (ThisEvent!=null)
         ThisEvent (this,args);  // ThisEvent could be null so you could get a null reference exception 
}
