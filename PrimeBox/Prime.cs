using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrimeBox
{
    public class Prime
    {
        public int Max = 10000000;
        public bool[] prime = new bool[10000001];
        public void GetPrime()
        {
            int i, j, l = (int)Math.Floor((double)Math.Sqrt(Max));
            for (i = 0; i <= Max; i++)
                prime[i] = false;
            for (i = 2; i <= l; i++)
            {
                if (prime[i] == false)
                    for (j = i * 2; j <= Max; j += i)
                        prime[j] = true;
            }

        }

    }
}
