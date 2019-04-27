﻿
using FishingBot.Methods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FishingBot
{
  public partial class GrindFish : Form
  {
    public GrindFish()
    {
      InitializeComponent();
    }

    private void Start_Click(object sender, EventArgs e)
    {
      var fisher = new Fisher();
      int timeSeconds;
      try
      {
        timeSeconds = int.Parse(Timer.Text);
      }
      catch {
        throw new Exception("Wrong supplied value for timer");
      }
      var bindKey = bind.Text;
      var rod = FishingRodBind.Text;
      if (string.IsNullOrEmpty(bindKey) || string.IsNullOrEmpty(rod)) throw new Exception("Have to input bind keys");
      fisher.Run(bindKey, rod, timeSeconds,this);
    }

    public void ChangeStatusText(string text,bool operatoren) {
      if (operatoren) StatusStuff.Text += text;
      else StatusStuff.Text = text;
      Application.DoEvents();
      return;
    }
    public void ShowMiniScreen(Bitmap picture) {
      pictureBox1.Image = picture;
      Application.DoEvents();
      return;
    }

  }
}
