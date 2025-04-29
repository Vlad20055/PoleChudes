using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PoleChudes
{
    public class BarabanViewModel : INotifyPropertyChanged
    {
        private double _angle = 0;
        public double Angle
        {
            get => _angle;
            set
            {
                if (_angle != value)
                {
                    _angle = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
