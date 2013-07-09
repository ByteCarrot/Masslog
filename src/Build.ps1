Set-Alias -Name MSBuild -Value C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe
Set-Alias -Name AspNet_Compiler -Value C:\Windows\Microsoft.NET\Framework\v4.0.30319\aspnet_compiler.exe

Properties {
	$BaseDir  = resolve-path .
	$ToolsDir = "$BaseDir\..\tools"
	$WebDir = "$BaseDir\Web"
	$CoreDir = "$BaseDir\Core"
	$ReleaseDir = "$BaseDir\Release"
	$SolutionItemsDir = "$BaseDir\Solution Items"
}

Task default -Depends Release

Task Clean {
	Exec { MSBuild Masslog.sln /t:Clean /p:Configuration=Debug }
}

Task Compile -Depends Clean {
	Exec { MSBuild Masslog.sln /t:Rebuild /p:Configuration=Debug }
}

Task Release -Depends Compile {
	Set-Alias -Name Zip -Value "$ToolsDir\7zip\7za.exe"

	if (Test-Path $ReleaseDir) 
	{
		Remove-Item $ReleaseDir -recurse
	}
	
	New-Item $ReleaseDir -type directory
		
	New-Item $ReleaseDir\Library -type directory
	Copy-Item $CoreDir\Release\ByteCarrot.Masslog.Core.v4.dll $ReleaseDir\Library
	Copy-Item $CoreDir\Release\ByteCarrot.Masslog.Core.v45.dll $ReleaseDir\Library

	Exec { MsBuild $WebDir\Web.csproj "/p:DeployOnBuild=true;PublishProfile=Release" }
	Exec { AspNet_Compiler -v / -p $ReleaseDir\Temp $ReleaseDir\Web  }
	Remove-Item $ReleaseDir\Temp -recurse
}