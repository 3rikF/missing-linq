
//-----------------------------------------------------------------------------------------------------------------------------------------
using ErikForwerk.TestAbstractions.Models;

using Xunit.Abstractions;

namespace ErikForwerk.MissingLinq.Tests;

//-----------------------------------------------------------------------------------------------------------------------------------------
public sealed class SplitTests(ITestOutputHelper toh) : TestBase(toh)
{
	//-----------------------------------------------------------------------------------------------------------------
	#region Test Data

	private static bool IsEven(int value)
		=> value % 2 == 0;

	public static TheoryData<int[], int[], int[]> ArraySplitValidData => new()
	{
		{ [1, 2, 3, 4, 5],	[2, 4],		[1, 3, 5]	},
		{ [2, 4, 6],		[2, 4, 6],	[]			},
		{ [],				[],			[]			},
	};

	public static TheoryData<int[], Func<int, bool>, string> ArraySplitInvalidData => new()
	{
		// source,		predicate,				expected Exception-param name
		{ null!,		FailTest<int, bool>,	"source"},
		{ [1, 2, 3],	null!,					"predicate"},
	};

	#endregion Test Data

	//-----------------------------------------------------------------------------------------------------------------
	#region Meta Tests

	[Theory]
	[InlineData(0, true)]
	[InlineData(1, false)]
	public void IsEven_ReturnsExpectedResult(int value, bool expected)
		=> Assert.Equal(expected, IsEven(value));

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
		TestConsole.WriteLine($"Testing with source {B(source?.GetType())}");

		//--- ACT -------------------------------------------------------------
		(int[]? trueItems, int[]? falseItems) = source.Split(IsEven);

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
		_ = source.Split(IsEven, sortInPlace: true);

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
		TestConsole.WriteLine($"Testing with source {B(source?.GetType())}");

		//--- ACT -------------------------------------------------------------
		var (trueItems, falseItems) = source.Split(IsEven);

		TestConsole.WriteLine($"Expected True       {B(expectedTrue)}");
		TestConsole.WriteLine($"Actual True         {B(trueItems)}");

		TestConsole.WriteLine($"Expected False      {B(expectedFalse)}");
		TestConsole.WriteLine($"Actual False        {B(falseItems)}");

		//--- ASSERT ----------------------------------------------------------
		Assert.Equal(expectedTrue,	trueItems.Order());
		Assert.Equal(expectedFalse,	falseItems.Order());
	}

	[Theory]
	[MemberData(nameof(ArraySplitInvalidData))]
	public void Split_IEnumerable_WithNullArguments_ThrowsArgumentNullException(IEnumerable<int> source, Func<int, bool> predicate, string expectedParamName)
	{
		//--- ARRANGE ---------------------------------------------------------
		TestConsole.WriteLine($"Testing with source {B(source?.GetType())}");

		//--- ACT -------------------------------------------------------------
		ArgumentNullException ex =  Assert.Throws<ArgumentNullException>(
			() => source.Split(predicate));

		//--- ASSERT ----------------------------------------------------------
		Assert.Equal(expectedParamName, ex.ParamName);
	}

	#endregion Test Methods: Split (IEnumerable)
}
