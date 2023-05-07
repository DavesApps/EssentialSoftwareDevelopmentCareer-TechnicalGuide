ThreadLocal<string> MyThreadName = new ThreadLocal<string>(() => 
{ return "Thread id:" + Thread.CurrentThread.ManagedThreadId; }); 

MyThreadName.Dispose(); 
