using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using apiexamen;
using System.Net.Http.Json;
using apiexamen.WSROkto;

namespace FrontEnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool flag = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            if (flag == false)
            {
                clear();
                ConsultarBDD();
            }
            else if(flag ==true)
            {
                
                ConsultarDLL();
            }

        }
        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            if (flag == false)
            {
                InsertarDatos();
            }
            else
            {
                InsertarDatosDll();
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (flag == false)
            {
                EliminarDatos();
            }
            else
            {
                EliminarDatosDll();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (flag == false)
            {
                BuscarDatos();
            }
            else
            {
                BuscarDatosDll();
            }
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            if (flag == false)
            {
                ActualizarDatos();
            }
            else
            {
                ActualizarDatoDll();
            }
        }

        private void rBtnD_Checked(object sender, RoutedEventArgs e)
        {
            flag = true;
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            flag = false;
        }


        //FUNCIONES-------------

        public void ConsultarBDD()
        {
            using (WSROkto.WSOktoClient client = new WSROkto.WSOktoClient())
            {
                var items = client.GetTblExamens()
               .Select(item => new { idExamen = item.idExamen, Nombre = item.Nombre, Descripcion = item.Descripcion });
                dgView.ItemsSource = items;
            }
        }

        public void ConsultarDLLnd()
        {
            if (txtBox2.Text != "" && txtBox3.Text != "")
            {
                try
                {
                    clsExamen clientDll = new clsExamen();
                    var items = clientDll.ConsultarDatos(txtBox2.Text, txtBox3.Text);
                    var mappedItems = items.Select(item => new { idExamen = item.idExamen, Nombre = item.Nombre, Descripcion = item.Descripcion });
                    dgView.ItemsSource = mappedItems;
                }
                catch (Exception ex)
                {
                   
                    txtBlock.Text = "Ocurrió un error al consultar los datos: " + ex.Message;
                }
            }
        }

        public void ConsultarDLL()
        {
            try
            {
                clsExamen clientDll = new clsExamen();
               List<tblExamen> items = clientDll.GetTblExamens();
                var mappedItems = items.Select(item => new { idExamen = item.idExamen, Nombre = item.Nombre, Descripcion = item.Descripcion });
                dgView.ItemsSource = mappedItems;
            }
            catch (Exception ex)
            {
                txtBlock.Text = "Ocurrió un error al consultar los datos: " + ex.Message;
            }
        }




        public void InsertarDatos()
        {
            using (WSROkto.WSOktoClient client = new WSROkto.WSOktoClient())
            {
                if (txtBox1.Text != "" && txtBox2.Text != "" && txtBox3.Text != "")
                {
                    var idExamen = int.Parse(txtBox1.Text);
                    var Nombre = txtBox2.Text;
                    var Descripcion = txtBox3.Text;
                    var resultado = client.AgregarExamen(idExamen, Nombre, Descripcion);
                    txtBlock.Text = string.Join(", ", resultado);
                    ConsultarBDD();
                }
            }

        }
        public void InsertarDatosDll()
        {
            try
            {
                clsExamen clientDll = new clsExamen();

                if (txtBox1.Text != "" && txtBox2.Text != "" && txtBox3.Text != "")
                {
                    var idExamen = int.Parse(txtBox1.Text);
                    var Nombre = txtBox2.Text;
                    var Descripcion = txtBox3.Text;
                    var resultado = clientDll.AgregarExamen(idExamen, Nombre, Descripcion);
                    txtBlock.Text = resultado.Item2;
                    ConsultarBDD();
                }
            }
            catch (Exception ex)
            {
                txtBlock.Text = "Ocurrió un error al insertar los datos: " + ex.Message;
            }
        }


        public void EliminarDatos()
        {
            using (WSROkto.WSOktoClient client = new WSROkto.WSOktoClient())
            {
                if (txtBox1.Text != "")
                {
                    var idExamen = int.Parse(txtBox1.Text);
                    var resultado = client.EliminarExamen(idExamen);
                    txtBlock.Text = string.Join(", ", resultado);
                    ConsultarBDD();
                }
            }

        }

        public void EliminarDatosDll()
        {
            try
            {
                clsExamen clientDll = new clsExamen();

                if (txtBox1.Text != "")
                {
                    var idExamen = int.Parse(txtBox1.Text);
                    var resultado = clientDll.EliminarExamen(idExamen);
                    txtBlock.Text = resultado.Item2;
                    ConsultarBDD();
                }
            }
            catch (Exception ex)
            {
                txtBlock.Text = "Ocurrió un error al eliminar los datos: " + ex.Message;
            }
        }


        public void BuscarDatos()
        {//DIFIERE SI LOS 3 CAMPOS HAN SIDO INGRESADOS O NO
            using (WSROkto.WSOktoClient client = new WSROkto.WSOktoClient())
            {

                if (txtBox1.Text != "" && txtBox2.Text != "" && txtBox3.Text != "")
                {
                    try
                    {
                        var items = client.ConsultarExamen(int.Parse(txtBox1.Text), txtBox2.Text, txtBox3.Text)
                       .Select(item => new { idExamen = item.idExamen, Nombre = item.Nombre, Descripcion = item.Descripcion });
                        dgView.ItemsSource = items;
                    }
                    catch (Exception ex)
                    {
                        txtBlock.Text = "No se encontro el registro";
                        clearGrid();

                    }
                }
                else if (txtBox1.Text != "")
                {
                   
                    try
                    {
                        var items = client.ConsultarExamenbyId(int.Parse(txtBox1.Text))
                   .Select(item => new { idExamen = item.idExamen, Nombre = item.Nombre, Descripcion = item.Descripcion });
                        dgView.ItemsSource = items;
                    }
                    catch (Exception ex)
                    {
                        txtBlock.Text = "No se encontro el registro";
                        clearGrid();
                    }
                }
            }
        }

        public void BuscarDatosDll()
        {
            try
            {
                clsExamen clientDll = new clsExamen();

                if (txtBox1.Text != "" && txtBox2.Text != "" && txtBox3.Text != "")
                {
                    var items = clientDll.ConsultarExamen(int.Parse(txtBox1.Text), txtBox2.Text, txtBox3.Text)
                        .Select(item => new { idExamen = item.idExamen, Nombre = item.Nombre, Descripcion = item.Descripcion });
                    dgView.ItemsSource = items;
                }
                else if (txtBox1.Text != "")
                {
                    var items = clientDll.ConsultarExamen(int.Parse(txtBox1.Text))
                        .Select(item => new { idExamen = item.idExamen, Nombre = item.Nombre, Descripcion = item.Descripcion });
                    dgView.ItemsSource = items;
                }
            }
            catch (Exception ex)
            {
                txtBlock.Text = "Ocurrió un error al buscar los datos: " + ex.Message;
                clearGrid();
            }
        }


        private void ActualizarDatos()
        {
            using (WSROkto.WSOktoClient client = new WSROkto.WSOktoClient())
            {
                if (txtBox1.Text != "" && txtBox2.Text != "" && txtBox3.Text != "")
                {
                    var idExamen = int.Parse(txtBox1.Text);
                    var Nombre = txtBox2.Text;
                    var Descripcion = txtBox3.Text;
                    var resultado = client.ActualizarExamen(idExamen, Nombre, Descripcion);
                    txtBlock.Text = string.Join(", ", resultado);
                    ConsultarBDD();
                }
            }
        }

        private void ActualizarDatoDll()
        {
            try
            {
                clsExamen clientDll = new clsExamen();

                if (txtBox1.Text != "" && txtBox2.Text != "" && txtBox3.Text != "")
                {
                    var idExamen = int.Parse(txtBox1.Text);
                    var Nombre = txtBox2.Text;
                    var Descripcion = txtBox3.Text;
                    var resultado = clientDll.ActualizarExamen(idExamen, Nombre, Descripcion);
                    txtBlock.Text = string.Join(", ", resultado);
                    ConsultarBDD();
                }
            }
            catch (Exception ex)
            {
                txtBlock.Text = "Ocurrió un error al actualizar los datos: " + ex.Message;
            }
        }


        public void clear (){
            txtBlock.Text = "";
        }
        public void clearGrid() 
        {
            dgView.ItemsSource = null;
            dgView.Items.Clear();
        }

        
    }
}
