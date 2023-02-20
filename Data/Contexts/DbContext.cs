using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Data.Contexts
{
    public static class DbContext
    {
        static DbContext()
        {
            Groups = new List<Group>();
        }
        public static List<Group> Groups { get; set; }
    }
}
