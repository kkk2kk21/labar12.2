using labar12._2;
using library;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace labar12._2
{
    public class MyHashtable<TKey, TValue> where TValue : IInit, ICloneable, new() where TKey : ICloneable
    {
        public Item<TKey, TValue>[] Items; // Пары ключ значение
        bool[] dFlag; // Массив флагов для удаления элементов
        int count = 0;
        double fillRatio;
        public int Capacity => Items.Length;
        public int Count => count;

        public MyHashtable(int size = 10, double fillRatio = 0.72)
        {
            Items = new Item<TKey, TValue>[size];
            dFlag = new bool[size];
            this.fillRatio = fillRatio;
        }

        public void Print()
        {
            if (count == 0) Console.WriteLine("Таблица пустая");
            else
            {
                for (int i = 0; i < Items.Length; i++)
                {
                    if (Items[i] != null)
                    {
                        if (Items[i].Key != null)
                        {
                            Console.WriteLine($"{i + 1}. Индекс: {GetIndex(Items[i].Key) + 1} |||Ключ|||: {Items[i].Key} |||Значение|||: {Items[i].Value}");
                        }
                        else
                        {
                            Console.WriteLine($"{i + 1}.");
                        }
                    }
                }
            }
        }

        public int GetIndex(TKey Key)
        {
            return Math.Abs(Key.GetHashCode()) % Capacity;
        }

        public Item<TKey, TValue> FindKeyByData(TKey key)
        {
            int index = Math.Abs(key.GetHashCode()) % Capacity;
            Item<TKey, TValue> item = Items[index];
            if (item != null && key.Equals(item.Key))
                return item;
            else
            {
                int current = index;
                while (current < Items.Length) // Ищем и идем до конца таблицы
                {
                    if (Items[current] != null)
                    {
                        if (key.Equals(Items[current].Key))
                            break;
                    }
                    current++;
                }
                if (current == Items.Length)
                {
                    current = 0;
                    while (current < index) // Идем с начала таблицы
                    {
                        if (Items[current] != null)
                        {
                            if (key.Equals(Items[current].Key))
                                break;
                        }
                        current++;
                    }
                    if (current == index) return default;
                }
                return Items[current];
            }

        }

        public bool RemoveData(TKey key)
        {
            Item<TKey, TValue> item = FindKeyByData(key);
            if (item != null)
            {
                count--;
                item.Key = default(TKey);
                item.Value = default(TValue);
                return true;
            }
            else
            {
                return false;
            }
        }
        void AddItem(TKey key, TValue value)
        {
            if (value == null) return;
            int index = GetIndex(key);
            int current = index;
            if (Items[index] != null)
            {
                while (current < Items.Length && Items[current] != null) // Ищем и идем до конца таблицы
                    current++;
                if (current == Items.Length)
                {
                    current = 0;
                    while (current < index && Items[current] != null) // Идем с начала таблицы
                        current++;
                    if (current == index) throw new Exception("Нет места в таблице");
                }
            }
            Item<TKey, TValue> item = new Item<TKey, TValue>(key, value); // Нашли место добавляем элемент
            Items[current] = item;
            dFlag[current] = true;
            count++;
        }
        public bool AddData(TKey key, TValue value)
        {
            if (Items[GetIndex(key)] != null)
            {
                if (key.Equals(Items[GetIndex(key)].Key) && value.Equals(Items[GetIndex(key)].Value))
                {
                    return false;
                }
            }
            if ((double)Count / Capacity > fillRatio)
            {
                Item<TKey, TValue>[] tempItems = Items;
                Items = new Item<TKey, TValue>[tempItems.Length * 2];
                dFlag = new bool[Items.Length * 2];
                count = 0;
                for (int i = 0; i < tempItems.Length; i++)
                {
                    if (tempItems[i] != null)
                        AddItem(tempItems[i].Key, tempItems[i].Value);
                }
            }
            AddItem(key, value);
            return true;
        }
    }
}