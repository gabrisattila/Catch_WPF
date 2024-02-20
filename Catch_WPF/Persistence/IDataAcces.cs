    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catch_Wpf.Persistence
{
    public interface IDataAcces
    {
        GameTable Load(String path);

        Task SaveAsync(String path, GameTable table);
    }
}
