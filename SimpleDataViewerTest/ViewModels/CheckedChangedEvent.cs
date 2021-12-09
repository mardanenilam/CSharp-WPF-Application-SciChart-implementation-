using System.Windows.Media;
using Prism.Events;

namespace SimpleDataViewerTest.ViewModels
{
    public class CheckedChangedEvent : PubSubEvent<(bool isChecked, string path, Color color)>
    {

    }
}