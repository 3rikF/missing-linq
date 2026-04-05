
using BenchmarkDotNet.Attributes;
using ErikForwerk.MissingLinq;

//-----------------------------------------------------------------------------------------------------------------------------------------
namespace SplitBenchmark;

//-----------------------------------------------------------------------------------------------------------------------------------------
[MemoryDiagnoser]
public class MissingLinqExtensionBenchmark
{
	private readonly Random _random				= new (42);
	private int[] _arrayData					= [];
	private List<int> _listData					= [];
	private IEnumerable<int> _enumerableData	= [];

	[Params(10, 1000, 10_000)]
	public int Count { get; set; }

	[GlobalSetup]
	public void Setup()
	{
		_listData	= [.. Enumerable.Range(1, Count).Select(_ => _random.Next(100000))];
		_arrayData	= [.. _listData];
		_enumerableData = _listData;
	}

	[Benchmark(Baseline = true)]
	public (int[] TrueItems, int[] FalseItems) Split_Array_InPlace()
		=> _arrayData.Split(x => x % 2 == 0, sortInPlace: true);

	[Benchmark]
	public (int[] TrueItems, int[] FalseItems) Split_Array_Copy()
		=> _arrayData.Split(x => x % 2 == 0, sortInPlace: false);

	[Benchmark]
	public (IEnumerable<int> TrueItems, IEnumerable<int> FalseItems) Split_Enumerable_OnList()
		=> _listData.Split(x => x % 2 == 0);
	
	[Benchmark]
	public (IEnumerable<int> TrueItems, IEnumerable<int> FalseItems) Split_Enumerable_OnEnumerable()
		=> _enumerableData.Split(x => x % 2 == 0);
}
