<h1><img width="64" src="screen_off/app.png" /> screen_off</h1>
<h2>turn off the screen by sending a message to the operation system. simulate natural screen off condition.</h2>

<hr/>

it compiled as a Windows-GUI subsystem, to prevent showing console window (it will show no window at all). <br/>
it will send a message using the <code>PostMessageW</code> API (Unicode compatible) of <code>user32.dll</code>, with the value: <code>-1, 0x0112, 0xF170, 2</code>.

<hr/>

built with SharpDevelop <code>v5.1.0.5216-0e58df71</code>, modified to support high DPI. <br/>
C# <code>compiler version</code>: <code>C# 3.0</code>. <br/>
C# <code>target framework</code>: <code>.Net Framework 2.0</code>. <br/>
which is the minimal requirement for be using a manifest.

<hr/>

this is the entire program (essentially..).

```csharp
using System;
using System.Threading;
using System.Runtime.InteropServices;

namespace screen_off{
  
  class Program{

  [DllImport("user32.dll", EntryPoint = "PostMessageW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
  public static extern int PostMessage(
    int hwnd,
    int wMsg,
    int wParam,
    int lParam);
  
  public static void Main(string[] args){
      Thread.Sleep(100);
   
      PostMessage(-1, 0x0112, 0xF170, 2);
      
      Thread.Sleep(100);
      
      Environment.ExitCode = 0;
      Environment.Exit(0);
    }
  }
}
```

the timeout before and after sleep helps to smooth-out any user input residue events.

this program can be used to simulate what happens when the screen turns off in a natural way,  
similar to timeout of the power-scheme,  
and to check solutions for <a href="https://gist.github.com/eladkarako/618dc70aaa73931e645257851d34f2aa#file-unfuck-windows-10-power-control-with-a-lot-of-registry-editing-md">https://gist.github.com/eladkarako/618dc70aaa73931e645257851d34f2aa#file-unfuck-windows-10-power-control-with-a-lot-of-registry-editing-md</a> for example.


<hr/>

this is the manifest used in this app (taken from <a href="https://github.com/eladkarako/manifest">https://github.com/eladkarako/manifest</a>):

```xml
<?xml version="1.0" encoding="UTF-8" standalone="yes"?> 
<assembly xmlns="urn:schemas-microsoft-com:asm.v1" manifestVersion="1.0"> 
  <dependency> 
    <dependentAssembly> 
      <assemblyIdentity name="Microsoft.Windows.Common-Controls" 
        version="6.0.0.0" publicKeyToken="6595b64144ccf1df" 
        type="win32" processorArchitecture="*" language="*" /> 
    </dependentAssembly> 
  </dependency> 
  <application xmlns="urn:schemas-microsoft-com:asm.v3"> 
     <windowsSettings> <dpiAware      xmlns="http://schemas.microsoft.com/SMI/2005/WindowsSettings">true/PM</dpiAware>                     </windowsSettings> 
     <windowsSettings> <dpiAwareness  xmlns="http://schemas.microsoft.com/SMI/2016/WindowsSettings">PerMonitorV2,PerMonitor</dpiAwareness> </windowsSettings> 
     <windowsSettings> <longPathAware xmlns="http://schemas.microsoft.com/SMI/2016/WindowsSettings">true</longPathAware>                   </windowsSettings> 
     <windowsSettings> <heapType      xmlns="http://schemas.microsoft.com/SMI/2020/WindowsSettings">SegmentHeap</heapType>                 </windowsSettings> 
  </application> 
  <trustInfo xmlns="urn:schemas-microsoft-com:asm.v3"> 
    <security> 
      <requestedPrivileges> 
        <requestedExecutionLevel level="asInvoker" uiAccess="false" /> 
      </requestedPrivileges> 
    </security> 
  </trustInfo> 
  <compatibility xmlns="urn:schemas-microsoft-com:compatibility.v1"> 
    <application> 
      <supportedOS Id="{8e0f7a12-bfb3-4fe8-b9a5-48fd50a15a9a}" /> 
      <supportedOS Id="{1f676c76-80e1-4239-95bb-83d0f6d0da78}" /> 
      <supportedOS Id="{4a2f28e3-53b9-4441-ba9c-d69d4a4a6e38}" /> 
      <supportedOS Id="{35138b9a-5d96-4fbd-8e2d-a2440225f93a}" /> 
      <supportedOS Id="{e2011457-1546-43c5-a5fe-008deee3d3f0}" /> 
    </application> 
  </compatibility> 
</assembly> 
 
```

the DPI awareness is useless since I do not use any forms really,  
also the dependent-assembly to apply theme to the forms is not really needed since I don't use forms.  
and long-path awareness is not needed since I do not use any direct files-I/O really,  
and the heaptype is not needed since there is not-much of memory assignment really,
the trust-info and compatibility are required. I've included the rest simply because it is a good practice for Windows-GUI subsystem (regardless of actual uses).

<hr/>

You can use this thing here anyway you would like to, it is a nice C# template, simplified for maximum compatibility,  
and it is licensed as "The UnLicense" - a.k.a. <strong>Public Domain</strong>.

<hr/>

<a href="https://www.virustotal.com/gui/file/60a96feb1d70c89c50804c9488f2252ea38f32281e9cfeeb99110b6a78d43812/detection">VirusTotal report: <code>screen_off.exe</code> 4KB</a>