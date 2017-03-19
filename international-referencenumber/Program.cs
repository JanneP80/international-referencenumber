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
        private static void Main(string[] args)
        {
            // The format of the international reference number is RFXX1234561
            // RF = 2715. And also add 00.

            var menuChoice = 0;

            do
            {
                menuChoice = OpenMenu();

                if (menuChoice == 1)
                {
                    ValidityCheckAndCreation(4, 20, 1);
                }
                if (menuChoice == 2)
                {
                    ValidityCheckAndCreation(3, 19, 2);
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

        private static void ValidityCheckAndCreation(int referMin, int referMax, int operOption)
        {
            if (operOption == 1)
            {
                Console.WriteLine("Please input reference number for validation (4-20 digits): ");
            }

            if (operOption == 2)
            {
                Console.WriteLine("Please input number for creating reference number (3-19 digits): ");
            }

            string referenceNumber = NumberInput(referMin, referMax, operOption);
            int checksum10 = CalculateChecksum(referenceNumber, operOption); // call method for calculating checksum
            FormatReference(referenceNumber, checksum10, operOption); // if 2 then add, if 1 then no adding
            // Console.WriteLine(referenceNumber);
        }

        private static string NumberInput(int v1, int v2, int v3)
        {
            string referenceNumber;
            b2:
            {
                referenceNumber = Console.ReadLine();
                referenceNumber = referenceNumber.Replace(" ", "");
                if (v3 == 1) // v3 ="1" check validity
                {
                    if (referenceNumber[0] == 'R' && referenceNumber[1] == 'F')
                    {
                        referenceNumber=referenceNumber.Remove(0,2);
                        
                        referenceNumber = referenceNumber.Insert(referenceNumber.Length, "2"); // 27 R
                        referenceNumber = referenceNumber.Insert(referenceNumber.Length, "7");
                        referenceNumber = referenceNumber.Insert(referenceNumber.Length, "1"); // 15 F
                        referenceNumber = referenceNumber.Insert(referenceNumber.Length, "5");

                        char a =referenceNumber[0];
                        char b= referenceNumber[1];
                        referenceNumber=referenceNumber.Remove(0,2);
                        referenceNumber = referenceNumber + a + b;
                        // Console.WriteLine("mr referee {0}", referenceNumber);
                        
                        v1 += 4;
                        v2 += 4; // fix the logic somewhere TODO!!!
                    }
                    else
                    {
                        Console.WriteLine("Please input correct international reference number: ");
                    }
                }

                if (referenceNumber.Length < v1 | referenceNumber.Length > v2 | !referenceNumber.All(Char.IsNumber))
                {
                    Console.WriteLine("Please input correct number: ");
                    goto b2;
                }
                else goto o1;
            }
            o1:
            // add RF = 2715. And also add 00
            if (v3 == 2) // v3 = "2" = create new ref
            {
                referenceNumber = referenceNumber + 271500;
            }
            // return referenceNumber
            return referenceNumber;
        }

        private static int CalculateChecksum(string referenceNumber, int v)
        {
            int checksum1 = 0;
            decimal counting = 0;
            decimal referenceNumberinteger = Convert.ToDecimal(referenceNumber);

            counting = (referenceNumberinteger % 97);
            checksum1 = (int)(98 - counting);
            Console.WriteLine("jakojään. {0} tark. {1}", counting, checksum1);
            if (v == 1)
            {
                checksum1 = (int)counting;
            }

            return checksum1;
        }

        private static string FormatReference(string referenceNumber, int checksum4, int v)
        {

            string end2 = checksum4.ToString();
            // If under 10, add extra zero
            if (checksum4 < 10)
            {
                end2 = '0' + end2;
            }
            else
            {
                // if over 10, ok

            }

            if (v == 2) // v = "2" = create new ref
            {
                referenceNumber = "RF" + end2 + referenceNumber;
                referenceNumber = referenceNumber.Remove(referenceNumber.Length - 6);

                var referenceInt = referenceNumber.Select(ch => ch - '0').ToArray();
                // for (int j = 0; j < (referenceInt.Length - 1); j++)
                for (int j = 0; j < (referenceInt.Length-1); j++)
                {
                    j += 4;
                     if (j > referenceInt.Length-1) goto o2;
                    referenceNumber = referenceNumber.Insert(j, " ");
                }
                 o2:
                Console.WriteLine("{0}", referenceNumber);
            }
            // Console.WriteLine(referenceNumber);
            //

            if (v == 1) // v="1" = check validity
            {
                var referenceInt = referenceNumber.Select(ch => ch - '0').ToArray();
                if (referenceInt[referenceInt.Length - 1] == checksum4) // compare last number
                {
                    
                    Console.WriteLine("{0} = {1} --> Referencenumber OK", checksum4, referenceInt[referenceInt.Length - 1]);
                }
                else
                    Console.WriteLine("Referencenumber Incorrect. {0} != {1}", (referenceInt[referenceInt.Length - 1]),
                        checksum4);

            }
             return referenceNumber;
        }
    }
}
