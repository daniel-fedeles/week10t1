using System.Collections.Generic;

namespace Week10t1
{
    public interface IGenericList<T> : IEnumerable<T>
    {
        void Add(T value);
        T GetElement(int index);
        void Remove(int index);
        void Insert(int index, T value);
        void ClearList();
        IDictionary<int, T> FindElement(T value);
    }
}