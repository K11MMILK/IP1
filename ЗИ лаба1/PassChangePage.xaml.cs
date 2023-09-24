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
    /// Логика взаимодействия для PassChangePage.xaml
    /// </summary>
    public partial class PassChangePage : Page
    {
        string login,pass;
        int id;
        public PassChangePage(string login)
        {
            InitializeComponent();
            loginBox.Text = null;
            this.login = login;
            foreach(var p in UsersEntities.GetContext().People.ToList())
            {
                if (p.login == login) { pass = p.pass; id = p.id; break; }
            }
        }

        private void backBtn_click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void regBtn_click(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text == pass || pass==null)
            {
                if (passBox.Password != passBox_Copy.Password)
                {
                    MessageBox.Show("Повтор нового пароля введен неверно");
                    return;
                }
                else if(UsersEntities.GetContext().People.Find(id).passLim==1 && !(passBox.Password.ToString().Any(c => char.IsDigit(c))&& passBox.Password.ToString().Any(c => char.IsLetter(c))))
                {
                    MessageBox.Show("Пароль должен иметь хотя бы 1 цифру и букву"); return;
                }
                else
                {
                    string connectionString = $"data source={UsersEntities.ConnectionString()};initial catalog=Users;integrated security=True;";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand($"update  People set pass='{passBox.Password.ToString()}' where id={id}", connection);
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }

                    MessageBox.Show("Пароль успешно изменен");
                    this.NavigationService.GoBack();
                }
            }
            else
            {
                MessageBox.Show("Неверно введен старый пароль");
                return;
            } 
        }
    }
}
