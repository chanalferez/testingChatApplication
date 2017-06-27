using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Sql;

namespace Test
{
    class Program
    {
        public static string constr = ConfigurationManager.ConnectionStrings["Test.Properties.Settings.dbTestConnectionString"].ToString();
        public static SqlConnection conn = new SqlConnection(constr);
        public static DataClasses1DataContext db = new DataClasses1DataContext();

        static void Main(string[] args)
        {
            START:
            try {
                Console.Clear();
                ViewEmp();
                int input = 1;
                while (input != 0) {
                    Console.WriteLine("\n\n  [0]    EXIT" +
                                  "\n  [1]    Add Employee" +
                                  "\n  [2]    Update Employee" +
                                  "\n  [3]    Delete Employee");
                    Console.Write("\n\nInput > ");
                    input = int.Parse(Console.ReadLine());

                    switch (input) {
                        case 0:
                            Console.Write("\n\n  Press any key to Exit.....");
                            Console.ReadKey();
                            Environment.Exit(0);
                            break;
                        case 1:
                            AddEmp();
                            Console.Write("\n\n\tSuccessfully Added!");
                            Console.ReadKey();
                            goto START;
                        case 2:
                            UpdateEmp();
                            goto START;
                        case 3:
                            DeleteEmp();
                            goto START;
                        default:
                            goto START;
                    }
                }
            }
            catch (Exception) {
                Console.Write("\n\t\tError Occured!!");
                Console.ReadKey();
                goto START;
            }
        }

        public static void ViewEmp()
        {
            Console.Write("\n\t\t\t\t    - DATA -");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM tblEmployee", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            Console.WriteLine("\n\nID\t" + "Lastname\t" + "Firstname\t" + "Middlename\t" +"Birthday\t" + "Age\n");
            if (reader.HasRows == true) {
                while (reader.Read()) {
                    Console.WriteLine(reader["EmpId"] + "\t"  +  reader["Lname"] + "\t\t" + reader["Fname"] + "\t\t" + reader["Mname"] + "\t" + DateTime.Parse(reader["Birthdate"].ToString()).ToShortDateString() + "\t" + reader["Age"]);
                }
            }
            else {
                Console.Write("\n\t\tNo Records!!!");
            }
            conn.Close();
        }

        public static void AddEmp()
        {
            Console.Clear();
            ViewEmp();

            string lname = "", fname = "", mname = "";
            DateTime Bday;
            int Age = 0;

            Console.Write("\n\nInput Lastname             > ");
            lname = Console.ReadLine();
            Console.Write("Input Firstname            > ");
            fname = Console.ReadLine();
            Console.Write("Input Middlename           > ");
            mname = Console.ReadLine();
            Console.Write("Input Birthday(mm/dd/yyyy) > ");
            Bday = DateTime.Parse(Console.ReadLine());
            Age = calculateAge(Bday);
            Console.Write("Age                        > " + Age);

            db.sp_AddEmp(lname, fname, mname, Bday, Age);
        }

        public static void UpdateEmp()
        {
            Console.Clear();
            ViewEmp();

            string lname = "", fname = "", mname = "", check = "";
            DateTime Bday;
            int Age = 0, EmpId = 0;

            Console.Write("\n\nChoose Employee Id to UPDATE >  ");
            EmpId = int.Parse(Console.ReadLine());
            Console.Write("Are you sure you want to UPDATE Employee " + EmpId +
                            "\nType [Y] to Delete [N] No : ");
            check = Console.ReadLine().ToUpper();

            if (check == "Y") {
                Console.Write("\n\nInput Lastname             > ");
                lname = Console.ReadLine();
                Console.Write("Input Firstname            > ");
                fname = Console.ReadLine();
                Console.Write("Input Middlename           > ");
                mname = Console.ReadLine();
                Console.Write("Input Birthday(mm/dd/yyyy) > ");
                Bday = DateTime.Parse(Console.ReadLine());
                Age = calculateAge(Bday);
                Console.WriteLine("Age                        > " + Age);
                Console.Write("\n\tPress any key......");
                Console.ReadKey();
                db.sp_UpdateEmp(EmpId, lname, fname, mname, Bday, Age);
                Console.Write("\n\n\tSuccessfully Updated!");
                Console.ReadKey();
            }
            else { }         
        }

        public static void DeleteEmp()
        {
            Console.Clear();
            ViewEmp();

            int EmpID = 0;
            string check;
            
            Console.Write("\n\nChoose Employee Id to DELETE >  ");
            EmpID = int.Parse(Console.ReadLine());
            Console.Write("Are you sure you want to DELETE Employee " + EmpID + 
                          "\nType [Y] to Delete [N] No : ");
            check = Console.ReadLine().ToUpper();

            if (check == "Y") {
                db.sp_DeleteEmp(EmpID);
                Console.Write("\n\n\tDeleted Successfully");
                Console.ReadKey();
            }
            else { }
        }

        public static int calculateAge(DateTime Date)
        {
            int month, date, year, age = 0;

            month = int.Parse(Date.Month.ToString());
            date = int.Parse(Date.Day.ToString());
            year = int.Parse(Date.Year.ToString());

            age = int.Parse(DateTime.Now.Year.ToString()) - year;

            if (month >= int.Parse(DateTime.Now.Month.ToString()))
            {
                if (month > int.Parse(DateTime.Now.Month.ToString()))
                {
                    age = age - 1;
                }
                else if (date > int.Parse(DateTime.Now.Day.ToString()))
                {
                    age = age - 1;
                }
            }
            return age;
        }
    }
}
