namespace Tetris
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timer1 = new System.Windows.Forms.Timer(components);
            labelScore = new Label();
            labelLines = new Label();
            menuStrip1 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            startToolStripMenuItem = new ToolStripMenuItem();
            сНачалаToolStripMenuItem = new ToolStripMenuItem();
            таблицаРекордовToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // labelScore
            // 
            labelScore.AutoSize = true;
            labelScore.BackColor = Color.Transparent;
            labelScore.Font = new Font("Showcard Gothic", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            labelScore.Location = new Point(293, 175);
            labelScore.Name = "labelScore";
            labelScore.Size = new Size(0, 23);
            labelScore.TabIndex = 0;
            // 
            // labelLines
            // 
            labelLines.AutoSize = true;
            labelLines.BackColor = Color.Transparent;
            labelLines.Font = new Font("Showcard Gothic", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            labelLines.Location = new Point(293, 222);
            labelLines.Name = "labelLines";
            labelLines.Size = new Size(0, 23);
            labelLines.TabIndex = 1;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(482, 28);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { startToolStripMenuItem, сНачалаToolStripMenuItem, таблицаРекордовToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(65, 24);
            toolStripMenuItem1.Text = "Меню";
            // 
            // startToolStripMenuItem
            // 
            startToolStripMenuItem.Name = "startToolStripMenuItem";
            startToolStripMenuItem.Size = new Size(224, 26);
            startToolStripMenuItem.Text = "Пауза";
            startToolStripMenuItem.Click += OnPauseButtonClick;
            // 
            // сНачалаToolStripMenuItem
            // 
            сНачалаToolStripMenuItem.Name = "сНачалаToolStripMenuItem";
            сНачалаToolStripMenuItem.Size = new Size(224, 26);
            сНачалаToolStripMenuItem.Text = "Заново";
            сНачалаToolStripMenuItem.Click += OnAgainButtonClick;
            // 
            // таблицаРекордовToolStripMenuItem
            // 
            таблицаРекордовToolStripMenuItem.Name = "таблицаРекордовToolStripMenuItem";
            таблицаРекордовToolStripMenuItem.Size = new Size(224, 26);
            таблицаРекордовToolStripMenuItem.Text = "Таблица очков";
            таблицаРекордовToolStripMenuItem.Click += ShowRecords;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(482, 553);
            Controls.Add(labelLines);
            Controls.Add(labelScore);
            Controls.Add(menuStrip1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tetris";
            Paint += OnPaint;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private Label labelScore;
        private Label labelLines;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem startToolStripMenuItem;
        private ToolStripMenuItem сНачалаToolStripMenuItem;
        private ToolStripMenuItem таблицаРекордовToolStripMenuItem;
    }
}