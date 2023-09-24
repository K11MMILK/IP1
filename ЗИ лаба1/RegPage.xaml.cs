using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();

        }

        private void backBtn_click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void regBtn_click(object sender, RoutedEventArgs e)
        {
            string connectionString = $"data source={UsersEntities.ConnectionString()};initial catalog=Users;integrated security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var user in UsersEntities.GetContext().People.ToList())
                {
                    if (user.login == loginBox.Text)
                    {
                        MessageBox.Show("Логин занят");
                        connection.Close();
                        return;
                    }
                }

                if (passBox.Password == null || loginBox.Text == null || passBox.Password == "" || loginBox.Text == "") 
                {
                    MessageBox.Show("Заполните все поля");
                    connection.Close ();
                    return;
                }

                SqlCommand cmd = new SqlCommand("", connection);
                cmd.CommandText = $"insert into People (login, pass, blocked, passLim) values ('{loginBox.Text}', '{passBox.Password.ToString()}', 0, 0)";
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Регистрация прошла успешно");
                    connection.Close();
                    this.NavigationService.GoBack();

                }
                catch { MessageBox.Show("Регистрация не удалась"); connection.Close(); return; }

                
            }

        }

    }
}
