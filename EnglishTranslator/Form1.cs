using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System;
using System.Diagnostics;
using static System.Net.WebRequestMethods;
using Timer = System.Windows.Forms.Timer;
using EnglishHelpRecordatory.Properties;
using System.Resources;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EnglishHelpRecordatory
{
    public partial class Form1 : Form
    {

        private bool isDragging = false;
        private Point lastCursorPos;
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        private const int HTLEFT = 0xA;
        private const int HTRIGHT = 0xB;
        private const int HTTOP = 0xC;
        private const int HTTOPLEFT = 0xD;
        private const int HTTOPRIGHT = 0xE;
        private const int HTBOTTOM = 0xF;
        private const int HTBOTTOMLEFT = 0x10;
        private const int HTBOTTOMRIGHT = 0x11;
        private bool manualMaximized = false;
        private Rectangle normalBounds;
        //Colores
        private Color yellow = Color.FromArgb(236, 243, 174);
        private Color blue = Color.FromArgb(54, 57, 83);
        private Color darkBlue = Color.FromArgb(14, 17, 43);
        private Color red = Color.FromArgb(240, 25, 25);

        //Variables de clase
        private FlowLayoutPanel panelButtonSearch;
        private List<Button> buttons;
        private DataGridView list;
        private Button close;
        private Button minimize;
        private Button maximize;
        private Button theme;
        private Button logo;
        private Panel panelTop;
        private TextBox english;
        private TextBox spanish;
        private Panel panelAddNuewWord;
        private Button add2;
        private bool isAddPanelEneable;
        //Classes
        private ButtonBox panelButtonInitializer;
        private WordsList listGrid;
        private WindowManagement windowsTopPanel;
        private Words_ABM addWordPanel;
        private Words_ABM modifyWordPanel;
        private SearchBar searchBar;
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;  //Elimina los bordes de windows
            this.Padding = new Padding(10);
            this.DoubleBuffered = true;

            this.MouseDown += Form_MouseDown;
            this.MouseMove += Form_MouseMove;
            this.MouseUp += Form_MouseUp;

            InitializeList();
            InitializeButtons();
            InitializeTopButtons();
            //InitializeNewWordPanel();

            isAddPanelEneable = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ExecutePanelButtons();
            ExecuteList();
            ExecuteTopPanel();
            ExecuteAddWordPanel();
            ExecuteSearchBar();
            panelButtonInitializer.ButtonClicked += ButtonBox_ButtonClicked;
            windowsTopPanel.ButtonCloseClicked += ButtonClose_ButtonClicked;
            windowsTopPanel.ButtonMinimizeClicked += ButtonMinimize_ButtonClicked;
            windowsTopPanel.ButtonMaximizeClicked += ButtonMaximize_ButtonClicked;
            searchBar.textChanged += RefreshList_TextChanged;
            panelTop.MouseDown += Form_MouseDown;
            panelTop.MouseMove += Form_MouseMove;
            panelTop.MouseUp += Form_MouseUp;
        }
        //  Funciones para la Clase WindowManagement - Botones de manejo de la ventana de windows    ///////////////////////////
        private void InitializeTopButtons()
        {
            panelTop = new Panel();
            logo = new Button();
            close = new Button();
            minimize = new Button();
            maximize = new Button();
            theme = new Button();

            panelTop.Controls.Add(close);
            panelTop.Controls.Add(minimize);
            panelTop.Controls.Add(maximize);
            //panelTop.Controls.Add(theme);
            panelTop.Controls.Add(logo);

            panelTop.BackColor = Color.Transparent;
            panelTop.Location = new Point(0, 0);
            panelTop.Margin = new Padding(3, 2, 3, 2);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1488, 53);
            panelTop.TabIndex = 10;

            logo.Enabled = false;
            logo.Location = new Point(0, 0);
            logo.Margin = new Padding(3, 2, 3, 2);
            logo.Name = "Logo";
            logo.Size = new Size(44, 44);
            logo.TabIndex = 0;

            close.Location = new Point(1444, 0);
            close.Margin = new Padding(3, 2, 3, 2);
            close.Name = "close";
            close.Size = new Size(44, 44);
            close.TabIndex = 0;

            minimize.Location = new Point(1356, 0);
            minimize.Name = "minimize";
            minimize.Size = new Size(44, 44);
            minimize.TabIndex = 2;
            minimize.UseVisualStyleBackColor = true;

            maximize.Location = new Point(1400, 0);
            maximize.Name = "maximize";
            maximize.Size = new Size(44, 44);
            maximize.TabIndex = 1;
            maximize.UseVisualStyleBackColor = true;
        }

        private void ExecuteTopPanel()
        {
            windowsTopPanel = new WindowManagement(panelTop, close, minimize, maximize, theme, logo);

            windowsTopPanel.ChangeColorButton(windowsTopPanel.close, Color.Transparent, Color.FromArgb(240, 25, 25));
            windowsTopPanel.ButtonImageSize(windowsTopPanel.close, Properties.Resources.x1, 25, 25, 44, 44);

            windowsTopPanel.ChangeColorButton(windowsTopPanel.maximize, Color.Transparent, Color.FromArgb(200, 200, 200));
            windowsTopPanel.ButtonImageSize(windowsTopPanel.maximize, Properties.Resources.max, 25, 25, 44, 44);

            windowsTopPanel.ChangeColorButton(windowsTopPanel.minimize, Color.Transparent, Color.FromArgb(200, 200, 200));
            windowsTopPanel.ButtonImageSize(windowsTopPanel.minimize, Properties.Resources.minus, 25, 25, 44, 44);

            windowsTopPanel.ChangeColorButton(windowsTopPanel.logo, Color.Transparent, Color.FromArgb(200, 200, 200));
            windowsTopPanel.ButtonImageSize(windowsTopPanel.logo, Properties.Resources.Logo, 25, 25, 44, 44);

            this.Controls.Add(windowsTopPanel.GetPanelTop());
        }

        private void ButtonClose_ButtonClicked(object sender, bool data)
        {
            this.Close();
        }


        private void ButtonMaximize_ButtonClicked(object sender, bool data)
        {
            if (!manualMaximized)
            {
                normalBounds = this.Bounds;

                MaximizeForm();
                manualMaximized = true;
            }
            else
            {
                this.Bounds = normalBounds;
                manualMaximized = false;
            }
        }

        private void MaximizeForm()
        {
            Rectangle workingArea = Screen.GetWorkingArea(this);

            this.Size = new Size(workingArea.Width, workingArea.Height - SystemInformation.CaptionHeight);
            this.Location = new Point(workingArea.Left, workingArea.Top);
        }

        private void ButtonMinimize_ButtonClicked(object sender, bool data)
        {
            WindowState = FormWindowState.Minimized;
        }

        //  Funciones para la Clase WindowManagement - Botones de manejo de la ventana de windows    ///////////////////////////

        //  Funciones para la Clase SearchBar - Barra de busqueda    ///////////////////////////

        private void ExecuteSearchBar()
        {
            searchBar = new SearchBar();

            this.Controls.Add(searchBar.GetSearchBar());
        }

        private void RefreshList_TextChanged(object sender, System.String data)
        {
            listGrid.LoadList(data);
        }

        //  Funciones para la Clase SearchBar - Barra de busqueda    ///////////////////////////

        //  Funciones para la Clase AddWord - Textbox  para agregar palabras al list    ///////////////////////////
        private void InitializeNewWordPanel()
        {
            panelAddNuewWord = new Panel();
            english = new TextBox();
            spanish = new TextBox();
            add2 = new Button();
            panelAddNuewWord.Controls.Add(english);
            panelAddNuewWord.Controls.Add(spanish);
            panelAddNuewWord.Controls.Add(add2);
        }
        private void ExecuteAddWordPanel()
        {
            addWordPanel = new Words_ABM(0);
            modifyWordPanel = new Words_ABM(1);

            addWordPanel.panelAddNuewWord.Visible = false;
            addWordPanel.panelAddNuewWord.Enabled = false;

            modifyWordPanel.panelAddNuewWord.Visible = false;
            modifyWordPanel.panelAddNuewWord.Enabled = false;

            ButtonImageSize(add, 25, 25);
            ButtonImageSize(modify, 25, 25);
            ButtonImageSize(delete, 25, 25);

            this.Controls.Add(addWordPanel.GetPanel());
            this.Controls.Add(modifyWordPanel.GetPanel());
        }

        //  Funciones para la Clase AddWord - Textbox  para agregar palabras al list    ///////////////////////////

        //  Funciones para la Clase ButtonBox - Botonera de letras A - Z    ///////////////////////////
        private void InitializeButtons()
        {
            buttons = new List<Button>();

            for (char c = 'A'; c <= 'Z'; c++)
            {
                Button button = new Button();
                button.Text = c.ToString();
                button.Name = "button" + c.ToString();

                buttons.Add(button);
            }
            Button buttonAll = new Button();
            buttonAll.Text = "All";
            buttonAll.Name = "buttonAll";

            buttons.Add(buttonAll);

        }
        private void ExecutePanelButtons()
        {
            panelButtonSearch = new FlowLayoutPanel();
            panelButtonInitializer = new ButtonBox(panelButtonSearch, buttons, Color.Transparent);
            panelButtonInitializer.InitializePanelButtonSearch();
            this.Controls.Add(panelButtonInitializer.GetPanelButtonSearch());
        }
        private void ButtonBox_ButtonClicked(object sender, string data)
        {
            listGrid.LoadList(data);
        }

        //  Funciones para la Clase ButtonBox - Botonera de letras A - Z    ///////////////////////////

        //  Funciones para la Clase WordsList - Lista de palabras ingles - espa�ol    ///////////////////////////
        private void InitializeList()
        {
            list = new DataGridView();
            list.SuspendLayout();
            list.AllowUserToAddRows = false;
            list.AllowUserToResizeRows = false;
            list.BackgroundColor = Color.FromArgb(192, 192, 255);
            list.CellBorderStyle = DataGridViewCellBorderStyle.None;
            list.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            list.Location = new Point(300, 138);
            list.Name = "dataGridViewPalabras";
            list.ReadOnly = true;
            list.RowHeadersVisible = false;
            list.RowHeadersWidth = 51;
            list.RowTemplate.Height = 30;
            list.ScrollBars = ScrollBars.None;
            list.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            list.Size = new Size(ClientSize.Width - 350, 700);
            list.ResumeLayout();
        }

        private void ExecuteList()
        {
            listGrid = new WordsList(list);
            listGrid.LoadList("");
            this.Controls.Add(listGrid.GetList());
            listGrid.UpdateListBoxItemSize(this.ClientSize);
        }

        //  Funciones para la Clase WordsList - Lista de palabras ingles - espa�ol    ///////////////////////////


        // Movimiento de la ventana       /////////////////////////////////////////////
        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursorPos = Cursor.Position;
            }
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int deltaX = Cursor.Position.X - lastCursorPos.X;
                int deltaY = Cursor.Position.Y - lastCursorPos.Y;
                this.Location = new Point(this.Location.X + deltaX, this.Location.Y + deltaY);
                lastCursorPos = Cursor.Position;
            }
        }

        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }
        // Movimiento de la ventana       /////////////////////////////////////////////


        // Resize    /////////////////////////////////////////////////////

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && this.ClientSize.Width > 0 && this.ClientSize.Height > 0)
            {
                Point screenPoint = new Point(m.LParam.ToInt32());
                Point clientPoint = this.PointToClient(screenPoint);

                if (clientPoint.X <= 5)
                    m.Result = (IntPtr)HTLEFT;
                else if (clientPoint.X >= this.ClientSize.Width - 5)
                    m.Result = (IntPtr)HTRIGHT;
                else if (clientPoint.Y <= 5)
                    m.Result = (IntPtr)HTTOP;
                else if (clientPoint.Y >= this.ClientSize.Height - 5)
                    m.Result = (IntPtr)HTBOTTOM;
                if (clientPoint.X <= 5 && clientPoint.Y <= 5)
                    m.Result = (IntPtr)HTTOPLEFT;
                else if (clientPoint.X >= this.ClientSize.Width - 5 && clientPoint.Y <= 5)
                    m.Result = (IntPtr)HTTOPRIGHT;
                else if (clientPoint.X <= 5 && clientPoint.Y >= this.ClientSize.Height - 5)
                    m.Result = (IntPtr)HTBOTTOMLEFT;
                else if (clientPoint.X >= this.ClientSize.Width - 5 && clientPoint.Y >= this.ClientSize.Height - 5)
                    m.Result = (IntPtr)HTBOTTOMRIGHT;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
            if (listGrid != null && windowsTopPanel != null)
            {
                listGrid.UpdateListBoxItemSize(this.ClientSize);
                windowsTopPanel.panelSize(new Size(this.Size.Width, 53));
            }
            UpdateButtonsPosition();
        }

        public void UpdateButtonsPosition()
        {
            add.Location = new Point(ClientSize.Width - 86, add.Location.Y);
            modify.Location = new Point(ClientSize.Width - 122, modify.Location.Y);
            delete.Location = new Point(ClientSize.Width - 158, delete.Location.Y);
        }
        // Resize    /////////////////////////////////////////////////////

        // Paint    /////////////////////////////////////////////////////
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (this.ClientRectangle.Width > 0 && this.ClientRectangle.Height > 0)
            {
                Color color1 = Color.FromArgb(14, 17, 43);
                Color color2 = Color.FromArgb(34, 47, 73);

                LinearGradientBrush gradientBrush = new LinearGradientBrush(
                    this.ClientRectangle,
                    color1,
                    color2,
                    LinearGradientMode.Vertical); 

                e.Graphics.FillRectangle(gradientBrush, this.ClientRectangle);
            }
        }

        private void add_MouseEnter(object sender, EventArgs e)
        {
            if (!isAddPanelEneable)
            {
                add.BackColor = blue;
                add.Image = Properties.Resources.plusWhite;
                ButtonImageSize(add, 25, 25);
            }
        }

        private void modify_MouseEnter(object sender, EventArgs e)
        {
            if (!isAddPanelEneable)
            {
                modify.BackColor = blue;
                modify.Image = Properties.Resources.modifyWhite;
                ButtonImageSize(modify, 25, 25);
            }
        }

        private void delete_MouseEnter(object sender, EventArgs e)
        {
            delete.BackColor = blue;
            delete.Image = Properties.Resources.deleteWhite;
            ButtonImageSize(delete, 25, 25);
        }

        private void add_MouseLeave(object sender, EventArgs e)
        {
            if (!isAddPanelEneable)
            {
                add.BackColor = Color.Transparent;
                add.Image = Properties.Resources.plusWhite;
                ButtonImageSize(add, 25, 25);
            }
        }

        private void modify_MouseLeave(object sender, EventArgs e)
        {
            if (!isAddPanelEneable)
            {
                modify.BackColor = Color.Transparent;
                modify.Image = Properties.Resources.modifyWhite;
                ButtonImageSize(modify, 25, 25);
            }
        }

        private void delete_MouseLeave(object sender, EventArgs e)
        {
            delete.BackColor = Color.Transparent;
            delete.Image = Properties.Resources.deleteWhite;
            ButtonImageSize(delete, 25, 25);

        }

        // Paint    /////////////////////////////////////////////////////

        // buttons    /////////////////////////////////////////////////////
        private void add_Click(object sender, EventArgs e)
        {
            if (isAddPanelEneable)
            {
                add.BackColor = blue;
                add.Image = Properties.Resources.plusWhite;
                ButtonImageSize(add, 25, 25);
                addWordPanel.panelAddNuewWord.Visible = false;
                addWordPanel.panelAddNuewWord.Enabled = false;
                isAddPanelEneable = false;
                listGrid.LoadList("");
            }
            else
            {
                add.BackColor = yellow;
                add.Image = Properties.Resources.plusBlack;
                ButtonImageSize(add, 25, 25);
                addWordPanel.panelAddNuewWord.BackColor = blue;
                addWordPanel.panelAddNuewWord.BringToFront();
                addWordPanel.panelAddNuewWord.Size = new Size(400, 100);
                addWordPanel.panelAddNuewWord.Location = new Point(1038, 122);
                addWordPanel.panelAddNuewWord.BorderStyle = BorderStyle.FixedSingle;
                ControlPaint.DrawBorder(addWordPanel.panelAddNuewWord.CreateGraphics(), addWordPanel.panelAddNuewWord.ClientRectangle, Color.Red, ButtonBorderStyle.Solid);
                addWordPanel.panelAddNuewWord.Visible = true;
                addWordPanel.panelAddNuewWord.Enabled = true;
                isAddPanelEneable = true;
            }

        }

        private void modify_Click(object sender, EventArgs e)
        {
            if (isAddPanelEneable)
            {
                modify.BackColor = blue;
                modify.Image = Properties.Resources.modifyWhite;
                ButtonImageSize(modify, 25, 25);
                modifyWordPanel.panelAddNuewWord.Visible = false;
                modifyWordPanel.panelAddNuewWord.Enabled = false;
                isAddPanelEneable = false;
                listGrid.LoadList("");
            }
            else
            {
                modify.BackColor = yellow;
                modify.Image = Properties.Resources.modifyBlack;
                ButtonImageSize(modify, 25, 25);
                modifyWordPanel.english.Text = listGrid.word.English;
                modifyWordPanel.spanish.Text = listGrid.word.Spanish;
                modifyWordPanel.word.English = listGrid.word.English;
                modifyWordPanel.word.Spanish = listGrid.word.Spanish;
                modifyWordPanel.panelAddNuewWord.BackColor = blue;
                modifyWordPanel.panelAddNuewWord.BringToFront();
                modifyWordPanel.panelAddNuewWord.Size = new Size(400, 100);
                modifyWordPanel.panelAddNuewWord.Location = new Point(1038, 122);
                modifyWordPanel.panelAddNuewWord.BorderStyle = BorderStyle.FixedSingle;
                ControlPaint.DrawBorder(modifyWordPanel.panelAddNuewWord.CreateGraphics(), modifyWordPanel.panelAddNuewWord.ClientRectangle, Color.Red, ButtonBorderStyle.Solid);
                modifyWordPanel.panelAddNuewWord.Visible = true;
                modifyWordPanel.panelAddNuewWord.Enabled = true;
                isAddPanelEneable = true;
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            modifyWordPanel.word.English = listGrid.word.English;
            modifyWordPanel.word.Spanish = listGrid.word.Spanish;
            modifyWordPanel.delete_Click();
            listGrid.LoadList("");
        }

        public void ButtonImageSize(Button buton, int imageWidth, int imageHeight)
        {
            Image resizedImage = new Bitmap(buton.Image, new Size(imageWidth, imageHeight));

            buton.Image = resizedImage;

        }
        // buttons    /////////////////////////////////////////////////////
    }
}
