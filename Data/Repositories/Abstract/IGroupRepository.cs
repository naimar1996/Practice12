using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Abstract
{
    public interface IGroupRepository
    {
        List<Group> GetAll();
        Group Get (int id);
        Group GetbyName(string name);
        void Add(Group group);
        void Update(Group group);
        void Delete(Group group);   

    }
}
