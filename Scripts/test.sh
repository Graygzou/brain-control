#!/bin/sh

coverage="./coverage"

# Execute the command
echo "Calculating coverage with OpenCover"
mono $(pwd)/coverage/OpenCover.4.6.519/tools/OpenCover.Console.exe \
  -target:"$(pwd)/packages/NUnit.Runners.2.6.4/tools/nunit-console.exe" \
  -targetargs:"./Library/ScriptAssemblies/Assembly-CSharp-Editor.dll" \
  -mergeoutput \
  -hideskipped:File \
  -output:$coverage/coverage.xml \
  -oldStyle \
  -filter:"+[*]* -[NewEditMode*]*" \
  -searchdirs:"./" \
  -register:user
echo "Done!"

echo "Unit test logs"
echo ""
cat $coverage/coverage.xml
echo ""

echo "v2 !"
mono $(pwd)/coverage/OpenCover.4.6.519/tools/OpenCover.Console.exe \
  -target:"$(pwd)/packages/NUnit.Runners.2.6.4/tools/nunit-console.exe" \
  -targetargs:"./Library/ScriptAssemblies/Assembly-CSharp-Editor.dll" \
  -mergeoutput \
  -hideskipped:File \
  -output:$coverage/coverage2.xml \
  -oldStyle \
  -filter:"+[*]* -[NewEditMode*]*" \
  -searchdirs:"./" \
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
