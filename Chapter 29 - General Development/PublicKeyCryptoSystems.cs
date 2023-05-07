// DO NOT USE THIS CODE FOR ACTUAL ENCRYPTION - It was kept simple to demonstrate the basics rather than realistic. But the principles are the same

Random random = new Random();  //Not suitable for actual encryption. Use crypto random.

int p = 229, q = 107; 	//pick two prime numbers (obviously MUCH bigger than these for 
				//real use. Small numbers used to focus on the code.)

int n = p * q;   //n= number to use with modulo operator

int z = (p - 1) * (q - 1);  // computer one higher than the limit of e (z=72)

// 1 < e < z  Find an e (pick a random number that matches the criteria
//Also has no common factos with z - I used IsPrime to enforce this but it really only
//needs to avoid common factors - Don’t ever use this random number generator for real
// encryption (use a cryptographic random number generator)
int e = random.Next(2, z - 1);
            
while (!IsPrime(e))		//This IsPrime is too slow for large prime’s ok for tiny
   e=random.Next(2, z - 1);
      

//find a good d where (e*d)-1 % z=0 (evenly divisible by z)
// This is probably not the best way to find a d that works
// But a quick dirty algo that seemed to work (Too slow for large prime’s don’t use)
// Some writeups say  d < z
int d;
for (d=1; d < z; d++)
{
    if ((((e * d) - 1) % z) == 0 && e!=d)
        break;  //d is good
}

//visualize our key pairs
var publicKey = (n, e);
var privateKey = (n, d);

//We will encrypt the letter A which is 65
BigInteger messageToEncrypt = 'A';

//Encrypt our message (umm well character in this case)
BigInteger encryptedMessage = BigInteger.ModPow(messageToEncrypt, publicKey.e, publicKey.n);

//Decrypt our message - result will be 65 'A'
BigInteger decryptedMessage = BigInteger.ModPow(encryptedMessage, privateKey.d, privateKey.n);



//IsPrime(n) is from Wikipedia (“Primality Test” - Wikipedia, n.d.) https://en.wikipedia.org/wiki/Primality_test
