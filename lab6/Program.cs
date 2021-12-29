using System;

namespace lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            var logBuffer = new LogBuffer("D:\\buff.txt");
                        for (var i = 1; i <= 50; i++)
                        {
                            logBuffer.Add(i.ToString());
                        }
                        Console.WriteLine("Press the Enter key to exit ... ");
                        Console.ReadLine();
                        logBuffer.Close();
        }
    }
}