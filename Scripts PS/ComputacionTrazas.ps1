 $files = Get-ChildItem "C:\Input\"
 foreach ($f in $files){
    Copy-Item .\home\Input\$f .
    Rename-Item $f initialtab.agb_new
    Start-Process .\mingw64.exe .\agb.exe -Wait
    Remove-Item initialtab.agb_new
    Get-ChildItem *.agbn -recurse | ForEach-Object {Move-Item $_ -Destination "C:\AGBN"}
    Get-ChildItem *.chk1,*.chk2 -recurse | ForEach-Object {Move-Item $_ -Destination "C:\CHK1CHK2"}
    Get-ChildItem *.summ -recurse | ForEach-Object {Move-Item $_ -Destination "C:\SUMM"}
}
