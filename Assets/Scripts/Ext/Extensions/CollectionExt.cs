using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public static class CollectionExt
{
	#region Get Random Element

	/// <summary>
	/// Returns Random index in list, or defaultIndex if list is empty
	/// </summary>
	public static int GetRandomIndexOrDefault<T>(this List<T> list, int defaultIndex = -1)
	{
		return list.Count == 0 ? defaultIndex : Random.Range(0, list.Count);
	}

	/// <summary>
	/// Returns Random index in collection, or defaultIndex if collection is empty
	/// </summary>
	public static int GetRandomIndexOrDefault<T>(this IEnumerable<T> collection, int defaultIndex = -1)
	{
		int count = collection.Count();
		return count == 0 ? defaultIndex : Random.Range(0, count);
	}

	/// <summary>
	/// Returns Random item in list, or default (null) if list is empty
	/// </summary>
	public static T GetRandomOrDefault<T>(this List<T> list)
	{
		return list.Count == 0 ? (default) : list[Random.Range(0, list.Count)];
	}

	/// <summary>
	/// Returns Random item in list, or defaultOverride if list is empty
	/// </summary>
	public static T GetRandomOrDefault<T>(this List<T> list, T defaultOverride = default)
	{
		return list.Count == 0 ? defaultOverride : list[Random.Range(0, list.Count)];
	}
	/// <summary>
	/// Returns Random item in array, or default (null) if list is empty
	/// </summary>
	public static T GetRandomOrDefault<T>(this T[] array)
	{
		return array.Length == 0 ? (default) : array[Random.Range(0, array.Length)];
	}
	/// <summary>
	/// Returns Random item in array, or defaultOverride if list is empty
	/// </summary>
	public static T GetRandomOrDefault<T>(this T[] array, T defaultOverride = default)
	{
		return array.Length == 0 ? defaultOverride : array[Random.Range(0, array.Length)];
	}

	/// <summary>
	/// Returns Random item in collection, or default (null) if collection is empty
	/// </summary>
	public static T GetRandomOrDefault<T>(this IEnumerable<T> collection)
	{
		int count = collection.Count();
		return count == 0 ? (default) : collection.ElementAt(Random.Range(0, count));
	}

	/// <summary>
	/// Returns Random item in collection, or defaultOverrde if collection is empty
	/// </summary>
	public static T GetRandomOrDefault<T>(this IEnumerable<T> collection, T defaultOverride = default)
	{
		int count = collection.Count();
		return count == 0 ? defaultOverride : collection.ElementAt(Random.Range(0, count));
	}

	/// <summary>
	/// Returns Random item in list, ignoring element on indexToIgnore
	/// </summary>
	public static T GetRandomOrDefaultIgnoringIndex<T>(this List<T> list, int indexToIgnore)
	{
		indexToIgnore = indexToIgnore < -1 ? -1 : indexToIgnore >= list.Count ? -1 : indexToIgnore;
		if (indexToIgnore == -1)
		{
			return list.GetRandomOrDefault();
		}

		if (list.Count >= 0 && list.Count <= 1)
		{
			return list.Count == 0 ? default : list.First();
		}
		else
		{
			int randomIndex = Random.Range(0, list.Count - 1);
			return randomIndex != 0 && randomIndex < indexToIgnore ? list[randomIndex] : list[randomIndex + 1];
		}
	}

	/// <summary>
	/// Returns Random item in array, ignoring element on indexToIgnore
	/// </summary>
	public static T GetRandomOrDefaultIgnoringIndex<T>(this T[] array, int indexToIgnore)
	{
		indexToIgnore = indexToIgnore < -1 ? -1 : indexToIgnore >= array.Length ? -1 : indexToIgnore;
		if (indexToIgnore == -1)
		{
			return array.GetRandomOrDefault();
		}

		if (array.Length >= 0 && array.Length <= 1)
		{
			return array.Length == 0 ? default : array.First();
		}
		else
		{
			int randomIndex = Random.Range(0, array.Length - 1);
			return randomIndex != 0 && randomIndex < indexToIgnore ? array[randomIndex] : array[randomIndex + 1];
		}
	}

	#endregion

	/// <summary>
	/// MinOrDefault (with overridable default value)
	/// </summary>
	public static TResult MinOrDefault<T, TResult>(this IEnumerable<T> collection, Func<T, TResult> selector, TResult defVal = default)
	{
		var comparer = Comparer<TResult>.Default;

		TResult minValue = defVal;
		bool valueIsSet = false;

		foreach (var item in collection)
		{
			TResult itemValue = selector.Invoke(item);

			if (valueIsSet == false)
			{
				minValue = itemValue;
				valueIsSet = true;
			}
			else
			{
				if (comparer.Compare(itemValue, minValue) == -1)
				{
					minValue = itemValue;
				}
			}
		}
		return minValue;
	}
	/// <summary>
	/// LINQ's FirstOrDefault with overridable default value
	/// </summary>
	public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, TSource defVal = default)
	{
		if (source == null) throw new ArgumentNullException("source");
		if (predicate == null) throw new ArgumentNullException("predicate");
		foreach (TSource element in source)
		{
			if (predicate(element)) return element;
		}
		return defVal;
	}

	public static bool IsNullOrEmpty<T>(this List<T> list)
	{
		return list == null || list.Count == 0;
	}
	public static bool IsNullOrEmpty<T>(this T[] array)
	{
		return array == null || array.Length == 0;
	}

	/// <summary>
	/// Same as LINQ's ElementAtOrDefault, but without unnecessary unboxing
	/// </summary>
	public static T ElementAtOrDefault<T>(this T[] array, int index)
	{
		if (array == null) throw new ArgumentNullException(nameof(array));
		if (index < 0 || index >= array.Length) return default;
		return array[index];
	}
	/// <summary>
	/// Same as LINQ's ElementAtOrDefault, but without unnecessary unboxing
	/// </summary>
	public static T ElementAtOrDefault<T>(this List<T> list, int index)
	{
		if (list == null) throw new ArgumentNullException(nameof(list));
		if (index < 0 || index >= list.Count) return default;
		return list[index];
	}

	public static T ElementAtOrValue<T>(this T[] array, int index, T value)
	{
		if (array == null) throw new ArgumentNullException(nameof(array));
		if (index < 0 || index >= array.Length) return value;
		return array[index];
	}

	/// <summary>
	/// Adds item to the list, if condition.Invoke(item) returns true.
	/// </summary>
	public static bool AddIf<T>(this List<T> list, T item, Func<T, bool> condition)
	{
		if (condition.Invoke(item))
		{
			list.Add(item);
			return true;
		}
		return false;
	}
	public static bool AddIfNotContains<T>(this List<T> list, T item)
	{
		if (list.Contains(item) == false)
		{
			list.Add(item);
			return true;
		}
		else return false;
	}
	public static bool AddIfNotContains<T>(this List<T> list, T item, Func<T, bool> predicate)
	{
		if (list.Any(x => predicate.Invoke(x)) == false)
		{
			list.Add(item);
			return true;
		}
		else return false;
	}
	/// <summary>
	/// Shorthand for 'var item = new MyItem(); list.Add(item);'
	/// </summary>
	public static T AddReturn<T>(this List<T> list, T item)
	{
		list.Add(item);
		return item;
	}

	public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
	{
		HashSet<TKey> set = new HashSet<TKey>();
		foreach (TSource element in source)
		{
			if (set.Add(keySelector(element)))
				yield return element;
		}
	}

	/// <summary>
	/// Gets first item in list and removes it from it
	/// </summary>
	public static T PopFirst<T>(this List<T> list)
	{
		T element = list.First();
		list.RemoveFirst();
		return element;
	}

	/// <summary>
	/// Gets last item in list and removes it from it
	/// </summary>
	public static T PopLast<T>(this List<T> list)
	{
		T element = list.Last();
		list.RemoveLast();
		return element;
	}

	#region Concat
	public static IEnumerable<T> Concat<T>(this IEnumerable<T> first, IEnumerable<T> second, IEnumerable<T> third)
	{
		if (first == null) throw new ArgumentNullException(nameof(first));
		if (second == null) throw new ArgumentNullException(nameof(second));
		if (third == null) throw new ArgumentNullException(nameof(third));
		foreach (T element in first) yield return element;
		foreach (T element in second) yield return element;
		foreach (T element in third) yield return element;
	}
	public static IEnumerable<T> Concat<T>(this IEnumerable<T> first, IEnumerable<T> second, IEnumerable<T> third, IEnumerable<T> fourth)
	{
		if (first == null) throw new ArgumentNullException(nameof(first));
		if (second == null) throw new ArgumentNullException(nameof(second));
		if (third == null) throw new ArgumentNullException(nameof(third));
		if (fourth == null) throw new ArgumentNullException(nameof(fourth));
		foreach (T element in first) yield return element;
		foreach (T element in second) yield return element;
		foreach (T element in third) yield return element;
		foreach (T element in fourth) yield return element;
	}
	public static IEnumerable<T> Concat<T>(this IEnumerable<T> first, IEnumerable<T> second, IEnumerable<T> third, IEnumerable<T> fourth, IEnumerable<T> fifth)
	{
		if (first == null) throw new ArgumentNullException(nameof(first));
		if (second == null) throw new ArgumentNullException(nameof(second));
		if (third == null) throw new ArgumentNullException(nameof(third));
		if (fourth == null) throw new ArgumentNullException(nameof(fourth));
		if (fifth == null) throw new ArgumentNullException(nameof(fifth));
		foreach (T element in first) yield return element;
		foreach (T element in second) yield return element;
		foreach (T element in third) yield return element;
		foreach (T element in fourth) yield return element;
		foreach (T element in fifth) yield return element;
	}
	public static IEnumerable<T> Concat<T>(this IEnumerable<T> first, IEnumerable<T> second, IEnumerable<T> third, IEnumerable<T> fourth, IEnumerable<T> fifth, IEnumerable<T> sixth)
	{
		if (first == null) throw new ArgumentNullException(nameof(first));
		if (second == null) throw new ArgumentNullException(nameof(second));
		if (third == null) throw new ArgumentNullException(nameof(third));
		if (fourth == null) throw new ArgumentNullException(nameof(fourth));
		if (fifth == null) throw new ArgumentNullException(nameof(fifth));
		if (sixth == null) throw new ArgumentNullException(nameof(sixth));
		foreach (T element in first) yield return element;
		foreach (T element in second) yield return element;
		foreach (T element in third) yield return element;
		foreach (T element in fourth) yield return element;
		foreach (T element in fifth) yield return element;
		foreach (T element in sixth) yield return element;
	}
	#endregion

	public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer = null)
	{
		return new HashSet<T>(source, comparer);
	}

	/// <summary>
	/// ForEach with delegate for IEnumerable
	/// </summary>
	public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
	{
		foreach (T item in collection)
			action.Invoke(item);
	}

	/// <summary>
	/// Shorthand for list.FindIndex(predicate), list.RemoveAt(index)
	/// </summary>
	public static bool RemoveFirst<T>(this List<T> list, Predicate<T> predicate)
	{
		int indexOfFirstPredicate = list.FindIndex(predicate);
		if (indexOfFirstPredicate == -1) return false;

		list.RemoveAt(indexOfFirstPredicate);
		return true;
	}

	/// <summary>
	/// Shorthand for list.RemoveAt(0)
	/// </summary>
	public static bool RemoveFirst<T>(this List<T> list)
	{
		if (list.Count == 0) return false;

		list.RemoveAt(0);
		return true;
	}

	/// <summary>
	/// Shorthand for list.FindLastIndex(predicate), list.RemoveAt(index)
	/// </summary>
	public static bool RemoveLast<T>(this List<T> list, Predicate<T> predicate)
	{
		int indexOfLastPredicate = list.FindLastIndex(predicate);
		if (indexOfLastPredicate == -1) return false;

		list.RemoveAt(indexOfLastPredicate);
		return true;
	}

	/// <summary>
	/// Shorthand for list.RemoveAt(list.Count - 1)
	/// </summary>
	public static bool RemoveLast<T>(this List<T> list)
	{
		if (list.Count == 0) return false;

		list.RemoveAt(list.Count - 1);
		return true;
	}
	
	/// <summary>
	/// Remove all nulls from the list
	/// </summary>
	public static bool RemoveNulls<T>(this List<T> list)
	{
		if (list.Count == 0) return false;

		bool nullsRemoved = false;
		for (int i = list.Count - 1; i >= 0; i--)
		{
			if (list[i] == null)
			{
				list.RemoveAt(i);
				nullsRemoved = true;
			}
		}
			
		return nullsRemoved;
	}

	// https://forum.unity.com/threads/random-numbers-with-a-weighted-chance.442190/
	public static int GetRandomWeightedIndex(float[] weights)
	{
		if (weights == null || weights.Length == 0) return -1;

		float w;
		float t = 0;
		int i;
		for (i = 0; i < weights.Length; i++)
		{
			w = weights[i];

			if (float.IsPositiveInfinity(w))
			{
				return i;
			}
			else if (w >= 0f && !float.IsNaN(w))
			{
				t += weights[i];
			}
		}

		float r = Random.value;
		float s = 0f;

		for (i = 0; i < weights.Length; i++)
		{
			w = weights[i];
			if (float.IsNaN(w) || w <= 0f) continue;

			s += w / t;
			if (s >= r) return i;
		}

		return -1;
	}
	public static int GetRandomWeightedIndex<T>(this List<T> list, Func<T, float> weightSelector)
	{
		if (list == null || list.Count == 0) return -1;

		float w;
		float t = 0;
		int i;

		for (i = 0; i < list.Count; i++)
		{
			w = weightSelector.Invoke(list[i]);

			if (float.IsPositiveInfinity(w))
			{
				return i;
			}
			else if (w >= 0f && !float.IsNaN(w))
			{
				t += w;
			}
		}

		float r = Random.value;
		float s = 0f;

		for (i = 0; i < list.Count; i++)
		{
			w = weightSelector.Invoke(list[i]);
			if (float.IsNaN(w) || w <= 0f) continue;

			s += w / t;
			if (s >= r) return i;
		}

		return -1;
	}

	public static T GetRandomWeightedOrDefault<T>(this IEnumerable<T> collection, Func<T, float> weightSelector)
	{
		int index = collection.GetRandomWeightedIndex(weightSelector);
		return collection.ElementAtOrDefault(index);
	}
	public static int GetRandomWeightedIndex<T>(this IEnumerable<T> collection, Func<T, float> weightSelector)
	{
		int count = collection.Count();
		if (collection == null || count == 0) return -1;

		float w;
		float t = 0;
		int i;

		for (i = 0; i < count; i++)
		{
			w = weightSelector.Invoke(collection.ElementAt(i));

			if (float.IsPositiveInfinity(w))
			{
				return i;
			}
			else if (w >= 0f && !float.IsNaN(w))
			{
				t += w;
			}
		}

		float r = Random.value;
		float s = 0f;

		for (i = 0; i < count; i++)
		{
			w = weightSelector.Invoke(collection.ElementAt(i));
			if (float.IsNaN(w) || w <= 0f) continue;

			s += w / t;
			if (s >= r) return i;
		}

		return -1;
	}

	public static int GetRandomWeightedIndex<T>(this T[] array, Func<T, float> weightSelector)
	{
		if (array == null || array.Length == 0) return -1;

		float w;
		float t = 0;
		int i;

		for (i = 0; i < array.Length; i++)
		{
			w = weightSelector.Invoke(array[i]);

			if (float.IsPositiveInfinity(w))
			{
				return i;
			}
			else if (w >= 0f && !float.IsNaN(w))
			{
				t += w;
			}
		}

		float r = Random.value;
		float s = 0f;

		for (i = 0; i < array.Length; i++)
		{
			w = weightSelector.Invoke(array[i]);
			if (float.IsNaN(w) || w <= 0f) continue;

			s += w / t;
			if (s >= r) return i;
		}

		return -1;
	}

	public static IEnumerable<T> Rotate<T>(this IEnumerable<T> collection, int offset)
	{
		return collection.Skip(offset).Concat(collection.Take(offset));
	}

	// https://thomaslevesque.com/2014/12/07/optimize-toarray-and-tolist-by-providing-the-number-of-elements/
	/// <summary>
	/// Same as LINQ's ToArray(), but with an ability to provide item count for memory effectiveness.
	/// IndexOutOfRange exception will be thrown if collection is larger than the provided item count.
	/// </summary>
	public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source, int count)
	{
		if (source == null) throw new ArgumentNullException("source");
		if (count < 0) throw new ArgumentOutOfRangeException("count");
		TSource[] array = new TSource[count];
		int i = 0;
		foreach (TSource item in source)
		{
			array[i++] = item;
		}
		return array;
	}
	/// <summary>
	/// Same as LINQ's ToList(), but with an ability to provide item count for memory effectiveness.
	/// </summary>
	public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source, int count)
	{
		if (source == null) throw new ArgumentNullException("source");
		if (count < 0) throw new ArgumentOutOfRangeException("count");
		List<TSource> list = new List<TSource>(count);
		foreach (TSource item in source)
		{
			list.Add(item);
		}
		return list;
	}

	/// <summary>
	/// Shorthand for T itemA = list[indexA]; list[indexA] = list[indexB]. list[indexB] = itemA.
	/// </summary>
	public static void Swap<T>(this List<T> list, int indexA, int indexB)
	{
		if (indexA < 0 || indexA >= list.Count) throw new ArgumentOutOfRangeException(nameof(indexA));
		if (indexB < 0 || indexB >= list.Count) throw new ArgumentOutOfRangeException(nameof(indexB));
		if (indexA == indexB) return;

		T itemA = list[indexA]; // cache reference of itemA
		list[indexA] = list[indexB]; // set itemB to itemA place
		list[indexB] = itemA; // set itemA to itemB place
	}

	/// <summary>
	/// Shorthand for T itemA = array[indexA]; array[indexA] = array[indexB]. array[indexB] = itemA.
	/// </summary>
	public static void Swap<T>(this T[] array, int indexA, int indexB)
	{
		if (indexA < 0 || indexA >= array.Length) throw new ArgumentOutOfRangeException(nameof(indexA));
		if (indexB < 0 || indexB >= array.Length) throw new ArgumentOutOfRangeException(nameof(indexB));
		if (indexA == indexB) return;

		T itemA = array[indexA]; // cache reference of itemA
		array[indexA] = array[indexB]; // set itemB to itemA place
		array[indexB] = itemA; // set itemA to itemB place
	}

	/// <summary>
	/// Shorthand for list[0]
	/// </summary>
	public static T First<T>(this List<T> list) => list[0];
	/// <summary>
	/// Shorthand for array[0]
	/// </summary>
	public static T First<T>(this T[] array) => array[0];
	/// <summary>
	/// Shorthand for list[list.Count - 1]
	/// </summary>
	public static T Last<T>(this List<T> list) => list[list.Count - 1];
	/// <summary>
	/// Shorthand for array[array.Length - 1]
	/// </summary>
	public static T Last<T>(this T[] array) => array[array.Length - 1];

	/// <summary>
	/// Returns element by index considering list as infinite circular collection.
	/// For example you have 4 items. So indexes are from 0 to 3.
	/// If you pass index 4, you will get again 0 item. If 5, then 1 item.
	/// </summary>
	public static T ElementAtOrDefaultCircular<T>(this List<T> list, int index)
	{
		if (list.Count == 0) return default;
		if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

		return list[index % list.Count];
	}
	/// <summary>
	/// Returns element by index considering array as infinite circular collection.
	/// For example you have 4 items. So indexes are from 0 to 3.
	/// If you pass index 4, you will get again 0 item. If 5, then 1 item.
	/// </summary>
	public static T ElementAtOrDefaultCircularO<T>(this T[] array, int index)
	{
		if (array.Length == 0) return default;
		if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

		return array[index % array.Length];
	}

	/// <summary>
	/// Sorts list with selector, without creating new collection object
	/// </summary>
	public static void SortBy<TSource, TKey>(this List<TSource> list, Func<TSource, TKey> keySelector)
	{
		list.Sort(new SortByComparer<TSource, TKey>(keySelector));
	}
	public static void SortByDescending<TSource, TKey>(this List<TSource> list, Func<TSource, TKey> keySelector)
	{
		list.Sort(new SortByComparer<TSource, TKey>(keySelector, reverse: true));
	}
	public static void SortBy<TSource, TKey>(this List<TSource> list, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		list.Sort(new SortByComparer<TSource, TKey>(keySelector, comparer));
	}
	public static void SortByDescending<TSource, TKey>(this List<TSource> list, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		list.Sort(new SortByComparer<TSource, TKey>(keySelector, comparer, reverse: true));
	}

	/// <summary>
	/// Sorts array with selector, without creating new collection object
	/// </summary>
	public static void SortBy<TSource, TKey>(this TSource[] array, Func<TSource, TKey> keySelector)
	{
		Array.Sort(array, new SortByComparer<TSource, TKey>(keySelector));
	}
	public static void SortByDescending<TSource, TKey>(this TSource[] array, Func<TSource, TKey> keySelector)
	{
		Array.Sort(array, new SortByComparer<TSource, TKey>(keySelector, reverse: true));
	}
	public static void SortBy<TSource, TKey>(this TSource[] array, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		Array.Sort(array, new SortByComparer<TSource, TKey>(keySelector, comparer));
	}
	public static void SortByDescending<TSource, TKey>(this TSource[] array, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		Array.Sort(array, new SortByComparer<TSource, TKey>(keySelector, comparer, reverse: true));
	}

	private class SortByComparer<TSource, TKey> : IComparer<TSource>
	{
		private Func<TSource, TKey> keySelector = null;
		private IComparer<TKey> keyComparer = null;
		private bool reverse = false;

		public SortByComparer(Func<TSource, TKey> keySelector) : this(keySelector, Comparer<TKey>.Default) { }
		public SortByComparer(Func<TSource, TKey> keySelector, bool reverse) : this(keySelector, Comparer<TKey>.Default, reverse) { }
		public SortByComparer(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : this(keySelector, comparer, false) { }
		public SortByComparer(Func<TSource, TKey> keySelector, IComparer<TKey> comparer, bool reverse)
		{
			this.keySelector = keySelector;
			this.keyComparer = comparer;
			this.reverse = reverse;
		}

		public int Compare(TSource x, TSource y)
		{
			if (reverse == false)
				return keyComparer.Compare(keySelector.Invoke(x), keySelector.Invoke(y));
			else
				return keyComparer.Compare(keySelector.Invoke(y), keySelector.Invoke(x));
		}
	}
}