﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.Devices;

namespace EnglishHelpRecordatory
{
    internal class AddWord
    {
        private TextBox english;
        private TextBox spanish;
        public Panel panelAddNuewWord;
        private Button add;
        public AddWord() 
        {
            panelAddNuewWord = new Panel();
            english = new TextBox();
            spanish = new TextBox();
            add = new Button();
            english.Text = "asd";
            spanish.Text = "123213";
            add.Text = "CARGAR";
            english.Location = new Point(10, 10);
            spanish.Location = new Point(210, 10);
            english.Size = new Size(180,30);
            spanish.Size = new Size(180, 30);
            add.Location = new Point(10, 60);
            add.Size = new Size(380, 30);
            add.FlatStyle = FlatStyle.Flat;
            add.FlatAppearance.BorderSize = 0;
            add.BackColor = Color.FromArgb(236, 243, 174);
            panelAddNuewWord.Controls.Add(english);
            panelAddNuewWord.Controls.Add(spanish);
            panelAddNuewWord.Controls.Add(add);
            add.Click += add_Click;
        }

        public Panel GetPanel()
        {
            return panelAddNuewWord;
        }

        private void add_Click(object sender, EventArgs e)
        {
                string campo1 = english.Text;
                string campo2 = spanish.Text;
                int resultado = 0;

                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Crear un comando para llamar al procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("InsertPalabra_sp", connection))
                    {
                        // Especificar que es un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Agregar los parámetros al comando
                        command.Parameters.AddWithValue("@ingles", campo1);
                        command.Parameters.AddWithValue("@espanol", campo2);

                        // Agregar parámetro de salida para el resultado
                        command.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;

                        // Ejecutar el procedimiento almacenado
                        command.ExecuteNonQuery();

                        // Obtener el resultado del parámetro de salida
                        resultado = (int)command.Parameters["@resultado"].Value;
                    }
                }

                if (resultado == 1)
                {
                    english.Text = "";
                    spanish.Text = "";
                }
                else
                {
                    MessageBox.Show("No se pudo cargar la palabra porque ya existe otra igual");
                    english.Text = "";
                    spanish.Text = "";
                }

                //CargarDatos("");

        }
    }
}