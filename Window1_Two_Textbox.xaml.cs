using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

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
        public class LogPar
        {
            public int Id { get; set; } 
            public string Log { get; set; } = "user";
            public string Par { get; set; } = "0000";
            public LogPar() { }
            public LogPar(string log, string par)
            {
                Log = log;
                Par = par;
            }
        }
        public class ApplicationContextDBLP : DbContext// выполняет функцию соединения с бд
        {
            public DbSet<LogPar> UserLogPass => Set< LogPar>();// непосредственно коллекция объектов. Именно она будет храниться в базе данных

            public ApplicationContextDBLP() => Database.EnsureCreated();// при создании этого объекта удостоверяяемся, что база данных существует и создаем, если нет
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)// говорим чем и куда сохранять наши данные
            {
                optionsBuilder.UseSqlite("Data Source=databaseLogPass.db");
            }
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            ApplicationContextDBLP one = new ApplicationContextDBLP();
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
