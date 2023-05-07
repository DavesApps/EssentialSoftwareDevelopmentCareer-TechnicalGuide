/// <summary>
        /// Recursive
        /// Calculate val*val + (val-1)*(val-1) + (val-2)*(val-2) continuing until val-N==1 includes that then stops
        /// 
        /// So val=3 would be (3*3)+(2*2)+(1*1)=14
        /// if val<=0 return 0
        /// </summary>
        public static int CalculateRecursive(int val)
        {
            	if (val == 1)
                return 1;

            	if (val <= 0)
                return (0);

            	return (val * val) + CalculateRecursive(val - 1);

        	   }

        /// <summary>
        /// Iterative
        /// 
        /// Calculate val*val + (val-1)*(val-1) + (val-2)*(val-2) continuing until val-N==1 includes that then stops
	  /// So val=3 would be (3*3)+(2*2)+(1*1)=14
        /// if val<=0 return 0
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int CalculateIterative(int val)
        {
            	if (val <= 0)
                return (0);

           		 if (val == 1)
                return 1;

           		 int total = 0;

            	while (val >=1)
            	{
                	total = total + (val * val);
                	val = val - 1;
            	}

            	return total;

        	   }
