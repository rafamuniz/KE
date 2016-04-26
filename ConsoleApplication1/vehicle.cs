using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class vehicle
    {
        private Int32 Id { get; set; }

        public String Name { get; set; }

        protected String Color { get; set; }

        internal Int32 Quantity { get; set; }
    }
}
