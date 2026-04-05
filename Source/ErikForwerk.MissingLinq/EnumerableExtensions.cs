
//-----------------------------------------------------------------------------------------------------------------------------------------
namespace ErikForwerk.MissingLinq;

//-----------------------------------------------------------------------------------------------------------------------------------------
public static class EnumerableExtensions
{
	//-----------------------------------------------------------------------------------------------------------------
	#region Split

	/// <summary>
	/// Splits the elements of a sequence into two groups based on a specified predicate.
	/// </summary>
	/// <remarks>
	/// If <paramref name="sortInPlace"/> is set to <see langword="true"/>, the original array may be modified and should not be used elsewhere after calling this method.
	/// If <see langword="false"/>, the method creates a copy of the source array before splitting.
	/// </remarks>
	/// <typeparam name="TItem">The type of the elements in the source array.</typeparam>
	/// <param name="source">The array to split into two groups based on the specified predicate. Cannot be null.</param>
	/// <param name="predicate">A function to test each element for a condition. Items for which this predicate returns <see langword="true"/> are
	/// placed in the first array; others are placed in the second array. Cannot be null.</param>
	/// <param name="sortInPlace">If <see langword="true"/>, the split is performed in place and the original array may be reordered. If <see
	/// langword="false"/>, the operation does not modify the original array. The default is <see langword="false"/>.</param>
	/// <returns>A tuple containing two arrays: the first with all elements for which the predicate returned <see langword="true"/>,
	/// and the second with all elements for which the predicate returned <see langword="false"/>. Both arrays preserve the
	/// relative order of the elements from the source array.</returns>
	public static (TItem[] TrueItems, TItem[] FalseItems) Split<TItem>(this TItem[] source, Func<TItem, bool> predicate, bool sortInPlace=false)
	{
		ArgumentNullException.ThrowIfNull(source);
		ArgumentNullException.ThrowIfNull(predicate);

		TItem[] workingSource	= sortInPlace ? source : [.. source];
		int nextTrueIndex		= 0;

		for (int i = 0; i < workingSource.Length; i++)
		{
			if (predicate(workingSource[i]))
			{
				if (i != nextTrueIndex)
					(workingSource[nextTrueIndex], workingSource[i]) = (workingSource[i], workingSource[nextTrueIndex]);

				nextTrueIndex++;
			}
		}

		return (workingSource[.. nextTrueIndex], workingSource[nextTrueIndex..]);
	}

	/// <summary>
	/// Splits the elements of a sequence into two groups based on a specified predicate.
	/// </summary>
	/// <remarks>
	/// The returned sequences are evaluated lazily.
	/// Enumerating either sequence will enumerate the source sequence and apply the predicate as needed.
	/// To avoid multiple enumerations of the source, consider materializing the results if the source is not a collection.
	/// </remarks>
	/// <typeparam name="TItem">The type of the elements in the source sequence.</typeparam>
	/// <param name="source">The sequence of elements to split. Cannot be null.</param>
	/// <param name="predicate">A function to test each element for a condition. Cannot be null.</param>
	/// <returns>
	/// A tuple containing two sequences: the first with elements for which the predicate returns <see langword="true"/>,
	/// and the second with elements for which the predicate returns <see langword="false"/>.
	/// </returns>
	public static (IEnumerable<TItem> TrueItems, IEnumerable<TItem> FalseItems) Split<TItem>(this IEnumerable<TItem> source, Func<TItem, bool> predicate)
		=> (source.Where(predicate), source.Where(item => !predicate(item)));

	#endregion Split

	//-----------------------------------------------------------------------------------------------------------------
	#region IsNullOrEmpty

	/// <summary>Determines whether the specified array is null or has no elements.</summary>
	/// <typeparam name="T">The type of the elements in the array.</typeparam>
	/// <param name="array">The array to check for <see langword="null"/> or emptiness.</param>
	/// <returns><see langword="true"/> if the array is <see langword="null"/> or has a <see cref="Array.Length"/> of 0; otherwise, <see langword="false"/>.</returns>
	public static bool IsNullOrEmpty<T>(this T[]? array)
		=> array is null || array.Length == 0;

	/// <summary>
	/// Determines whether the specified list is null or contains no elements.
	/// </summary>
	/// <typeparam name="T">The type of elements in the list.</typeparam>
	/// <param name="list">The list to check for <see langword="null"/> or emptiness.</param>
	/// <returns><see langword="true"/> if the list is <see langword="null"/> or has a <see cref="IList.Count"/> of 0; otherwise, <see langword="false"/>.</returns>
	public static bool IsNullOrEmpty<T>(this IList<T>? list)
		=> list is null || list.Count == 0;

	/// <summary>
	/// Determines whether the specified collection is null or contains no elements.
	/// </summary>
	/// <typeparam name="T">The type of elements in the collection.</typeparam>
	/// <param name="collection">The collection to check for <see langword="null"/> or emptiness.</param>
	/// <returns><see langword="true"/> if the collection is <see langword="null"/> or contains no elements; otherwise, <see langword="false"/>.</returns>
	public static bool IsNullOrEmpty<T>(this IEnumerable<T>? collection)
		=> collection is null || !collection.Any();

	#endregion Neue Region
}
