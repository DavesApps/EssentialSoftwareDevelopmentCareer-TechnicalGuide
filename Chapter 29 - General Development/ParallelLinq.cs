var source = Enumerable.Range(1, 20000);
//Opt in to PLINQ with AsParallel.
 var evenNumbers = from num in source.AsParallel() where num % 2 == 0 select num; 
