using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCard
{
    class Card
    {
        public CardPattern Pattern { get ; set;}
        public CardNum Num { get ; set; }
        public Card()
        {
            
        }
        public Card(CardPattern patten, CardNum num)
        {
            Pattern = patten;
            Num = num;
        }
    }
}
