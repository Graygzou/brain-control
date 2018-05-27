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
	-logFile $(pwd)/Tests/unity.log \
	-projectPath $(pwd) \
  -editorTestsResultFile "$(pwd)/Tests/$project-test-results.xml" \
	-runEditorTests \
	-quit

results=$?

echo "Unit test logs"
cat $(pwd)/test-results.xml

# Exit if tests failed
if [ $results -ne 0 ]
then
  echo "Failed unit tests. Exited with $results"
else
  echo "All tests passed. Exited with $results"
fi

set +e
