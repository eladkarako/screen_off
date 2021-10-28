using System;
using System.Threading;
using System.Runtime.InteropServices;

namespace screen_off{
  
  class Program{

/*
 * PostMessageW
 * place a message in the message queue and return (async. better then SendMessageW which is sync).
 * https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-postmessagew
 * https://docs.microsoft.com/en-us/windows/win32/winmsg/about-messages-and-message-queues
 * https://www.codeproject.com/Tips/1029254/SendMessage-and-PostMessage
 * notes: explicitly specify 'W' and 'Unicode' variation.
 */

//[CLSCompliantAttribute(false)]
  [return: MarshalAs(UnmanagedType.Bool)]
  [DllImport("user32.dll", EntryPoint = "PostMessageW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
  public static extern bool PostMessage(IntPtr hwnd
                                       ,UInt32 wMsg
                                       ,IntPtr wParam
                                       ,IntPtr lParam
                                       );
  
  
  public static void Main(string[] args){
      Thread.Sleep(200);Thread.Sleep(200);Thread.Sleep(200); //let events finish. small sleeps to avoid thread hanging.

      PostMessage(
                  (IntPtr)0xFFFF   // HWND_BROADCAST  
                 ,(UInt32)0x0112   // WM_SYSCOMMAND   https://docs.microsoft.com/en-us/windows/win32/menurc/wm-syscommand 
                 ,(IntPtr)0xF170   // SC_MONITORPOWER 
                 ,(IntPtr)0x0002   // POWER_OFF       
                 );
      
      Thread.Sleep(200);          //smooth-out event-calls.
      
      Environment.ExitCode = 0;   //program always ends with success, regardless of the PostMessage result.
      Environment.Exit(0);        //explicit exit the program (unload).
    }
  }
}