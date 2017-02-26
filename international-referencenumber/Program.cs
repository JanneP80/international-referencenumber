using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace international_referencenumber
{
    class Program
    {
        static void Main(string[] args)
        {
            // The format of the international reference number is RFXX1234561
            // RF = 2715. And also add 00.

            int menuChoice = 0;

            do
            {
                menuChoice = OpenMenu();

                if (menuChoice == 1)
                {
                    ValidityCheckAndCreation(4,20,1);
                }
                if (menuChoice == 2)
                {
                    ValidityCheckAndCreation(3,19,2);
                }
                
            } while (menuChoice != 3); // menu 3 for exit
            Console.ReadKey();
        }

        private static int OpenMenu()
        {
            Console.WriteLine("\nInternational Reference number creator.\nMenu: press number for operation:\n");
            Console.WriteLine("1. Check validity of international reference number.");
            Console.WriteLine("2. Create new international reference number.");
            Console.WriteLine("3. Exit");
            var result = Console.ReadLine();
            return Convert.ToInt32(result); // don't select empty line
        }

        private static void ValidityCheckAndCreation(int referMax, int referMin, int operOption )
        {
            if (operOption == 1)
            {
                Console.WriteLine("Please input reference number for validation (4-20 digits): ");
            }

            if (operOption == 2)
            {
                Console.WriteLine("Please input number for creating reference number (3-19 digits): ");
            }
            
            string referenceNumber = NumberInput(referMin, referMax);
            int checksum10 = calculateChecksum(referenceNumber, operOption); // call method for calculating checksum
            referenceNumber = FormatReference(referenceNumber, checksum10, operOption); // if 2 then add, if 1 then no adding
            //Console.WriteLine(referenceNumber);
        }

        

    }
}
