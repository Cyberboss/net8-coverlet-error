# Coverlet Bug Demo

## No Coverage

For guaranteed clean execuion, execute with:

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

## Only Workaround

In [Common.props](./Common.props) set `ShowRepro` to `false`. The only thing this does is inline the usage of a `const enum` but somehow it fixes both of the above cases.

This is reproducible on net 6, 7, and 8 preview 6.
