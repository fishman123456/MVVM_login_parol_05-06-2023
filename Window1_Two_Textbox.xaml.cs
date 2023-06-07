using Microsoft.Data.Sqlite;

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
using System.Windows.Shapes;

namespace MVVM_login_parol_05_06_2023
{
    /// <summary>
    /// Логика взаимодействия для Window1_Two_Textbox.xaml
    /// </summary>
    public partial class Window1_Two_Textbox : Window
    {
        string dbConnectionsString = @"Data Sourse = databaseLogPass.db";
        public Window1_Two_Textbox()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            // создаем соединение с базой данных
            SqliteConnection sqliteCon = new SqliteConnection(dbConnectionsString);
            //открываем соединение с базой данных
            try
            {
                sqliteCon.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
    }
}
