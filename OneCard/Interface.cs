using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCard
{

    interface ICard
    {
        CardPattern Pattern { get; set; }
        CardNum Num { get; set; }
    }
}
