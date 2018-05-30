#!/bin/sh

coverage="./coverage"
rm -rf $coverage
mkdir $coverage

# Execute the command
echo "Calculating coverage with OpenCover"
/coverage/OpenCover.4.6.519/tools/OpenCover.Console.exe \
  -target:"$(pwd)/packages/NUnit.Runners.2.6.4/tools/nunit-console.exe" \
  -targetargs:"./Library/ScriptAssemblies/Assembly-CSharp-Editor-Editor.dll" \
  -mergeoutput \
  -hideskipped:File \
  -output:$coverage/coverage.xml \
  -oldStyle \
  -filer:"+[Brain-Control*]*" \
  -searchdirs:"./" \
  -register:user

if [ -n "$COVERALLS_REPO_TOKEN" ]
then
  mono ./coveralls/coveralls.net.0.7.0/tools/csmacnz.Coveralls.exe --opencover -i coverage/coverage.xml --useRelativePaths
fi
