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
	-runEditorTests \
	-editorTestsResultFile $(pwd)/unit-test-results.xml \

results=$?

ls -l

echo "Unit test logs"
cat $(pwd)/unit-test-results.xml

# Exit if tests failed
if [ $results -ne 0 ]
then
  echo "Failed unit tests. Exited with $results"
else
  echo "All tests passed. Exited with $results"
fi

# Convert the file in the NUnit v2.0
csc /target:exe /out:convert.exe ./Scripts/convert.cs
mono convert.exe "unit-test-results.xml"

# Test for codecov
curl -s https://codecov.io/bash > codecov
chmod +x codecov
# NUnit v2.0
./codecov -f $(pwd)/TestResultV2.xml -t 3c5ce3f9-ddde-4db1-a62e-f0d35e9112ec
# NUnit v3.0
./codecov -f $(pwd)/unit-test-results.xml -t 3c5ce3f9-ddde-4db1-a62e-f0d35e9112ec

set +e
