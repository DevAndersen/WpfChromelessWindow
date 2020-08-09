using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace DevAndersen.WpfChromelessWindow.Demo
{
    public class DecoratedWindowViewModel : ChromelessWindowViewModel
    {
        public DecoratedWindowViewModel(ChromelessWindow window) : base(window)
        {
        }

        public override void WindowUpdate()
        {
            base.WindowUpdate();
            NotifyPropertyChanged(nameof(BorderWidth));
            NotifyPropertyChanged(nameof(WindowTitleHeight));
        }

        public override double WindowTitleHeight => base.WindowTitleHeight + (Window.WindowState == WindowState.Maximized ? GetMaximizedMarginThickness().Top : 0);

        public virtual Thickness BorderWidth => new Thickness(Window.WindowState == WindowState.Maximized ? 0 : 3);
    }
}
