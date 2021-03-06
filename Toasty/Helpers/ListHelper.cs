using System.Collections.Generic;

namespace Toasty.Helpers
{
	public static class ListHelper
	{
		private static T Swap<T>(this IList<T> list, int indexA, int indexB)
		{
			var tmp = list[indexA];
			list[indexA] = list[indexB];
			list[indexB] = tmp;
			return list[indexB];
		}

		public static T MoveToTop<T>(this IList<T> list, T item)
		{
			var index = list.IndexOf(item);
			list.RemoveAt(index);
			list.Insert(0, item);
			return item;
		}

		public static T MoveToBottom<T>(this IList<T> list, T item)
		{
			var index = list.IndexOf(item);
			list.RemoveAt(index);
			list.Insert(list.Count, item);
			return item;
		}

		public static T MoveUp<T>(this IList<T> list, T item)
		{
			var index = list.IndexOf(item);

			return index == 0 ?
				list.MoveToBottom(item) :
				list.Swap(index, index - 1);
		}

		public static T MoveDown<T>(this IList<T> list, T item)
		{
			var index = list.IndexOf(item);

			return list.Count - 1 < index + 1 ?
				list.MoveToTop(item) :
				list.Swap(index, index + 1);
		}
	}
}