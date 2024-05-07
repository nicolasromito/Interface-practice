using static System.Runtime.InteropServices.JavaScript.JSType;
using Timer = System.Windows.Forms.Timer;

namespace EnglishHelpRecordatory
{
    internal class ButtonBox
    {
        private readonly FlowLayoutPanel panelButtonSearch;
        private readonly List<Button> buttons;
        private readonly Color buttonBackColor;
        private Button ultimoBotonPresionado = null;
        private const int animationSpeed = 10; // Velocidad de la animación
        private const int targetWidth = 100; // Ancho original del botón
        private const int expandedWidth = 200; // Ancho expandido del botón
        public event EventHandler<string> ButtonClicked;
        public ButtonBox(FlowLayoutPanel panelButtonSearch, List<Button> buttons, Color buttonBackColor)
        {
            this.panelButtonSearch = panelButtonSearch;
            this.buttons = buttons;
            this.buttonBackColor = buttonBackColor;
        }
        public FlowLayoutPanel GetPanelButtonSearch()
        {
            return panelButtonSearch;
        }

        public void InitializePanelButtonSearch()
        {
            panelButtonSearch.Name = "panelButtonSearch";
            panelButtonSearch.FlowDirection = FlowDirection.TopDown;
            panelButtonSearch.BackColor = Color.Transparent;
            panelButtonSearch.AutoSize = false;
            panelButtonSearch.Size = new Size(220, 844);
            panelButtonSearch.Location = new Point(2, 76);

            foreach (var button in buttons)
            {
                panelButtonSearch.Controls.Add(button);
                button.ForeColor = Color.White;
                button.BackColor = buttonBackColor;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.Size = new Size(100, 25);
                
                button.UseVisualStyleBackColor = true;
                button.Enter += buttonLetra_Click;
                button.MouseEnter += Button_MouseEnter;
                button.MouseLeave += Button_MouseLeave;  
            }
           
        }

        private void buttonVisual(object sender)
        {
            Button button = (Button)sender;
            if (ultimoBotonPresionado != null && ultimoBotonPresionado != button)
            {
                ultimoBotonPresionado.ForeColor = Color.White;
                ultimoBotonPresionado.BackColor = Color.Transparent;
                ultimoBotonPresionado.Width = targetWidth;
            }
            ultimoBotonPresionado = button;
            AnimateButton(button, expandedWidth);
            button.ForeColor = Color.Black;
            button.BackColor = Color.FromArgb(236, 243, 174);
        }
        private void buttonLetra_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            buttonVisual(button);
            string letra = button.Text;
            if(letra == "All")
            {
                ButtonClicked?.Invoke(this, "");
            }
            else
            {
                ButtonClicked?.Invoke(this, letra);
            }
            
        }
        private void buttonAll_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            buttonVisual(button);
            //LoadList("");
        }
        private void Button_MouseEnter(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (ultimoBotonPresionado != button)
            {
                button.ForeColor = Color.White;
                button.BackColor = Color.FromArgb(131, 145, 182);
            }
        }

        // Evento que se dispara cuando el mouse sale del botón
        private void Button_MouseLeave(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (ultimoBotonPresionado != button)
            {
                button.ForeColor = Color.White;
                button.BackColor = Color.Transparent;
            }
        }

        // Método para animar el botón
        private void AnimateButton(Button button, int targetWidth)
        {
            Timer timer = new Timer();
            timer.Interval = 1; // Intervalo de tiempo para la animación
            int step = targetWidth > button.Width ? animationSpeed : -animationSpeed; // Determina si se debe expandir o contraer el botón

            timer.Tick += (sender, e) =>
            {
                button.Width += step; // Ajusta el ancho del botón

                // Si se alcanza el tamaño deseado o se sobrepasa, detiene el temporizador y ajusta el tamaño final del botón
                if ((step > 0 && button.Width >= targetWidth) || (step < 0 && button.Width <= targetWidth))
                {
                    timer.Stop();
                    button.Width = targetWidth;
                }
            };

            timer.Start(); // Inicia la animación
        }

        private void ChangeColorButton(Button button, Color color1, Color color2)
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
