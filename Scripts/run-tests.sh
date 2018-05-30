#! /bin/sh

# This file is based on an example by SebastianJay. See the original example here:
#	https://github.com/SebastianJay/unity-ci-test/

project="Brain-Control"

## Run the editor unit tests
echo "Running editor unit tests for $project"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
	-batchmode \
	-nographics \
	-silent-crashes \
	-projectPath $(pwd) \
	-editorTestsResultFile $(pwd)/unit-test-results.xml \
	-testFilter $(pwd)/Assets/Library/ScriptAssemblies/Assembly-CSharp.dll \
	-runEditorTests

results=$?

echo "Unit test logs"
echo ""
cat $(pwd)/unit-test-results.xml
echo ""

# Exit if tests failed
if [ $results -ne 0 ]
then
  echo "Failed unit tests. Exited with $results"
else
  echo "All tests passed. Exited with $results"
fi

#Test for coveralls
if [ -n "$COVERALLS_REPO_TOKEN" ]
then
	echo "Coveralls"
  mono ./coveralls/coveralls.net.0.7.0/tools/csmacnz.Coveralls.exe --opencover -i $(pwd)/unit-test-results.xml --useRelativePaths
	echo "Done"
fi

# Test for codecov
curl -s https://codecov.io/bash > codecov
chmod +x codecov
# NUnit v3.0
./codecov -f $(pwd)/unit-test-results.xml -t 3c5ce3f9-ddde-4db1-a62e-f0d35e9112ec

set +e
