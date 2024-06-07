using Microsoft.Data.SqlClient;
using System.Windows;

namespace Eesoft_YP
{
    public partial class App : Application
    {
        public SqlConnection cn = new SqlConnection(@"Data Source=MSI;Initial Catalog=Eesoft;Integrated Security=True;Trust Server Certificate=True;MultipleActiveResultSets=true");
        public SqlCommand cmd;
        public SqlDataReader dr;
        public App()
        {
            try
            {
                cn.Close();
                cn.Open();
            }catch (Exception ex)
            {
                MessageBox.Show("Ошибка при соединении с БД: ",ex.Message);
            }
        }
    }
}
