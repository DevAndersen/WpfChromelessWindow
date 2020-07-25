using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DevAndersen.WpfChromelessWindow.Demo
{
    public partial class MinimalistWindow : ChromelessWindow
    {
        public MinimalistWindow() : base(30)
        {
            InitializeComponent();
        }

        private void BtnToggle_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }
    }
}
