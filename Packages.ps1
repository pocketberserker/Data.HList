C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe Data.HList.sln /property:Configuration=Release /property:VisualStudioVersion=12.0 /target:rebuild

.\.nuget\nuget.exe pack .\CSharp.Data.HList\CSharp.Data.HList.csproj -Symbols -Properties VisualStudioVersion=12.0
.\.nuget\nuget.exe pack .\FSharp.Data.HList\FSharp.Data.HList.fsproj -Symbols -Properties VisualStudioVersion=12.0

if(Test-Path "nuget-packages")
{
  rm nuget-packages -Recurse -Force
}
mkdir nuget-packages

mv .\*.nupkg nuget-packages

ls .\nuget-packages\*.nupkg | ?{
  -not $_.Name.Contains('.symbols.')
} | %{
  echo "..\.nuget\nuget.exe push $_" >> .\nuget-packages\Push-All.ps1
}
