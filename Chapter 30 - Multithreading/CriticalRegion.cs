public void DoMyWork() 
{ 
    // Code here can be aborted without affecting other work 
    Thread.BeginCriticalRegion();
     // Protected code here 
     Thread.EndCriticalRegion(); 
}