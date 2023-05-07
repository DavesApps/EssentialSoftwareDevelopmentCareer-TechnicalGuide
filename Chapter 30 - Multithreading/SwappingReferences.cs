volatile List<int> _instance = new List<int>(); 
public int Count { get { return _instance.Count; } }
public void AddMyItem(int item) { 
         lock (_lockObject) 
          {
 	           List<int> items = new List<int>(_instance);     //copy
                items.Add(item); 
                _instance = items;   
          }  
}  // … delete methods, etc.
