#!/bin/sh

coverage="./coverage"

# Execute the command
# OpenCover is meant to be executed on Windows... Fail.
echo "Calculating coverage with OpenCover"
mono $(pwd)/packages/OpenCover.4.6.519/tools/OpenCover.Console.exe \
  -target:"$(pwd)/testrunner/NUnit.ConsoleRunner.3.8.0/tools/nunit3-console.exe" \
  -targetargs:"/nologo /noshadow $(pwd)/Library/ScriptAssemblies/Assembly-CSharp.dll" \
  -output:$coverage/coverage.xml \
  -filter:"+[*]* -[Assembly-CSharp*]*" \
  -register:user
echo "Done!"

echo "Unit test logs"
echo ""
cat $coverage/coverage.xml
echo ""

echo "v2 !"
mono $(pwd)/packages/OpenCover.4.6.519/tools/OpenCover.Console.exe \
  -target:"$(pwd)/testrunner/NUnit.ConsoleRunner.3.8.0/tools/nunit3-console.exe" \
  -targetargs:"/nologo /noshadow $(pwd)/Library/ScriptAssemblies/Assembly-CSharp-Editor.dll" \
  -output:$coverage/coverage2.xml \
  -filter:"+[*]* -[Assembly-CSharp-Editor*]*" \
  -register:user
echo "Done2!"

echo "Unit test logs2"
echo ""
cat $coverage/coverage2.xml
echo ""

if [ -n "$COVERALLS_REPO_TOKEN" ]
then
  mono ./coveralls/coveralls.net.0.7.0/tools/csmacnz.Coveralls.exe --opencover -i coverage/coverage.xml --useRelativePaths
fi


echo "Calculating coverage with dotCover"
mono $(pwd)/packages/JetBrains.dotCover.CommandLineTools.2018.1.1/tools/dotCover.exe analyse TargetExecutable:"$(pwd)/testrunner/NUnit.ConsoleRunner.3.8.0/tools/nunit3-console.exe" TargetArguments:"$(pwd)/Assets/Editor" Output:report.xml
echo "Done!"

echo "dotCover report"
echo ""
cat ./report.xml
echo ""
