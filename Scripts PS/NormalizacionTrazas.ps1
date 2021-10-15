 $filesAGBN = Get-ChildItem "C:\AGBN"
 $filesKPT = Get-ChildItem "C:\KPT"
 
 foreach ($f in $filesAGBN){

    $fileName = $f.BaseName
    Copy-Item C:\AGBN\$f C:\home
    
        foreach ($f2 in $filesKPT){
            if($f2.Name -match $fileName){
            Copy-Item C:\msys64\home\alber\Input\KPT\$f2 C:\home
            
            Rename-Item C:\home\$f ainpnew2
            Rename-Item C:\home\$f2 ainpnew1

            Start-Process C:\msys64\mingw64.exe .agbn.exe -Wait

            Remove-Item C:\msys64\home\ainpnew2
            Remove-Item C:\msys64\home\ainpnew1
            Copy-Item C:\msys64\home\aoutnew C:\msys64\home\Output\AGBN_Normalizadas
            Rename-Item C:\msys64\home\Output\AGBN_Normalizadas\aoutnew $fileName + "2.agbn"

            }
        }  
     