using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bytezaria
{
    public interface ICollection<T>
    {
        void Add(T item);
        void Delete(T item);
        IIterator<T> GetIterator();
        IIterator<T> GetReverseIterator();
        void RemoveAt(int i);
        void RemoveAll();
        void Remove(T item);
    }

    public class DoubledLinkedList<T> : ICollection<T>
    {
        public List<T> list;

        public DoubledLinkedList()
        {
            list = new List<T>();
        }

        public void Add(T item)
        {
            list.Add(item);
        }

        public void Delete(T item)
        {
            list.Remove(item);
        }

        public IIterator<T> GetIterator()
        {
            return new DoubledLinkedListIterator<T>(this);
        }

        public IIterator<T> GetReverseIterator()
        {
           return new DoubledLinkedListReverseIterator<T>(this);
        }

        public void Remove(T item)
        {
            list.Remove(item);
        }

        public void RemoveAll()
        {
            list.Clear();
        }

        public void RemoveAt(int i)
        {
            list.RemoveAt(i);
        }
    }
    public class Vector<T> : ICollection<T>
    {
        public List<T> list;

        public Vector()
        {
            list = new List<T>();
        }
        public void Add(T item)
        {
            list.Add(item);
        }

        public void Delete(T item)
        {
            list.Remove(item);
        }

        public IIterator<T> GetIterator()
        {
           return new VectorIterator<T>(this);
        }

        public IIterator<T> GetReverseIterator()
        {
           return new VectorReverseIterator<T>(this);
        }

        public void Remove(T item)
        {
            list.Remove(item);
        }

        public void RemoveAll()
        {
            list.Clear();
        }

        public void RemoveAt(int i)
        {
            list.RemoveAt(i);
        }
    }
    public class SortedArray<T>: ICollection<T>
    {
        public List<T> list;
        private Comparer<T> comparer;

        public SortedArray(Comparer<T> cmp)
        {
            list = new List<T>();
            comparer = cmp;
        }

        public void Add(T item)
        {
            bool added = false;
            if(list.Count == 0)
            {
                list.Add(item);
                return;
            }
            List<T> tmp = new List<T>();
            Bytezaria.IIterator<T> it = GetIterator();
            while (it.HasNext())
            {
                if (comparer.Compare(item, it.Current()) < 0 && added == false)
                {
                    added = true;
                    tmp.Add(item);
                }
                tmp.Add(it.Current());
                it.Next();
            }
            if(list.Count == tmp.Count)
            {
                tmp.Add(item);
            }
            list = tmp;
        }

        public void Delete(T item)
        {
            bool deleted = false;
            if(list.Count == 0)
            {
                return;
            }
            List<T> tmp = new List<T>();
            Bytezaria.IIterator<T> it = GetIterator();
            while (it.HasNext())
            {
                if (comparer.Compare(item, it.Current()) == 0 && deleted == false)
                {
                    deleted = true;
                    continue;
                }
                tmp.Add(it.Current());
                it.Next();
            }
            list = tmp;
        }

        public int? Find(T item)
        {
            int left = 0;
            int right = list.Count - 1;
            int middle = 0;

            while (left <= right)
            {
                middle = (left + right) / 2;
                if(comparer.Compare(item, list[middle]) == 0)
                {
                    return middle;
                }
                else if (comparer.Compare(item, list[middle]) < 0)
                {
                    right = middle - 1;
                }
                else
                {
                    left = middle + 1;
                }
            }
            return null;
        }

        private T BinSearch()
        {
            return default;
        }

        public IIterator<T> GetIterator()
        {
            return new SortedArrayIterator<T>(this);
        }

        public IIterator<T> GetReverseIterator()
        {
            return new SortedArrayInverseIterator<T>(this);
        }

        public void RemoveAt(int i)
        {
            list.RemoveAt(i);
        }

        public void RemoveAll()
        {
            list.Clear();
        }

        public void Remove(T item)
        {
            list.Remove(item);
        }
    }
}
