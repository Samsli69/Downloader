using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projets
{
    internal class ASCII
    {
        private string ascii;
        public ASCII(string ascii)
        {
            this.ascii = ascii;
        }

        public override string ToString()
        {
            return ascii.ToString();
        }
    }
}
