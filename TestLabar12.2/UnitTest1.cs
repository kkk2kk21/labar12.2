using library;
using labar12._2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace labar12._2.Tests
{
    [TestClass]
    public class MyHashtableTests
    {
        private class TestKey : ICloneable
        {
            public int Value { get; set; }

            public object Clone()
            {
                return new TestKey { Value = this.Value };
            }

            public override bool Equals(object obj)
            {
                return obj is TestKey key && Value == key.Value;
            }

            public override int GetHashCode()
            {
                return Value.GetHashCode();
            }
        }

        private class TestValue : IInit, ICloneable
        {
            public int Data { get; set; }

            public void Init()
            {
                Data = 0;
            }

            public void RandomInit()
            {
                Data = new Random().Next(); // Простой пример инициализации случайным числом
            }

            public object Clone()
            {
                return new TestValue { Data = this.Data };
            }

            public override bool Equals(object obj)
            {
                return obj is TestValue value && Data == value.Data;
            }

            public override int GetHashCode()
            {
                return Data.GetHashCode();
            }
        }

        [TestMethod]
        public void AddData_ShouldAddItem()
        {
            var hashtable = new MyHashtable<TestKey, TestValue>();
            var key = new TestKey { Value = 1 };
            var value = new TestValue { Data = 42 };

            bool result = hashtable.AddData(key, value);

            Assert.IsTrue(result);
            Assert.AreEqual(1, hashtable.Count);
            Assert.AreEqual(value, hashtable.FindKeyByData(key).Value);
        }

        [TestMethod]
        public void RemoveData_ShouldRemoveItem()
        {
            var hashtable = new MyHashtable<TestKey, TestValue>();
            var key = new TestKey { Value = 1 };
            var value = new TestValue { Data = 42 };

            hashtable.AddData(key, value);
            bool result = hashtable.RemoveData(key);

            Assert.IsTrue(result);
            Assert.AreEqual(0, hashtable.Count);
            Assert.IsNull(hashtable.FindKeyByData(key));
        }

        [TestMethod]
        public void FindKeyByData_ShouldReturnCorrectItem()
        {
            var hashtable = new MyHashtable<TestKey, TestValue>();
            var key = new TestKey { Value = 1 };
            var value = new TestValue { Data = 42 };

            hashtable.AddData(key, value);
            var item = hashtable.FindKeyByData(key);

            Assert.IsNotNull(item);
            Assert.AreEqual(value, item.Value);
        }

        [TestMethod]
        public void Print_ShouldPrintEmptyTable()
        {
            var hashtable = new MyHashtable<TestKey, TestValue>();

            // Проверяем, что выводит "Таблица пустая"
            // при печати пустой таблицы
            Assert.IsTrue(ConsoleOutputMatches(() => hashtable.Print(), "Таблица пустая"));
        }

        [TestMethod]
        public void GetIndex_ShouldReturnCorrectIndex()
        {
            var hashtable = new MyHashtable<TestKey, TestValue>();
            var key = new TestKey { Value = 1 };

            // Проверяем, что метод GetIndex() возвращает
            // правильный индекс для заданного ключа
            Assert.AreEqual(hashtable.GetIndex(key), Math.Abs(key.GetHashCode()) % hashtable.Capacity);
        }

        [TestMethod]
        public void FindKeyByData_ShouldReturnNullWhenNotFound()
        {
            var hashtable = new MyHashtable<TestKey, TestValue>();
            var key = new TestKey { Value = 1 };

            // Проверяем, что FindKeyByData() возвращает null,
            // когда элемент с заданным ключом не найден
            Assert.IsNull(hashtable.FindKeyByData(key));
        }

        [TestMethod]
        public void RemoveData_ShouldReturnFalseWhenNotFound()
        {
            var hashtable = new MyHashtable<TestKey, TestValue>();
            var key = new TestKey { Value = 1 };

            // Проверяем, что RemoveData() возвращает false,
            // когда элемент с заданным ключом не найден
            Assert.IsFalse(hashtable.RemoveData(key));
        }

        [TestMethod]
        public void ItemGetHashCode_ShouldReturnSameHashCodeForSameKey()
        {
            var key1 = new TestKey { Value = 1 };
            var key2 = new TestKey { Value = 1 };
            var value1 = new TestValue { Data = 42 };

            // Проверяем, что для одного и того же ключа
            // возвращается одинаковый хэш-код
            var item1 = new Item<TestKey, TestValue>(key1, value1);
            var item2 = new Item<TestKey, TestValue>(key2, value1);

            Assert.AreEqual(item1.GetHashCode(), item2.GetHashCode());
        }

        // Метод для сравнения вывода в консоль
        private bool ConsoleOutputMatches(Action action, string expectedOutput)
        {
            var consoleOutput = new StringWriter();
            var originalConsoleOut = Console.Out;
            Console.SetOut(consoleOutput);

            action();

            Console.SetOut(originalConsoleOut);
            return consoleOutput.ToString().Trim() == expectedOutput;
        }
    }
}
