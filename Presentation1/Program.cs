using Core.Constants;
using Core.Helper;
using Data.Repositories.Concrete;
using Presentation1.Services;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Presentation1
{
    public static class Program
    {
        private readonly static GroupService _groupService;

        static Program()
        {
            _groupService = new GroupService();
        }
        static void Main()
        {
            ConsoleHelper.WriteWithColor("---Welcome!---", ConsoleColor.Cyan);
            while (true)
            {

                ConsoleHelper.WriteWithColor(" 1 - Create Group", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor(" 2 - Update Group", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor(" 3 - Delete Group", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor(" 4 - Get All Group", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor(" 5 - Get Group by ID", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor(" 6 - Get Group by Name", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor(" 0 - Exit", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor(" Select one of the options above", ConsoleColor.Cyan);

                int number;
                bool isSucceeded = int.TryParse(Console.ReadLine(), out number);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("The entered number is not a correct format!", ConsoleColor.Red);
                }
                else
                {
                    if (!(number >= 0 && number <= 6))
                    {
                        ConsoleHelper.WriteWithColor("There is not such a number in the list!", ConsoleColor.Red);
                    }
                    else
                    {
                        switch (number)
                        {
                            case (int)GroupOptions.CreateGroup:
                                _groupService.Create();
                                break;

                            case (int)GroupOptions.UpdateGroup:
                                _groupService.GetUpdate();
                                break;

                            case (int)GroupOptions.DeleteGroup:
                                _groupService.Delete();
                                break;

                            case (int)GroupOptions.GetAllGroup:
                                _groupService.GetAll();
                                break
                                    ;
                            case (int)GroupOptions.GetGroupbyID:
                                _groupService.GetGroupbyID();
                                break;

                            case (int)GroupOptions.GetGroupbyName:
                                _groupService.GetGroupbyName();
                                break;

                            case (int)GroupOptions.Exit:
                                _groupService.Exit();
                                break;

                            default:
                                break;
                        }
                    }


                }
            }

        }
    }
}