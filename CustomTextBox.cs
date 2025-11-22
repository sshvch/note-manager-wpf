using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WpfApp2
{
    public class CustomTextBox
    {
        public Border border;
        public TextBox tb;
        private Label hint;
        private bool hasText = false;

        public string Text
        {
            get { return tb.Text; }
            set { tb.Text = value; }

        }          
        public CustomTextBox()
        {
            InitializingElement();
            SetupCustomTextBox();
        }

        private void SetupCustomTextBox()
        {
            var canvas = new Canvas();

            Canvas.SetTop(tb,20);
            Canvas.SetLeft(tb,5);
            Canvas.SetTop(hint,21);
            Canvas.SetLeft(hint,6);

            canvas.Children.Add(tb);
            canvas.Children.Add(hint);

            border.Child = canvas;

        }

        private void InitializingElement()
        {
            border = new Border()
            {                 
                Width = 200,
                Height = 50,
                Background = Brushes.Transparent,
            };

            tb = new TextBox()
            {
                MaxLength = 30,
                Width = 200,
                Height = 25,
                FontSize = 14,
                BorderThickness = new Thickness(0, 0, 0, 2),
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Black,
            };
            tb.GotFocus += (s, e) =>
            {
                Canvas.SetTop(hint, 6);
                Canvas.SetLeft(hint, 4);
            };
            tb.LostFocus += (s, e) =>
            {
                 if (tb.Text == "")
                        {
                            Canvas.SetTop(hint, 21);
                            Canvas.SetLeft(hint, 5);
                        }
            };

            hint = new Label()
            {
                Content = "Введите заметку",
                Foreground = Brushes.Black,
                FontSize = 12,
                Background = Brushes.Transparent,
                Padding = new Thickness(0),
                IsHitTestVisible = false
            };
        }
    }
}