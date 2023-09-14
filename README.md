Forked from puppetsw/AutoCAD_UnitTest

# Updated Sep 14, 2023 by leaveblackgithub
1. Add Supports to AutoCAD 2019
2. The extentreports-dotnet-cli deprecates ReportUnit. Use extent-framework / extentreports-dotnet-cli to replace ReportUnit. https://github.com/extent-framework/extentreports-dotnet-cli
3. Add Trustedpaths in scr file.
4. Move example tests to separate project to isolate changes.

# AutoCAD Test Runner

An AutoCAD test runner using the NUnitLite framework. 

Inspired by the work done by CADBloke. https://github.com/CADbloke/CADtest

## Running

Update the TestLoader.scr file with your project output directory.

```
netload "C:\Users\scott\RiderProjects\AutoCAD_UnitTest\TestRunnerACAD\bin\Debug\TestRunnerACAD.dll"
RunTests
```

Make sure to add your project output directory to AutoCAD's trusted folders. Also add references to acdbmgd.dll etc.

Set your run configuration as follows...

![image](https://user-images.githubusercontent.com/79826944/144696304-57bf5f8e-4461-47e7-8d63-6b443cc81363.png)

Tested on AutoCAD 2017.
