using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
using ICS.Models.Com;

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
         var timer = new Timer(_sender =>{
                 var value = Global.KL_M4514Provider;
                 var state = value.CheckSerialPort(value);
                if (state.Status == RunStatus.Success)
                {
                    value.SetData();
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        WaterLevel.Text = value.waterLevelValue;
                        WaterTemperature.Text = value.waterTemperatureValue;
                    });
                }
            },null, 100, 1000);
        }


        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
        Close();
        }

        private void Minstate_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
     
        private void UIElement_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }   
        }
    }
}
