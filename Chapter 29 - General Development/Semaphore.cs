class HazardChecker   {
  static SemaphoreSlim _semaphore = new SemaphoreSlim (5);    // 5 People allowed in
   static void Main()
  {
       for (int i = 1; i <= 7; i++) new Thread (Enter).Start (i);
  }
   static void Enter (object id)   {
    Console.WriteLine ("Person ID: "+id + " would like to enter.");
    _semaphore.Wait();
    Console.WriteLine ("Person ID:"+id + " ID is in!");           // Only five threads
    Thread.Sleep (500 * (int) id);               // sit down for a while up to 500*id milliseconds
    Console.WriteLine ("Person ID:"+id + " is leaving soon.");      
    _semaphore.Release();
    Console.WriteLine ("Person ID:"+id + " has left now.");   
  }
}
