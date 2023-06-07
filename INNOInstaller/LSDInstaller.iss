#define MY_PROGRAM "..\LandscapeSprinklerDesigner\bin\Debug\LandscapeSprinklerDesigner.exe"
#define MY_VERSION GetFileVersion(MY_PROGRAM)
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
MinVersion=6.1.7600
AppCopyright=2011
AppName={#MY_PRODUCT_NAME}
ChangesAssociations=true
DefaultDirName={pf}\Trippn Technology\LandscapeSprinklerDesigner
UninstallDisplayIcon={app}\LandscapeSprinklerDesigner.exe
DefaultGroupName={#MY_PRODUCT_NAME}
WizardImageFile=banner.bmp
SetupIconFile=LandscapeSprinklerDesigner.ico
WizardSmallImageFile=setup.bmp
OutputBaseFilename=lsdsetup_{#MY_VERSION}
WizardImageStretch=false
AppVerName={#MY_PRODUCT_NAME} {#MY_VERSION}
LicenseFile=EULA.txt
AppPublisher=Tripp'n Technology, LLC.
AppVersion={#MY_VERSION}
UninstallDisplayName={#MY_PRODUCT_NAME}

[Files]
;Source: "dotNetFx45_Full_setup.exe"; DestDir: "{tmp}"; Flags: ignoreversion; Check: NeedsDotNETFramework
Source: "..\LandscapeSprinklerDesigner\bin\Debug\LandscapeSprinklerDesigner.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\LandscapeSprinklerDesigner\bin\Debug\*.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\LandscapeSprinklerDesigner\bin\Debug\LandscapeSprinklerDesigner.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\LandscapeSprinklerDesigner\bin\Debug\Inventory.sqlite"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\LandscapeSprinklerDesigner\bin\Debug\lsd.palette"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\LandscapeSprinklerDesigner\bin\Debug\Transforms\*.xsl"; DestDir: "{app}\Transforms"; Flags: ignoreversion
Source: "..\LandscapeSprinklerDesigner\bin\Debug\x86\SQLite.Interop.dll"; DestDir: "{app}\x86"
Source: "..\LandscapeSprinklerDesigner\bin\Debug\plugins\*.dll"; DestDir: "{app}\plugins"; Flags: ignoreversion
Source: "..\LandscapeSprinklerDesigner\bin\Debug\updater\*.*"; DestDir: "{app}\updater"; Flags: ignoreversion

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
