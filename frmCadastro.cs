﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Security.Cryptography;


namespace Software_Trelisa
{
    public partial class frmCadastro : Form
    {
        public frmCadastro()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Data Source=.;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                SqlDataAdapter adapter = new SqlDataAdapter();
                connection.Open();
                SqlCommand cmd = new SqlCommand(@"INSERT INTO USUARIO (nome, email, senha) VALUES (@nome, @email, @senha)", connection);
                
                cmd.Parameters.Add("@nome", SqlDbType.VarChar, 70).Value = txtNome.Text;
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = txtEmail.Text;

                //hash password
                Password password = new Password(txtPassword.Text);
                cmd.Parameters.Add("@senha", SqlDbType.VarChar, 100).Value = password.Encrypt();
                adapter.InsertCommand = cmd;
                adapter.InsertCommand.ExecuteNonQuery();
                cmd.Dispose();
                connection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}