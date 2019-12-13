using System;
using System.Collections;
using System.Collections.Generic;

namespace Week10t1
{
    public class GenericList<T> : IGenericList<T>
    {
        private static T[] _array = new T[0];
        private T[] _items;
        private const int _defaultCapacity = 10;
        private int _size;

        public GenericList()
        {
            _items = _array;
        }


        public GenericList(int capacity)
        {
            if (capacity < 0)
            {
                throw new IndexOutOfRangeException(nameof(capacity));
            }
            _items = capacity == 0 ? _array : new T[capacity];
        }

        public GenericList(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection is ICollection<T> newArray)
            {
                var count = newArray.Count;
                if (count == 0)
                {
                    _items = _array;
                }
                else
                {
                    _items = new T[count];
                    newArray.CopyTo(_items, 0);
                    _size = count;
                }

            }
            else
            {
                _size = 0;
                _items = _array;
                foreach (T t in collection)
                {
                    Add(t);
                }
            }
        }

        public void Add(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (_size == _items.Length)
            {
                EnsureCapacity(_size + 1);
            }

            _items[_size++] = value;
        }

        public T GetElement(int index) => (uint)index >= (uint)_size ? throw new ArgumentOutOfRangeException(nameof(index)) : _items[index];


        public void Remove(int index)
        {
            if ((uint)index >= (uint)_size)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            --_size;
            if (index < _size)
            {
                Array.Copy(_items, index + 1, _items, index, _size - index);
            }

            _items[_size] = default;
        }

        public void Insert(int index, T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if ((uint)index > (uint)_size)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (_size == _items.Length)
            {
                EnsureCapacity(_size + 1);
            }

            if (index < _size)
            {
                Array.Copy(_items, index, _items, index + 1, _size - index);
            }
            _items[index] = value;
            ++_size;
        }

        public void ClearList()
        {
            if (_size > 0)
            {
                Array.Clear(_items, 0, _size);
                _size = 0;
            }
        }

        public IDictionary<int, T> FindElement(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            IDictionary<int, T> itemDictionary = new Dictionary<int, T>();
            EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
            for (int index = 0; index < _size; ++index)
            {
                if (equalityComparer.Equals(_items[index], value))
                {
                    itemDictionary.Add(index, value);
                }
            }
            return itemDictionary;
        }
        public int Count
        {
            get
            {
                return _size;
            }
        }

        private void EnsureCapacity(int min)
        {
            if (min <= 0) throw new ArgumentOutOfRangeException(nameof(min));
            if (_items.Length >= min)
            {
                return;
            }

            var num = _items.Length == 0 ? _defaultCapacity : _items.Length * 2;
            if ((uint)num > 2146435071U)
            {
                num = 2146435071;
            }

            if (num < min)
            {
                num = min;
            }

            Capacity = num;
        }
        public int Capacity
        {
            get => _items.Length;
            set
            {
                if (value < _size)
                {
                    throw new IndexOutOfRangeException(nameof(value));
                }

                if (value == _items.Length)
                {
                    return;
                }

                if (value > 0)
                {
                    T[] objArray = new T[value];
                    if (_size > 0)
                    {
                        Array.Copy(_items, 0, objArray, 0, _size);
                    }

                    _items = objArray;
                }
                else
                    _items = _array;
            }
        }


        public T this[int index]
        {
            get => (uint)index >= (uint)_size ? throw new ArgumentOutOfRangeException(nameof(index)) : _items[index];
            set
            {
                if ((uint)index >= (uint)_size)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                if (value != null)
                {
                    _items[index] = value;
                }
                else
                {
                    throw new ArgumentNullException(nameof(value));
                }
            }
        }

        private IEnumerable<T> Events()
        {
            foreach (T item in _items)
            {
                yield return item;
            }

        }

        public IEnumerator<T> GetEnumerator() => Events().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public T Max<T>() where T : IComparable<T>
        {
            T[] values = _items as T[];
            T max = values[0];
            for (int i = 1; i < values.Length; i++)
                if (values[i].CompareTo(max) > 0) max = values[i];
            return max;
        }
        public T Min<T>() where T : IComparable<T>
        {
            T[] values = _items as T[];
            T min = values[0];
            for (int i = 1; i < values.Length; i++)
                if (values[i].CompareTo(min) < 0) min = values[i];
            return min;
        }
    }
}