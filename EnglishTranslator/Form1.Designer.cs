using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Policy;
using System.Windows.Forms;

namespace EnglishHelpRecordatory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            openAddWordsPanel = new Button();
            SuspendLayout();
            // 
            // openAddWordsPanel
            // 
            openAddWordsPanel.Location = new Point(1402, 85);
            openAddWordsPanel.Name = "openAddWordsPanel";
            openAddWordsPanel.Size = new Size(36, 36);
            openAddWordsPanel.BackColor = Color.FromArgb(236, 243, 174);
            openAddWordsPanel.FlatStyle = FlatStyle.Flat;
            openAddWordsPanel.FlatAppearance.BorderSize = 0;
            openAddWordsPanel.TabIndex = 12;
            openAddWordsPanel.UseVisualStyleBackColor = true;
            openAddWordsPanel.Click += button1_Click_1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(14, 17, 43);
            ClientSize = new Size(1488, 920);
            Controls.Add(openAddWordsPanel);
            Margin = new Padding(3, 2, 3, 2);
            MinimumSize = new Size(1488, 920);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            Paint += Form1_Paint;
            Resize += Form1_Resize;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button openAddWordsPanel;
    }
}
