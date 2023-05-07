class AutoResetEventTest
    {
        static AutoResetEvent _autoresetEvent = new AutoResetEvent(false);
        static void MainThread()
        {
            new Thread(WaitOnAutoResetEvent).Start();
            Thread.Sleep(3000);                  // delay a bit
            _autoresetEvent.Set();                    // notify the other thread
        }

        static void WaitOnAutoResetEvent()
        {
            Console.WriteLine("Waiting for Set() to be called...");
            _autoresetEvent.WaitOne();                // Wait for notification
            Console.WriteLine("Signaled and running...");
        }
    }
