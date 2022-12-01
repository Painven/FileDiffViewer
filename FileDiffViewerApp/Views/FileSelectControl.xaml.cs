using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FileDiffViewerApp.Views;
/// <summary>
/// Interaction logic for FileSelectControl.xaml
/// </summary>
public partial class FileSelectControl : UserControl, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(FileSelectControl), new PropertyMetadata("<Без названия>"));

    public string SelectedFileName
    {
        get { return (string)GetValue(SelectedFileNameProperty); }
        set 
        { 
            SetValue(SelectedFileNameProperty, value);

            FileLines = File.ReadAllLines(value)
                .Select((line, index) => new FileLine
                {
                    LineNumber = index + 1,
                    LineData = line
                })
                .ToList();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FileLines)));
        }
    }

    // Using a DependencyProperty as the backing store for SelectedFileName.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SelectedFileNameProperty =
        DependencyProperty.Register("SelectedFileName", typeof(string), typeof(FileSelectControl), new PropertyMetadata("C:/"));
  
    public List<FileLine> FileLines { get; private set; }

    public ICommand SelectFileCommand { get; }

    public FileSelectControl()
    {
        InitializeComponent();
        SelectFileCommand = new LambdaCommand(SelectFile);
        DataContext = this;
    }

    private void SelectFile(object obj)
    {
        var ofd = new OpenFileDialog();
        if(ofd.ShowDialog() == true)
        {
            SelectedFileName = ofd.FileName;
        }
    }
}

public record FileLine
{
    public int LineNumber { get; init; }
    public string LineData { get; init; }
}
