using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для MainWindow1.xaml
    /// </summary>
    public partial class MainWindow1 : Window
    {
        //public MainWindow1()
        //{
        //    InitializeComponent();
        //}
        ApplicationContextU db = new ApplicationContextU();
        public MainWindow1()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        // при загрузке окна
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // гарантируем, что база данных создана
            db.Database.EnsureCreated();
            // загружаем данные из БД
            db.Users.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.Users.Local.ToObservableCollection();
        }

        // добавление
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Window1 Window1 = new Window1(new User());
            if (Window1.ShowDialog() == true)
            {
                User User = Window1.User;
                db.Users.Add(User);
                db.SaveChanges();
            }
        }
        // редактирование
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            User? user = usersList.SelectedItem as User;
            // если ни одного объекта не выделено, выходим
            if (user is null) return;

            Window1 Window1 = new Window1(new User
            {
                Id = user.Id,
                Age = user.Age,
                Name = user.Name
            });

            if (Window1.ShowDialog() == true)
            {
                // получаем измененный объект
                user = db.Users.Find(Window1.User.Id);
                if (user != null)
                {
                    user.Age = Window1.User.Age;
                    user.Name = Window1.User.Name;
                    db.SaveChanges();
                    usersList.Items.Refresh();
                }
            }
        }
        // удаление
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            User? user = usersList.SelectedItem as User;
            // если ни одного объекта не выделено, выходим
            if (user is null) return;
            db.Users.Remove(user);
            db.SaveChanges();
        }
    }
}

