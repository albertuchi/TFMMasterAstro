 $files = Get-ChildItem "C:\msys64\home\alber\Input\"
 foreach ($f in $files){
    Copy-Item .\home\alber\Input\$f .
    Rename-Item $f initialtab.agb_new
    Start-Process .\mingw64.exe .\agb.exe -Wait
    Remove-Item initialtab.agb_new
    Get-ChildItem *.agbn -recurse | ForEach-Object {Move-Item $_ -Destination "C:\msys64\home\alber\Output\AGBN"}
    Get-ChildItem *.chk1,*.chk2 -recurse | ForEach-Object {Move-Item $_ -Destination "C:\msys64\home\alber\Output\CHK1CHK2"}
    Get-ChildItem *.summ -recurse | ForEach-Object {Move-Item $_ -Destination "C:\msys64\home\alber\Output\SUMM"}
}
