using System.Windows;
using System.Windows.Controls;

namespace FileDiffViewerApp.Views;
/// <summary>
/// Interaction logic for FileSelectControl.xaml
/// </summary>
public partial class FileSelectControl : UserControl
{
    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(FileSelectControl), new PropertyMetadata("<Без названия>"));


    public FileSelectControl()
    {
        InitializeComponent();
        DataContext = this;
    }
}
