using System;
using System.Collections.Generic;
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

namespace Eesoft_YP
{
    public partial class ClientBlock : UserControl
    {
        public ClientBlock()
        {
            InitializeComponent();
        }

        private void Email_GotFocus(object sender, RoutedEventArgs e)
        {
            string email = Email.Text;

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                if (email != "Почта" && email != "")
                {
                    Email.BorderBrush = (SolidColorBrush)Application.Current.FindResource("red_0");
                }
            }
        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            string email = Email.Text;

            if (email == "Почта")
            {
                return;
            }

            if(email != "")
            {
                Email.BorderBrush = (SolidColorBrush)Application.Current.FindResource("green_0");
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") && email != "")
            {
                Email.BorderBrush = (SolidColorBrush)Application.Current.FindResource("red_0");
                return;
            }
        }

        private void Email_LostFocus(object sender, RoutedEventArgs e)
        {
            string email = Email.Text;

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") && email != "")
            {
                if (email != "Почта")
                {
                    Email.BorderBrush = (SolidColorBrush)Application.Current.FindResource("red_0");
                }
            }
        }
    }
}
