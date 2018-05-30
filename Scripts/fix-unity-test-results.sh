#!/bin/sh
#
# Usage: ./fix-unity-test-results.sh
#
# Unity uses NUnit under the hood for its test framework. Their command line tool outputs a NUnit
# test result XML file. See documentation:
#   * https://github.com/nunit/docs/wiki/Test-Result-XML-Format#test-suite
#
# The format of this file is currently incorrect. The root element for NUnit XML test results files
# should be a '<test-run>' tag, which is missing from Unity's test results file. See issue on forum:
#   * https://forum.unity3d.com/threads/unity-test-tools.218287/page-6#post-3188590
#
# This script uses the XMLStarlet command line tool and 'script/build/fix-unity-test-results.xslt'
# to convert the incorrect test results XML file into the correct format. This allows the Jenkins
# NUnit plugin to convert the NUnit test results file into a JUnit test results file so Jenkins can
# understand and record test results.
#
set -euo pipefail

xml tr $(pwd)/Scripts/fix-unity-test-results.xslt $(pwd)/unit-test-results.xml > $(pwd)/nunit-test-results.xml
