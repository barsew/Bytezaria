using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bytezaria
{
    public static class Algorithm
    {
        public static T Find<T>(ICollection<T> collection, Func<T, bool> predicate, bool searchDirection)
        {
            if (searchDirection)
            {
                Bytezaria.IIterator<T> it = collection.GetIterator();
                while (it.HasNext())
                {
                    if (predicate(it.Current()))
                    {
                        return it.Current();
                    }
                    it.Next();
                }
            }
            else
            {
                Bytezaria.IIterator<T> it = collection.GetReverseIterator();
                while (it.HasNext())
                {
                    if (predicate(it.Current()))
                    {
                        return it.Current();
                    }
                    it.Next();
                }
            }
            return default(T);
        }
        public static void Print<T>(ICollection<T> collection, Func<T, bool> predicate, bool searchDirection)
        {
            if (searchDirection)
            {
                Bytezaria.IIterator<T> it = collection.GetIterator();
                while (it.HasNext())
                {
                    if (predicate(it.Current()))
                    {
                        Console.WriteLine(it.Current().ToString());
                    }
                    it.Next();
                }
            }
            else
            {
                Bytezaria.IIterator<T> it = collection.GetReverseIterator();
                while (it.HasNext())
                {
                    if (predicate(it.Current()))
                    {
                        Console.WriteLine(it.Current().ToString());
                    }
                    it.Next();
                }
            }
        }
        public static T Find<T>(IIterator<T> it, Func<T, bool> predicate)
        {
            it.First();
            while (it.HasNext())
            {
                if (predicate(it.Current()))
                {
                    return it.Current();
                }
                it.Next();
            }
    
            return default(T);
        }
        public static void ForEach<T>(IIterator<T> it, Func<T, T> function)
        {
            it.First();
            while (it.HasNext())
            {
                T tmp = it.Current();
                function(tmp);
                it.Next();
            }
        }
        public static int CountIf<T>(IIterator<T> it, Func<T, bool> predicate)
        {
            it.First();
            int counter = 0;
            while (it.HasNext())
            {
                if (predicate(it.Current()))
                {
                    counter++;
                }
                it.Next();
            }

            return counter;
        }
    }
}
