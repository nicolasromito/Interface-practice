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
            add = new Button();
            modify = new Button();
            delete = new Button();
            SuspendLayout();
            // 
            // add
            // 
            add.BackColor = Color.Transparent;
            add.FlatAppearance.BorderSize = 0;
            add.FlatStyle = FlatStyle.Flat;
            add.Image = Properties.Resources.plusWhite;
            add.Location = new Point(1402, 85);
            add.Name = "add";
            add.Size = new Size(36, 36);
            add.TabIndex = 12;
            add.UseVisualStyleBackColor = true;
            add.Click += add_Click;
            add.MouseEnter += add_MouseEnter;
            add.MouseLeave += add_MouseLeave;
            // 
            // modify
            // 
            modify.BackColor = Color.Transparent;
            modify.FlatAppearance.BorderSize = 0;
            modify.FlatStyle = FlatStyle.Flat;
            modify.Image = Properties.Resources.modifyWhite;
            modify.Location = new Point(1366, 85);
            modify.Name = "modify";
            modify.Size = new Size(36, 36);
            modify.TabIndex = 12;
            modify.UseVisualStyleBackColor = true;
            modify.Click += modify_Click;
            modify.MouseEnter += modify_MouseEnter;
            modify.MouseLeave += modify_MouseLeave;
            // 
            // delete
            // 
            delete.BackColor = Color.Transparent;
            delete.FlatAppearance.BorderSize = 0;
            delete.FlatStyle = FlatStyle.Flat;
            delete.Image = Properties.Resources.deleteWhite;
            delete.Location = new Point(1330, 85);
            delete.Name = "delete";
            delete.Size = new Size(36, 36);
            delete.TabIndex = 12;
            delete.UseVisualStyleBackColor = true;
            delete.Click += delete_Click;
            delete.MouseEnter += delete_MouseEnter;
            delete.MouseLeave += delete_MouseLeave;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(14, 17, 43);
            ClientSize = new Size(1488, 920);
            Controls.Add(delete);
            Controls.Add(modify);
            Controls.Add(add);
            Margin = new Padding(3, 2, 3, 2);
            MinimumSize = new Size(1488, 920);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            Paint += Form1_Paint;
            Resize += Form1_Resize;
            ResumeLayout(false);
        }

        #endregion
        private Button add;
        private Button modify;
        private Button delete;
    }
}
