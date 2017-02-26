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

        private static void ValidityCheckAndCreation(int referMin, int referMax, int operOption )
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
            int checksum10 = calculateChecksum(referenceNumber, operOption); // call method for calculating checksum
            referenceNumber = FormatReference(referenceNumber, checksum10, operOption); // if 2 then add, if 1 then no adding
            //Console.WriteLine(referenceNumber);
        }

        private static string NumberInput(int v1, int v2, int v3)
        {
            string referenceNumber;
        b2:
            {
                referenceNumber = Console.ReadLine();
                referenceNumber = referenceNumber.Replace(" ", "");
                if (v3 == 1)
                {
                    if (referenceNumber[0] == 'R' && referenceNumber[1] == 'F')
                    {
                        // referenceNumber = referenceNumber.Insert(1, "0"); // adding 2715 into the end 
                        // referenceNumber = referenceNumber.Insert(3, "0");
                        referenceNumber = referenceNumber.Replace('R', '2'); // 27
                        referenceNumber = referenceNumber.Insert(1, "7");
                        referenceNumber = referenceNumber.Replace('F', '1'); // 15
                        referenceNumber = referenceNumber.Insert(3, "5");
                        Console.WriteLine("mr referee{0}", referenceNumber);
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
            if (v3 == 2)
            {
                referenceNumber = referenceNumber + 271500;
            }
            // return referenceNumber
            return referenceNumber;
        }

        private static int calculateChecksum(string referenceNumber, int v)
        {
            int calculationSum = 0;
            int[] weightedMultipler = new int[] { 7, 3, 1, 7, 3, 1, 7, 3, 1, 7, 3, 1, 7, 3, 1, 7, 3, 1, 7 }; // new int[19] 

            if (v == 1)
            {
                referenceNumber = referenceNumber.Remove(referenceNumber.Length - 1);
                // First remove checksum from the end
            }
            var referenceInt = referenceNumber.Select(ch => ch - '0').ToArray();
            Array.Reverse(referenceInt); // reverse array for right to left calcs

            for (int i = 0; i < referenceInt.Length; i++) // calc 
            {
                calculationSum += (referenceInt[i] * weightedMultipler[i]); // from right to left multiply as the array is reversed and shortened
            }

            int sumCeil = (int)(Math.Ceiling(calculationSum / 10.0d) * 10);
            int checksum1 = sumCeil - calculationSum;

            return checksum1;
        }

        private static string FormatReference(string referenceNumber, int checksum4, int v)
        {
            if (v == 2)
            {
                referenceNumber = referenceNumber + checksum4;
            }
            // Console.WriteLine(referenceNumber);

            var referenceInt = referenceNumber.Select(ch => ch - '0').ToArray();
            if (referenceInt[referenceInt.Length - 1] == checksum4) // compare last number
            {
                // for (int j = 0; j < (referenceInt.Length - 1); j++)
                for (int j = (referenceInt.Length); j > 0; j--)
                {
                    j -= 5;
                    if (j < 1) goto o2;
                    referenceNumber = referenceNumber.Insert(j, " ");
                }
            o2:
                Console.WriteLine("{0} OK", referenceNumber);
            }
            else
                Console.WriteLine("Referencenumber Incorrect. {0} != {1}", (referenceInt[referenceInt.Length - 1]), checksum4);

            return referenceNumber;
        }
    }
}
