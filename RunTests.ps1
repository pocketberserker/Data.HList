.\.nuget\NuGet.exe Install Persimmon.Console -Pre -OutputDirectory packages -ExcludeVersion

$inputs = @(
  ".\FSharp.Data.HList.Tests\bin\Debug\FSharp.Data.HList.Tests.dll"
)

.\packages\Persimmon.Console\tools\Persimmon.Console.exe --parallel $inputs
