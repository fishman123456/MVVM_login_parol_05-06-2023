using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MVVM_login_parol_05_06_2023
{
    public class UserPassword : INotifyPropertyChanged
    {
        public int Id { get; set; } //  Id необходим для соханения его в бд
        public string Password { get; set; }
        public string Username { get; set; }
        public string PasswordBind
        {
            get { return Password; }
            set
            {
                Password = value;
                OnPropertyChanged("PasswordBind");// триггеим событие - вызываем обновление во всех view событие
            }
        }
        public string UsernameBind
        {
            get { return Username; }
            set
            {
                Username = value;
                OnPropertyChanged("UsernameBind");// триггеим событие - вызываем обновление во всех view событие
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    public class ApplicationContext : DbContext// выполняет функцию соединения с бд
    {
        public DbSet<UserPassword> UserLogPass => Set<UserPassword>();// непосредственно коллекция объектов. Именно она будет храниться в базе данных

        public ApplicationContext() => Database.EnsureCreated();// при создании этого объекта удостоверяяемся, что база данных существует и создаем, если нет
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)// говорим чем и куда сохранять наши данные
        {
            optionsBuilder.UseSqlite("Data Source=UserLoginPasswords.db");
        }
    }
    class Model : INotifyPropertyChanged
    {
        private ApplicationContext db;// контекст для работы с бд. Коллекция собак в нем

        public void AssembleNewUserLoginPasswords() // стандартный набор собак
        {
            AddUserLoginPassword(new UserPassword { Username = "Петр", Password ="0000" }) ;
            //AddDog(new Dog { DogName = "Волчья собака Сарлоса" });
            //AddDog(new Dog { DogName = "Шиба-ину" });
            //AddDog(new Dog { DogName = "Сибирский хаски" });
        }

        public List<UserPassword> UserLoginPasswords() // метод возвращает текущее, коллекции собак
        {
            return this.db.UserLogPass.ToList<UserPassword>();
        }
        // сравниваем значение в текстбоксе
        public void AddUserLoginPassword(UserPassword UserLogPass) // добавить
        {
            this.db.UserLogPass.Add(UserLogPass);
            UserPassworSync();
        }

        //public void RemoveDog(Dog dog) // Удалить
        //{
        //    this.db.Dogs.Remove(dog);
        //    Dogs_sync();
        //}

        public void UserPassworSync() // Сохраняем изменения в бд
        {
            this.db.SaveChanges();
            //MessageBox.Show("Объекты сохранены");
            OnPropertyChanged("UserLogPass");// уведомляем отображение о изменении 
        }


        public Model()
        {
           
            this.db = new ApplicationContext();// создаем контекст (сессию для работы с бд)
            this.AssembleNewUserLoginPasswords();
            Debug.WriteLine("Объектов в базе данных: " + this.db.UserLogPass.ToList<UserPassword>().Count.ToString());
        }


        public event PropertyChangedEventHandler PropertyChanged;// событие для уведомления вида пользователя
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
