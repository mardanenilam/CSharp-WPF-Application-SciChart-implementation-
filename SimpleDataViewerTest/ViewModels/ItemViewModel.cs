using System.IO;
using System.Windows;
using System.Windows.Media;
using Prism.Events;
using Prism.Mvvm;

namespace SimpleDataViewerTest.ViewModels
{
    public class ItemViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private bool _isChecked;
        private string _filePath;
        private Color _dataSeriesColor;
        public ItemViewModel(bool isChecked, string filePath, IEventAggregator eventAggregator,Color dataSeriesColor)
        {
            _eventAggregator = eventAggregator;
            DataSeriesColor = dataSeriesColor;
            FilePath = filePath;
            IsChecked = isChecked;
        }
        public Color DataSeriesColor
        {
            get => _dataSeriesColor;
            set => SetProperty(ref _dataSeriesColor , value);
        }
        public string FilePath
        {
            get => _filePath;
            set
            {
                if(SetProperty(ref _filePath, value))  
                    RaisePropertyChanged(FileName);
            }
        }
        public string FileName => Path.GetFileName(FilePath);
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (SetProperty(ref _isChecked, value))
                    _eventAggregator.GetEvent<CheckedChangedEvent>().Publish((_isChecked, FilePath,DataSeriesColor));
            }
        }
    }
}
