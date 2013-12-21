using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
//using MySql.Data.MySqlClient;
using Nito.AsyncEx;

namespace Condition_Comment_Converter
{
    class Program
    {
        private static int totalSkippedScripts, totalLoadedScripts;

        static void Main(string[] args)
        {
            AsyncContext.Run(() => MainAsync(args));
        }

        static async void MainAsync(string[] args)
        {
            FillConditionStrings();

        WriteSqlInformation:
            Console.WriteLine("SQL Information:");
            Console.Write("Host: ");
            string host = Console.ReadLine();
            Console.Write("Port: ");
            string portStr = Console.ReadLine();
            UInt32 port;
            UInt32.TryParse(portStr, out port);
            Console.Write("User: ");
            string user = Console.ReadLine();
            Console.Write("Pass: ");
            string pass = Console.ReadLine();
            Console.Write("World DB: ");
            string worldDB = Console.ReadLine();
            Console.Write("Print old comment (0/1): ");
            string printOldCommentStr = Console.ReadLine();
            bool printOldComment = printOldCommentStr == "1";

            Console.WriteLine("\nConnecting...\n");

            Console.WriteLine("\n\n\nThe converting has finished. A total of {0} scripts were loaded of which {1} were skipped because their comments already fit the correct codestyle.", totalLoadedScripts, totalSkippedScripts);
            Console.WriteLine("If you wish to open the output file with your selected .sql file editor, press the Enter key.");

            if (Console.ReadKey().Key == ConsoleKey.Enter)
                Process.Start("output.sql");
        }

        private static void FillConditionStrings()
        {

        }
    }
}
