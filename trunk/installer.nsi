; Script generated by the HM NIS Edit Script Wizard.

; HM NIS Edit Wizard helper defines
!define PRODUCT_NAME "Madden Amp"
!define PRODUCT_VERSION "2.1.0"
!define PRODUCT_PUBLISHER "Tributech"
!define PRODUCT_WEB_SITE "http://gommo.homelinux.net"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\MaddenEditor.exe"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"
!define PRODUCT_STARTMENU_REGVAL "NSIS:StartMenuDir"

; MUI 1.67 compatible ------
!include "MUI.nsh"

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\modern-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

Page custom CheckDotNet
; Welcome page
!insertmacro MUI_PAGE_WELCOME
; License page
!insertmacro MUI_PAGE_LICENSE "licence.txt"
; Directory page
!insertmacro MUI_PAGE_DIRECTORY
; Start menu page
var ICONS_GROUP
!define MUI_STARTMENUPAGE_NODISABLE
!define MUI_STARTMENUPAGE_DEFAULTFOLDER "Madden Amp"
!define MUI_STARTMENUPAGE_REGISTRY_ROOT "${PRODUCT_UNINST_ROOT_KEY}"
!define MUI_STARTMENUPAGE_REGISTRY_KEY "${PRODUCT_UNINST_KEY}"
!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "${PRODUCT_STARTMENU_REGVAL}"
!insertmacro MUI_PAGE_STARTMENU Application $ICONS_GROUP
; Instfiles page
!insertmacro MUI_PAGE_INSTFILES
; Finish page
!define MUI_FINISHPAGE_RUN "$INSTDIR\MaddenEditor.exe"
!define MUI_FINISHPAGE_SHOWREADME "$INSTDIR\readme.txt"
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_INSTFILES

; Language files
!insertmacro MUI_LANGUAGE "English"

; MUI end ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "MaddenAmpSetup-${PRODUCT_VERSION}.exe"
InstallDir "$PROGRAMFILES\Madden Amp"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" ""
ShowInstDetails show
ShowUnInstDetails show

Section "MainSection" SEC01
  SetOutPath "$INSTDIR"
  SetOverwrite ifnewer
  File "MaddenEditor\bin\Release\MaddenEditor.exe"
  File "MaddenEditor\bin\Release\tdbaccess.dll"
  File "licence.txt"
  File "readme.txt"
  File "MaddenEditor\Docs\contributions.txt"
  File "MaddenEditor\Docs\DBFileStructure.doc"
  File "06files\MFE-2006-TEST-2.fra"

; Shortcuts
  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
  CreateDirectory "$SMPROGRAMS\$ICONS_GROUP"
  CreateShortCut "$SMPROGRAMS\$ICONS_GROUP\Madden Amp (${PRODUCT_VERSION}).lnk" "$INSTDIR\MaddenEditor.exe"

  CreateShortCut "$DESKTOP\Madden Amp.lnk" "$INSTDIR\MaddenEditor.exe"
  CreateShortCut "$SMPROGRAMS\$ICONS_GROUP\readme.txt.lnk" "$INSTDIR\readme.txt"
  !insertmacro MUI_STARTMENU_WRITE_END
SectionEnd

Section -AdditionalIcons
  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
  CreateShortCut "$SMPROGRAMS\$ICONS_GROUP\Uninstall.lnk" "$INSTDIR\uninst.exe"
  !insertmacro MUI_STARTMENU_WRITE_END
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\MaddenEditor.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\MaddenEditor.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd

Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) was successfully removed from your computer."
FunctionEnd

Function un.onInit
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Are you sure you want to completely remove $(^Name) and all of its components?" IDYES +2
  Abort
FunctionEnd

Section Uninstall
  !insertmacro MUI_STARTMENU_GETFOLDER "Application" $ICONS_GROUP
  Delete "$INSTDIR\uninst.exe"
  Delete "$INSTDIR\readme.txt"
  Delete "$INSTDIR\licence.txt"
  Delete "$INSTDIR\tdbaccess.dll"
  Delete "$INSTDIR\MaddenEditor.exe"
  Delete "$INSTDIR\contributions.txt"
  Delete "$INSTDIR\DBFileStructure.doc"
  Delete "$INSTDIR\MFE-2006-TEST-2.fra"

  Delete "$SMPROGRAMS\$ICONS_GROUP\Uninstall.lnk"
  Delete "$DESKTOP\Madden Amp.lnk"
  Delete "$SMPROGRAMS\$ICONS_GROUP\Madden Amp (${PRODUCT_VERSION}).lnk"
  Delete "$SMPROGRAMS\$ICONS_GROUP\readme.txt.lnk"

  RMDir "$SMPROGRAMS\$ICONS_GROUP"
  RMDir "$INSTDIR"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  SetAutoClose true
SectionEnd

; Function: GetDotNet
; Author: Joe Cincotta ( joe@pixolut.com )
; Date: 24/6/2004
;
; Detect the .NET Framework installation. Download if not present.
; NOTES: 
; Requires a default web browser to be installed.
; This function will quit if the framework has not been found. 
; you could replace this with a 'click when done' dialog or something
; similar which waits for the installation process to complete.
; - Note that my quick and dirty exe exists approach could be updated
; to use the registry approaches in the other .NET detect examples...
;
; Usage:
;   Call GetDotNet
;

Function CheckDotNet
  Call GetDotNet
FunctionEnd

Function GetDotNet
  IfFileExists "$WINDIR\Microsoft.NET\Framework\v2.0.50727\installUtil.exe" NextStep
  MessageBox MB_YESNOCANCEL|MB_ICONEXCLAMATION "You must have the Microsoft .NET \
Framework 2.0 Installed to use this application. $\n$\nClick 'Yes' to open your browser \
to the download page. $\nClick 'No' to ignore and install anyway. $\nClick 'Cancel' to cancel installation" IDYES yes IDNO no
  Quit
yes:
  ExecShell Open "http://www.microsoft.com/downloads/details.aspx?FamilyID=0856eacb-4362-4b0d-8edd-aab15c5e04f5&DisplayLang=en" SW_SHOWNORMAL
  MessageBox MB_OK|MB_ICONEXCLAMATION "Click 'OK' when your download and install is complete. $\n$\n After \
you click 'OK' this installation will continue"
no:
NextStep:
FunctionEnd

