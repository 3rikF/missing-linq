
//-----------------------------------------------------------------------------------------------------------------------------------------
using System.Collections.ObjectModel;

namespace ErikForwerk.MissingLinq.Tests;

//-----------------------------------------------------------------------------------------------------------------------------------------
public sealed class IsNullOrEmptyTests
{
	//-----------------------------------------------------------------------------------------------------------------
	#region TestData

	public static TheoryData<object[]?, bool> ArrayIsNullOrEmptyData => new()
	{
		{ null,			true },
		{ [],			true },
		{ [1],			false },
		{ [1, 2, 3],	false },
	};

	public static TheoryData<List<object>?, bool> ListIsNullOrEmptyData => new()
	{
		{ null,			true },
		{ [],			true },
		{ [1],			false },
		{ [1, 2, 3],	false },
	};

	public static TheoryData<IEnumerable<object>?, bool> EnumerableIsNullOrEmptyData => new()
	{
		{ null,			true },
		{ [],			true },
		{ [1],			false },
		{ [1, 2, 3],	false },
	};

	#endregion TestData

	//-----------------------------------------------------------------------------------------------------------------
	#region Meta Tests

	[Fact]
	public void ArrayIsNullOrEmptyData_HasData()
		=> Assert.NotEmpty(ArrayIsNullOrEmptyData);

	[Fact]
	public void ListIsNullOrEmptyData_HasData()
		=> Assert.NotEmpty(ListIsNullOrEmptyData);

	[Fact]
	public void EnumerableIsNullOrEmptyData_HasData()
		=> Assert.NotEmpty(EnumerableIsNullOrEmptyData);

	#endregion Meta Tests

	//-----------------------------------------------------------------------------------------------------------------
	#region IsNullOrEmpty

	[Theory]
	[MemberData(nameof(ArrayIsNullOrEmptyData))]
	public void IsNullOrEmpty_WithArray_ReturnsExpectedResult(object[]? testData, bool expectedResult)
	{
		//--- Act -------------------------------------------------------------
		bool result = testData.IsNullOrEmpty();

		//--- Assert ----------------------------------------------------------
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[MemberData(nameof(ListIsNullOrEmptyData))]
	public void IsNullOrEmpty_WithList_ReturnsExpectedResult(List<object>? testData, bool expectedResult)
	{
		//--- Act -------------------------------------------------------------
		bool result = testData.IsNullOrEmpty();

		//--- Assert ----------------------------------------------------------
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[MemberData(nameof(EnumerableIsNullOrEmptyData))]
	public void IsNullOrEmpty_WithCollection_ReturnsExpectedResult(IEnumerable<object>? testData, bool expectedResult)
	{
		//--- Act -------------------------------------------------------------
		bool result = testData.IsNullOrEmpty();

		//--- Assert ----------------------------------------------------------
		Assert.Equal(expectedResult, result);
	}

	#endregion IsNullOrEmpty
}
