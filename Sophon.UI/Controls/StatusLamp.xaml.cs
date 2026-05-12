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

namespace Sophon.UI.Controls
{
    /// <summary>
    /// StatusLamp.xaml 的交互逻辑
    /// </summary>
    public partial class StatusLamp : UserControl
    {
        public StatusLamp()
        {
            InitializeComponent();
        }

        public double Size
        {
            get { return (double)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }
        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register("Size", typeof(double), typeof(StatusLamp), new PropertyMetadata(20.0));

        public LampStatus Status
        {
            get { return (LampStatus)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(LampStatus), typeof(StatusLamp),
                new PropertyMetadata(LampStatus.Off, OnStatusChanged));

        private static void OnStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var lamp = d as StatusLamp;
            switch ((LampStatus)e.NewValue)
            {
                case LampStatus.Running:
                    lamp.UpdateLamp(Colors.LimeGreen, Colors.Green, true);
                    break;
                case LampStatus.Alarm:
                    lamp.UpdateLamp(Colors.Tomato, Colors.Red, true);
                    break;
                case LampStatus.Warning:
                    lamp.UpdateLamp(Colors.Yellow, Colors.Orange, true);
                    break;
                case LampStatus.Off:
                default:
                    lamp.UpdateLamp(Colors.White, Colors.LightGray, false);
                    break;
            }
        }

        private void UpdateLamp(Color centerColor, Color edgeColor, bool isGlow)
        {
            ColorStop2.Color = centerColor;
            ColorStop3.Color = edgeColor;
            LampShadow.Color = isGlow ? centerColor : Colors.Transparent;
        }
    }

    public enum LampStatus
    {
        Off,      // 灰色
        Running,  // 绿色
        Alarm,    // 红色
        Warning   // 黄色
    }
}
