**** Force Power Shell to use .NET 4.0 ****
(required by Build.ps1 script to get current version of assemblies)

reg add hklm\software\microsoft\.netframework /v OnlyUseLatestCLR /t REG_DWORD /d 1
reg add hklm\software\wow6432node\microsoft\.netframework /v OnlyUseLatestCLR /t REG_DWORD /d 1

**** Disable ****
reg add hklm\software\microsoft\.netframework /v OnlyUseLatestCLR /t REG_DWORD /d 0
reg add hklm\software\wow6432node\microsoft\.netframework /v OnlyUseLatestCLR /t REG_DWORD /d 0