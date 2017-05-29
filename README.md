PCO.Net - A .Net wrapper for Planning Center (http://planning.center)

The Planning Center API is detailed here: http://planningcenter.github.io/api-docs

To use:

1) Get a personal access token from https://api.planningcenteronline.com/oauth/applications
2) Set the appropriate token and secret values in HttpTransport.cs


Testing

The unittests project runs fine in Visual Studio for Mac. To run the tests in Visual Studio on Windows, add the NUnit 2.x Test adaptor from Tools->Extensions and Updates. 


To test from Powershell:

Add-Type -Path ".\PCO.Net\bin\Debug\PCO.Net.dll"

[PCO.Net.ClassFactory]::Instance = New-Object PCO.Net.ClassFactoryImpl

$pso = New-Object PCO.Net.Services

$pso.GetPlansAsync("Central Gathering", 10).GetAwaiter().GetResult()
