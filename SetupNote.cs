using System;
using System.Collections.Generic;
using System.Linq;

using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApp2
{
     public class SettingsNotePanel : MyPanel
    {
        // Для создания только одного объекта
        private static SettingsNotePanel settingsNotePanel;

        public event EventHandler SetButtonClicked;
        public event EventHandler DeliteButtonClicked;
        public Note editorNote;  
        //Элементы окна
        private   Label label;      
        private  Button CloseButton;
        private  Button SetButton;
        private  Button DeliteButton;

        private SettingsNotePanel()
        {
            InitializeComponents();
            SetupCanvas();
            SetupLockElements();
            SetupEvents();
        }

        public static SettingsNotePanel Create()
        {
            if (settingsNotePanel == null) { settingsNotePanel = new SettingsNotePanel(); }
            return settingsNotePanel;
        }
        public void Show(Note _note) 
        {
            editorNote = _note;
            border.Visibility = Visibility.Visible;           
        }
        private  void InitializeComponents()
        {                   
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
            label = new Label()
            {
                FontSize = 18,
                Content = "Выберите действие"
            };
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
            SetButton = new Button()
            {
                Width = 90,
                Height = 30,
                Content = "Редактировать",
                Background = new LinearGradientBrush(
                    Color.FromRgb(120, 120, 120),
                    Color.FromRgb(80, 80, 80),
                    90),
                Foreground = Brushes.White,
                FontWeight = FontWeights.Bold,
                FontSize = 10,
                Margin = new Thickness(0, 15, 0, 0),

                BorderThickness = new Thickness(0),
            };
            DeliteButton = new Button()
            {
                Width=90,
                Height=30,
                Content = "Удалить",
                Background = new LinearGradientBrush(
                    Color.FromRgb(120, 120, 120),
                    Color.FromRgb(80, 80, 80),
                    90),
                Foreground = Brushes.White,
                FontWeight = FontWeights.Bold,
                FontSize = 10,
                Margin = new Thickness(0, 15, 0, 0),
                BorderThickness = new Thickness(0),
            };
 
        }
        private  void SetupEvents()
        {
            CloseButton.Click  += (s, e) => { border.Visibility = Visibility.Collapsed; };
            SetButton.Click   += (s, e) => { SetButtonClicked.Invoke(SetButton, EventArgs.Empty);Hide(); };
            DeliteButton.Click += (s, e) => { DeliteButtonClicked.Invoke(DeliteButton, EventArgs.Empty); };
        }
        private  void SetupLockElements()
        {
            AddElement(element: CloseButton,   top:5,   left:245);
            AddElement(element: label,         top: 40, left: 50);
            AddElement(element: SetButton,     top: 90, left: 50);
            AddElement(element: DeliteButton,  top: 90, left: 150);                 
        }
        

    }
}
