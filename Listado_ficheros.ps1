$files = Get-ChildItem "C:\Users\alber\OneDrive\Máster VIU\Asignaturas\14MAST Trabajo Fin de Máster\NetCoreCode\file_foragbext_ae\FEHp006"
 foreach ($f in $files){
 if ($f.Extension -eq ".chs") {Write-Output $f.Name}
 }