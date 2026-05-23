
using BenchmarkDotNet.Attributes;
using ErikForwerk.MissingLinq;

//-----------------------------------------------------------------------------------------------------------------------------------------
namespace SplitBenchmark;

//-----------------------------------------------------------------------------------------------------------------------------------------
[MemoryDiagnoser]
public class JoinStringsBenchmark
{
	private static readonly string SEPERATOR_STRING	= ", ";
	private static readonly char SEPERATOR_CHAR		= ';';

	private IEnumerable<string> _data = [];

	[Params(1, 10, 1000)]
	public int Count { get; set; }

	[GlobalSetup]
	public void Setup()
		=> _data = [.. Enumerable.Range(1, Count).Select(i => $"item{i}")];

	[Benchmark(Baseline = true)]
	public string JoinB_StringJoin_StringSeparator()
		=> _data.Join(SEPERATOR_STRING);

	[Benchmark]
	public string JoinB_StringJoin_CharSeparator()
		=> _data.Join(SEPERATOR_CHAR);
}
