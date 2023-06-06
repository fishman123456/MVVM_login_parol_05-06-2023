using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_login_parol_05_06_2023
{
    public class ViewModel : INotifyPropertyChanged
    {
        Model model; // объект модели
        

        public ViewModel() // конструктор
        {
            this.model = new Model();
        }
        public List<UserPassword> UserLogPass
        {
            get { return this.model.UserLoginPasswords(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }

}
