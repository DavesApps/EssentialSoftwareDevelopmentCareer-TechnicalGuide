int slotData = randomGenerator.Next(1, 10000);
 int threadId = Thread.CurrentThread.ManagedThreadId; 

Thread.SetData(Thread.GetNamedDataSlot("MyRandomValue"), slotData); 
