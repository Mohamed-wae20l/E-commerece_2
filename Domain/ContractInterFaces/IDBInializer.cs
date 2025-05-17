using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ContractInterFaces
{
   public interface IDBInializer
    {
        Task InializeAsync();
    }
}
