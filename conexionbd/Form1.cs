using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace conexionbd
{
    public partial class Form1 : Form
    {
        static string conexionstring = @"server= DESKTOP-MH79PIJ\SQLEXPRESS ; database= master ; integrated security= true ";
        SqlConnection conexion = new SqlConnection(conexionstring);

        public Form1()
        {
            InitializeComponent();
            Bitmap img = new Bitmap(Application.StartupPath + @"\img\ima1.jpg");
            this.BackgroundImage = img;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btn_Conectar_Click(object sender, EventArgs e)
        {
            conexion.Open();
            MessageBox.Show("Conexion ala BD:" + conexion.Database + "  ha sido exitoso");

        }

        private void btn_Desconectar_Click(object sender, EventArgs e)
        {
            conexion.Close();
            MessageBox.Show("Se ha desconectado correctamente");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_Consulta.Text == "")
            {
                string query = "Select * from Personas";
                SqlCommand comando = new SqlCommand(query, conexion);
                SqlDataAdapter data = new SqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                data.Fill(tabla);
                dgv_Consulta.DataSource = tabla;
            }
            else
            {
                string query = "Select * from Personas where Pais = '"+txt_Consulta.Text+"'";
                SqlCommand comando = new SqlCommand(query, conexion);
                SqlDataAdapter data = new SqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                data.Fill(tabla);
                dgv_Consulta.DataSource = tabla;

            }
        }

        public void consulta()
        {
            string query = "Select * from Personas";
            SqlCommand comando = new SqlCommand(query, conexion);
            SqlDataAdapter data = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            data.Fill(tabla);
            dgv_Consulta.DataSource = tabla;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string cadena = "Insert into personas ([Id],[Nombre],[Edad],[Pais]) values ('"+txt_Id.Text+"','"+txt_Nombre.Text+"','"+txt_Edad.Text+"','"+txt_Pais.Text+"')";
            SqlCommand comando = new SqlCommand(cadena,conexion);
            comando.ExecuteNonQuery();
            MessageBox.Show("La persona:------"+ txt_Nombre.Text+"----Se ha agregado correctamente");

            txt_Id.Text = "";
            txt_Nombre.Text = "";
            txt_Edad.Text = "";
            txt_Pais.Text = "";


            consulta();
        }

        private void btn_Eliminar_Click(object sender, EventArgs e)
        {
            int flag = 0;

            string cadena = "Delete from Personas where Nombre = '"+txt_Nombre.Text+"'";
            SqlCommand comando = new SqlCommand(cadena, conexion);
            flag = comando.ExecuteNonQuery();

            if (flag == 1)
            {
                MessageBox.Show("Se realizo el delete correctamente");
            }
            else
            {
                MessageBox.Show("Nose encontro  la persona");
            }
            txt_Nombre.Text = "";
            consulta();
        }

        private void btn_Actualizar_Click(object sender, EventArgs e)
        {
            int flag = 0;

            string cadena = "Update Personas set Nombre = '"+txt_Nombre.Text+"', Edad = '"+txt_Edad.Text+"', Pais = '"+txt_Pais.Text+"', Id = '"+txt_Id.Text+"' where Nombre = '"+txt_Nomnuevo.Text+"' ";
            SqlCommand comando = new SqlCommand(cadena, conexion);
            flag = comando.ExecuteNonQuery();

            if (flag == 1)
            {
                MessageBox.Show("Se realizo el cambio correctamente");
            }
            else
            {
                MessageBox.Show("Nose encontro  la persona");
            }
            txt_Nombre.Text = "";
            txt_Id.Text = "";
            txt_Nomnuevo.Text = "";
            txt_Edad.Text = "";
            txt_Pais.Text = "";
            consulta();




        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
