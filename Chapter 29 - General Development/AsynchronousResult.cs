IAsyncResult result = caller.BeginInvoke(4000, out threadId, null, null);
// Wait for the WaitHandle to become signaled. 
result.AsyncWaitHandle.WaitOne();
