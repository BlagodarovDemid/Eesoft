using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Data.SqlClient;

namespace Eesoft_YP
{
    public partial class PersonList : Window
    {
        string SearchText = String.Empty;
        public PersonList()
        {
            InitializeComponent();
        }

        private void Refresh()
        {
            string AddingText = " WHERE FirstName LIKE '%" + SearchText + "%' or MiddleName LIKE '%" + SearchText + "%' or LastName LIKE '%" + SearchText + "%'";
            if (Role.SelectedIndex > 0)
            {
                People.Children.Clear();
                try
                {
                    ((App)Application.Current).cmd = new SqlCommand("SELECT * FROM Client" + AddingText, ((App)Application.Current).cn);
                    ((App)Application.Current).dr = ((App)Application.Current).cmd.ExecuteReader();
                }
                catch 
                {
                    ((App)Application.Current).dr.Close();
                    ((App)Application.Current).cmd = new SqlCommand("SELECT * FROM Client" + AddingText, ((App)Application.Current).cn);
                    ((App)Application.Current).dr = ((App)Application.Current).cmd.ExecuteReader();
                }

                while (((App)Application.Current).dr.Read())
                {
                    ClientBlock client = new ClientBlock();
                    client.ID.Text = ((App)Application.Current).dr["ID"].ToString();
                    client.FirstName.Text = ((App)Application.Current).dr["FirstName"].ToString();
                    client.MiddleName.Text = ((App)Application.Current).dr["MiddleName"].ToString();
                    client.LastName.Text = ((App)Application.Current).dr["LastName"].ToString();
                    client.Phone.Text = ((App)Application.Current).dr["Phone"].ToString();
                    client.Email.Text = ((App)Application.Current).dr["Email"].ToString();

                    People.Children.Add(client);
                }
            }
            else 
            {
                People.Children.Clear();
                try
                {
                    ((App)Application.Current).cmd = new SqlCommand("SELECT * FROM Agent" + AddingText, ((App)Application.Current).cn);
                    ((App)Application.Current).dr = ((App)Application.Current).cmd.ExecuteReader();
                }
                catch
                {
                    ((App)Application.Current).dr.Close();
                    ((App)Application.Current).cmd = new SqlCommand("SELECT * FROM Agent" + AddingText, ((App)Application.Current).cn);
                    ((App)Application.Current).dr = ((App)Application.Current).cmd.ExecuteReader();
                }

                while (((App)Application.Current).dr.Read())
                {
                    AgentBlock agent = new AgentBlock();
                    agent.ID.Text = ((App)Application.Current).dr["ID"].ToString();
                    agent.FirstName.Text = ((App)Application.Current).dr["FirstName"].ToString();
                    agent.MiddleName.Text = ((App)Application.Current).dr["MiddleName"].ToString();
                    agent.LastName.Text = ((App)Application.Current).dr["LastName"].ToString();
                    agent.DealShare.Text = ((App)Application.Current).dr["DealShare"].ToString();

                    People.Children.Add(agent);
                }
            }
            ((App)Application.Current).dr.Close();
        }

        private void People_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Wrap_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void DragWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Switch_Person(object sender, RoutedEventArgs e)
        {
            if (People != null)
            {
                if (Role.SelectedIndex == 0)
                {
                    Deal_Phone.Text = "Доля от комиссии";
                    Email.Visibility = Visibility.Hidden;
                }
                else
                {
                    Deal_Phone.Text = "Телефон";
                    Email.Visibility = Visibility.Visible;
                }

                Refresh();
            }
        }

        private void EstateList_Click(object sender, RoutedEventArgs e)
        {
            EstateList estateList = new EstateList();
            this.Close();
            estateList.Show();
        }
        private void DealsList_Click(object sender, RoutedEventArgs e)
        {
            DealsList dealsList = new DealsList();
            this.Close();
            dealsList.Show();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchText = Search.Text;
            if(SearchText == "") { SearchText = string.Empty; }

            Refresh();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string first = FirstName.Text;
            string middle = MiddleName.Text;
            string last = LastName.Text;
            string deal_phone = Deal_Phone.Text;
            string email = Email.Text;

            if(Role.SelectedIndex == 0)
            {
                if (first == "" || middle == "" || last == "") { MessageBox.Show("Заполните поля ФИО", "Ошибка"); return; }
                if (Convert.ToInt32(deal_phone) < 0 || Convert.ToInt32(deal_phone) > 100) { MessageBox.Show("Поле 'Доля от комиссии' не должно быть меньше 0 или превышать значение 100","Ошибка"); return; }
                
                try
                {
                    ((App)Application.Current).cmd = new SqlCommand("INSERT INTO Agent VALUES('" + first + "','" + middle + "','" + last + "','" + deal_phone + "')",((App)Application.Current).cn);
                    ((App)Application.Current).cmd.ExecuteNonQuery();
                }
                catch
                {
                    ((App)Application.Current).dr.Close();
                    ((App)Application.Current).cmd = new SqlCommand("INSERT INTO Agent VALUES('" + first + "','" + middle + "','" + last + "','" + deal_phone + "')", ((App)Application.Current).cn);
                    ((App)Application.Current).cmd.ExecuteNonQuery();
                }
            }
            else
            {
                if( (deal_phone == "" && email == "") || (deal_phone == "Телефон" && email == "Почта") ) { MessageBox.Show("Введите почту или телефон", "Ошибка"); return; }
                if(deal_phone == "Телефон") { deal_phone = String.Empty; }
                if(email == "Почта") { email = String.Empty; }
                if (email != "" && email != "Почта")
                {
                    if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    {
                        MessageBox.Show("Неверный формат почты", "Ошибка"); 
                        return;
                    }
                }

                try
                {
                    ((App)Application.Current).cmd = new SqlCommand("INSERT INTO Client VALUES('" + first + "','" + middle + "','" + last + "','" + deal_phone + "','" + email + "')", ((App)Application.Current).cn);
                    ((App)Application.Current).cmd.ExecuteNonQuery();
                }
                catch
                {
                    ((App)Application.Current).dr.Close();
                    ((App)Application.Current).cmd = new SqlCommand("INSERT INTO Client VALUES('" + first + "','" + middle + "','" + last + "','" + deal_phone + "','" + email + "')", ((App)Application.Current).cn);
                    ((App)Application.Current).cmd.ExecuteNonQuery();
                }
            }
        }

        private void FirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (FirstName.Text == "Имя")
            {
                FirstName.Text = string.Empty;
            }
        }
        private void MiddleName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (MiddleName.Text == "Фамилия")
            {
                MiddleName.Text = string.Empty;
            }
        }
        private void LastName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (LastName.Text == "Отчество")
            {
                LastName.Text = string.Empty;
            }
        }
        private void DealPhone_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Deal_Phone.Text == "Доля от комиссии")
            {
                Deal_Phone.Text = string.Empty;
            }
        }
        private void FirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (FirstName.Text == "")
            {
                FirstName.Text = "Имя";
            }
        }
        private void MiddleName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (MiddleName.Text == "")
            {
                MiddleName.Text = "Фамилия";
            }
        }
        private void LastName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (LastName.Text == "")
            {
                LastName.Text = "Отчество";
            }
        }
        private void DealPhone_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Deal_Phone.Text == "")
            {
                Deal_Phone.Text = "Доля от комиссии";
            }
        }
        private void Email_GotFocus(object sender, RoutedEventArgs e)
        {
            string email = Email.Text;
            if(email == "Почта") { Email.Text = string.Empty; return; }

            Notification.Visibility = Visibility.Hidden;
            Email.VerticalAlignment = VerticalAlignment.Center;

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                Email.BorderBrush = (SolidColorBrush)Application.Current.FindResource("red_0");
            }
        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            string email = Email.Text;

            if (email == "Почта")
            {
                return;
            }

            Notification.Visibility = Visibility.Visible;
            Notification.Content = "Всё прекрасно!";
            Notification.Foreground = (SolidColorBrush)Application.Current.FindResource("green_0");
            Email.BorderBrush = (SolidColorBrush)Application.Current.FindResource("green_0");
            Email.VerticalAlignment = VerticalAlignment.Top;

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                Notification.Content = "Всё не прекрасно!";
                Email.BorderBrush = (SolidColorBrush)Application.Current.FindResource("red_0");
                Notification.Foreground = (SolidColorBrush)Application.Current.FindResource("red_0");
                return;
            }
        }

        private void Email_LostFocus(object sender, RoutedEventArgs e)
        {
            string email = Email.Text;
            if(email == "") { Email.Text = "Почта"; return; }

            Notification.Visibility = Visibility.Hidden;
            Email.VerticalAlignment = VerticalAlignment.Center;

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                if (email != "Почта")
                {
                    Email.BorderBrush = (SolidColorBrush)Application.Current.FindResource("red_0");
                }
            }
        }
    }
}