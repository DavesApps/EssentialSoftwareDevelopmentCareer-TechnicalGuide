public static event EventHandler MyEvent;
var handlers = MyEvent;
if (handlers != null)
	handlers(this, args);
