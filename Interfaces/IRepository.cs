using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wayfare.Interfaces
{
  internal interface IRepository<T, Tid>
  {
    List<T> GetAll();

    T GetById(Tid id);

    T Create(T data);

    void Edit(T data);

    string Delete(Tid id);


  }
}