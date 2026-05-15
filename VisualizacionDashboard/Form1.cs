using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogicaNegociosNorthwind;

namespace EntidadesNorthwind
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            chart1.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonCargar_Click(object sender, EventArgs e)
        {
            //aquí limpio mis controles de visualización de datos
            dataGridView1.DataSource = null;
            chart1.DataSource = null;
            chart1.Series[0].Points.Clear();

            //aquí a una variable de tipo objeto le asigno el objeto que recibe en los metodos de las otras capas
            //ésta variable se carga dependiendo de los parametros que reciba de los ComboBox, a su vez con éste objeto puedo cargar 
            //mis controles visuales
            object data = LogicaConsultasLinQyADO.DevolverDatosConsulta(comboBoxCONSULTA.SelectedIndex, cbMetodoConsulta.SelectedIndex);

            dataGridView1.DataSource = data;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            chart1.DataSource = data;
            bool esLinq = cbMetodoConsulta.SelectedIndex == 1;

            switch (comboBoxCONSULTA.SelectedIndex)
            {
                case 0: 
                    chart1.Series[0].XValueMember = "Categoría";
                    chart1.Series[0].YValueMembers = esLinq ? "Total_Generado" : "Total Generado";
                    break;

                case 1: 
                    chart1.Series[0].XValueMember = "Producto";
                    chart1.Series[0].YValueMembers = esLinq ? "Unidades_Vendidas" : "Unidades Vendidas";
                    break;

                case 2: 
                    chart1.Series[0].XValueMember = "Empleado";
                    chart1.Series[0].YValueMembers = esLinq ? "Total_Pedidos" : "Total Pedidos";
                    break;

                case 3: 
                    chart1.Series[0].XValueMember = esLinq ? "País_Destino" : "País Destino";
                    chart1.Series[0].YValueMembers = esLinq ? "Total_Fletes" : "Total Fletes";
                    break;

                case 4: 
                    chart1.Series[0].XValueMember = "Cliente";
                    chart1.Series[0].YValueMembers = esLinq ? "Cantidad_Compras" : "Cantidad Compras";
                    break;
            }

            chart1.DataBind();

        }
    }
}
