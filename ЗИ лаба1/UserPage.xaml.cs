using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        string login;
        public UserPage(string login)
        {
            InitializeComponent();
            this.login = login;
            if (login != "Admin")
            {
                peopleCheckListBtn.Visibility = Visibility.Collapsed;
            }
        }

        private void passChangeBtn_click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PassChangePage(login));
        }

        private void peopleCheckListBtn_click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new UserListPage());
        }

        private void backBtn_click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
