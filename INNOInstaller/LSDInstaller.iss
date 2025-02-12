; Uncomment to increase log level
;#pragma verboselevel 10

; Uncomment to include external file
;#include "defines.iss"

#define RELEASE_PATH "..\LandscapeSprinklerDesigner\bin\Release\net9.0-windows"
#define EXE_NAME "LandscapeSprinklerDesigner.exe"
#define PROGRAM_PATH AddBackslash(RELEASE_PATH) + EXE_NAME
#define MY_PROGRAM "..\LandscapeSprinklerDesigner\bin\Release\net9.0-windows\LandscapeSprinklerDesigner.exe"
#define MY_VERSION GetVersionNumbersString(PROGRAM_PATH)
#define MY_COMPANY GetStringFileInfo(MY_PROGRAM, COMPANY_NAME)
#define MY_COPYRIGHT GetStringFileInfo(MY_PROGRAM, LEGAL_COPYRIGHT)
#define MY_PRODUCT_NAME GetStringFileInfo(MY_PROGRAM, PRODUCT_NAME)
#define DOT_NET_4_5_VERSION 378389

[Setup]
VersionInfoVersion={#MY_VERSION}
VersionInfoCompany={#MY_COMPANY}
VersionInfoDescription={#MY_PRODUCT_NAME} Installer
VersionInfoTextVersion={#MY_VERSION}
VersionInfoCopyright={#MY_COPYRIGHT}
VersionInfoProductName={#MY_PRODUCT_NAME}
VersionInfoProductVersion={#MY_VERSION}
; 6.1sp1 represents Windows 7 Service Pack 1
MinVersion=6.1sp1 
AppCopyright=2025
AppName=AppName
ChangesAssociations=true
DefaultDirName={commonpf}\Trippn Technology\LandscapeSprinklerDesigner
UninstallDisplayIcon={app}\LandscapeSprinklerDesigner.exe
DefaultGroupName={#MY_PRODUCT_NAME}
WizardImageFile=banner.bmp
SetupIconFile=LandscapeSprinklerDesigner.ico
WizardSmallImageFile=setup.bmp
OutputBaseFilename=lsdsetup_{#MY_VERSION}
WizardImageStretch=false
AppVerName={#MY_PRODUCT_NAME} {#MY_VERSION}
LicenseFile=EULA.txt
AppPublisher={#MY_COMPANY}
AppVersion={#MY_VERSION}
UninstallDisplayName={#MY_PRODUCT_NAME}

[Files]
;Source: "dotNetFx45_Full_setup.exe"; DestDir: "{tmp}"; Flags: ignoreversion; Check: NeedsDotNETFramework
Source: "{#RELEASE_PATH}\LandscapeSprinklerDesigner.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#RELEASE_PATH}\*.dll"; DestDir: "{app}"; Flags: ignoreversion
;Source: "{#RELEASE_PATH}\LandscapeSprinklerDesigner.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#RELEASE_PATH}\Inventory.sqlite"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#RELEASE_PATH}\palette.json"; DestDir: "{app}"; Flags: ignoreversion
;Source: "{#RELEASE_PATH}\Transforms\*.xsl"; DestDir: "{app}\Transforms"; Flags: ignoreversion
;Source: "{#RELEASE_PATH}\x86\SQLite.Interop.dll"; DestDir: "{app}\x86"
Source: "{#RELEASE_PATH}\plugins\*.dll"; DestDir: "{app}\plugins"; Flags: ignoreversion
Source: "{#RELEASE_PATH}\updater\*.*"; DestDir: "{app}\updater"; Flags: ignoreversion

[Dirs]
Name: {app}\; Permissions: everyone-modify

[Icons]
Name: {group}\Landscape Sprinkler Designer; Filename: {app}\LandscapeSprinklerDesigner.exe; WorkingDir: {app}
Name: {group}\Remove Landscape Sprinkler Designer; Filename: {uninstallexe}; WorkingDir: {app}
Name: {userdesktop}\Landscape Sprinkler Designer; Filename: {app}\LandscapeSprinklerDesigner.exe; WorkingDir: {app}

[Run]
Filename: {tmp}\dotNetFx45_Full_setup.exe; WorkingDir: {tmp}; Flags: skipifdoesntexist; StatusMsg: Installing Microsoft .NET Framework 4.5
Filename: {app}\LandscapeSprinklerDesigner.exe; WorkingDir: {app}; Description: Run Landscape Sprinkler Designer; Flags: postinstall nowait

[Registry]
Root: HKCU; Subkey: Software\Tripp'n Technology; ValueType: none; Flags: uninsdeletekeyifempty createvalueifdoesntexist
Root: HKCU; Subkey: Software\Tripp'n Technology\LandscapeSprinklerDesigner; ValueType: none; Flags: createvalueifdoesntexist uninsdeletekey
Root: HKCR; SubKey: .lsd; ValueType: string; ValueData: LandscapeSprinklerDesigner; Flags: uninsdeletekey
Root: HKCR; SubKey: .lsdx; ValueType: string; ValueData: LandscapeSprinklerDesigner; Flags: uninsdeletekey
Root: HKCR; SubKey: LandscapeSprinklerDesigner; ValueType: string; ValueData: Landscape Sprinkler Designer File; Flags: uninsdeletekey
Root: HKCR; SubKey: LandscapeSprinklerDesigner\Shell\Open\Command; ValueType: string; ValueData: """{app}\LandscapeSprinklerDesigner.exe"" ""%1"""; Flags: uninsdeletevalue

[ThirdParty]
CompileLogFile=D:\src\tnt.lsd\INNOInstaller\inno.log

[PreCompile]
Name: "D:\src\tnt.lsd\INNOInstaller\GetSetupInfo\bin\Debug\net9.0\GetSetupInfo.exe"; Parameters: "/e ..\LandscapeSprinklerDesigner\bin\Release\net9.0-windows\LandscapeSprinklerDesigner.exe /o defines.iss"

[Code]
// Checks for .NET
function NeedsDotNETFramework() : Boolean;
var
	success: boolean;
	release: cardinal;
begin
    success := RegQueryDWordValue(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', release);
    Result := (success = False) or not (release >= {#DOT_NET_4_5_VERSION});
end;
