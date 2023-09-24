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
    /// Логика взаимодействия для EnterPage.xaml
    /// </summary>
    public partial class EnterPage : Page
    {
        int wrongPass = 0;
        public EnterPage()
        {
            InitializeComponent();
            wrongPass = 0;
        }

        private void backBtn_click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void regBtn_click(object sender, RoutedEventArgs e)
        {
           UsersEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
           
                foreach (var user in UsersEntities.GetContext().People.ToList())
                {
                    if (user.login == loginBox.Text)
                    {
                    if (user.blocked==1) { MessageBox.Show("Вы заблокированы"); return; }
                    else
                    if (user.pass == null)
                        {
                           
                            this.NavigationService.Navigate(new UserPage(user.login));
                            return;
                        }
                        
                    }
                }


                        if (passBox.Password == null || loginBox.Text == null || passBox.Password == "" || loginBox.Text == "")
                {
                    MessageBox.Show("Заполните все поля");
                  
                    return;
                }

                foreach (var user in UsersEntities.GetContext().People.ToList())
                {
                    if (user.login == loginBox.Text)
                    {
                        if (user.pass == passBox.Password.ToString())
                        {
                            wrongPass = 0;
                            this.NavigationService.Navigate(new UserPage(loginBox.Text));
                            return;
                        }
                        else { MessageBox.Show("Неверный пароль");
                            passBox.Password = "";
                           
                            wrongPass++;
                            if(wrongPass == 3) System.Windows.Application.Current.Shutdown();
                            return;
                        }
                    }
                    
                }
                { MessageBox.Show("Неверный логин"); }
           

        }
    }
}
