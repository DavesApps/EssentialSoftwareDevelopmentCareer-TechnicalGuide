private static object volatile instance = null;
private static readonly object lockobj = new object(); 

public Static Object MySingleton 
{
 	get
	 {
		 if (instance == null) //First check outside lock
		{
				 lock (lockobj)
				 {
					 if (instance == null) //second check inside lock
					{
						 instance = new Singleton(); 
					 }
				 }
		 }
 		return instance;
	 }
 } 
