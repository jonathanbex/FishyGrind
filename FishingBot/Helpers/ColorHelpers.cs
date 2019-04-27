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
    //earlier tries
    //public bool colorHelper(string hex)
    //{
    //  if (hex.StartsWith("#ff")) return true;
    //  if (hex.StartsWith("#fffff")) return true;
    //  if (hex.StartsWith("#c7c0d")) return true;
    //  if (hex.StartsWith("#beb7c")) return true;
    //  if (hex.StartsWith("#c1bcd")) return true;
    //  if (hex.StartsWith("#7e758")) return true;
    //  if (hex.StartsWith("#958da")) return true;
    //  if (hex.StartsWith("#e4dae")) return true;
    //  if (hex.StartsWith("#bfb4c8")) return true;
    //  return false;
    //}
    public bool ColorHelper(Color color)
    {
      //if (color.R > 180 && color.G > 180 && color.A > 180) return true;
      if (color.R > 240 && color.G > 240 && color.B > 240) return true;


      return false;
    }
    //earlier tries
    public bool FindFishingFloat(Color color)
    {
      if (FindFishingFloatHelper(color, "#8bb0da")) return true;
     // if (FindFishingFloatHelper(color, "#1a4cad")) return true;
     // if (FindFishingFloatHelper(color, "#1b489e")) return true;
      if (FindFishingFloatHelper(color, "#236abc")) return true;
      //if (FindFishingFloatHelper(color, "#16327c")) return true;
      //if (FindFishingFloatHelper(color, "#1c419a")) return true;
      //if (FindFishingFloatHelper(color, "#2772e0")) return true;
      if (FindFishingFloatHelper(color, "#1f4fb7")) return true;
      //if (FindFishingFloatHelper(color, "#19388c")) return true;
      //if (FindFishingFloatHelper(color, "#1b3a8e")) return true;
      if (FindFishingFloatHelper(color, "#2550a5")) return true;
      if (FindFishingFloatHelper(color, "#8169a9")) return true;
      if (FindFishingFloatHelper(color, "#625f96")) return true;
     
      //if (FindFishingFloatHelper(color, "#3a81d4")) return true;
      //if (FindFishingFloatHelper(color, "#7b7db4")) return true;
      //if (FindFishingFloatHelper(color, "#8a97cb")) return true;
      return false;
    }
    public bool FindFishingFloatHelper(Color color,string hex)
    {
      var checkBlue = ColorTranslator.FromHtml(hex);
      if (ColorsAreClose(color, checkBlue)) return true;
      return false;
    }
    bool ColorsAreClose(Color a, Color z, int threshold = 30)
    {
      var diff =  Math.Abs(a.R - z.R) + Math.Abs(a.G - z.G) + Math.Abs(a.B - z.B);
      if (diff <= threshold) {
        var colorA = ColorTranslator.ToHtml(a);
        var colorB = ColorTranslator.ToHtml(z);
        return true;
      }
      return false;
    }
  }
}
