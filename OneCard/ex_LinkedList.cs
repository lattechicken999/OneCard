using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCard
{
    public static class Ex_LinkedList
    {
        public static LinkedListNode<T> CycleNext<T>(this LinkedList<T> linkedList, LinkedListNode<T> thisNode)
        {
            if(thisNode.Next == null)
            {
                return linkedList.First;
            }
            else
            {
                return thisNode.Next;
            }
        }

        public static LinkedListNode<T> CyclePrevious<T>(this LinkedList<T> linkedList, LinkedListNode<T> thisNode)
        {
            if (thisNode.Previous == null)
            {
                return linkedList.Last;
            }
            else
            {
                return thisNode.Previous;
            }
        }
    }
}
