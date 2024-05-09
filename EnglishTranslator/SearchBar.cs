using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnglishHelpRecordatory.Properties;

namespace EnglishHelpRecordatory
{
    internal class SearchBar
    {
        private PictureBox lupa;
        private Panel panelSearch;
        private TextBox textBoxSearch;
        public event EventHandler<string> textChanged;
        private bool isChanged;
        public SearchBar()
        {
            isChanged = false;
            lupa = new PictureBox();
            panelSearch = new Panel();
            textBoxSearch = new TextBox();

            textBoxSearch.BackColor = Color.FromArgb(54, 57, 83);
            textBoxSearch.BorderStyle = BorderStyle.None;
            textBoxSearch.Font = new Font("Yu Gothic UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBoxSearch.ForeColor = Color.White;
            textBoxSearch.Location = new Point(50, 8);
            textBoxSearch.Margin = new Padding(3, 2, 3, 2);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new Size(608, 27);
            textBoxSearch.TabIndex = 9;
            textBoxSearch.Text = "Search...";
            textBoxSearch.TextChanged += textBoxSearch_TextChanged;
            textBoxSearch.Enter += textBoxSearch_Enter;
            textBoxSearch.Leave += textBoxSearch_Leave;

            lupa.BackColor = Color.Transparent;
            lupa.Enabled = false;
            lupa.Location = new Point(-10, 3);
            lupa.Margin = new Padding(3, 2, 3, 2);
            lupa.Name = "Lupa";
            lupa.Size = new Size(54, 32);
            lupa.SizeMode = PictureBoxSizeMode.Zoom;
            lupa.TabIndex = 3;
            lupa.TabStop = false;

            panelSearch.BackColor = Color.FromArgb(54, 57, 83);
            panelSearch.Controls.Add(lupa);
            panelSearch.Controls.Add(textBoxSearch);
            panelSearch.Location = new Point(310, 56);
            panelSearch.Margin = new Padding(3, 2, 3, 2);
            panelSearch.Name = "panelSearch";
            panelSearch.Size = new Size(669, 45);
            panelSearch.TabIndex = 11;
        }

        public Panel GetSearchBar()
        {
            return panelSearch;
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Search...")
            {
                textChanged?.Invoke(this, "");
            }
            else
            {
                textChanged?.Invoke(this, textBox.Text);
            }
            
        }
        private void textBoxSearch_Enter(object sender, EventArgs e)
        {
            if (!isChanged)
            {
                textBoxSearch.Text = "";
                isChanged = true;
            }
        }

        private void textBoxSearch_Leave(object sender, EventArgs e)
        {
            if (isChanged)
            {
                if(textBoxSearch.Text == "")
                {
                    textBoxSearch.Text = "Search...";
                    isChanged = false;
                }
            }
        }
    }
}
