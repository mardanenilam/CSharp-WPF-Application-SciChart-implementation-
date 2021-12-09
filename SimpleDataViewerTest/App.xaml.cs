using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Prism.Ioc;
using Prism.Unity;
using SciChart.Charting.Visuals;
using SimpleDataViewerTest.Views;

namespace SimpleDataViewerTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override Window CreateShell()
        {
            var w = Container.Resolve<DataViewerView>();
            return w;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            SciChartSurface.SetRuntimeLicenseKey("a0l0rR5Gdee4Dlvlbpp6Lo9dt+SW/6vfx/NefakvgYtc/he8pYEUCEp2fTDZqvkW+xq27isslXJTj/WBbwp0NS7Bo0yfFLIxLP/2hVha+Yqf1N04TSwlwjsFYkzTz/Z1YjOM9NThX4Xu4Ysb6I2UNfVvKcqq12CJUponXDEANx24Ciouvk+C5ys1DEVyF9JHbAG2puVV693sAIaMrl2f8SiKp96HImw0LUSMn9WicWGQ73EMYfDBEwKne6dAxYnSmOCH1InUbSEj0wOGfuR7t6/vE6BkL7LfSmrSrvzheYVFIFEKRTH+qG2rsQvy1ks+jc3KeeWN1+qvBejAyV9HkwZhGjGOp4q+22sq38K3hBxuM6KDMfJXMgzdoURh1qstOB/nkT9Z7ggHW+TH8m+P7Kb1c5PsKsNpQfDmqEPXKgjtxZ2EW/gnHP4k5hZ25d+5dSiSQz1VlEtvbpdlP0+C4oSbaVKXRZcuCkoVm/GjLo50yuBR3m1X");
            base.OnStartup(e);
        }
    }
}
