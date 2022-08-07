using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeComandService
{
    internal static class HelperService
    {
        internal static void ShowDirectories(string directoryName)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(directoryName);
            Console.ResetColor();
        }

        internal static void ShowFiles(string fileName)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(fileName);
            Console.ResetColor();
        }

        internal static void ShowRootFolder(string rootFolderName)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(rootFolderName);
            Console.ResetColor();
        }

        internal static void ShowPath(string path)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(path);
            Console.ResetColor();
        }

        internal static void ShowErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        internal static void ThrowException(string message)
        {
            throw new Exception(message);
        }
    }
}
