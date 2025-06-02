using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Domain.Entities
{
    public class SuperGameChoicePanel : INotifyPropertyChanged
    {
        private bool _isVisible = false;
        public string Description { get; set; } = "Желаете сыграть в СУПЕР-ИГРУ?";
        public string SuperGameText { get; set; } = "Играть!";
        public string RefuseText { get; set; } = "Отказаться!";

        public bool IsVisible { get => _isVisible; set { if (_isVisible != value) { _isVisible = value; OnPropertyChanged(); } } }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
