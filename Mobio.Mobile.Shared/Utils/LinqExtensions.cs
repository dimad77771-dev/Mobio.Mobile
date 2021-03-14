using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public static class LinqExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> arr, Action<T> action)
		{
			foreach(var obj in arr)
			{
				action(obj);
			}
		}

		public static void RemoveRange<T>(this Collection<T> arr, IEnumerable<T> delrows)
		{
			var delarr = delrows.ToArray();
			foreach (var del in delarr)
			{
				arr.Remove(del);
			}
		}

		public static void RemoveRange<T>(this Collection<T> arr, Func<T, bool> func)
		{
			var delarr = arr.Where(func).ToArray();
			foreach (var del in delarr)
			{
				arr.Remove(del);
			}
		}


		public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> arr)
		{
			return new ObservableCollection<T>(arr.ToArray());
		}
		
		public static IEnumerable<Tuple<T,T>> Join<T, TKey>(this IEnumerable<T> a1, IEnumerable<T> a2, Func<T, TKey> keySelector)
		{
			return a1.Join<T, T, TKey, Tuple<T,T>>(a2, keySelector, keySelector, (q1,q2) => new Tuple<T,T>(q1, q2));
		}
		

		public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, TSource second)
		{
			return first.Union(new TSource[] { second });
		}

		public static int FindIndex<T>(this ObservableCollection<T> arr, Predicate<T> match)
		{
			var index = arr.ToList().FindIndex(match);
			return index;
		}

	}
}
