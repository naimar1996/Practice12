using Core.Entities;
using Core.Helper;
using Data.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Presentation1.Services
{
    public class GroupService
    {
        private readonly GroupRepository _grouprepository;
        public GroupService()
        {
            _grouprepository = new GroupRepository();
        }

        public void GetAll()
        {

            var groups = _grouprepository.GetAll();

            ConsoleHelper.WriteWithColor("---All Groups---", ConsoleColor.Cyan);

            foreach (var group in groups)
            {
                ConsoleHelper.WriteWithColor($"Id: {group.Id},Name:{group.Name},Max size : {group.MaxSize},Start date: {group.StartDate.ToShortDateString()},End date : {group.EndDate.ToShortDateString()}", ConsoleColor.DarkYellow);
            }
        }
        public void GetGroupbyID()
        {
            var groups = _grouprepository.GetAll();
            if (groups.Count == 0)
            {
            AreyousureDesc: ConsoleHelper.WriteWithColor(" Do you want to creat a new group? ", ConsoleColor.DarkYellow);
                char decision;
                bool isSucceed = char.TryParse(Console.ReadLine(), out decision);
                if (!isSucceed)
                {
                    ConsoleHelper.WriteWithColor("Your choice is not a correct format!", ConsoleColor.Red);
                }
                if (!(decision == 'a' || decision == 'b'))
                {
                    ConsoleHelper.WriteWithColor(" Your choice is not correct!", ConsoleColor.Red);
                    goto AreyousureDesc;
                }
                if (decision == 'a')
                {
                    Create();
                }
                else
                {

                EnterIDDesc: ConsoleHelper.WriteWithColor("--- Enter ID,please---", ConsoleColor.Cyan);
                    int id;
                    bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
                    if (isSucceeded)
                    {
                        ConsoleHelper.WriteWithColor("The entered ID is incorrect!", ConsoleColor.Red);
                        goto EnterIDDesc;
                    }
                    var group = _grouprepository.Get(id);
                    if (group == null)
                    {
                        ConsoleHelper.WriteWithColor("There is not any group in this ID!", ConsoleColor.Red);
                    }
                    ConsoleHelper.WriteWithColor($"Id: {group.Id},Name:{group.Name},Max size : {group.MaxSize},Start date: {group.StartDate.ToShortDateString()},End date : {group.EndDate.ToShortDateString()}", ConsoleColor.DarkYellow);
                }
            }

        }
        public void GetGroupbyName()
        {
            GetAll();
            ConsoleHelper.WriteWithColor(" Enter a name of group,please", ConsoleColor.Cyan);
            string name = Console.ReadLine();
            var group = _grouprepository.GetbyName(name);
            if (group == null)
            {
                ConsoleHelper.WriteWithColor("There is not any group in this name", ConsoleColor.Red);
            }
            ConsoleHelper.WriteWithColor($"Id: {group.Id},Name:{group.Name},Max size : {group.MaxSize},Start date: {group.StartDate.ToShortDateString()},End date : {group.EndDate.ToShortDateString()}", ConsoleColor.DarkYellow);
        }
        public void GetUpdate()
        {
            GetAll();
        IDorNameDesc: ConsoleHelper.WriteWithColor(" Which do you want to enter? \n a)ID  \n b)Name ");
            char decision;
            bool isSucceeded = char.TryParse(Console.ReadLine(), out decision);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Your choice is not a correct format!", ConsoleColor.Red);
                goto IDorNameDesc;
            }
            if (!(decision == 'a' || decision == 'b'))
            {
                ConsoleHelper.WriteWithColor(" The entered letter is not correct", ConsoleColor.Red);
            }
            if (decision == 'a')
            {
            IDDesc: ConsoleHelper.WriteWithColor(" Enter an ID of the group", ConsoleColor.Cyan);
                char id;
                isSucceeded = char.TryParse(Console.ReadLine(), out id);

                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("The entered ID is incorrect!", ConsoleColor.Red);
                    goto IDDesc;
                }

                var group = _grouprepository.Get(id);
                if (group == null)
                {
                    ConsoleHelper.WriteWithColor(" There is not any group in this ID!", ConsoleColor.Red);
                }
                InternalUpdate(group);

            }
                else
            {
            NameDesc: ConsoleHelper.WriteWithColor("Enter a name of the group,please", ConsoleColor.Cyan);
                string name = Console.ReadLine();
                var group = _grouprepository.GetbyName(name);
                if (group == null)
                {
                    ConsoleHelper.WriteWithColor(" There is not any group in this Name!", ConsoleColor.Red);
                }
                InternalUpdate(group);

            }

        }
        private void InternalUpdate(Group group)
        {
                ConsoleHelper.WriteWithColor(" Enter new name,please", ConsoleColor.Cyan);
               string name = Console.ReadLine();

            maxSizeDesc: int maxSize;
              bool  isSucceeded = int.TryParse(Console.ReadLine(), out maxSize);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("The entered is not a correct format", ConsoleColor.Red);
                    goto maxSizeDesc;
                }

            StartDateDes: ConsoleHelper.WriteWithColor("Enter  new start date,please", ConsoleColor.Cyan);
                DateTime startDate;
                isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("The start date is not a correct format!", ConsoleColor.Red);
                    goto StartDateDes;
                }
            EndDateDes: ConsoleHelper.WriteWithColor("Enter new end date,please", ConsoleColor.Cyan);
                DateTime endDate;
                isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("The end date is not a correct format!", ConsoleColor.Red);
                    goto EndDateDes;
                }
                if (startDate > endDate)
                {
                    ConsoleHelper.WriteWithColor("The end date must be bigger than the start date", ConsoleColor.Red);
                    goto EndDateDes;
                }
                group.Name = name;
                group.MaxSize = maxSize;
                group.StartDate = startDate;
                group.EndDate = endDate;
                _grouprepository.Update(group);
        }
        public void Create()
        {
            ConsoleHelper.WriteWithColor("---Enter name, please---", ConsoleColor.Cyan);
            string name = Console.ReadLine();

        MaxSizeDes: ConsoleHelper.WriteWithColor("---Enter group max - size,please---", ConsoleColor.Cyan);
            int maxSize;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out maxSize);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Max size is not a correct format!", ConsoleColor.Red);
                goto MaxSizeDes;
            }
            if (maxSize > 18)
            {
                ConsoleHelper.WriteWithColor("Max size must be less than or equal to 18", ConsoleColor.Red);
                goto MaxSizeDes;
            }

        StartDateDes: ConsoleHelper.WriteWithColor("Enter start date,please", ConsoleColor.Cyan);
            DateTime startDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("The start date is not a correct format!", ConsoleColor.Red);
                goto StartDateDes;
            }
        EndDateDes: ConsoleHelper.WriteWithColor("Enter end date,please", ConsoleColor.Cyan);
            DateTime endDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("The end date is not a correct format!", ConsoleColor.Red);
                goto EndDateDes;
            }
            if (startDate > endDate)
            {
                ConsoleHelper.WriteWithColor("The end date must be bigger than the start date", ConsoleColor.Red);
                goto EndDateDes;
            }

            var group = new Core.Entities.Group
            {
                Name = name,
                MaxSize = maxSize,
                StartDate = startDate,
                EndDate = endDate,
            };

            _grouprepository.Add(group);

            ConsoleHelper.WriteWithColor($"The group succesfully created with Name:{group.Name},Max size : {group.MaxSize},Start date: {group.StartDate.ToShortDateString()},End date : {group.EndDate.ToShortDateString()}", ConsoleColor.DarkYellow);
        }
        public void Delete()
        {
            GetAll();

        IdDes: ConsoleHelper.WriteWithColor("Enter end date,please", ConsoleColor.Cyan);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("The ID is not a correct format!", ConsoleColor.Red);
                goto IdDes;
            }
            var dbGroup = _grouprepository.Get(id);
            if (dbGroup == null)
            {
                ConsoleHelper.WriteWithColor("There is not any in this ID!", ConsoleColor.Red);
            }
            else
            {
                _grouprepository.Delete(dbGroup);
                ConsoleHelper.WriteWithColor("The group successfully deleted!", ConsoleColor.Green);
            }

        }
        public void Exit()
        {
        AreyousureDesc: ConsoleHelper.WriteWithColor("Are you sure? \n a) yes \n b) no ", ConsoleColor.DarkYellow);
            char decision;
            bool isSucceeded = char.TryParse(Console.ReadLine(), out decision);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Your choice is not a correct format!", ConsoleColor.Red);
            }
            if (!(decision == 'a' || decision == 'b'))
            {
                ConsoleHelper.WriteWithColor(" Your choice is not correct!", ConsoleColor.Red);
                goto AreyousureDesc;
            }
            if (decision == 'a')
            {
                return;
            }

        }
    }
}


       






