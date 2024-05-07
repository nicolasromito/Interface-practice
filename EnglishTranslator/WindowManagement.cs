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
        public event EventHandler<bool> ButtonClicked;

        public WindowManagement(Panel panelTop, Button close, Button minimize, Button maximize, Button theme, Button logo)
        {
            this.panelTop = panelTop;
            this.close = close;
            this.minimize = minimize;
            this.maximize = maximize;
            this.theme = theme;
            this.logo = logo;

            this.close.Click += button1_Click_1;
            this.logo = logo;
        }
        public Panel GetPanelTop()
        {
            return panelTop;
        }



        public void ButtonImageSize(Button buton, Image image, int imageWidth, int imageHeight, int buttonWidth, int buttonHeight)
        {
            Image resizedImage = new Bitmap(image, new Size(imageWidth, imageHeight));

            // Asignar la imagen al botón
            buton.Image = resizedImage;

            // Establecer el tamaño del botón para que coincida con el tamaño de la imagen
            buton.Size = new Size(buttonWidth, buttonHeight);
        }
        //private void UpdateTopPanelButtons()
        //{

        //    close.Location = new Point(this.ClientSize.Width - 44, close.Location.Y);
        //    panelTop.Size = new Size(this.ClientSize.Width, panelTop.Size.Height);
        //}

        private void button1_Click_1(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this,true);
        }

        public void ChangeColorButton(Button button, Color color1, Color color2)
        {

            button.MouseEnter += (sender, e) =>
            {
                button.BackColor = color2;
                // Ajustar el estilo del botón
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;

                // Forzar el redibujado del botón para que se aplique el gradiente
                button.Invalidate();
            };
            // Manejador de evento para MouseLeave
            button.MouseLeave += (sender, e) =>
            {
                button.BackColor = color1;

                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;

                // Forzar el redibujado del botón para que se aplique el gradiente
                button.Invalidate();
            };

            button.BackColor = Color.Transparent;

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;

            // Forzar el redibujado del botón para que se aplique el gradiente
            button.Invalidate();

        }
    }


}
