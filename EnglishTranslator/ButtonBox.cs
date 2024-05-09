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
        private bool isPressed = false;
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
                button.Tag = false;
                button.UseVisualStyleBackColor = true;
                button.Click += buttonLetra_Click;
                button.MouseLeave += Button_MouseLeave;
                button.MouseEnter += Button_MouseEnter;
            }
           
        }

        private void buttonVisual(object sender)
        {
            Button button = (Button)sender;
            if ((bool)button.Tag)
            {
                if (ultimoBotonPresionado != null && (bool)ultimoBotonPresionado.Tag && button != ultimoBotonPresionado)
                {
                    ultimoBotonPresionado.Tag = !(bool)ultimoBotonPresionado.Tag;
                    ultimoBotonPresionado.ForeColor = Color.White;
                    ultimoBotonPresionado.BackColor = Color.Transparent;
                    ultimoBotonPresionado.Width = targetWidth;
                }
                AnimateButton(button, expandedWidth);
                button.ForeColor = Color.Black;
                button.BackColor = Color.FromArgb(236, 243, 174);
            }
            else
            {
                button.ForeColor = Color.White;
                button.BackColor = Color.Transparent;
                button.Width = targetWidth;
            }
            ultimoBotonPresionado = button;
        }
        private void buttonLetra_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.Tag = !(bool)button.Tag;
            buttonVisual(button);
            string letra = button.Text;
            if(letra == "All")
            {
                ButtonClicked?.Invoke(this, "");
            }
            else
            {
                if ((bool)button.Tag)
                {
                    ButtonClicked?.Invoke(this, letra);
                }
                else
                {
                    ButtonClicked?.Invoke(this, "");
                } 
            }
            
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (!(bool)button.Tag)
            {
                button.ForeColor = Color.White;
                button.BackColor = Color.FromArgb(131, 145, 182);
                button.Width = targetWidth;
            }
        }
        private void Button_MouseLeave(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (!(bool)button.Tag)
            {
                button.ForeColor = Color.White;
                button.BackColor = Color.Transparent;
                button.Width = targetWidth;
            }

        }

        private void AnimateButton(Button button, int targetWidth)
        {
            Timer timer = new Timer();
            timer.Interval = 1; 
            int step = targetWidth > button.Width ? animationSpeed : -animationSpeed; 

            timer.Tick += (sender, e) =>
            {
                button.Width += step; 

                if ((step > 0 && button.Width >= targetWidth) || (step < 0 && button.Width <= targetWidth))
                {
                    timer.Stop();
                    button.Width = targetWidth;
                }
            };

            timer.Start(); 
        }

        
    }
}
