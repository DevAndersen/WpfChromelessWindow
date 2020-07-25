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
    public partial class ChooseAWindow : Window
    {
        public ChooseAWindow()
        {
            InitializeComponent();
        }

        private void BtnMinimalist_Click(object sender, RoutedEventArgs e)
        {
            new MinimalistWindow().Show();
        }

        private void BtnDecorated_Click(object sender, RoutedEventArgs e)
        {
            new DecoratedWindow().Show();
        }
    }
}
