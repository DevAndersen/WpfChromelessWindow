using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DevAndersen.WpfChromelessWindow
{
    public class ChromelessWindow : Window
    {
        protected readonly ChromelessWindowViewModel viewModel;

        /// <summary>
        /// The height of the window's title bar.
        /// This is used to determine how much of the window's top can be used for dragging the window around, toggling resizable/maximized (double-clicking), as well as the right-click context menu.
        /// </summary>
        public double WindowTitleHeight { get; }

        public ChromelessWindow(double windowTitleHeight) : base()
        {
            WindowTitleHeight = windowTitleHeight;
            viewModel = new ChromelessWindowViewModel(this);
            DataContext = viewModel;

            Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/DevAndersen.WpfChromelessWindow;component/ChromelessWindowStyle.xaml")
            });
            Style = FindResource("ChromelessWindowStyle") as Style;
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo e)
        {
            base.OnRenderSizeChanged(e);
            viewModel.WindowUpdate();
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            viewModel.WindowUpdate();
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            viewModel.WindowUpdate();
        }

        protected override void OnDpiChanged(DpiScale oldDpi, DpiScale newDpi)
        {
            base.OnDpiChanged(oldDpi, newDpi);
            viewModel.WindowUpdate();
        }
    }
}
