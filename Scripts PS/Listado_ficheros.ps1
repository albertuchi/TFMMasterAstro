$files = Get-ChildItem "C:\file_foragbext_ae\FEHp006"
 foreach ($f in $files){
 if ($f.Extension -eq ".chs") {Write-Output $f.Name}
 }