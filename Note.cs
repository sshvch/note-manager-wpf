using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp2
{
    public class Note : MyPanel
    {
        public event Action<Note> Click;               
        public string Text { get;  set; }
        public string DescriptionText { get; set; }
        public int Id;
        public bool IsComplited = false;
        private NoteView noteView;


        public Note() { noteView = new NoteView(this); }
        public Note(int _Id,string _Text,string _descriptionText,bool _IsComplited=false)
        {
            noteView = new NoteView(this);
            Id = _Id;
            Text = _Text;
            DescriptionText= _descriptionText;
            IsComplited= _IsComplited;
        }


        public  void UpdateId(int _Id) { Id = _Id; }
        public StackPanel CreateVisualNote() 
        { 
            var noteView=  new NoteView(this);          
            return noteView.GetVisualNote();
        }
        public void NoteClick() { Click.Invoke(this); }
        public void Complited()
        {
            IsComplited = Convert.ToBoolean(Math.Abs(Convert.ToInt32(IsComplited) - 1));
        }
        
    }

    public class NoteView
    {
        private Note note;
        private TextBlock label;
        private CheckBox checkBox;
        private TextBox description;
        private Button ShowHide;
        private StackPanel mainPanel;

        public NoteView(Note _note)
        {
            note = _note;
            InitializingElements();
            Setupevent();
        }

        private void InitializingElements()
        {

            mainPanel = new StackPanel()
            {
                Background = new LinearGradientBrush(
                                Color.FromRgb(60, 60, 60),
                                    Color.FromRgb(40, 40, 40),
                                                                    90),
                Margin = new Thickness(5),
            };
            label = new TextBlock()
            {
                Text = note.Text,
                Foreground = Brushes.WhiteSmoke,
                FontSize = 14,
                FontWeight = FontWeights.Medium,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Padding = new Thickness(5, 0, 0, 0)
            };            
            //Создание и настройка Checkbox
            checkBox = new CheckBox()
            {
                FontSize = 14,
                Margin = new Thickness(0, 0, 10, 0),
                IsChecked = note.IsComplited,
                Foreground = Brushes.WhiteSmoke,
                VerticalAlignment = VerticalAlignment.Center
            };
            checkBox.IsChecked = note.IsComplited;
            SetStrikethroughText(checkBox, label);
            //Кнопка показа описания
            ShowHide = new Button()
            {
                Width = 24,
                Height = 24,
                Margin = new Thickness(0, 0, 10, 0),
                Content = "▼",
                Background = new LinearGradientBrush(
                    Color.FromRgb(100, 100, 100),
                    Color.FromRgb(70, 70, 70),
                    90),
                Foreground = Brushes.White,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                BorderThickness = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Center,
                Cursor = Cursors.Hand
            };
            //Описание заметки
            description = new TextBox()
            {
                Text = note.DescriptionText,
                Width = 200,
                MinHeight = 30,
                MaxHeight = 50,
                TextWrapping = TextWrapping.Wrap,
                AcceptsReturn = true,
                AcceptsTab = true,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
                FontSize = 12,
                Visibility = Visibility.Collapsed,
                IsReadOnly = true,
                Background = new SolidColorBrush(Color.FromRgb(50, 50, 50)),
                Foreground = Brushes.WhiteSmoke,
                BorderBrush = new SolidColorBrush(Color.FromRgb(100, 100, 100)),
                BorderThickness = new Thickness(1),
                Padding = new Thickness(5),
                VerticalAlignment = VerticalAlignment.Top
            };
        }
        private void Setupevent()
        {
            mainPanel.PreviewMouseRightButtonDown += (s, e) => 
            {
                note.NoteClick();               
            };
            checkBox.Click += (s, e) =>
            {
                note.Complited();
                SetStrikethroughText(checkBox, label);
            };
            ShowHide.Click += (s, e) =>
            {
                if (description.Visibility == Visibility.Collapsed)
                {
                    description.Visibility = Visibility.Visible;
                    ShowHide.Content = "▲";
                }
                else
                {
                    description.Visibility = Visibility.Collapsed;
                    ShowHide.Content = "▼";
                }
            };
        }
        public StackPanel GetVisualNote()
        {
 
            var firstPanel = new DockPanel()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Margin = new Thickness(0, 0, 0, 5)
            };
            var nextPanel = new DockPanel()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            // Добавляем в первую линию 
            firstPanel.Children.Add(checkBox);
            firstPanel.Children.Add(ShowHide);
            firstPanel.Children.Add(label);

            // Добавляем во вторую линию 
            nextPanel.Children.Add(description);

            // Добавляем две линии в основную панель
            mainPanel.Children.Add(firstPanel);
            mainPanel.Children.Add(nextPanel);

            return mainPanel;
        }
        private void SetStrikethroughText(CheckBox chB, TextBlock tb)
        {
            if (chB.IsChecked == true) { tb.TextDecorations = TextDecorations.Strikethrough; }
            else { tb.TextDecorations = null; }
        }

    }
}
