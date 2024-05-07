using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EnglishHelpRecordatory
{
    internal class WordsList
    {
        //private readonly List<Object> list;
        private DataGridView list;

        public WordsList(DataGridView list)
        {
            this.list = list;
            this.list.MouseWheel += list_MouseWheel;
            this.list.CellPainting += list_CellPainting;
        }
        public DataGridView GetList()
        {
            return list;
        }

        public void LoadList(String texto)
        {
            try
            {
                // Leer el contenido del archivo JSON
                string jsonFilePath = "data.json";
                string json = File.ReadAllText(jsonFilePath);

                // Deserializar el JSON a una lista de objetos
                List<Word> dataList = JsonConvert.DeserializeObject<List<Word>>(json);

                // Establecer la fuente de datos del DataGridView
                list.DataSource = dataList;

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
        private void list_MouseWheel(object sender, MouseEventArgs e)  //Poder hacer scroll en la lista
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
