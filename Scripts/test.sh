#!/bin/sh

OPENCOVER=$(pwd)/coverage/OpenCover.4.6.519/tools/OpenCover.Console.exe

coverage="./coverage"
rm -rf $coverage
mkdir $coverage

# Execute the command
echo "Calculating coverage with OpenCover"
$OPENCOVER \
  -target:"$(pwd)/packages/NUnit.Runners.2.6.4/tools/nunit-console.exe" \
  -targetargs:"./Library/ScriptAssemblies/Assembly-CSharp.dll" \
  -mergeoutput \
  -hideskipped:File \
  -output:$coverage/coverage.xml \
  -oldStyle \
  -filer:"+[Brain-Control*]*" \
  -searchdirs:"./" \
  -register:user

# Test with NUnit 2
