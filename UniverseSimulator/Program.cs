using System;
using System.Runtime.InteropServices;

namespace UniverseSimulator
{
    public static class Program
    {
        [DllImport("kernel32.dll")]
        private static extern int AllocConsole();

        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionMethod);
            AllocConsole();

            using (var game = new Game1())
                game.Run();
        }

        static void UnhandledExceptionMethod(object sender, UnhandledExceptionEventArgs args)
        {
            Console.WriteLine("Sender: " + sender + "\n" + ((Exception)args.ExceptionObject).Message + "\n" + ((Exception)args.ExceptionObject).StackTrace);
            Console.ReadKey(true);
        }
    }
}
