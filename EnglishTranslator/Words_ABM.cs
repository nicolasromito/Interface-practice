using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.Devices;
using System.Xml;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace EnglishHelpRecordatory
{
    internal class Words_ABM
    {
        public TextBox english;
        public TextBox spanish;
        public Panel panelAddNuewWord;
        private Button add;
        private Button modify;
        public Word word;
        public Words_ABM(int action) 
        {
            word = new Word();
            panelAddNuewWord = new Panel();
            english = new TextBox();
            spanish = new TextBox();
            add = new Button();
            modify = new Button();
            english.Text = "";
            spanish.Text = "";
            add.Text = "CARGAR";
            modify.Text = "MODIFICAR";

            english.Location = new Point(10, 10);
            spanish.Location = new Point(210, 10);
            english.Size = new Size(180,30);
            spanish.Size = new Size(180, 30);
            add.Location = new Point(10, 60);
            add.Size = new Size(380, 30);
            add.FlatStyle = FlatStyle.Flat;
            add.FlatAppearance.BorderSize = 0;
            add.BackColor = Color.FromArgb(236, 243, 174);
            

            modify.Location = new Point(10, 60);
            modify.Size = new Size(380, 30);
            modify.FlatStyle = FlatStyle.Flat;
            modify.FlatAppearance.BorderSize = 0;
            modify.BackColor = Color.FromArgb(236, 243, 174);
            switch (action)
            {
                case 0:
                    panelAddNuewWord.Controls.Add(add);
                    break;
                case 1:
                    panelAddNuewWord.Controls.Add(modify);
                    break;
                case 2:
                    break;
                default:
                    panelAddNuewWord.Controls.Add(add);
                    break;
            };
                
            panelAddNuewWord.Controls.Add(english);
            panelAddNuewWord.Controls.Add(spanish);
            

            add.Click += add_Click;
            modify.Click += modify_Click;
        }

        public Panel GetPanel()
        {
            return panelAddNuewWord;
        }

        private void add_Click(object sender, EventArgs e)
        {
            try
            {
                string campo1 = english.Text;
                string campo2 = spanish.Text;

                string jsonFilePath = "data.json";

                List<Word> dataList;
                if (File.Exists(jsonFilePath))
                {
                    string json = File.ReadAllText(jsonFilePath);
                    dataList = JsonConvert.DeserializeObject<List<Word>>(json);
                }
                else
                {
                    dataList = new List<Word>();
                }

                dataList.Add(new Word { English = campo1, Spanish = campo2 });

                string newJson = JsonConvert.SerializeObject(dataList, Formatting.Indented);

                File.WriteAllText(jsonFilePath, newJson);

                MessageBox.Show("Datos guardados correctamente en el archivo JSON.");

                english.Text = "";
                spanish.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los datos en el archivo JSON: " + ex.Message);
            }

            //string campo1 = english.Text;
            //string campo2 = spanish.Text;
            //int resultado = 0;

            //string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    // Crear un comando para llamar al procedimiento almacenado
            //    using (SqlCommand command = new SqlCommand("InsertPalabra_sp", connection))
            //    {
            //        // Especificar que es un procedimiento almacenado
            //        command.CommandType = CommandType.StoredProcedure;

            //        // Agregar los parámetros al comando
            //        command.Parameters.AddWithValue("@ingles", campo1);
            //        command.Parameters.AddWithValue("@espanol", campo2);

            //        // Agregar parámetro de salida para el resultado
            //        command.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;

            //        // Ejecutar el procedimiento almacenado
            //        command.ExecuteNonQuery();

            //        // Obtener el resultado del parámetro de salida
            //        resultado = (int)command.Parameters["@resultado"].Value;
            //    }
            //}

            //if (resultado == 1)
            //{
            //    english.Text = "";
            //    spanish.Text = "";
            //}
            //else
            //{
            //    MessageBox.Show("No se pudo cargar la palabra porque ya existe otra igual");
            //    english.Text = "";
            //    spanish.Text = "";
            //}
        }

        private void modify_Click(object sender, EventArgs e)
        {
            try
            {
                string campo1 = english.Text;
                string campo2 = spanish.Text;

                string jsonFilePath = "data.json";

                List<Word> dataList;
                if (File.Exists(jsonFilePath))
                {
                    string json = File.ReadAllText(jsonFilePath);
                    dataList = JsonConvert.DeserializeObject<List<Word>>(json);
                }
                else
                {
                    dataList = new List<Word>();
                }
                Word palabraModificar = dataList.FirstOrDefault(w => w.English == word.English);

                if (palabraModificar != null)
                {
                    palabraModificar.English = campo1;
                    palabraModificar.Spanish = campo2;

                    string newJson = JsonConvert.SerializeObject(dataList, Formatting.Indented);

                    File.WriteAllText(jsonFilePath, newJson);

                    MessageBox.Show("Palabra modificada correctamente en el archivo JSON.");
                }
                else
                {
                    MessageBox.Show("La palabra que intentas modificar no existe en el archivo JSON.");
                }
                english.Text = "";
                spanish.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar la palabra en el archivo JSON: " + ex.Message);
            }


            //string campo1 = english.Text;
            //string campo2 = spanish.Text;
            //int resultado = 0;

            //string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    // Crear un comando para llamar al procedimiento almacenado
            //    using (SqlCommand command = new SqlCommand("InsertPalabra_sp", connection))
            //    {
            //        // Especificar que es un procedimiento almacenado
            //        command.CommandType = CommandType.StoredProcedure;

            //        // Agregar los parámetros al comando
            //        command.Parameters.AddWithValue("@ingles", campo1);
            //        command.Parameters.AddWithValue("@espanol", campo2);

            //        // Agregar parámetro de salida para el resultado
            //        command.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;

            //        // Ejecutar el procedimiento almacenado
            //        command.ExecuteNonQuery();

            //        // Obtener el resultado del parámetro de salida
            //        resultado = (int)command.Parameters["@resultado"].Value;
            //    }
            //}

            //if (resultado == 1)
            //{
            //    english.Text = "";
            //    spanish.Text = "";
            //}
            //else
            //{
            //    MessageBox.Show("No se pudo cargar la palabra porque ya existe otra igual");
            //    english.Text = "";
            //    spanish.Text = "";
            //}
        }

        public void delete_Click()
        {
            try
            {
                string jsonFilePath = "data.json";

                List<Word> dataList;
                if (File.Exists(jsonFilePath))
                {
                    string json = File.ReadAllText(jsonFilePath);
                    dataList = JsonConvert.DeserializeObject<List<Word>>(json);
                }
                else
                {
                    dataList = new List<Word>();
                }
                Word palabraEliminar = dataList.FirstOrDefault(w => w.English == word.English && w.Spanish == word.Spanish);

                if (palabraEliminar != null)
                {
                    DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar la palabra?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        dataList.Remove(palabraEliminar);

                        string newJson = JsonConvert.SerializeObject(dataList, Formatting.Indented);

                        File.WriteAllText(jsonFilePath, newJson);

                        MessageBox.Show("Palabra eliminada correctamente del archivo JSON.");
                    }
                    else
                    {
                        MessageBox.Show("La eliminación de la palabra ha sido cancelada.");
                    }
                }
                else
                {
                    MessageBox.Show("La palabra que intentas eliminar no existe en el archivo JSON.");
                }
                english.Text = "";
                spanish.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la palabra del archivo JSON: " + ex.Message);
            }

        }
    }
}