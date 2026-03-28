using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AvaloniaApp.Models;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly UniversalSet<string> _mySet;

        private string _inputValue;
        private string _statusMessage;
        private ObservableCollection<string> _elementsList;

        public MainWindowViewModel()
        {
            _mySet = new UniversalSet<string>();
            _inputValue = string.Empty;
            _statusMessage = "Множество создано. Ожидание ввода.";
            _elementsList = new ObservableCollection<string>();

            AddCommand = new RelayCommand(AddElement);
            RemoveCommand = new RelayCommand(RemoveElement);
            ClearCommand = new RelayCommand(ClearSet);
            CheckCommand = new RelayCommand(CheckElement);
        }

        public string InputValue
        {
            get => _inputValue;
            set => SetProperty(ref _inputValue, value);
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public ObservableCollection<string> ElementsList
        {
            get => _elementsList;
            set => SetProperty(ref _elementsList, value);
        }

        public int SetCount => _mySet.Count;
        public bool IsSetEmpty => _mySet.IsEmpty;

        public IRelayCommand AddCommand { get; }
        public IRelayCommand RemoveCommand { get; }
        public IRelayCommand ClearCommand { get; }
        public IRelayCommand CheckCommand { get; }

        private void AddElement()
        {
            if (string.IsNullOrWhiteSpace(InputValue))
            {
                StatusMessage = "Ошибка: Введите значение.";
                return;
            }

            bool wasAdded = !_mySet.Contains(InputValue);
            _mySet.Add(InputValue);

            UpdateView();

            StatusMessage = wasAdded
                ? $"Элемент '{InputValue}' добавлен."
                : $"Элемент '{InputValue}' уже существует (не добавлен).";

            InputValue = string.Empty;
        }

        private void RemoveElement()
        {
            if (string.IsNullOrWhiteSpace(InputValue))
            {
                StatusMessage = "Ошибка: Введите значение для удаления.";
                return;
            }

            bool wasRemoved = _mySet.Contains(InputValue);
            _mySet.Remove(InputValue);

            UpdateView();

            StatusMessage = wasRemoved
                ? $"Элемент '{InputValue}' удален."
                : $"Элемента '{InputValue}' не было во множестве.";

            InputValue = string.Empty;
        }

        private void ClearSet()
        {
            _mySet.Clear();
            UpdateView();
            StatusMessage = "Множество очищено.";
        }

        private void CheckElement()
        {
            if (string.IsNullOrWhiteSpace(InputValue))
            {
                StatusMessage = "Ошибка: Введите значение для проверки.";
                return;
            }

            bool exists = _mySet.Contains(InputValue); 

            StatusMessage = exists
                ? $"✓ Элемент '{InputValue}' НАЙДЕН во множестве."
                : $"✗ Элемент '{InputValue}' НЕ найден во множестве.";
        }

        private void UpdateView()
        {
            ElementsList.Clear();
            foreach (var item in _mySet.GetElements())
            {
                ElementsList.Add(item);
            }

            OnPropertyChanged(nameof(SetCount));
            OnPropertyChanged(nameof(IsSetEmpty));
        }
    }
}