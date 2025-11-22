using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApp2
{
    public   class CreateNotePanel: MyPanel
    {
        private static CreateNotePanel createNotePanel;
        public event EventHandler<(string tbText, string descriptionText)> SaveNoteButtonClicked;
        // Данные для заметки
        private TextBox description;      
        private CustomTextBox tb;
        private DateTime data;
        //Кнопки
        private  Button CloseButton;
        private  Button SaveNoteButton;
        private CreateNotePanel()
        {
            InitializeComponents();
            SetupEvents();
            SetupCanvas();
            SetupLockElements();
        }


        public void Show(Note _note)
        {
            tb.Text = _note.Text;
            description.Text = _note.DescriptionText;
            border.Visibility = Visibility.Visible;
        }
        public void Clear() { tb.Text = ""; description.Text = ""; }
        public static CreateNotePanel Create()
        {
            if (createNotePanel != null) { return createNotePanel; }
            createNotePanel = new CreateNotePanel();
            return createNotePanel;
        }
 
        private  void SetupLockElements()
        {
            AddElement(element: CloseButton,    top: 0,   left: 245);
            AddElement(element: tb.border,      top: 30,  left: 20);   
            AddElement(element: description,    top: 80,  left: 22);
            AddElement(element: SaveNoteButton, top: 120, left: 90);
        }
        private  void SetupEvents()
        {
            SaveNoteButton.Click += (s, e) =>
            {
                SaveNoteButtonClicked.Invoke(SaveNoteButton, (tbText: tb.Text, descriptionText: description.Text));
                Clear();
                Hide();
            };
            CloseButton.Click += (s, e) => { border.Visibility = Visibility.Collapsed;};
        }       
        private  void InitializeComponents()
        {
            tb = new CustomTextBox();
            CloseButton = new Button()
            {
                Width = 40,
                Height = 25,
                Margin = new Thickness(10),
                Background = new LinearGradientBrush(
                                 Color.FromRgb(220, 80, 80),
                                 Color.FromRgb(180, 50, 50),
                                                          90),
                Foreground = Brushes.White,
                Content = "X",
                HorizontalAlignment = HorizontalAlignment.Right,
                FontWeight = FontWeights.Bold,
                FontSize = 14,
                BorderThickness = new Thickness(0),
                Cursor = Cursors.Hand
            };
            SaveNoteButton = new Button()
            {
                Content = "Сохранить",
                Background = new LinearGradientBrush(
                    Color.FromRgb(120, 120, 120),
                    Color.FromRgb(80, 80, 80),
                    90),
                Foreground = Brushes.White,
                FontWeight = FontWeights.Bold,
                FontSize = 14,
                Margin = new Thickness(0, 15, 0, 0),
                Padding = new Thickness(20, 10, 20, 10),
                BorderThickness = new Thickness(0),
            };
            border = new Border()
            {
                Width = 300,
                Height = 180,
                Background = Brushes.AliceBlue,
                BorderBrush = new SolidColorBrush(Colors.Gray),
                BorderThickness = new Thickness(1),
                Visibility = Visibility.Collapsed,
                CornerRadius = new CornerRadius(15)
            };
            description = new TextBox()
            {
                Width = 200,
                MinHeight = 30,           // Начальная высота
                MaxHeight = 50,          // Максимальная высота
                TextWrapping = TextWrapping.Wrap,          // Перенос по словам
                AcceptsReturn = true,                      // Разрешить Enter для новой строки
                AcceptsTab = true,                         // Разрешить Tab
                //VerticalScrollBarVisibility = ScrollBarVisibility.Auto, // Авто-скролл
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled, // Отключить горизонтальный скролл
                FontSize = 14
            };
        }
       

    }
}
