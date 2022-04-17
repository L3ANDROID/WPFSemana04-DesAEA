using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFSemana04
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection = new SqlConnection("data source = DESKTOP-KK9QJBS; initial catalog = School; Integrated Security = True;");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            List<Person> personas = new List<Person>();
            
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("BuscarPersonaNombre", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@FirstName";
                param.SqlDbType = SqlDbType.NVarChar;
                param.Value = "";

                cmd.Parameters.Add(param);

                //sin dataTable, usando dataSet
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    personas.Add(new Person
                    {
                        Id = Convert.ToInt32(reader["PersonID"]),
                        FirstName = Convert.ToString(reader["FirstName"]),
                        LastName = Convert.ToString(reader["LastName"])
                    });
                }

                dgvPeople.ItemsSource = personas;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
