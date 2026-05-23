//-----------------------------------------------------------------------------------------------------------------------------------------
namespace ErikForwerk.MissingLinq.Tests;

//-----------------------------------------------------------------------------------------------------------------------------------------
public sealed class JoinStringsTests
{
	//-----------------------------------------------------------------------------------------------------------------
	#region Test Data

	public static TheoryData<string[], string, string> JoinWithStringSeparatorValidData => new()
	{
		{ ["a", "b", "c"],	", ",	"a, b, c"	},
		{ ["a", "b", "c"],	"",		"abc"		},
		{ ["a", "b", "c"],	"-",	"a-b-c"		},
		{ ["only"],			", ",	"only"		},
		{ [],				", ",	""			},
	};

	public static TheoryData<string[], char, string> JoinWithCharSeparatorValidData => new()
	{
		{ ["a", "b", "c"],	',',	"a,b,c"	},
		{ ["a", "b", "c"],	'-',	"a-b-c"	},
		{ ["only"],			',',	"only"	},
		{ [],				',',	""		},
	};

	public static TheoryData<IEnumerable<string>?, string?, string?> JoinWithStringSeparatorInvalidData => new()
	{
		{ null!,		", ",	"source"		},
		{ ["a", "b"],	null!,	"separator"	},
	};

	#endregion Test Data

	//-----------------------------------------------------------------------------------------------------------------
	#region Meta Tests

	[Fact]
	public void JoinWithStringSeparatorValidData_HasData()
		=> Assert.NotEmpty(JoinWithStringSeparatorValidData);

	[Fact]
	public void JoinWithCharSeparatorValidData_HasData()
		=> Assert.NotEmpty(JoinWithCharSeparatorValidData);

	[Fact]
	public void JoinWithStringSeparatorInvalidData_HasData()
		=> Assert.NotEmpty(JoinWithStringSeparatorInvalidData);

	#endregion Meta Tests

	//-----------------------------------------------------------------------------------------------------------------
	#region Test Methods: Join (string separator)

	[Theory]
	[MemberData(nameof(JoinWithStringSeparatorValidData))]
	public void Join_WithStringSeparator_ReturnsExpectedResult(IEnumerable<string> source, string separator, string expected)
	{
		//--- ARRANGE ---------------------------------------------------------

		//--- ACT -------------------------------------------------------------
		string result = source.Join(separator);

		//--- ASSERT ----------------------------------------------------------
		Assert.Equal(expected, result);
	}

	[Theory]
	[MemberData(nameof(JoinWithStringSeparatorInvalidData))]
	public void Join_WithStringSeparator_WithNullArguments_ThrowsArgumentNullException(IEnumerable<string> source, string separator, string expectedParamName)
	{
		//--- ARRANGE ---------------------------------------------------------

		//--- ACT -------------------------------------------------------------
		ArgumentNullException ex = Assert.Throws<ArgumentNullException>(
			() => source.Join(separator));

		//--- ASSERT ----------------------------------------------------------
		Assert.Equal(expectedParamName, ex.ParamName);
	}

	#endregion Test Methods: Join (string separator)

	//-----------------------------------------------------------------------------------------------------------------
	#region Test Methods: Join (char separator)

	[Theory]
	[MemberData(nameof(JoinWithCharSeparatorValidData))]
	public void Join_WithCharSeparator_ReturnsExpectedResult(IEnumerable<string> source, char separator, string expected)
	{
		//--- ARRANGE ---------------------------------------------------------

		//--- ACT -------------------------------------------------------------
		string result = source.Join(separator);

		//--- ASSERT ----------------------------------------------------------
		Assert.Equal(expected, result);
	}

	[Fact]
	public void Join_WithCharSeparator_WithNullSource_ThrowsArgumentNullException()
	{
		//--- ARRANGE ---------------------------------------------------------
		const char				SEPARATOR		= ',';
		const string			EXPECTED_PARAM_NAME	= "source";
		IEnumerable<string>?	source			= null;

		//--- ACT -------------------------------------------------------------
		ArgumentNullException ex = Assert.Throws<ArgumentNullException>(
			() => source!.Join(SEPARATOR));

		//--- ASSERT ----------------------------------------------------------
		Assert.Equal(EXPECTED_PARAM_NAME, ex.ParamName);
	}

	#endregion Test Methods: Join (char separator)
}
