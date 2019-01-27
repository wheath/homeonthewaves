copy HomeOnTheWaves.nsi exe_build
cd exe_build
"C:\Program Files (x86)\NSIS\makensis.exe" HomeOnTheWaves.nsi
cd ..
copy exe_build\homeonthewavesinstaller.exe .
