; example1.nsi
;
; This script is perhaps one of the simplest NSIs you can make. All of the
; optional settings are left to their default settings. The installer simply 
; prompts the user asking them where to install, and drops a copy of example1.nsi
; there. 

;--------------------------------

; The name of the installer
Name "HomeOnTheWaves"

; The file to write
OutFile "homeonthewavesinstaller.exe"

; The default installation directory
InstallDir $DESKTOP\HomeOnTheWaves

; Request application privileges for Windows Vista
RequestExecutionLevel user

;--------------------------------

; Pages

Page directory
Page instfiles

;--------------------------------

; The stuff to install
Section "" ;No components page, name is not important

  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  file "Home On the Waves.exe"
  file /r "Home On the Waves_Data"
  file /r MonoBleedingEdge
  file UnityCrashHandler64.exe
  file UnityPlayer.dll
  file WinPixEventRuntime.dll
  
  ExecShell "" "$INSTDIR\Home On the Waves.exe"
SectionEnd ; end the section
