MSBuild.exe move-all.sln /p:Configuration=Debug /p:Platform="Any CPU"
if errorlevel 1 goto :eof
rd /q /s test-from
md test-from
echo a >test-from\file1.txt
echo a >test-from\file2.txt
md test-from\folder1
md test-from\folder2
rd /q /s test-to
md test-to
cd test-from
..\bin\Debug\move-all ..\test-to
cd ..
dir test-from
dir test-to
