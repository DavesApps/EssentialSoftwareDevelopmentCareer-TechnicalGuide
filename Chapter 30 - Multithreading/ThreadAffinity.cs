BeginThreadAffinity, EndThreadAffinity


[SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.ControlThread)]
 public class WrapperClassForThreadAffinity
  { 
    [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.ControlThread)]
     public void PerformMyTask()
      { 
        // Code that does not have thread affinity goes here.
         Thread.BeginThreadAffinity(); 
         // Code that has a thread affinity goes here. 
         Thread.EndThreadAffinity(); 
         // And More code that doesn’t have a thread affinity. 
      }
  }