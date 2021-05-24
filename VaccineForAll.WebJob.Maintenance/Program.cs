using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccineForAll.Libraries;

namespace VaccineForAll.WebJob.Maintenance
{
    class Program
    {
        static void Main(string[] args)
        {
            //Main
            Console.WriteLine(" ***** VaccineForAll.WebJob.Maintenance - Start *****");
            OperationsCRUD.MaintenanceResetHasProcessed();
            Console.WriteLine(" ***** VaccineForAll.WebJob.Maintenance - End *****");
        }
    }
}
