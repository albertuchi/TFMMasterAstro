 $filesAGBN = Get-ChildItem "C:\msys64\home\alber\Output\AGBN"
 $filesKPT = Get-ChildItem "C:\msys64\home\alber\Input\KPT"
 
 foreach ($f in $filesAGBN){

    $fileName = $f.BaseName
    Copy-Item C:\msys64\home\alber\Output\AGBN\$f C:\msys64\home\alber
    
        foreach ($f2 in $filesKPT){
            if($f2.Name -match $fileName){
            Copy-Item C:\msys64\home\alber\Input\KPT\$f2 C:\msys64\home\alber
            
            Rename-Item C:\msys64\home\alber\$f ainpnew2
            Rename-Item C:\msys64\home\alber\$f2 ainpnew1

            Start-Process C:\msys64\mingw64.exe .agbn.exe -Wait

            Remove-Item C:\msys64\home\alber\ainpnew2
            Remove-Item C:\msys64\home\alber\ainpnew1
            Copy-Item C:\msys64\home\alber\aoutnew C:\msys64\home\alber\Output\AGBN_Normalizadas
            Rename-Item C:\msys64\home\alber\Output\AGBN_Normalizadas\aoutnew $fileName + "2.agbn"

            }
        }  
     
     
     
       
    <#
    
    
    Rename-Item $f initialtab.agb_new
    Start-Process .\mingw64.exe .\agb.exe -Wait
    Remove-Item initialtab.agb_new
    Get-ChildItem *.agbn -recurse | ForEach-Object {Move-Item $_ -Destination "C:\msys64\home\alber\Output\AGBN"}
    Get-ChildItem *.chk1,*.chk2 -recurse | ForEach-Object {Move-Item $_ -Destination "C:\msys64\home\alber\Output\CHK1CHK2"}
    Get-ChildItem *.summ -recurse | ForEach-Object {Move-Item $_ -Destination "C:\msys64\home\alber\Output\SUMM"}#>
}
