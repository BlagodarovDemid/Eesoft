using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Data.SqlClient;

namespace Eesoft_YP
{
    public partial class EstateList : Window
    {
        string SearchText = string.Empty;
        public EstateList()
        {
            InitializeComponent();
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

        private void PersonList_Click(object sender, RoutedEventArgs e)
        {
            PersonList personList = new PersonList();
            this.Close();
            personList.Show();
        }
        private void DealsList_Click(object sender, RoutedEventArgs e)
        {
            DealsList dealsList = new DealsList();
            this.Close();
            dealsList.Show();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchText = Search.Text;
            if (SearchText == "") { SearchText = string.Empty; }

            Refresh();
        }
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        private void Refresh()
        {
            string AddingText = " WHERE Address_City LIKE '%" + SearchText + "%'";
            Estate.Children.Clear();
            try
            {
                ((App)Application.Current).cmd = new SqlCommand("SELECT * FROM Estate" + AddingText, ((App)Application.Current).cn);
                ((App)Application.Current).dr = ((App)Application.Current).cmd.ExecuteReader();
                SqlDataAdapter adapter = new SqlDataAdapter();
            }
            catch
            {
                ((App)Application.Current).dr.Close();
                ((App)Application.Current).cmd = new SqlCommand("SELECT * FROM Estate" + AddingText, ((App)Application.Current).cn);
                ((App)Application.Current).dr = ((App)Application.Current).cmd.ExecuteReader();
            }
            while (((App)Application.Current).dr.Read())
            {
                if (Type.SelectedIndex == 0)
                {
                    Estate_Apartment estate = new Estate_Apartment();
                    estate.ID.Text = ((App)Application.Current).dr["ID"].ToString();
                    estate.City.Text = ((App)Application.Current).dr["Address_City"].ToString();
                    estate.District_ID.Text = ((App)Application.Current).dr["District_ID"].ToString();
                    estate.House.Text = ((App)Application.Current).dr["Address_House"].ToString();
                    estate.Number.Text = ((App)Application.Current).dr["Address_Number"].ToString();
                    estate.Latitude.Text = ((App)Application.Current).dr["Coordinate_latitude"].ToString();
                    estate.Longitude.Text = ((App)Application.Current).dr["Coordinate_longitude"].ToString();
                    estate.Floor.Text = ((App)Application.Current).dr["Floor"].ToString();
                    estate.Rooms.Text = ((App)Application.Current).dr["Rooms"].ToString();
                    estate.TotalArea.Text = ((App)Application.Current).dr["TotalArea"].ToString();

                    Estate.Children.Add(estate);
                }
                if (Type.SelectedIndex == 1)
                {
                    Estate_House estate = new Estate_House();
                    estate.ID.Text = ((App)Application.Current).dr["ID"].ToString();
                    estate.City.Text = ((App)Application.Current).dr["Address_City"].ToString();
                    estate.District_ID.Text = ((App)Application.Current).dr["District_ID"].ToString();
                    estate.House.Text = ((App)Application.Current).dr["Address_House"].ToString();
                    estate.Latitude.Text = ((App)Application.Current).dr["Coordinate_latitude"].ToString();
                    estate.Longitude.Text = ((App)Application.Current).dr["Coordinate_longitude"].ToString();
                    estate.TotalFloors.Text = ((App)Application.Current).dr["Floor"].ToString();
                    estate.Rooms.Text = ((App)Application.Current).dr["Rooms"].ToString();
                    estate.TotalArea.Text = ((App)Application.Current).dr["TotalArea"].ToString();

                    Estate.Children.Add(estate);
                }
                if (Type.SelectedIndex == 2)
                {
                    Estate_Lands estate = new Estate_Lands();
                    estate.ID.Text = ((App)Application.Current).dr["ID"].ToString();
                    estate.City.Text = ((App)Application.Current).dr["Address_City"].ToString();
                    estate.District_ID.Text = ((App)Application.Current).dr["District_ID"].ToString();
                    estate.Latitude.Text = ((App)Application.Current).dr["Coordinate_latitude"].ToString();
                    estate.Longitude.Text = ((App)Application.Current).dr["Coordinate_longitude"].ToString();
                    estate.TotalArea.Text = ((App)Application.Current).dr["TotalArea"].ToString();

                    Estate.Children.Add(estate);
                }
            }
        }
        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Estate != null)
            {
                if (Type.SelectedIndex == 0)
                {
                    House.IsEnabled = true;
                    Number.IsEnabled = true;
                    Floor.IsEnabled = true;
                    Rooms.IsEnabled = true;
                    TotalFloor.IsEnabled = false;
                }
                if (Type.SelectedIndex == 1)
                {
                    House.IsEnabled = true;
                    Number.IsEnabled = false;
                    Floor.IsEnabled = false;
                    Rooms.IsEnabled = true;
                    TotalFloor.IsEnabled = true;
                }
                if (Type.SelectedIndex == 2)
                {
                    House.IsEnabled = false;
                    Number.IsEnabled = false;
                    Floor.IsEnabled = false;
                    Rooms.IsEnabled = false;
                    TotalFloor.IsEnabled = false;
                }
                Refresh();
            }
        }
        private void Estate_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void District_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ((App)Application.Current).cmd = new SqlCommand("SELECT * FROM Districts", ((App)Application.Current).cn);
                ((App)Application.Current).dr = ((App)Application.Current).cmd.ExecuteReader();
            }
            catch
            {
                ((App)Application.Current).dr.Close();
                ((App)Application.Current).cmd = new SqlCommand("SELECT * FROM Districts", ((App)Application.Current).cn);
                ((App)Application.Current).dr = ((App)Application.Current).cmd.ExecuteReader();
            }
            while (((App)Application.Current).dr.Read())
            {
                ComboBoxItem boxItem = new ComboBoxItem();
                boxItem.Foreground = (SolidColorBrush)Application.Current.FindResource("grey_5");
                boxItem.VerticalContentAlignment = VerticalAlignment.Center;
                boxItem.FontFamily = (FontFamily)Application.Current.FindResource("Roboto-Regular");
                boxItem.Content = ((App)Application.Current).dr["Name"].ToString();

                District.Items.Add(boxItem);
            }

            ((App)Application.Current).dr.Close();
        }

        private void Adding_Click(object sender, RoutedEventArgs e)
        {
            if(Latitude.Text != string.Empty)
            {

            }

            if(Type.SelectedIndex == 0)
            {

            }
            if (Type.SelectedIndex == 1)
            {

            }
            if (Type.SelectedIndex == 2)
            {

            }
        }

        private void City_GotFocus(object sender, RoutedEventArgs e)
        {
            if (City.Text == "Город")
            {
                City.Text = string.Empty;
            }
        }
        private void City_LostFocus(object sender, RoutedEventArgs e)
        {
            if (City.Text == "")
            {
                City.Text = "Город";
            }
        }
        private void House_GotFocus(object sender, RoutedEventArgs e)
        {
            if (House.Text == "Адрес дома")
            {
                House.Text = string.Empty;
            }
        }
        private void House_LostFocus(object sender, RoutedEventArgs e)
        {
            if (House.Text == "")
            {
                House.Text = "Адрес дома";
            }
        }
        private void Number_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Number.Text == "Номер")
            {
                Number.Text = string.Empty;
            }
        }
        private void Number_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Number.Text == "")
            {
                Number.Text = "Номер";
            }
        }
        private void Latitude_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Latitude.Text == "Широта")
            {
                Latitude.Text = string.Empty;
            }
        }
        private void Latitude_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Latitude.Text == "")
            {
                Latitude.Text = "Широта";
            }
        }
        private void Longitude_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Longitude.Text == "Долгота")
            {
                Longitude.Text = string.Empty;
            }
        }
        private void Longitude_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Longitude.Text == "")
            {
                Longitude.Text = "Долгота";
            }
        }
        private void Floor_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Floor.Text == "Этаж")
            {
                Floor.Text = string.Empty;
            }
        }
        private void Floor_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Floor.Text == "")
            {
                Floor.Text = "Этаж";
            }
        }
        private void TotalFloor_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TotalFloor.Text == "Этажность")
            {
                TotalFloor.Text = string.Empty;
            }
        }
        private void TotalFloor_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TotalFloor.Text == "")
            {
                TotalFloor.Text = "Этажность";
            }
        }
        private void Rooms_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Rooms.Text == "Количество комнат")
            {
                Rooms.Text = string.Empty;
            }
        }
        private void Rooms_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Rooms.Text == "")
            {
                Rooms.Text = "Количество комнат";
            }
        }
        private void TotalArea_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TotalArea.Text == "Площадь")
            {
                TotalArea.Text = string.Empty;
            }
        }
        private void TotalArea_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TotalArea.Text == "")
            {
                TotalArea.Text = "Площадь";
            }
        }
    }
}
