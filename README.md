# AutoCAD Test Runner

An AutoCAD test runner using the NUnitLite framework.

## Running

Update the TestLoader.scr file with your project output directory.

```
netload "C:\Users\scott\RiderProjects\AutoCAD_UnitTest\TestRunnerACAD\bin\Debug\TestRunnerACAD.dll"
RunTests
```

Make sure to add your project output directory to AutoCAD's trusted folders.

Set your run configuration as follows...

![image](https://user-images.githubusercontent.com/79826944/144696304-57bf5f8e-4461-47e7-8d63-6b443cc81363.png)

Tested on AutoCAD 2017.
