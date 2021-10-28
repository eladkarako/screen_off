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