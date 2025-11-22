using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    public partial class MainWindow : Window
 
    {
        public SettingsNotePanel setPanel;
        public CreateNotePanel createPanel;
        public MainWin mainWin;
        ServiceNote serviceNote;
 
        public MainWindow()
        {
            InitializeComponent();          

            this.Width = 400;
            this.Height = 500;
 
            setPanel = SettingsNotePanel.Create();
            createPanel = CreateNotePanel.Create();
            mainWin = MainWin.Create();

            serviceNote = new ServiceNote(mainWin,setPanel,createPanel);
            serviceNote.AddNotesForFile();
            this.Closed += (sender, e) => { serviceNote.WriteNotesInFile(); };

            SetupPanels();            
            this.Content = mainWin.canvas;
        }

        private void SetupPanels()
        {
            SetupCreateNotePanel();
            SetupMainWin();
            SetupSettingsPanel();
        }
        private void SetupSettingsPanel()
        {
            mainWin.AddElement(setPanel.border, 50, 50);
            setPanel.DeliteButtonClicked +=(s,e) => serviceNote.DeleteNote(setPanel.editorNote);
            setPanel.SetButtonClicked += (s, e) => createPanel.Show(setPanel.editorNote);
        }
        private void SetupMainWin()
        {
            // подписка на событие 
            mainWin.AddNoteClicked += (s, e) => { createPanel.Show(); };
        }
        private void SetupCreateNotePanel()
        {
            //Добавление на главное окно
            mainWin.AddElement(createPanel.border, 50, 50);
            // Подписка на событие
            createPanel.SaveNoteButtonClicked += (s, e) => 
            {
                serviceNote.AddNote(e.tbText,e.descriptionText);
            };
        }


    }
}