using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DevAndersen.WpfChromelessWindow
{
    public class ChromelessWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The height of the window title bar.
        /// </summary>
        public virtual double WindowTitleHeight => Window.WindowTitleHeight - ResizeBorderThickness.Top;

        /// <summary>
        /// The thickness of the dynamic margin, which ensures that the proper actual window region fits correctly within the current screen's working area.
        /// </summary>
        public virtual Thickness DynamicMargin => Window.WindowState == WindowState.Maximized ? GetMaximizedMarginThickness() : new Thickness(0);

        /// <summary>
        /// The thickness of the area around the window that allows you to resize it.
        /// </summary>
        public virtual Thickness ResizeBorderThickness => new Thickness(5);

        /// <summary>
        /// The window that the view model is associated with.
        /// Used for accessing and manipulating the window in view model, and for accessing window properties from XAML.
        /// </summary>
        public ChromelessWindow Window { get; private set; }

        public ChromelessWindowViewModel(ChromelessWindow window)
        {
            Window = window;
        }

        /// <summary>
        /// Figures out the correct thickness for the dynamic margin, that will make the window content fit correctly within the screen's working area.
        /// </summary>
        /// <returns></returns>
        public Thickness GetMaximizedMarginThickness()
        {
            Border dynamicMarginDetector = (Border)Window.Template.FindName("DynamicMarginDetector", Window);
            System.Drawing.Rectangle area = System.Windows.Forms.Screen.FromHandle(new System.Windows.Interop.WindowInteropHelper(Window).Handle).WorkingArea;
            Point detectorCorner = dynamicMarginDetector.PointToScreen(new Point(0, 0));

            PresentationSource presentationSource = PresentationSource.FromVisual(Window);
            double dpiScaleX = presentationSource.CompositionTarget.TransformToDevice.M11;
            double dpiScaleY = presentationSource.CompositionTarget.TransformToDevice.M22;

            double areaRight = area.Width / dpiScaleX;
            double areaBottom = area.Height / dpiScaleY;
            double offsetX = area.Left - detectorCorner.X;
            double offsetY = area.Top - detectorCorner.Y;

            double top = offsetX / dpiScaleX;
            double left = offsetY / dpiScaleY;
            double right = (dynamicMarginDetector.ActualWidth - offsetX - areaRight) / dpiScaleX;
            double bottom = (dynamicMarginDetector.ActualHeight - offsetY - areaBottom) / dpiScaleY;

            return new Thickness(left, top, right, bottom);
        }

        /// <summary>
        /// Call this method when the window's location or state changes, in order to update the dynamic margin (or other elements).
        /// </summary>
        public virtual void WindowUpdate()
        {
            NotifyPropertyChanged(nameof(DynamicMargin));
        }

        /// <summary>
        /// <see cref="INotifyPropertyChanged"/> method.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
