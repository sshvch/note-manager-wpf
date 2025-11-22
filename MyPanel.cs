using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp2
{
    public abstract class MyPanel
    {
        public Border border;
        public Canvas canvas;

        public MyPanel()
        {
            border = new Border();
            canvas = new Canvas();
        }
        public void AddElement(UIElement element, int top, int left)
        {
            Canvas.SetTop(element, top);
            Canvas.SetLeft(element, left);
            canvas.Children.Add(element);
        }
        public void Hide() { border.Visibility = Visibility.Collapsed; }
        public void Show() { border.Visibility = Visibility.Visible; }
        public void SetupCanvas() { border.Child = canvas; }
    }
}
