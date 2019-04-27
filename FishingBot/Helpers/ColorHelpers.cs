using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBot.Helpers
{
 public class ColorHelpers
  {
    public bool colorHelper(string hex)
    {
      if (hex.StartsWith("#ff")) return true;
      if (hex.StartsWith("#fffff")) return true;
      if (hex.StartsWith("#c7c0d")) return true;
      if (hex.StartsWith("#beb7c")) return true;
      if (hex.StartsWith("#c1bcd")) return true;
      if (hex.StartsWith("#7e758")) return true;
      if (hex.StartsWith("#958da")) return true;
      if (hex.StartsWith("#e4dae")) return true;
      if (hex.StartsWith("#bfb4c8")) return true;
      return false;
    }
    public bool AnotherColorHelper(Color color)
    {
      //if (color.R > 180 && color.G > 180 && color.A > 180) return true;
      if (color.R > 185 && color.G > 185 && color.A > 185) return true;
      return false;
    }
    public bool FindFishingFloat(string hex)
    {
      if (hex.StartsWith("#0a173b")) return true;
      if (hex.Equals("#1a4cad")) return true;
      if (hex.Equals("#163480")) return true;
      if (hex.Equals("#453d34")) return true;
      if (hex.Equals("#2f1b13")) return true;
      if (hex.Equals("#484835")) return true;
      if (hex.Equals("#1b489e")) return true;
      if (hex.Equals("#236abc")) return true;
      if (hex.Equals("#16327c")) return true;
      if (hex.Equals("#1c419a")) return true;
      if (hex.Equals("#2772e0")) return true;
      if (hex.Equals("#1f4fb7")) return true;
      if (hex.Equals("#153278")) return true;
      if (hex.Equals("#19388c")) return true;
      if (hex.Equals("#1b3a8e")) return true;
      if (hex.Equals("#2550a5")) return true;
      if (hex.Equals("#3a81d4")) return true;

      return false;
    }
  }
}
