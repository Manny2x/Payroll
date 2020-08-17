using System;
using System.IO;

namespace Payroll
{
    class Program
    {
        static int universalStaffPay = 0;
        static readonly string Pass = File.ReadAllText(@"c:\temp\Password.txt");
        static void Main()
        {
            // Window Settings            
            Console.Title = "Payroll App";
            Directory.CreateDirectory(@"c:\temp");
            Console.BackgroundColor = ConsoleColor.DarkGreen;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;

            int ManagerChoice;

            string passwordChoice;

        // PassWord Function
        checkPass:
            Console.WriteLine("Enter Password: ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            passwordChoice = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Black;

            if (passwordChoice.Equals(Pass))
            {
                Console.Clear();
            }
            else
            {
                Console.WriteLine("PassWord Incorrect");
                Console.ReadLine();

                goto checkPass;
            }
            // Main Menu
            while (true)
            {
            reChoose:
                MainDrawStrings();

                ManagerChoice = Convert.ToInt32(Console.ReadLine());
                // Sub-Menu Choice Function

                switch (ManagerChoice)
                {
                    case 1:
                        Console.Clear();
                        StaffPay();
                        break;

                    case 2:
                        Console.Clear();
                        AddStaff();
                        break;

                    case 3:
                        Console.Clear();
                        RemoveStaff();
                        break;

                    case 4:
                        Console.Clear();
                        GetStaffMem();
                        break;
                    case 5:
                        Console.Clear();
                        SeeLogs();
                        break;
                    case 6:
                        Console.Clear();
                        ChangePass();
                        break;
                    default:
                        Console.WriteLine("You did not give a correct choice, re-choose please!");
                        Console.Read();
                        goto reChoose;
                }
            }
        }

        public static void MainDrawStrings()
        {
            Console.Clear();
            Console.WriteLine("============================================================" +
                "============================================================");
            Console.WriteLine("                                                  PAYROLL APP");
            Console.WriteLine("=============================================================" +
                "=========================================================== ");

            Console.WriteLine("If You Would Like To See Your Staff's Payments Press 1: \n" +
                "If You Would Like To Add A Staff Member Press 2: \n" +
                "If You Would Like To Remove A Staff Member Press 3: \n" +
                "If You Would Like To See All The Staff Members Press 4: \n" +
                "If You Would Like To See The Logs Press 5: \n" +
                "If You Would Like To Change The Password Press 6: \n" +
                "                                                       Time until next payday: " + GetTimeToPayday() + " days");

        }

        public static int GetTimeToPayday()
        {
            string month = DateTime.Now.ToString("MM");

            if (month == "09"|| month == "04" || month == "06" ||month == "11")
            {
                return 32 - DateTime.Now.Day;
            } else if(month == "02")
            {
                int year = DateTime.Now.Year;
                if (year % 4 == 0)
                {
                    if (year % 100 == 0)
                    {
                        if (year % 400 == 0)
                            return 30 - DateTime.Now.Day;
                        else
                            return 29 - DateTime.Now.Day;
                    }
                    else
                        return 30 - DateTime.Now.Day;
                }
                else
                {
                    return 29 - DateTime.Now.Day;
                }
            }
            else
            {
                return 32 - DateTime.Now.Day;
            }
        }
        public static void ChangePass()
        {
            toPass:
            Console.WriteLine("Please give the password: ");
            string originalPass = Console.ReadLine();

            if (originalPass != Pass) {
                Console.WriteLine("Incorrect Password");
                goto toPass;
            } else
            {
                Console.WriteLine("Enter the new password: ");

                File.WriteAllText(@"c:\temp\Password.txt", Console.ReadLine());
    

                LogAction("Password changed");
            }


        }

        static void LogAction(string action)
        {

            if (!File.Exists(@"c:\temp\ActionLogs.txt")){
                using StreamWriter streamCreate =
                        File.CreateText(@"c:\temp\ActionLogs.txt");
            }

            using StreamWriter streamWriter =
                    new StreamWriter(@"c:\temp\ActionLogs.txt");
            string time = DateTime.Now.ToString("yyyy - MM - dd hh: mm:ss");

            string writeText = "At: " + time + 
                    "\n ACTION -- " + action + " took place" + "\n";

            streamWriter.WriteLine(writeText);
        }
        

        static String SeeLogs()
        {
            Console.WriteLine(File.ReadAllText(@"c:\temp\ActionLogs.txt"));
            Console.ReadLine();
            return File.ReadAllText(@"c:\temp\ActionLogs.txt");           
        }

     
        static void GetStaffMem()
        {
            Console.Clear();
            string[] files = Directory.GetFiles(@"c:\temp");

            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Equals(@"c:\temp\ActionLogs.txt"))
                    continue;
                Console.WriteLine(File.ReadAllText(files[i]) +
                    "\n");
                Console.ReadLine();
            }

            LogAction("Showed staff information");
        }
        static void StaffPay()
        {
            string StaffChoice;
            int StaffChoice2;
            int StaffHours;

            Console.Clear();

        beginning:
            // Staff Pay Function 

            Console.WriteLine("Enter The Full Name Of Staff");
            StaffChoice = Console.ReadLine();
            // A

            if (File.Exists(@"c:\temp\" + StaffChoice + ".txt"))
            {
                string text = File.ReadAllText(@"c:\temp\" + StaffChoice + ".txt");
                Console.WriteLine(text);
                Console.ReadLine();

                Console.Clear();

                Console.WriteLine("To Find Staff Pay Press 1 : \n" +
               "To Find Staff Holiday Pay Press 2 : ");
                StaffChoice2 = Convert.ToInt32(Console.ReadLine());

                if (StaffChoice2 == 1)
                {
                    string bonus;

                    Console.WriteLine("Please Enter {0} Hours Worked: ", StaffChoice);
                    StaffHours = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Does {0} Have Any Bonuses?", StaffChoice);
                    bonus = Console.ReadLine();

                    if (bonus == "Yes")
                    {
                        int bonusValue;

                        Console.Write("How Much Is The Bonus?");
                        bonusValue = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Pay : £{0}", (StaffHours * universalStaffPay) + bonusValue);
                        Console.ReadLine();
                    }
                    else if (bonus == "No")
                    {

                        Console.Write("Pay : £{0}", StaffHours * universalStaffPay);
                        Console.ReadLine();
                        Console.Clear();

                    }
                }
                else if (StaffChoice2 == 2)
                {
                    Console.WriteLine("Holiday Pay : £{0}", (3 * universalStaffPay) * 28);
                    Console.ReadLine(); 
                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine("File Does Not Exist, maybe you need to create a new user");
                Console.ReadLine();

                goto beginning;
            }

            LogAction("Displayed " + StaffChoice + "'s pay and position");
        }
        static void AddStaff()
        {

            // Addition Staff Function
            string staffSName;
            int staffSPay;
            string StaffsPos;

            
            Console.Write("Please Enter The Name Of Staff: ");
            staffSName = Console.ReadLine();
            String path = @"c:\temp\" + staffSName + ".txt";
            using StreamWriter sw = File.CreateText(path);
            sw.WriteLine(staffSName);

            Console.Write("Please Enter Staff's Position: ");
            StaffsPos = Console.ReadLine();
            sw.WriteLine(StaffsPos);


            Console.Write("Please Enter Staff's Pay Per Hour: ");
            staffSPay = Convert.ToInt32(Console.ReadLine());
            sw.WriteLine("Pay : £{0}", staffSPay);
            universalStaffPay = staffSPay;
            sw.Close();

            LogAction("Created " + staffSName + "'s profile");
            Console.Clear();
        }
        static void RemoveStaff()
        {
            // Remove Staff Function 

            string RemovedStaff;


            Console.WriteLine("Enter Name Of Staff (or !clear logs)");
            RemovedStaff = Console.ReadLine();

            if (File.Exists(@"c:\temp\" + RemovedStaff + ".txt"))
            {
                File.Delete(@"c:\temp\" + RemovedStaff + ".txt");
                Console.WriteLine("{0} Has Been Removed", RemovedStaff);
                Console.ReadLine();
                LogAction("Removed " + RemovedStaff + " from payroll");
                Console.Clear();

            } else if(RemovedStaff.Equals("!clear logs"))
            {
                File.Delete(@"c:\temp\ActionLogs.txt");
                Console.WriteLine("Logs have been cleared");
                LogAction("Logs Cleared");
                Console.ReadLine();
                // Recreate file
                using StreamWriter stream = File.CreateText(@"c:\temp\ActionLogs.txt");
                Console.Clear();
            }
            else
            {
                Console.WriteLine("File Does Not Exist (Maybe {0} has already been removed) ", RemovedStaff);
                Console.ReadLine();
                LogAction("Failed attempt to remove " + RemovedStaff + " from payroll");
                Console.Clear();

            }
        }
    }
}