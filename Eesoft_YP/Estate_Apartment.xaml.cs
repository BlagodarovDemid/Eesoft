using Microsoft.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Eesoft_YP
{
    public partial class Estate_Apartment : UserControl
    {
        public Estate_Apartment()
        {
            InitializeComponent();
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

            try
            {
                ((App)Application.Current).cmd = new SqlCommand("SELECT Name FROM Districts WHERE ID='" + District_ID.Text + "'", ((App)Application.Current).cn);

                ComboBoxItem item = new ComboBoxItem();
                item.Foreground = (SolidColorBrush)Application.Current.FindResource("grey_5");
                item.VerticalContentAlignment = VerticalAlignment.Center;
                item.FontFamily = (FontFamily)Application.Current.FindResource("Roboto-Regular");
                item.Content = ((App)Application.Current).cmd.ExecuteScalar();
                
                District.Items.Add(item);
                District.SelectedValue = item;

            }
            catch
            {
                ((App)Application.Current).dr.Close();
                ((App)Application.Current).cmd = new SqlCommand("SELECT Name FROM Districts WHERE ID='" + District_ID.Text + "'", ((App)Application.Current).cn);

                ComboBoxItem item = new ComboBoxItem();
                item.Foreground = (SolidColorBrush)Application.Current.FindResource("grey_5");
                item.VerticalContentAlignment = VerticalAlignment.Center;
                item.FontFamily = (FontFamily)Application.Current.FindResource("Roboto-Regular");
                item.Content = ((App)Application.Current).cmd.ExecuteScalar();

                District.Items.Add(item);
                District.SelectedValue = item;
            }
        }
    }
}