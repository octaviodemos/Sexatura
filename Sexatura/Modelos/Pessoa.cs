using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sexatura.Modelos
{
    public class Pessoa : INotifyPropertyChanged
    {
        private int _id;
        private string _sexo;
        private double _altura;

        public int Id { get; set; }
        public string Sexo
        {
            get { return _sexo; }
            set
            {
                if (_sexo != value)
                {
                    _sexo = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double Altura
        {
            get { return _altura; }
            set
            {
                if (_altura != value)
                {
                    _altura = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string MostrandoPessoas => $"{Sexo}, {Altura}m";

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public override string ToString()
        {
            return $"{Sexo}, {Altura}m";
        }
    }
}
