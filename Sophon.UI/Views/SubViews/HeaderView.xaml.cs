using Prism.DryIoc;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sophon.UI.Views
{
    /// <summary>
    /// HeaderView.xaml 的交互逻辑
    /// </summary>
    public partial class HeaderView : UserControl
    {
        public HeaderView()
        {
            InitializeComponent();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var window = Window.GetWindow(this);

            if (window != null)
            {
                if (e.ButtonState == MouseButtonState.Pressed)
                {
                    window.DragMove();
                }

                if (e.ClickCount == 2)
                {
                    ToggleMaximize(window);
                }
            }
        }

        private void ToggleMaximize(Window window)
        {
            if (window.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Normal;
            }
            else
            {
                window.WindowState = WindowState.Maximized;
            }
        }

        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window != null)
            {
                window.WindowState = WindowState.Minimized;
            }
        }

        private void MaxButton_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window != null)
            {
                ToggleMaximize(window);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window != null)
            {
                window.Close();
                PrismApplication.Current.Shutdown();
            }
        }
    }
}