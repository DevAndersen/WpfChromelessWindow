using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace DevAndersen.WpfChromelessWindow
{
    public class ChromelessWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public double WindowTitleHeight => window.WindowTitleHeight;

        /// <summary>
        /// The thickness of the dynamic margin, which ensures that the proper actual window region fits correctly within the current screen's working area.
        /// </summary>
        public Thickness DynamicMargin
        {
            get
            {
                return window.WindowState switch
                {
                    WindowState.Maximized => GetMaximizedMarginThickness(),
                    _ => new Thickness(0),
                };
            }
        }

        public virtual Thickness ResizeBorderThickness => new Thickness(5);

        private readonly ChromelessWindow window;

        public ChromelessWindowViewModel(ChromelessWindow window)
        {
            this.window = window;
        }

        /// <summary>
        /// Figures out the correct thickness, for the dynamic margin, that will make the window content fit correctly within the screen's working area.
        /// </summary>
        /// <returns></returns>
        private Thickness GetMaximizedMarginThickness()
        {
            Border dynamicMarginDetector = (Border)window.Template.FindName("DynamicMarginDetector", window);
            Point topLeftCorner = dynamicMarginDetector.PointToScreen(new Point(0, 0));

            System.Drawing.Rectangle area = Screen.FromHandle(new System.Windows.Interop.WindowInteropHelper(window).Handle).WorkingArea;

            PresentationSource presentationSource = PresentationSource.FromVisual(window);
            double dpiScaleX = presentationSource.CompositionTarget.TransformToDevice.M11;
            double dpiScaleY = presentationSource.CompositionTarget.TransformToDevice.M22;

            double areaRight = area.Width / dpiScaleX;
            double areaBottom = area.Height / dpiScaleY;

            double width = dynamicMarginDetector.ActualWidth;
            double height = dynamicMarginDetector.ActualHeight;

            double left = (area.Left - topLeftCorner.X) / dpiScaleX;
            double top = (area.Top - topLeftCorner.Y) / dpiScaleY;
            double right = -(areaRight - area.Left - width + left);
            double bottom = -(areaBottom - area.Top - height + top);
            return new Thickness(left, top, right, bottom);
        }

        /// <summary>
        /// Updates the dynamic margin.
        /// Call this method when the window's location or state changes.
        /// </summary>
        public void WindowUpdate()
        {
            NotifyPropertyChanged(nameof(DynamicMargin));
        }

        public void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
