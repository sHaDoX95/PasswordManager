using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    interface IReaderWriter
    {
        string Reader(string fileName);
        bool Writer(string data, string fileName);
    }
}