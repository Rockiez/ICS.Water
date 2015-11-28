using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ICS.Common;
using ICS.Acquisition;
namespace ICS.Water
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ResultEntity state = Global.KL_M4514Provider.CheckSerialPort(Global.KL_M4514Provider);

        bool heatstate;

        private void controlHeatingButtor (object sender, MouseButtonEventArgs e)
        {
            if (state.Status == RunStatus.Success)
            {
                var commond = Global.ADAM4150Provider;
                    if (commond.OnOff(heatstate ? ADAM4150FuncID.OffSocket1 : ADAM4150FuncID.OnSocket1))
                    {
                        textOnOff.Text = heatstate ? "STOP" : "START" ;
                        heatstate = !heatstate;
                    }
                    else
                    {
                        MessageBox.Show("操作失败");
                    }
            }
        }
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var timer = new LazyTimer(_sender =>
            {
                var t = (LazyTimer) _sender[0];

                if (state.Status == RunStatus.Success)
                {
                    var Value = Global.KL_M4514Provider;
                    Value.SetData();
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        WaterLevel.Text = Value.waterLevelValue;
                        WaterTemperature.Text = Value.waterTemperatureValue;
                    });
                }

                t.Reset();
            }, 100, 1000);
        }


        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
        Close();
        }

        private void Minstate_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
