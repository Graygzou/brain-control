#! /bin/sh

# This file is based on an example by SebastianJay. See the original example here:
#	https://github.com/SebastianJay/unity-ci-test/
set -euo pipefail

project="Brain-Control"

# Run the editor unit tests.
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

# Print results of
if [ $results -ne 0 ]
then
	echo "------------------------"
  echo "Failed unit tests."
	echo "------------------------"
else
	echo "------------------------"
  echo "All tests passed!"
	echo "------------------------"
fi

# Print the Unity results.
echo "Unity test logs"
echo ""
cat $(pwd)/unit-test-results.xml
echo ""

# Convert the Unity test in NUnit Test with xmlskarlet.
echo "Converting file in NUnit xml format..."
xml tr $(pwd)/Scripts/fix-unity-test-results.xslt $(pwd)/unit-test-results.xml > $(pwd)/nunit-test-results.xml

# Print NUnit results.
echo "NUnit test logs"
echo ""
cat $(pwd)/nunit-test-results.xml
echo ""

echo "--------------------------------------------------"
echo "Sending data coverage to third party software"
echo "--------------------------------------------------"

#Test for coveralls
if [ -n "$COVERALLS_REPO_TOKEN" ]
then
	echo ""
	echo "Sending data to Coveralls..."
  mono ./coveralls/coveralls.net.0.7.0/tools/csmacnz.Coveralls.exe --opencover -i $(pwd)/nunit-test-results.xml --useRelativePaths
fi

# Test for codecov
echo "Sending data to Codecov..."
curl -s https://codecov.io/bash > codecov
chmod +x codecov
# NUnit v3.0
./codecov -f $(pwd)/nunit-test-results.xml -t 3c5ce3f9-ddde-4db1-a62e-f0d35e9112ec

set +e
