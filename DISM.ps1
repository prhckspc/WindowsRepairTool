#Import-Module DISM | DISM /Online /Cleanup-Image /RestoreHealth | Select-String -Pattern '\d\d\.\d\%|\d\.\d\%|\d\d\d\.\d\%'| Select -Expand Matches | Select -Expand Value
Set-ExecutionPolicy -Scope CurrentUser -ExecutionPolicy Unrestricted
Repair-WindowsImage -RestoreHealth -Online