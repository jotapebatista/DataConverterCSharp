using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataConverterApp
{
    public interface IDataReader<T>
    {
        T Read(string filePath);
    }
}
