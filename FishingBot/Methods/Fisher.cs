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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace FishingBot.Methods
{
  class Fisher
  {
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
    private bool pause = false;
    private string rodBind;
    Logger logger;
    GrindFish mainWindow = null;
    IntPtr h;
    ColorHelpers colorHelpers;

    public Fisher() {
      this.colorHelpers = new ColorHelpers();
      var config = new NLog.Config.LoggingConfiguration();

      var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "file.txt" };

      config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);

      NLog.LogManager.Configuration = config;
      this.logger = NLog.LogManager.GetCurrentClassLogger();
    }


    public void Run(string bind,string rodBind, int timer, GrindFish main)
    {
      //focus process
      var process = Process.GetProcessesByName("WOW").FirstOrDefault();
      if (process == null) return;
      WindowsIdentity user = WindowsIdentity.GetCurrent();
      WindowsPrincipal principal = new WindowsPrincipal(user);
      bool isadmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
      process.WaitForInputIdle();
      h = process.MainWindowHandle;
      HotkeyManager.Current.AddOrReplace("Pause", Key.P, ModifierKeys.Control, hotkeyPressed);
      SetForegroundWindow(h);
      this.bind = bind;
      this.timer = timer;
      this.mainWindow = main;
      this.rodBind = rodBind;
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

      runBot();


    }

    public Bitmap GetPicture(Point location)
    {
      Size size = new Size(200, 200);
      Bitmap screenPixel = new Bitmap(200, 200, PixelFormat.Format32bppArgb);
      using (Graphics gdest = Graphics.FromImage(screenPixel))
      {
        location.X -= 100;
        location.Y -= 100;
        gdest.CopyFromScreen(location, new Point(0, 0), size);
        mainWindow.Invoke(new MethodInvoker(() => mainWindow.ShowMiniScreen(screenPixel)));
        return screenPixel;
      }


    }


    private void hotkeyPressed(object sender, HotkeyEventArgs e)
    {
      pause = !pause;
      if (!pause)
      {
        runBot();
      }
      else mainWindow.Invoke(new MethodInvoker(() => mainWindow.ChangeStatusText("Paused", false)));
      e.Handled = true;
    }

    private void runBot()
    {
      while (!pause)
      {
        mainWindow.Invoke(new MethodInvoker(() => mainWindow.ChangeStatusText("Analyzing", false)));
        Point cursor = new Point();
        GetCursorPos(ref cursor);
        SendKeys.SendWait(rodBind);
        Thread.Sleep(200);


        var c = GetPicture(cursor);
        //var hej = CheckColors(c);
        var inPosition = FindPos(c);
        if (!inPosition)
        {
          inPosition = GetInPos(cursor, c);
        }
        var FishHooked = false;
        int triesLoop = 0;
        while (!FishHooked && inPosition)
        {
          if (pause) break;
          GetCursorPos(ref cursor);
          c = GetPicture(cursor);
          var fishwasHooked = CatchFish(c);
          //need to get owning form to display what pixel we see now.
          //if color matches
          if (fishwasHooked)
          {
            mainWindow.Invoke(new MethodInvoker(() => mainWindow.ChangeStatusText(" Fish Found " + DateTime.Now.ToString(), false)));
            //send left button down
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
             byte KEYBDEVENTF_SHIFTVIRTUAL = 0x10;
           byte KEYBDEVENTF_SHIFTSCANCODE = 0x2A;
          int KEYBDEVENTF_KEYDOWN = 0;
            int KEYBDEVENTF_KEYUP = 2;
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
          Thread.Sleep(200);
          triesLoop++;
          if (triesLoop == 100) FishHooked = true;
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
   
          if (FindPos(c))
          {
            h += 20;
            SetCursorPos(x, h);
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
    private bool CatchFish(Bitmap screen)
    {
      for (int x = 0; x < screen.Width; x++)
      {
        for (int y = 0; y < screen.Height; y++)
        {
          var color = screen.GetPixel(x, y);
          if (colorHelpers.AnotherColorHelper(color)) {
            logger.Info("Catching madafaka with " + ColorTranslator.ToHtml(color));
              return true;
          }

        }
      }
      return false;
    }

    private bool CheckColors(Bitmap screen)
    {
      for (int x = 0; x < screen.Width; x++)
      {
        for (int y = 0; y < screen.Height; y++)
        {
          var color = screen.GetPixel(x, y);
          if (colorHelpers.colorHelper(ColorTranslator.ToHtml(color)))
          {
            logger.Info("Catching madafaka with " + ColorTranslator.ToHtml(color));
            return true;
          }

        }
      }
      return false;
    }

    private bool FindPos(Bitmap screen)
    {
      for (int x = 0; x < screen.Width; x++)
      {
        for (int y = 0; y < screen.Height; y++)
        {
          var color = screen.GetPixel(x, y);
          if (colorHelpers.FindFishingFloat(ColorTranslator.ToHtml(color)))
          {
            logger.Info("Found Float " + ColorTranslator.ToHtml(color));
            return true;
          }

        }
      }
      return false;
    }





  }
}
