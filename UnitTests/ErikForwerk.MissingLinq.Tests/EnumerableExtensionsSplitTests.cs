
//-----------------------------------------------------------------------------------------------------------------------------------------
namespace ErikForwerk.MissingLinq.Tests;

//-----------------------------------------------------------------------------------------------------------------------------------------
public sealed class EnumerableExtensionsSplitTests
{
	//-----------------------------------------------------------------------------------------------------------------
	#region Test Data

	public static TheoryData<int[], int[], int[]> ArraySplitValidData => new()
	{
		{ [1, 2, 3, 4, 5],	[2, 4],		[1, 3, 5]	},
		{ [2, 4, 6],		[2, 4, 6],	[]			},
		{ [],				[],			[]			},
	};

	public static TheoryData<int[], Func<int, bool>, string> ArraySplitInvalidData => new()
	{
		{ null!,		x => x > 0, "source"},
		{ [1, 2, 3],	null!,		"predicate"},
	};

	#endregion Test Data

	//-----------------------------------------------------------------------------------------------------------------
	#region Meta Tests

	[Fact]
	public void ArraySplitValidData_HasData()
		=> Assert.NotEmpty(ArraySplitValidData);

	[Fact]
	public void ArraySplitInvalidData_HasData()
		=> Assert.NotEmpty(ArraySplitInvalidData);

	#endregion Meta Tests

	//-----------------------------------------------------------------------------------------------------------------
	#region Test Methods: Split (Array)

	[Theory]
	[MemberData(nameof(ArraySplitValidData))]
	public void Split_Array_WithValidData_ReturnsCorrectPartitions(int[] source, int[] expectedTrue, int[] expectedFalse)
	{
		//--- ARRANGE ---------------------------------------------------------

		//--- ACT -------------------------------------------------------------
		(int[]? trueItems, int[]? falseItems) = source.Split(x => x % 2 == 0);

		//--- ASSERT ----------------------------------------------------------
		Assert.Equal(expectedTrue,	trueItems.Order());
		Assert.Equal(expectedFalse,	falseItems.Order());
	}


	[Theory]
	[MemberData(nameof(ArraySplitInvalidData))]
	public void Split_Array_WithNullArguments_ThrowsException(int[] source, Func<int, bool> predicate, string expectedParamName)
	{
		//--- ARRANGE ---------------------------------------------------------

		//--- ACT -------------------------------------------------------------
		ArgumentNullException ex = Assert.ThrowsAny<ArgumentNullException>(
			() => source.Split(predicate));

		//--- ASSERT ----------------------------------------------------------
		Assert.Equal(expectedParamName, ex.ParamName);
	}

	[Fact]
	public void Split_Array_WithSortInPlace_ModifiesSourceArray()
	{
		//--- ARRANGE ---------------------------------------------------------
		int[] source = [1, 2, 3, 4, 5];

		//--- ACT -------------------------------------------------------------
		source.Split(x => x % 2 == 0, sortInPlace: true);

		//--- ASSERT ----------------------------------------------------------
		Assert.NotEqual([1, 2, 3, 4, 5], source);
	}

	#endregion Test Methods: Split (Array)

	//-----------------------------------------------------------------------------------------------------------------
	#region Test Methods: Split (IEnumerable)

	[Theory]
	[MemberData(nameof(ArraySplitValidData))]
	public void Split_IEnumerable_WithValidData_ReturnsCorrectPartitions(IEnumerable<int> source, int[] expectedTrue, int[] expectedFalse)
	{
		//--- ARRANGE ---------------------------------------------------------

		//--- ACT -------------------------------------------------------------
		var (trueItems, falseItems) = source.Split(x => x % 2 == 0);

		//--- ASSERT ----------------------------------------------------------
		Assert.Equal(expectedTrue,	trueItems.Order());
		Assert.Equal(expectedFalse,	falseItems.Order());
	}

	[Theory]
	[MemberData(nameof(ArraySplitInvalidData))]
	public void Split_IEnumerable_WithNullArguments_ThrowsArgumentNullException(IEnumerable<int> source, Func<int, bool> predicate, string expectedParamName)
	{
		//--- ACT -------------------------------------------------------------
		ArgumentNullException ex =  Assert.Throws<ArgumentNullException>(
			() => source.Split(predicate));

		//--- ASSERT ----------------------------------------------------------
		Assert.Equal(expectedParamName, ex.ParamName);
	}

	#endregion Test Methods: Split (IEnumerable)
}
