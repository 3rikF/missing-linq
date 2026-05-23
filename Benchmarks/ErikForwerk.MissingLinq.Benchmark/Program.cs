
using BenchmarkDotNet.Running;

using SplitBenchmark;

//-----------------------------------------------------------------------------------------------------------------------------------------
Console.WriteLine( "Select benchmark to run:");
Console.WriteLine($"  1  -  Split Enumerations ({nameof(SplitExtensionBenchmark)})");
Console.WriteLine($"  2  -  Join Strings ({nameof(JoinStringsBenchmark)})");
Console.WriteLine( "  0  -  All");
Console.Write("Your choice: ");

switch (Console.ReadLine()?.Trim())
{
	case "1":
		_ = BenchmarkRunner.Run<SplitExtensionBenchmark>();
		break;

	case "2":
		_ = BenchmarkRunner.Run<JoinStringsBenchmark>();
		break;

	default:
		_ = BenchmarkRunner.Run<SplitExtensionBenchmark>();
		_ = BenchmarkRunner.Run<JoinStringsBenchmark>();
		break;
}
