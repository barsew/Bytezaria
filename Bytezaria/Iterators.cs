using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bytezaria
{
    public interface IIterator<T>
    {
        bool HasNext();
        void Next();
        T Current();
        void First();
    }

    public class DoubledLinkedListIterator<T> : IIterator<T>
    {
        private DoubledLinkedList<T> _list;
        private int index = 0;


        public DoubledLinkedListIterator(DoubledLinkedList<T> list)
        {
            _list = list;
            index = 0;
        }
        public bool HasNext()
        {
            return index < _list.list.Count;
        }

        public void Next()
        {
            index++;
        }
        public T Current() 
        {
            return _list.list[index];
        }

        public void First()
        {
            index = 0;
        }
    }
    public class DoubledLinkedListReverseIterator<T> : IIterator<T>
    {
        private DoubledLinkedList<T> _list;
        private int index = 0;
        public DoubledLinkedListReverseIterator(DoubledLinkedList<T> list)
        {
            _list = list;
            index = list.list.Count - 1;
        }
        public bool HasNext()
        {
            return index >= 0;
        }

        public void Next()
        {
            index--;
        }
        public T Current()
        {
            return _list.list[index];
        }

        public void First()
        {
            index = _list.list.Count - 1;
        }
    }
    public class VectorIterator<T> : IIterator<T>
    {
        private Vector<T> vector;
        private int index = 0;

        public VectorIterator(Vector<T> vector) 
        {
            this.vector = vector;
            index = 0;
        }

        public bool HasNext()
        {
            return index < vector.list.Count;
        }
        public void Next()
        {
            index++;
        }
        public T Current()
        {
            return vector.list[index];
        }

        public void First()
        {
            index = 0;
        }
    }
    public class VectorReverseIterator<T> : IIterator<T>
    {
        private Vector<T> vector;
        private int index = 0;

        public VectorReverseIterator(Vector<T> vector)
        {
            this.vector = vector;
            index = vector.list.Count - 1;
        }

        public bool HasNext()
        {
            return index >= 0;
        }

        public void Next()
        {
            index--;
        }
        public T Current()
        {
            return vector.list[index];
        }

        public void First()
        {
            index = vector.list.Count - 1;
        }
    }
    public class SortedArrayIterator<T> : IIterator<T>
    {
        private SortedArray<T> _sa;
        private int index = 0;


        public SortedArrayIterator(SortedArray<T> sa)
        {
            _sa = sa;
            index = 0;
        }
        public bool HasNext()
        {
            return index < _sa.list.Count;
        }

        public void Next()
        {
            index++;
        }
        public T Current()
        {
            return _sa.list[index];
        }

        public void First()
        {
            index = 0;
        }
    }
    public class SortedArrayInverseIterator<T> : IIterator<T>
    {
        private SortedArray<T> _sa;
        private int index = 0;

        public SortedArrayInverseIterator(SortedArray<T> sa)
        {
            _sa = sa;
            index = sa.list.Count;
        }
        public bool HasNext()
        {
            return index >= 0;
        }

        public void Next()
        {
            index--;
        }
        public T Current()
        {
            return _sa.list[index];
        }

        public void First()
        {
            index = _sa.list.Count;
        }
    }
}


