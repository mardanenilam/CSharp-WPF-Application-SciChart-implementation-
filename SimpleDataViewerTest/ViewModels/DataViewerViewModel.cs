using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Model.DataSeries;


namespace SimpleDataViewerTest.ViewModels
{
    public class DataViewerViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private ItemViewModel _selectedFile;
        private string _chartTitle = "SciChart";
        private string _xAxisTitle = "XAxis";
        private string _yAxisTitle = "YAxis";
        private List<Color> colorList = new List<Color> {Colors.Aqua, Colors.Red , Colors.Lime, Colors.Teal,Colors.Maroon,Colors.Tomato,Colors.Yellow, Colors.MediumVioletRed, Colors.Navy,Colors.Fuchsia};

        public static LineRenderableSeriesViewModel CreateSeriesViewModel (List<double> x, List<double> y, Color seriesColor, string path)
        {
            var renderableSeriesViewmodel = new LineRenderableSeriesViewModel
            {
                DataSeries = GetXyDataSeries(x, y), 
                Stroke = seriesColor,
                Tag = path
            };
            return renderableSeriesViewmodel;
        }
        private static IXyDataSeries<double, double> GetXyDataSeries(List<double> x, List<double> y)
        {
            var dataSeries = new XyDataSeries<double, double>();
            dataSeries.Append(x, y);
            return dataSeries;
        }
        public DataViewerViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            AddFileCommand = new DelegateCommand(OnAddFile);
            FileSelectCommand = new DelegateCommand(OnFileSelect);
            DeselectAllCommand = new DelegateCommand(OnDeselectAll);
            RemoveSelectedCommand = new DelegateCommand(onRemoveSelected);
            ClearListCommand = new DelegateCommand(OnClearList);

            FileCollection = new ObservableCollection<ItemViewModel>();
            RenderableSeriesViewModels = new ObservableCollection<LineRenderableSeriesViewModel>();
            _eventAggregator.GetEvent<CheckedChangedEvent>().Subscribe(OnCheckedChanged);
            
        }
        public ObservableCollection<LineRenderableSeriesViewModel> RenderableSeriesViewModels { get; }

        public ICommand AddFileCommand { get; }
        public ICommand FileSelectCommand { get; }
        public ICommand DeselectAllCommand { get; }
        public ICommand RemoveSelectedCommand { get; }
        public ICommand ClearListCommand { get; }
        public string ChartTitle
        {
            get => _chartTitle;
            set => SetProperty(ref _chartTitle,value);
        }
        public string XAxisTitle
        {
            get => _xAxisTitle;
            set => SetProperty(ref _xAxisTitle, value);
        }
        public string YAxisTitle
        {
            get => _yAxisTitle;
            set => SetProperty(ref _yAxisTitle, value);
        }
        public ObservableCollection<ItemViewModel> FileCollection { get; set; }
        public ItemViewModel SelectedFile
        {
            get => _selectedFile;
            set
            {
                SetProperty(ref _selectedFile, value);
                MessageBox.Show("selected");
            }
        }
        private void OnAddFile()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "scan files (*.scn) | *.scn|dat-files (*.dat) | *.dat|csv files (*.csv)|*.csv|asc files (*.asc)|*.asc",
                FilterIndex = 2,
                Multiselect = true
            };
            if (openFileDialog.ShowDialog()==true)
            {
                var duplicateCounter = 0;
                foreach (var filePath in openFileDialog.FileNames)
                {
                    var duplicate = FileCollection.FirstOrDefault(model => model.FilePath == filePath);
                    if (duplicate != null) duplicateCounter++;
                    else
                    {
                        var colorCount = colorList.Count;
                        var fileCount = FileCollection.Count;
                        var result = fileCount % colorCount;
                        var dataSeriesColor = colorList[result];
                        ItemViewModel item= new ItemViewModel(true,filePath, _eventAggregator, dataSeriesColor);
                        FileCollection.Add(item);
                    }
                }
                if (duplicateCounter == 1)
                {
                    MessageBox.Show("Selected file is already in the list");
                }

                else if (duplicateCounter > 1)
                {
                    MessageBox.Show($"{duplicateCounter} selected files are already in the list");
                }
            }
        }
        private void OnFileSelect()
        {
            MessageBox.Show("Hellooo");
        }
        private void OnDeselectAll()
        {
            MessageBox.Show("You have clicked on 'DeselectAll' button!!");
        }

        private void onRemoveSelected()
        {
            MessageBox.Show("Removed selected files");
        }
        private void OnClearList()
        {
            MessageBox.Show("List Cleared");
        }
        private void OnCheckedChanged( (bool isChecked, string path,Color color) tuple)
        {
            if (tuple.isChecked)
            {
                var data = FileHelper.ParseFile(tuple.path);
                var result=CreateSeriesViewModel(data.xValues,data.yValues,tuple.color, tuple.path);
                RenderableSeriesViewModels.Add(result);
            }
            else
            {
                var seriesToRemove = RenderableSeriesViewModels.FirstOrDefault(series=>(string)series.Tag==tuple.path);
                if (seriesToRemove != null)
                {
                    RenderableSeriesViewModels.Remove(seriesToRemove);
                }
            }
        }
    }
}
     