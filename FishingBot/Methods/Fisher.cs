using FishingBot.Helpers;
using FishingBot.Models;
using NHotkey;
using NHotkey.Wpf;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;

namespace FishingBot.Methods
{
  class Fisher
  {
    //Windows Api
    [DllImport("user32.dll")]
    static extern bool GetCursorPos(ref Point lpPoint);
    [DllImport("User32.dll")]
    static extern int SetForegroundWindow(IntPtr point);
    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
    [DllImport("user32.dll", EntryPoint = "keybd_event", CharSet = CharSet.Auto, ExactSpelling = true)]
    public static extern void keybd_event(byte vk, byte scan, int flags, int extrainfo);
    [DllImport("User32.dll")]
    private static extern bool SetCursorPos(int X, int Y);
    [DllImport("user32.dll")]
    public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

    private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
    private const int MOUSEEVENTF_RIGHTUP = 0x10;
    private string bind;
    private int timer;
    private int lureTimer;
    private bool pause = false;
    private string rodBind;
    private DateTime EndTime;
    private DateTime LastAppendedLure;
    byte KEYBDEVENTF_SHIFTVIRTUAL = 0x10;
    byte KEYBDEVENTF_SHIFTSCANCODE = 0x2A;
    int KEYBDEVENTF_KEYDOWN = 0;
    int KEYBDEVENTF_KEYUP = 2;
    IntPtr h;

    Point cursor = new Point();
    Rect windowRec = new Rect();

    Logger logger;
    GrindFish mainWindow = null;
    ColorHelpers colorHelpers;
    Bitmap startPos = null;

    public Fisher() {
      this.colorHelpers = new ColorHelpers();

      var config = new NLog.Config.LoggingConfiguration();

      var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "file.txt" };

      config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);
 
      NLog.LogManager.Configuration = config;

      this.logger = NLog.LogManager.GetCurrentClassLogger();
    }


    public void Run(string bind,string rodBind, int timer,int lureTimer, GrindFish main)
    {
      this.bind = bind;
      this.timer = timer;
      this.mainWindow = main;
      this.rodBind = rodBind;
      this.EndTime = DateTime.Now.AddMinutes(timer);
      this.lureTimer = lureTimer;
      //focus process
      //vanilla
      var process = Process.GetProcessesByName("WOW").FirstOrDefault();
      //classic
      if (process == null) process = Process.GetProcessesByName("WowB").FirstOrDefault();

      if (process == null) return;

      //legacy check if administrator, no need.
      //WindowsIdentity user = WindowsIdentity.GetCurrent();
      //WindowsPrincipal principal = new WindowsPrincipal(user);
      //bool isadmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
  
      process.WaitForInputIdle();
      h = process.MainWindowHandle;

      //global hotkey for pause
      HotkeyManager.Current.AddOrReplace("Pause", Key.P, ModifierKeys.Control, HotKeyPressed);

      SetForegroundWindow(h);


      //5 seconds to get cursor into position
      mainWindow.Invoke(new MethodInvoker(() => mainWindow.ChangeStatusText("Starting in", false)));
      Thread.Sleep(1000);
      mainWindow.Invoke(new MethodInvoker(() => mainWindow.ChangeStatusText("5", false)));
      Thread.Sleep(1000);
      mainWindow.Invoke(new MethodInvoker(() => mainWindow.ChangeStatusText("4", false)));
      Thread.Sleep(1000);
      mainWindow.Invoke(new MethodInvoker(() => mainWindow.ChangeStatusText("3", false)));
      Thread.Sleep(1000);
      mainWindow.Invoke(new MethodInvoker(() => mainWindow.ChangeStatusText("2", false)));
      Thread.Sleep(1000);
      mainWindow.Invoke(new MethodInvoker(() => mainWindow.ChangeStatusText("1", false)));
      Thread.Sleep(1000);

      RunBot();


    }

   



    private void RunBot()
    {
      while (!pause)
      {
        if (DateTime.Now > EndTime) LogOut();
        mainWindow.Invoke(new MethodInvoker(() => mainWindow.ChangeStatusText("Analyzing", false)));

        GetCursorPos(ref cursor);

        //lure check
        if (LastAppendedLure == null || DateTime.Now > LastAppendedLure.AddMinutes(lureTimer)) {
          AppendLure();
          LastAppendedLure = DateTime.Now;
        }

        SendKeys.SendWait(rodBind);
        Thread.Sleep(1000);


        var pic = GetPicture(cursor);
        var inPosition = GetInPos(cursor, pic);
        
        var FishHooked = false;
        GetCursorPos(ref cursor);

         startPos = get50x100(GetPicture(cursor));
        int triesLoop = 0;
        while (!FishHooked && inPosition)
        {
          if (pause) break;
          GetCursorPos(ref cursor);
          pic = GetPicture(cursor);
          var fishwasHooked = CatchFish(pic,startPos);
          //need to get owning form to display what pixel we see now.
          //if color matches
          if (fishwasHooked)
          {
            mainWindow.Invoke(new MethodInvoker(() => mainWindow.ChangeStatusText(" Fish Found " + DateTime.Now.ToString(), false)));
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
   
            Thread.Sleep(50);
            keybd_event(KEYBDEVENTF_SHIFTVIRTUAL, KEYBDEVENTF_SHIFTSCANCODE, KEYBDEVENTF_KEYDOWN, 0);
            Thread.Sleep(50);
            mouse_event(MOUSEEVENTF_RIGHTDOWN, X, Y, 0, 0);
            Thread.Sleep(50);
            mouse_event(MOUSEEVENTF_RIGHTUP, X, Y, 0, 0);
            Thread.Sleep(50);
            keybd_event(KEYBDEVENTF_SHIFTVIRTUAL, KEYBDEVENTF_SHIFTSCANCODE, KEYBDEVENTF_KEYUP, 0);
            Thread.Sleep(500);
            FishHooked = true;
          }
          Thread.Sleep(100);
          triesLoop++;
          if (triesLoop == 250) FishHooked = true;
        }
        Thread.Sleep(2000);
      }


    }


    public bool GetInPos(Point cursor, Bitmap c)
    {
      Rect windowRec = new Rect();
      GetWindowRect(h, ref windowRec);
      var middleY = (windowRec.Bottom - windowRec.Top) / 2;
      var middleX = (windowRec.Right - windowRec.Left) / 2;
      cursor.X = middleX;
      cursor.Y = middleY;
      SetCursorPos(cursor.X, cursor.Y);
      var inPosition = false;
      while (!inPosition)
      {
        GetCursorPos(ref cursor);
        int x = cursor.X;
        int y = windowRec.Top + 150;

        Thread.Sleep(100);
        for (int h = y; h < windowRec.Bottom; h += 10)
        {
          SetCursorPos(x, h);
          GetCursorPos(ref cursor);
          Thread.Sleep(100);
          c = GetPicture(cursor);
          var posOfFoat = FindPos(c);
          if (posOfFoat != null)
          {
            //h = (h) + posOfFoat.Y;
            //x = (x) + posOfFoat.X;
            //SetCursorPos(x, h);
            return true;
          }
          var check = h + 10;
          if (check >= windowRec.Bottom) inPosition = true;
          if (pause) break;
        }
        if (pause) break;
      }
      return false;
    }

    private bool CatchFish(Bitmap screen,Bitmap startPos)
    {
      var screensmall = get50x100(screen);
      mainWindow.Invoke(new MethodInvoker(() => mainWindow.ShowMiniScreenDebug1(startPos)));
      mainWindow.Invoke(new MethodInvoker(() => mainWindow.ShowMiniScreenDebug2(screensmall)));
      var screenSmallHash = GetHash(screensmall);
      var startposHash = GetHash(startPos);
      float equalElements = Compare(screenSmallHash, startposHash);
      mainWindow.Invoke(new MethodInvoker(() => mainWindow.ShowMiniScreenDebugText2(equalElements)));
      logger.Info("Light Diff " + equalElements);
      if (equalElements > 1.03)
      {
        logger.Info("returning true");
        return true;
      }
      //for (int x = 0; x < screen.Width; x++)
      //{
      //  for (int y = 0; y < screen.Height; y++)
      //  {
      //    var color = screen.GetPixel(x, y);
      //    if (colorHelpers.ColorHelper(color))
      //    {
      //      logger.Info("Catching madafaka with " + ColorTranslator.ToHtml(color));
      //      mainWindow.Invoke(new MethodInvoker(() => mainWindow.ShowMiniScreenDebug1(startPos)));
      //      mainWindow.Invoke(new MethodInvoker(() => mainWindow.ShowMiniScreenDebug2(screensmall)));
      //      return true;
      //    }

      //  }
      //}
      return false;
    }

    private CursorPos FindPos(Bitmap screen)
    {
      var cursor = new CursorPos { X = 0, Y = 0 };
      for (int x = 0; x < screen.Width; x++)
      {
        for (int y = 0; y < screen.Height; y++)
        {
          var color = screen.GetPixel(x, y);
          if (colorHelpers.FindFishingFloat(color))
          {
            logger.Info("Found Float " + ColorTranslator.ToHtml(color));
            cursor.X = x;
            cursor.Y = y;
            return cursor;
          }

        }
      }
      return null;
    }
    private Bitmap GetPicture(Point location)
    {
      Size size = new Size(200, 200);
      Bitmap screenPixel = new Bitmap(200, 200, PixelFormat.Format32bppArgb);
      using (Graphics gdest = Graphics.FromImage(screenPixel))
      {
        location.X -= 75;
        location.Y -= 25;
        gdest.CopyFromScreen(location, new Point(0, 0), size);
        mainWindow.Invoke(new MethodInvoker(() => mainWindow.ShowMiniScreen(screenPixel)));
        return screenPixel;
      }


    }
    private Bitmap get50x100(Bitmap src) {
      Bitmap screenPixel = new Bitmap(50, 100, PixelFormat.Format32bppArgb);
      using (Graphics gdest = Graphics.FromImage(screenPixel))
      {
        gdest.DrawImage(src, new Rectangle(0, 0,50,100), new Rectangle(50, 25, 50, 100), GraphicsUnit.Pixel);
        return screenPixel;
      }
    }
    private int GetHash(Bitmap bmpSource)
    {
      int rgbTotal = 0;
      for (int j = 0; j < bmpSource.Height; j++)
      {
        for (int i = 0; i < bmpSource.Width; i++)
        {
          var pixel = bmpSource.GetPixel(i, j);
          rgbTotal += pixel.R + pixel.G + pixel.B;
        }
      }
      return rgbTotal;
    }
    private float Compare(int hash1, int hash2) {
      return (float) hash1 / hash2;
    }

    private void HotKeyPressed(object sender, HotkeyEventArgs e)
    {
      pause = !pause;
      if (!pause)
      {
        RunBot();
      }
      else mainWindow.Invoke(new MethodInvoker(() => mainWindow.ChangeStatusText("Paused", false)));
      e.Handled = true;
    }
    
    private void LogOut()
    {

      SendKeys.SendWait("~");
      SendKeys.SendWait("/");
      SendKeys.SendWait("C");
      SendKeys.SendWait("A");
      SendKeys.SendWait("M");
      SendKeys.SendWait("P");
      SendKeys.SendWait("~");
      pause = true;
    }

    private void AppendLure()
    {
      GetCursorPos(ref cursor);
      uint X = (uint)Cursor.Position.X;
      uint Y = (uint)Cursor.Position.Y;
      SendKeys.SendWait(bind);
      Thread.Sleep(6000);

      GetWindowRect(h, ref windowRec);
      var middleY = (windowRec.Bottom - windowRec.Top) / 2;
      var middleX = (windowRec.Right - windowRec.Left) / 2;
      cursor.X = middleX;
      cursor.Y = middleY;
      SetCursorPos(cursor.X, cursor.Y);


      mouse_event(MOUSEEVENTF_RIGHTDOWN, X, Y, 0, 0);
      Thread.Sleep(50);
      mouse_event(MOUSEEVENTF_RIGHTUP, X, Y, 0, 0);
    }





  }
}
