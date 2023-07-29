# Coverlet Bug Demo

This is reproducible on net 6, 7, and 8 preview 6.

A more "in the wild" repro can be found in https://github.com/tgstation/tgstation-server/tree/fc007543dbfa888cd274e5a54a512e830641fd1b by running the `tests/Tgstation.Server.Host.Tests` project.

## No Coverage

For guaranteed clean execution, execute with:

Powershell:
```ps
Remove-Item -Recurse -Force Library/bin, Library/obj, Test/bin, Test/obj;dotnet restore;dotnet test -c Debug --collect:"XPlat Code Coverage" --settings test.runsettings --results-directory TestResults
```
or Bash:
```sh
rm -rf Library/bin, Library/obj, Test/bin, Test/obj && dotnet restore; dotnet test -c Debug --collect:"XPlat Code Coverage" --settings test.runsettings --results-directory TestResults
```

No coverage output will be generated in the cobertura.xml

## Broken Coverage

In [Common.props](./Common.props) change `PdbType` to `PdbOnly` or `Full`. Running the above command again will yield an error as warning repeated many times:
```
 ---> (Inner Exception #11) System.IO.IOException: The process cannot access the file 'S:\workspace\net8-coverlet-error\Test\bin\Debug\net8.0\Library.pdb' because it is being used by another process.
   at System.IO.FileSystem.CopyFile(String sourceFullPath, String destFullPath, Boolean overwrite)
   at Coverlet.Core.Helpers.FileSystem.Copy(String sourceFileName, String destFileName, Boolean overwrite) in /_/src/coverlet.core/Helpers/FileSystem.cs:line 35
   at Coverlet.Core.Helpers.InstrumentationHelper.<>c__DisplayClass16_0.<RestoreOriginalModule>b__1() in /_/src/coverlet.core/Helpers/InstrumentationHelper.cs:line 277
   at Coverlet.Core.Helpers.RetryHelper.<>c__DisplayClass0_0.<Retry>b__0() in /_/src/coverlet.core/Helpers/RetryHelper.cs:line 28
   at Coverlet.Core.Helpers.RetryHelper.Do[T](Func`1 action, Func`1 backoffStrategy, Int32 maxAttemptCount) in /_/src/coverlet.core/Helpers/RetryHelper.cs:line 55<---

   --- End of inner exception stack trace ---
   at Coverlet.Collector.DataCollection.CoverageManager.InstrumentModules() in /_/src/coverlet.collector/DataCollection/CoverageManager.cs:line 72
   at Coverlet.Collector.DataCollection.CoverletCoverageCollector.OnSessionStart(Object sender, SessionStartEventArgs sessionStartEventArgs) in /_/src/coverlet.collector/DataCollection/CoverletCoverageCollector.cs:line 143.
```

## Workaround 1

In [Common.props](./Common.props) set `ShowRepro` to `false`. The only thing this does is inline the usage of the `const Microsoft.Extensions.Logging.LogLevel` `enum`.

## Workaround 2

In [Common.props](./Common.props) set `ShowRepro` to `true` and `AddExplicitProjectReference` to `true`. The only thing this does is explicitly add the `Microsoft.Extensions.Logging.Abstractions` package which is included by default in the AspNetCore SDK.
