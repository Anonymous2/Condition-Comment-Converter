using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
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

            string allUpdateQueries = String.Empty, allUpdateQueriesWithErrors = String.Empty;

            WorldDatabase worldDatabase = new WorldDatabase(host, port, user, pass, worldDB);

            if (!worldDatabase.CanConnectToDatabase(worldDatabase.connectionString))
            {
                Console.WriteLine("\nA database connection could not be established with the given database information. Press any key to try again with new information.\n");
                Console.ReadKey();
                goto WriteSqlInformation;
            }

            List<Condition> conditions = await worldDatabase.GetConditions();

            if (conditions.Count <= 0)
            {
                Console.WriteLine("\nNo conditions were found in the database. Press any key to try again with new database information.\n");
                Console.ReadKey();
                goto WriteSqlInformation;
            }

            Console.WriteLine("\nA database connection has been successfully established with the given database information. A total of " + conditions.Count + " scripts were found to be converted to proper commenting.\nPress any key to start the process!\n\n");
            Console.ReadKey();

            File.Delete("output.sql");

            for (int i = 0; i < conditions.Count; ++i)
            {
                Condition condition = conditions[i];
                totalLoadedScripts++;
                string fullLine = String.Empty;
            }

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
