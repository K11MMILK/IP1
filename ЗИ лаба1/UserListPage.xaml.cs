using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для UserListPage.xaml
    /// </summary>
    public partial class UserListPage : Page
    {
        public UserListPage()
        {
            InitializeComponent();
            UsersEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            DGridItems.ItemsSource = UsersEntities.GetContext().People.ToList();

        }

        private void Users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
      
    

        private void backBtn_click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void passLimBtn_Click(object sender, RoutedEventArgs e)
        {
            int id = ((sender as Button).DataContext as People).id;

            string connectionString = $"data source={UsersEntities.ConnectionString()};initial catalog=Users;integrated security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (((sender as Button).DataContext as People).passLim == 0)
                {
                    SqlCommand cmd = new SqlCommand($"update People set passLim=1 where id ={id}", connection);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand($"update People set passLim=0 where id ={id}", connection);
                    cmd.ExecuteNonQuery();
                }
                UsersEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridItems.ItemsSource = UsersEntities.GetContext().People.ToList();
                connection.Close();


            }

        }

        private void BanBtn_Click(object sender, RoutedEventArgs e)
        {
            int id = ((sender as Button).DataContext as People).id;
            if (((sender as Button).DataContext as People).login == "Admin"){ MessageBox.Show("Ты дурак?");return; }
            string connectionString = $"data source={UsersEntities.ConnectionString()};initial catalog=Users;integrated security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (((sender as Button).DataContext as People).blocked == 0)
                {
                    SqlCommand cmd = new SqlCommand($"update  People set blocked=1 where id ={id}", connection);
                    cmd.ExecuteNonQuery();
                }
                else {
                    SqlCommand cmd = new SqlCommand($"update People set blocked=0 where id ={id}", connection);
                    cmd.ExecuteNonQuery();
                }
                UsersEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridItems.ItemsSource = UsersEntities.GetContext().People.ToList();
                connection.Close();


            }
        }


        private void AddBtn_click(object sender, RoutedEventArgs e)
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

                if ( loginBox.Text == null || loginBox.Text == "")
                {
                    MessageBox.Show("Заполните полe");
                    connection.Close();
                    return;
                }

                SqlCommand cmd = new SqlCommand("", connection);
                cmd.CommandText = $"insert into People (login, blocked, passLim) values ('{loginBox.Text}',0,0)";
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Регистрация прошла успешно");
                    connection.Close();
                    UsersEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                    DGridItems.ItemsSource = UsersEntities.GetContext().People.ToList();
                    return;
                }
                catch { MessageBox.Show("Регистрация не удалась"); connection.Close(); return; }


            }
        }
    }
}
