using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
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


namespace ЗИ_лаба1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string connectionString = $"data source={UsersEntities.ConnectionString()};initial catalog=Users;integrated security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var user in UsersEntities.GetContext().People.ToList())
                {
                    if (user.login == "Admin")
                    {
                        
                        connection.Close();
                        frame.Navigate(new MainPage());
                    }
                }

                SqlCommand cmd = new SqlCommand("", connection);
                cmd.CommandText = $"insert into People (login, blocked, passLim) values ('Admin',0, 0)";
                try
                {
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    frame.Navigate(new MainPage());

                }
                catch { }


            }

            frame.Navigate(new MainPage());
            //frame.Navigate(new CnStrPage());
        }
    }
}
