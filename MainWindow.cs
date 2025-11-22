using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp2
{
    public class MainWin:MyPanel
    {
        private static MainWin mainWin;
        public   StackPanel forNotes;
        private  Button AddNote;
        public   event EventHandler AddNoteClicked;

        private MainWin()
        {
            InitializeComponents();
            SetupEvents();
            SetupLockControls();
        }
        public static MainWin Create()
        {
            if (mainWin != null) return mainWin;
            mainWin = new MainWin();
            return mainWin;
        }
        private  void SetupLockControls()
        {
            canvas.Children.Add(AddNote);
            Canvas.SetTop(forNotes, 50);
            canvas.Children.Add(forNotes);
        }
        private  void SetupEvents()
        {
            AddNote.Click += (s, e) => {AddNoteClicked.Invoke(AddNote,EventArgs.Empty);}; 
        }
        private  void InitializeComponents()
        {
            forNotes= new StackPanel() { Background=Brushes.DimGray,Width=400,Height=500};
            AddNote = new Button()
            {
                Width = 40,
                Height = 40,
                Content = '+',
                Background = Brushes.AliceBlue,
            };
        }
    }
}
