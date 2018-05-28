#! /bin/sh

# This file is based on an example. See the original example here:
#	https://automationrhapsody.com/convert-nunit-3-nunit-2-results-xml-file/

$assemblyNunitEngine = 'nunit.engine.api.dll';
$assemblyNunitWriter = 'nunit-v2-result-writer.dll';
$inputV3Xml = 'unit-test-results.xml';
$outputV2Xml = 'TestResultV2.xml';

Add-Type -Path $assemblyNunitEngine;
Add-Type -Path $assemblyNunitWriter;

$xmldoc = New-Object -TypeName System.Xml.XmlDataDocument;
$fs = New-Object -TypeName System.IO.FileStream -ArgumentList $inputV3Xml,'Open','Read';
$xmldoc.Load($fs);
$xmlnode = $xmldoc.GetElementsByTagName('test-run').Item(0);
$writer = New-Object -TypeName NUnit.Engine.Addins.NUnit2XmlResultWriter;
$writer.WriteResultFile($xmlnode, $outputV2Xml);

# Test for codecov
curl -s https://codecov.io/bash > codecov
chmod +x codecov
./codecov -f $(pwd)/TestResultV2.xml -t 3c5ce3f9-ddde-4db1-a62e-f0d35e9112ec

set +e
