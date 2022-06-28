// See https://aka.ms/new-console-template for more information

using LogAn;

// fake dipendency injection
var analyzer = new LogAnalyzer(new FileExtensionManager());

Console.WriteLine($"Is 'file.slf' valid?: {analyzer.IsValidLogFileName("file.slf")}");
Console.WriteLine($"Is 'file.SLF' valid?: {analyzer.IsValidLogFileName("file.SLF")}");
Console.WriteLine($"Is 'file.foo' valid?: {analyzer.IsValidLogFileName("file.foo")}");
Console.WriteLine($"Is 'file.log' valid?: {analyzer.IsValidLogFileName("file.log")}");
Console.WriteLine($"Is 'file.LOG' valid?: {analyzer.IsValidLogFileName("file.LOG")}");