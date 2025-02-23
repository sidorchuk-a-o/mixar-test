using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using UnityRandom = UnityEngine.Random;

namespace AD.ToolsCollection
{
    /// <summary> 
    /// Методы расширения для перечислений и коллекций 
    /// </summary>
    public static class ListExtensions
    {
        // == Collection Pool ==

        /// <summary>
        /// Преобразовать коллекцию в список из пула <see cref="ListPool{T}"/>
        /// </summary>
        /// <param name="values">Коллекция элементов</param>
        /// <returns>Список элементов</returns>
        public static List<T> ToListPool<T>(this IEnumerable<T> values)
        {
            ListPool<T>.Get(out var list);

            list.AddRange(values);

            return list;
        }

        /// <summary>
        /// Отправить список в <see cref="ListPool{T}"/>
        /// </summary>
        /// <param name="list">Список элементов</param>
        public static void ReleaseListPool<T>(this List<T> list)
        {
            if (list is null)
            {
                return;
            }

            try
            {
                ListPool<T>.Release(list);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        /// <summary>
        /// Преобразовать коллекцию в массив из пула <see cref="ArrayPool{T}"/>
        /// </summary>
        /// <param name="values">Коллекция элементов</param>
        /// <returns>Массив элементов</returns>
        public static T[] ToArrayPool<T>(this IEnumerable<T> values)
        {
            var i = 0;
            var length = values.Count();

            var array = ArrayPool<T>.Shared.Rent(length);

            foreach (var value in values)
            {
                array[i] = value;

                i++;
            }

            return array;
        }

        /// <summary>
        /// Отправить массив в <see cref="ArrayPool{T}"/>
        /// </summary>
        /// <param name="array">Массив элементов</param>
        public static void ReleaseArrayPool<T>(this T[] array)
        {
            if (array is null)
            {
                return;
            }

            try
            {
                ArrayPool<T>.Shared.Return(array, clearArray: true);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        /// <summary>
        /// Преобразовать коллекцию в хешсет из пула <see cref="HashSetPool{T}"/>
        /// </summary>
        /// <param name="values">Коллекция элементов</param>
        /// <returns>Хешсет элементов</returns>
        public static HashSet<T> ToHashSetPool<T>(this IEnumerable<T> values)
        {
            HashSetPool<T>.Get(out var set);

            set.AddValues(values);

            return set;
        }

        /// <summary>
        /// Отправить хешсет в <see cref="HashSetPool{T}"/>
        /// </summary>
        /// <param name="set">Хешсет элементов</param>
        public static void ReleaseHashSetPool<T>(this HashSet<T> set)
        {
            if (set is null)
            {
                return;
            }

            try
            {
                HashSetPool<T>.Release(set);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        /// <summary>
        /// Отправить словарь в <see cref="DictionaryPool{T1,T2}"/>
        /// </summary>
        /// <param name="dictionary">Словарь элементов</param>
        public static void ReleaseDictionaryPool<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            if (dictionary is null)
            {
                return;
            }

            try
            {
                DictionaryPool<TKey, TValue>.Release(dictionary);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        // == Common ==

        public static void Dispose<TValue>(this ICollection<TValue> collection)
            where TValue : IDisposable
        {
            foreach (var value in collection)
            {
                value?.Dispose();
            }

            collection.Clear();
        }

        public static void Dispose<TKey, TValue>(this ICollection<KeyValuePair<TKey, TValue>> dict)
            where TValue : IDisposable
        {
            foreach (var item in dict)
            {
                item.Value?.Dispose();
            }

            dict.Clear();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> values)
        {
            return values == null || values.Any() == false;
        }

        /// <summary>
        /// Получение перечисления из массива
        /// </summary>
        /// <param name="array">Список элементов</param>
        /// <returns>Перечисление на основе списка</returns>
        public static IEnumerable<T> GetEnumerable<T>(params T[] array)
        {
            foreach (var value in array)
            {
                yield return value;
            }
        }

        public static void TryAdd<T>(this ICollection<T> collection, T value)
        {
            if (collection.Contains(value))
            {
                return;
            }

            collection.Add(value);
        }

        /// <summary>
        /// Добавить значения в коллекцию
        /// </summary>
        /// <param name="collection">Исходная коллекция элементов</param>
        /// <param name="values">Коллекция новых элементов</param>
        public static void AddValues<T>(this ICollection<T> collection, IEnumerable<T> values)
        {
            if (collection is not null && collection.IsReadOnly)
            {
                Debug.LogException(new InvalidOperationException("Коллекция только для чтения"));

                return;
            }

            if (collection is null || values is null)
            {
                return;
            }

            foreach (var value in values)
            {
                collection.Add(value);
            }
        }

        public static void AddRange<T>(this IList list, IEnumerable<T> values)
        {
            if (list is not null && list.IsReadOnly)
            {
                Debug.LogException(new InvalidOperationException("Коллекция только для чтения"));

                return;
            }

            if (list is null || values is null)
            {
                return;
            }

            foreach (var value in values)
            {
                list.Add(value);
            }
        }

        public static void RemoveRange<T>(this IList list, IEnumerable<T> values)
        {
            if (list is not null && list.IsReadOnly)
            {
                Debug.LogException(new InvalidOperationException("Коллекция только для чтения"));

                return;
            }

            if (list is null || values is null)
            {
                return;
            }

            foreach (var value in values)
            {
                list.Remove(value);
            }
        }

        /// <summary>
        /// Получить индекс элемента массива
        /// </summary>
        /// <param name="array">Массив элементов</param>
        /// <param name="value">Элемент массива</param>
        /// <returns>Индекс элемента массива</returns>
        public static int IndexOf<T>(this T[] array, T value)
        {
            return Array.IndexOf(array, value);
        }

        /// <summary> 
        /// Получение следующего элемента коллекции
        /// </summary>
        /// <param name="collection">Коллекция элементов</param>
        /// <param name="index">Индекс элемента</param>
        /// <param name="loop">Это зацикленная коллекция?</param>
        /// <returns>Следующий элемент коллекции</returns>
        public static T Next<T>(this IReadOnlyCollection<T> collection, ref int index, bool loop = false)
        {
            var count = collection.Count;

            if (count == 0)
            {
                return default;
            }

            index++;

            if (index >= count)
            {
                index = loop ? 0 : count - 1;
            }

            return collection.ElementAtOrDefault(index);
        }

        /// <summary> 
        /// Получение предыдущего элемента коллекции
        /// </summary>
        /// <param name="collection">Коллекция элементов</param>
        /// <param name="index">Индекс элемента</param>
        /// <param name="loop">Это зацикленная коллекция?</param>
        /// <returns>Предыдущий элемент коллекции</returns>
        public static T Prev<T>(this IReadOnlyCollection<T> collection, ref int index, bool loop = false)
        {
            var count = collection.Count;

            if (count == 0)
            {
                return default;
            }

            index--;

            if (index < 0)
            {
                index = loop ? count - 1 : 0;
            }

            return collection.ElementAtOrDefault(index);
        }

        // == Random ==

        /// <summary>
        /// Перемешивание элементов коллекции
        /// </summary>
        /// <param name="collection">Коллекция элементов</param>
        /// <returns>Результат перемешивания (список взят из <see cref="ListPool{T}"/>)</returns>
        public static List<T> Shuffle<T>(this IReadOnlyCollection<T> collection)
        {
            var count = collection.Count;
            var result = collection.ToListPool();

            for (var i = 0; i < count; ++i)
            {
                var j = UnityRandom.Range(0, i + 1);

                if (j != i)
                {
                    result[i] = result[j];
                }

                result[j] = collection.ElementAt(i);
            }

            return result;
        }

        public static void ForEach<T>(this IReadOnlyCollection<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action.SafeInvoke(item);
            }
        }

        /// <summary>
        /// Получение случайного элемента коллекции
        /// </summary>
        /// <param name="collection">Коллекция элементов</param>
        /// <returns>Случайный элемент коллекции</returns>
        public static T RandomValue<T>(this IEnumerable<T> collection)
        {
            if (collection is null)
            {
                return default;
            }

            var count = collection.Count();

            if (count == 0)
            {
                return default;
            }

            var index = UnityRandom.Range(0, count);

            return collection.ElementAt(index);
        }

        public static IEnumerable<T> RandomValues<T>(this IEnumerable<T> collection, int count)
        {
            return collection
                .OrderBy(x => UnityRandom.value)
                .Take(count);
        }

        // == Sort ==

        /// <summary>
        /// Сортировка методом вставок
        /// </summary>
        /// <param name="collection">Исходный набор элементов</param>
        /// <param name="comparison">Сравнение элементов</param>
        /// <returns>Результат сортировки (список взят из <see cref="ListPool{T}"/>)</returns>
        public static IEnumerable<T> InsertionSort<T>(this IEnumerable<T> collection, Comparison<T> comparison)
        {
            var count = collection.Count();
            var result = collection.ToListPool();

            for (var j = 0; j < count; j++)
            {
                var i = j - 1;
                var key = result[j];

                while (i >= 0 && comparison.Invoke(result[i], key) > 0)
                {
                    result[i + 1] = result[i];
                    i--;
                }

                result[i + 1] = key;
            }

            return result;
        }

        // == Weighted Selection ==

        /// <summary>
        /// Выбор элемента коллекции на основе его веса 
        /// </summary>
        /// <param name="collection">Коллекция элементов</param>
        /// <param name="getWeight">Делегат получения веса определенного элемента</param>
        /// <returns>Случайно выбранный элемент из <paramref name="collection" /></returns>
        public static T WeightedSelection<T>(this IEnumerable<T> collection, Func<T, int> getWeight)
        {
            return collection.ElementAt(collection.WeightedSelectionIndex(getWeight));
        }

        /// <summary>
        /// Выбор элемента коллекции на основе его веса 
        /// </summary>
        /// <param name="collection">Коллекция элементов</param>
        /// <param name="getWeight">Делегат получения веса определенного элемента</param>
        /// <returns>Случайно выбранный элемент из <paramref name="collection" /></returns>
        public static T WeightedSelection<T>(this IEnumerable<T> collection, Func<T, float> getWeight)
        {
            return collection.ElementAt(collection.WeightedSelectionIndex(getWeight));
        }

        /// <summary>
        /// Выбор элемента коллекции на основе его веса 
        /// </summary>
        /// <param name="collection">Коллекция элементов</param>
        /// <param name="weightSum">Сумма всех весов элементов</param>
        /// <param name="getWeight">Делегат получения веса определенного элемента</param>
        /// <returns>Случайно выбранный элемент из <paramref name="collection" /></returns>
        public static T WeightedSelection<T>(this IEnumerable<T> collection, int weightSum, Func<T, int> getWeight)
        {
            return collection.ElementAt(collection.WeightedSelectionIndex(weightSum, getWeight));
        }

        /// <summary>
        /// Выбор элемента коллекции на основе его веса 
        /// </summary>
        /// <param name="collection">Коллекция элементов</param>
        /// <param name="weightSum">Сумма всех весов элементов</param>
        /// <param name="getWeight">Делегат получения веса определенного элемента</param>
        /// <returns>Случайно выбранный элемент из <paramref name="collection" /></returns>
        public static T WeightedSelection<T>(this IEnumerable<T> collection, float weightSum, Func<T, float> getWeight)
        {
            return collection.ElementAt(collection.WeightedSelectionIndex(weightSum, getWeight));
        }

        /// <summary>
        /// Выбор индекса элемента коллекции на основе его веса 
        /// </summary>
        /// <param name="collection">Коллекция элементов</param>
        /// <param name="getWeight">Делегат получения веса определенного элемента</param>
        /// <returns>Случайно выбранный элемент из <paramref name="collection" /></returns>
        public static int WeightedSelectionIndex<T>(this IEnumerable<T> collection, Func<T, int> getWeight)
        {
            return collection.WeightedSelectionIndex(collection.Sum(getWeight), getWeight);
        }

        /// <summary>
        /// Выбор индекса элемента коллекции на основе его веса 
        /// </summary>
        /// <param name="collection">Коллекция элементов</param>
        /// <param name="getWeight">Делегат получения веса определенного элемента</param>
        /// <returns>Случайно выбранный элемент из <paramref name="collection" /></returns>
        public static int WeightedSelectionIndex<T>(this IEnumerable<T> collection, Func<T, float> getWeight)
        {
            return collection.WeightedSelectionIndex(collection.Sum(getWeight), getWeight);
        }

        /// <summary>
        /// Выбор индекса элемента коллекции на основе его веса 
        /// </summary>
        /// <param name="collection">Коллекция элементов</param>
        /// <param name="weightSum">Сумма всех весов элементов</param>
        /// <param name="getWeight">Делегат получения веса определенного элемента</param>
        /// <returns>Случайно выбранный элемент из <paramref name="collection" /></returns>
        public static int WeightedSelectionIndex<T>(
            this IEnumerable<T> collection,
            int weightSum,
            Func<T, int> getWeight)
        {
            if (weightSum <= 0)
            {
                throw new ArgumentException("WeightSum - должно быть положительным значением", "weightSum");
            }

            var selectionIndex = 0;
            var selectionWeightIndex = UnityRandom.Range(0, weightSum);
            var elementCount = collection.Count();

            if (elementCount == 0)
            {
                throw new InvalidOperationException("Коллекция не должна быть пустой");
            }

            var itemWeight = getWeight(collection.ElementAt(selectionIndex));

            while (selectionWeightIndex >= itemWeight)
            {
                selectionWeightIndex -= itemWeight;
                selectionIndex++;

                if (selectionIndex >= elementCount)
                {
                    throw new ArgumentException("Индекс вышел за диапазон поиска. WeightSum точно правильный?", "weightSum");
                }

                itemWeight = getWeight(collection.ElementAt(selectionIndex));
            }

            return selectionIndex;
        }

        /// <summary>
        /// Выбор индекса элемента коллекции на основе его веса 
        /// </summary>
        /// <param name="collection">Коллекция элементов</param>
        /// <param name="weightSum">Сумма всех весов элементов</param>
        /// <param name="getWeight">Делегат получения веса определенного элемента</param>
        /// <returns>Случайно выбранный элемент из <paramref name="collection" /></returns>
        public static int WeightedSelectionIndex<T>(
            this IEnumerable<T> collection,
            float weightSum,
            Func<T, float> getWeight)
        {
            if (weightSum <= 0)
            {
                throw new ArgumentException("WeightSum - должно быть положительным значением", "weightSum");
            }

            var selectionIndex = 0;

            var selectedWeight = UnityRandom.value * weightSum;
            var elementCount = collection.Count();

            if (elementCount == 0)
            {
                throw new InvalidOperationException("Коллекция не должна быть пустой");
            }

            var itemWeight = getWeight(collection.ElementAt(selectionIndex));

            while (selectedWeight >= itemWeight)
            {
                selectedWeight -= itemWeight;
                selectionIndex++;

                if (selectionIndex >= elementCount)
                {
                    throw new ArgumentException("Индекс вышел за диапазон поиска. WeightSum точно правильный?", "weightSum");
                }

                itemWeight = getWeight(collection.ElementAt(selectionIndex));
            }

            return selectionIndex;
        }

        public static IEnumerable<T[]> Chunk<T>(this IEnumerable<T> source, int size)
        {
            if (source is null)
            {
                return null;
            }

            if (size < 1)
            {
                throw new ArgumentOutOfRangeException("size", "size - не может быть меньше 1");
            }

            if (source is T[] array)
            {
                return array.Length != 0
                    ? ArrayChunkIterator(array, size)
                    : Array.Empty<T[]>();
            }

            return EnumerableChunkIterator(source, size);
        }

        private static IEnumerable<T[]> ArrayChunkIterator<T>(T[] source, int size)
        {
            var index = 0;

            while (index < source.Length)
            {
                var length = Math.Min(size, source.Length - index);
                var chunk = new ReadOnlySpan<T>(source, index, length).ToArray();

                index += chunk.Length;

                yield return chunk;
            }
        }

        private static IEnumerable<TSource[]> EnumerableChunkIterator<TSource>(IEnumerable<TSource> source, int size)
        {
            using IEnumerator<TSource> e = source.GetEnumerator();

            if (e.MoveNext())
            {
                int i;
                var arraySize = Math.Min(size, 4);

                do
                {
                    var array = new TSource[arraySize];

                    i = 1;
                    array[0] = e.Current;

                    if (size != array.Length)
                    {
                        for (; i < size && e.MoveNext(); i++)
                        {
                            if (i >= array.Length)
                            {
                                arraySize = (int)Math.Min((uint)size, 2 * (uint)array.Length);
                                Array.Resize(ref array, arraySize);
                            }

                            array[i] = e.Current;
                        }
                    }
                    else
                    {
                        var local = array;

                        for (; (uint)i < (uint)local.Length && e.MoveNext(); i++)
                        {
                            local[i] = e.Current;
                        }
                    }

                    if (i != array.Length)
                    {
                        Array.Resize(ref array, i);
                    }

                    yield return array;
                }
                while (i >= size && e.MoveNext());
            }
        }
    }
}