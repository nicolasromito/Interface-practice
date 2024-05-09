using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Globalization;

namespace EnglishHelpRecordatory
{
    internal class WordsList
    {
        private DataGridView list;
        public Word word;

        public WordsList(DataGridView list)
        {
            this.list = list;
            this.list.MouseWheel += list_MouseWheel;
            this.list.CellPainting += list_CellPainting;
            this.list.CellClick += DataGridView1_CellClick;
            word = new Word();
        }
        public DataGridView GetList()
        {
            return list;
        }

        public void LoadList(String texto)
        {
            try
            {
                string jsonFilePath = "data.json";
                string json = File.ReadAllText(jsonFilePath);
        
                List<Word> dataList = JsonConvert.DeserializeObject<List<Word>>(json);

                List<Word> palabrasFiltradas = dataList.Where(w => w.English.ToLower().StartsWith(texto.ToLower())).ToList();// || w.Spanish.ToLower().Contains(texto.ToLower())).ToList();
                list.DataSource = palabrasFiltradas.OrderBy(w => w.English).ToList();

                foreach (DataGridViewColumn column in list.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos desde el archivo JSON: " + ex.Message);
            }

            //try
            //{
            //    string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            //    // Crear una conexión
            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        // Abrir la conexión
            //        connection.Open();

            //        // Crear un comando para llamar al procedimiento almacenado
            //        using (SqlCommand command = new SqlCommand("BuscarPalabras_sp", connection))
            //        {
            //            // Especificar que es un procedimiento almacenado
            //            command.CommandType = CommandType.StoredProcedure;
            //            command.Parameters.AddWithValue("@ingles", texto); // Ajusta el valor según sea necesario
            //            command.Parameters.AddWithValue("@espanol", "");
            //            // Crear un adaptador de datos para obtener los resultados del procedimiento almacenado
            //            SqlDataAdapter adapter = new SqlDataAdapter(command);

            //            // Crear una tabla para contener los datos
            //            DataTable table = new DataTable();

            //            // Llenar la tabla con los resultados del procedimiento almacenado
            //            adapter.Fill(table);

            //            // Establecer la fuente de datos del DataGridView
            //            list.DataSource = table;
            //        }
            //    }

            //    foreach (DataGridViewColumn column in list.Columns)
            //    {
            //        column.SortMode = DataGridViewColumnSortMode.NotSortable;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error al cargar los datos: " + ex.Message);
            //}

        }

        public void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) 
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex != -1)
                {
                    DataGridViewRow filaSeleccionada = list.Rows[e.RowIndex];

                    string campo1 = filaSeleccionada.Cells["English"].Value.ToString();
                    string campo2 = filaSeleccionada.Cells["Spanish"].Value.ToString();

                    word.English = campo1;
                    word.Spanish = campo2;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al manejar el evento de doble clic en el DataGridView: " + ex.Message);
            }

        }
        private void list_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0 && list.FirstDisplayedScrollingRowIndex > 0)
            {
                list.FirstDisplayedScrollingRowIndex--;
            }
            else if (e.Delta < 0 && list.FirstDisplayedScrollingRowIndex < list.RowCount - 1)
            {
                list.FirstDisplayedScrollingRowIndex++;
            }
           ((HandledMouseEventArgs)e).Handled = true;
        }

        private void list_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;


            if (this.list.SelectedCells.Count > 0 &&
                this.list.SelectedCells[0].RowIndex == e.RowIndex)
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 204, 153);
            }
            else
            {
                e.CellStyle.BackColor = Color.FromArgb(192, 192, 255);
            }
            e.PaintBackground(e.ClipBounds, true);
            e.PaintContent(e.ClipBounds);
            e.Handled = true;
        }

        public void UpdateListBoxItemSize(Size ClientSize)
        {
            int newXSize = ClientSize.Width - list.Location.X - 50;
            list.Size = new Size(newXSize, 700);
            int columnWidth = 0;
            if (list.Columns.Count > 0)
            {
                columnWidth = newXSize / list.Columns.Count;
                foreach (DataGridViewColumn column in list.Columns)
                {
                    column.Width = columnWidth;
                }
            }
        }
    }
}
