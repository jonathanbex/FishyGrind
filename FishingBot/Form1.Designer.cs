namespace FishingBot
{
  partial class GrindFish
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.Start = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.Timer = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.bind = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.StatusStuff = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.FishingRodBind = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // Start
      // 
      this.Start.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
      this.Start.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
      this.Start.Location = new System.Drawing.Point(12, 129);
      this.Start.Name = "Start";
      this.Start.Size = new System.Drawing.Size(168, 71);
      this.Start.TabIndex = 0;
      this.Start.Text = "Start";
      this.Start.UseVisualStyleBackColor = false;
      this.Start.Click += new System.EventHandler(this.Start_Click);
      // 
      // Timer
      // 
      this.Timer.Location = new System.Drawing.Point(12, 28);
      this.Timer.Name = "Timer";
      this.Timer.Size = new System.Drawing.Size(168, 20);
      this.Timer.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(9, 12);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(97, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Fish Time (minutes)";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(9, 51);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(85, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Lure Macro Bind";
      // 
      // bind
      // 
      this.bind.Location = new System.Drawing.Point(12, 67);
      this.bind.Name = "bind";
      this.bind.Size = new System.Drawing.Size(168, 20);
      this.bind.TabIndex = 4;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(239, 12);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(86, 13);
      this.label3.TabIndex = 5;
      this.label3.Text = "Detection Status";
      // 
      // StatusStuff
      // 
      this.StatusStuff.Location = new System.Drawing.Point(242, 28);
      this.StatusStuff.Multiline = true;
      this.StatusStuff.Name = "StatusStuff";
      this.StatusStuff.ReadOnly = true;
      this.StatusStuff.Size = new System.Drawing.Size(191, 51);
      this.StatusStuff.TabIndex = 6;
      this.StatusStuff.Text = "Loading...";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(12, 203);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(82, 13);
      this.label4.TabIndex = 7;
      this.label4.Text = "Pause = Ctrl + p";
      // 
      // pictureBox1
      // 
      this.pictureBox1.Location = new System.Drawing.Point(242, 82);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(191, 145);
      this.pictureBox1.TabIndex = 8;
      this.pictureBox1.TabStop = false;
      // 
      // FishingRodBind
      // 
      this.FishingRodBind.Location = new System.Drawing.Point(12, 103);
      this.FishingRodBind.Name = "FishingRodBind";
      this.FishingRodBind.Size = new System.Drawing.Size(168, 20);
      this.FishingRodBind.TabIndex = 10;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(9, 87);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(87, 13);
      this.label5.TabIndex = 9;
      this.label5.Text = "Fishing Rod Bind";
      // 
      // GrindFish
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(445, 239);
      this.Controls.Add(this.FishingRodBind);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.StatusStuff);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.bind);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.Timer);
      this.Controls.Add(this.Start);
      this.Name = "GrindFish";
      this.Text = "GrindFish";
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button Start;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.TextBox Timer;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox bind;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox StatusStuff;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.TextBox FishingRodBind;
    private System.Windows.Forms.Label label5;
  }
}

