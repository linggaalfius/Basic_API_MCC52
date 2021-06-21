using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interfaces
{
    public interface IGeneralRepository<Entity, Key> where Entity : class
    {
        IEnumerable<Entity> Get();
        Entity Get(Key key);
        int Insert(Entity e);
        int Update(Entity e, Key Key);
        int Delete(Key Key);
    }
}
