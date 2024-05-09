using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishHelpRecordatory
{
    internal class WindowManagement
    {
        private readonly Panel panelTop;
        public readonly Button close;
        public readonly Button minimize;
        public readonly Button maximize;
        private readonly Button theme;
        public readonly Button logo;
        public event EventHandler<bool> ButtonCloseClicked;
        public event EventHandler<bool> ButtonMinimizeClicked;
        public event EventHandler<bool> ButtonMaximizeClicked;
        public WindowManagement(Panel panelTop, Button close, Button minimize, Button maximize, Button theme, Button logo)
        {
            this.panelTop = panelTop;
            this.close = close;
            this.minimize = minimize;
            this.maximize = maximize;
            this.theme = theme;
            this.logo = logo;

            this.close.Click += buttonClose_Click;
            this.minimize.Click += ButtonMinimize_Click;
            this.maximize.Click += ButtonMaximize_Click;
            this.logo = logo;
        }
        public Panel GetPanelTop()
        {
            return panelTop;
        }



        public void ButtonImageSize(Button buton, Image image, int imageWidth, int imageHeight, int buttonWidth, int buttonHeight)
        {
            Image resizedImage = new Bitmap(image, new Size(imageWidth, imageHeight));

            buton.Image = resizedImage;

            buton.Size = new Size(buttonWidth, buttonHeight);
        }


        private void buttonClose_Click(object sender, EventArgs e)
        {
            ButtonCloseClicked?.Invoke(this,true);
        }
        private void ButtonMaximize_Click(object sender, EventArgs e)
        {
            ButtonMaximizeClicked?.Invoke(this, true);
        }

        private void ButtonMinimize_Click(object sender, EventArgs e)
        {
            ButtonMinimizeClicked?.Invoke(this, true);
        }


        public void ChangeColorButton(Button button, Color color1, Color color2)
        {

            button.MouseEnter += (sender, e) =>
            {
                button.BackColor = color2;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;

                button.Invalidate();
            };

            button.MouseLeave += (sender, e) =>
            {
                button.BackColor = color1;

                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;

                button.Invalidate();
            };

            button.BackColor = Color.Transparent;

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;

            button.Invalidate();

        }
        public void panelSize(Size windowsSize)
        {
            panelTop.Size = windowsSize;
            close.Location = new Point(windowsSize.Width - 44,0);
            maximize.Location = new Point(windowsSize.Width - 88, 0);
            minimize.Location = new Point(windowsSize.Width - 128, 0);
        }
    }
}
