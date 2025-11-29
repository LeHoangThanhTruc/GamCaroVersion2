// See https://aka.ms/new-console-template for more information
//Entry point Server
using GameCaroShared;
using System;

namespace GameCaroServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("GameCaro Server");

            ServerManager server = new ServerManager();
            server.Start(); // cổng mặc định

            Console.WriteLine("Server is running. Press Enter to exit...");
            Console.ReadLine();
        }
    }
}

