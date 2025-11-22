using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using static WpfApp2.ServiceNote;

namespace WpfApp2
{
    public  class ServiceNote
    {
        public List<Note> notes = new List<Note>();
        private string pathFile = "DataBase.txt";
        private MainWin mainWin;
        private SettingsNotePanel setPanel;
        private CreateNotePanel createPanel;


        public enum TypeSort
        {
            Id=1,
            Text,
            Data,
        }
        public ServiceNote(MainWin _mainWin, SettingsNotePanel _setPanel, CreateNotePanel _createPanel)
        {
            mainWin = _mainWin;
            setPanel = _setPanel;
            createPanel = _createPanel;
        }
        public  void UpDateIdNote()
        {
            for (int i = 0; i < notes.Count; i++)
            {
                var _note = notes[i];
                var _id = i + 1;
                _note.UpdateId(_id);
            }
        }
        public void ShowNotes()
        {
            mainWin.forNotes.Children.Clear();
            NotesSorted(TypeSort.Id);
            foreach (var item in notes)
            {
                //элементы заметки
                var elementsNote = item.CreateVisualNote();
                item.Click += (note) => setPanel.Show(note);
                mainWin.forNotes.Children.Add(elementsNote);
            }
        }    
        private void NotesSorted(TypeSort tp)
        {
            switch (tp)
            {
                case TypeSort.Id:
                    notes = notes.OrderBy(x => x.Id).ToList();
                    break;
                //case TypeSort.Text:
                //    notes = notes.OrderBy(x => x.Text).ToList();
                //    break;
                //case TypeSort.Description:
                //    notes = notes.OrderBy(x => x.DescriptText).ToList();
                //    break;
                //default:
                //    notes = notes.OrderBy(x => x.Id).ToList(); // сортировка по умолчанию
                //    break;
            }
        }
        public void DeleteNote(Note _note)
        {
            notes.Remove(_note);
            UpDateIdNote();
            setPanel.Hide();
            ShowNotes();
        }
        public void DeleteNote(string _text)
        {
            var notesForDelete = notes.Where(n => n.Text == _text).ToList();
           foreach(var item in notesForDelete) this.notes.Remove( (Note)item);
        }
        public void DeleteNote(int Id)
        {
            var notesForDelete=notes.Where(n=>n.Id == Id);
            foreach (var item in notesForDelete) notes.Remove(item);
            ShowNotes();
        }
        public void AddNote(string text,string descriptText,bool isComplited=false)
        {
            Note note;
            if (setPanel.editorNote != null)
            {
                var newId = setPanel.editorNote.Id;
                notes.Remove(setPanel.editorNote); setPanel.editorNote = null;
                note = new Note(newId, text, descriptText,isComplited);
            }
            else { note = new Note(notes.Count + 1, text, descriptText,isComplited); }  
            notes.Add(note);
            ShowNotes();                   
        }
        public void AddNotesForFile()
        {
            try
            {
                string[] lines = File.ReadAllLines(pathFile);
                foreach (string line in lines)
                {
                    // Разделяем строку по точке с запятой
                    string[] parts = line.Split(';');

                    if (parts.Length >= 2)
                    {
                        bool _IsComplited = false;
                        if (parts[0] == "False") { _IsComplited = false; }
                        else { _IsComplited = true; }

                        string _Text = parts[1];
                        string _DescriptText = parts[2];
                        AddNote(_Text, _DescriptText, _IsComplited);
                    }
                }
            }
            catch
            {

            }
        }
        public void WriteNotesInFile()
        {
            List<string> lines = new List<string>();
            if (File.Exists(pathFile))
            {
                foreach (Note item in notes)
                {
                    string line = item.IsComplited + ";" + item.Text+";"+item.DescriptionText;
                    lines.Add(line);
                }
                File.WriteAllLines(pathFile, lines);
            }
            else { MessageBox.Show("Нет файла"); }
           
        }
    }
}
