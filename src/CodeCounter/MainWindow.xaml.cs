using System;
using System.Windows;

namespace CodeCounter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CodeCounting counter;

        public MainWindow()
        {
            InitializeComponent();
            counter = new CodeCounting();
        }

        private void BrowseFolder_Click(object sender, RoutedEventArgs e)
        {
            // Browse folder
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    tb_folder.Text = dialog.SelectedPath;
            }
        }

        private void CheckAll_Click(object sender, RoutedEventArgs e)
        {
            // Check all programming languages
            cb_csharp.IsChecked = true;
            cb_cplus.IsChecked = true;
            cb_c.IsChecked = true;
            cb_java.IsChecked = true;
            cb_python.IsChecked = true;
            cb_vhdl.IsChecked = true;
            cb_matlab.IsChecked = true;
            cb_html.IsChecked = true;
        }

        private void UncheckAll_Click(object sender, RoutedEventArgs e)
        {
            // Uncheck all programming languages
            cb_csharp.IsChecked = false;
            cb_cplus.IsChecked = false;
            cb_c.IsChecked = false;
            cb_java.IsChecked = false;
            cb_python.IsChecked = false;
            cb_vhdl.IsChecked = false;
            cb_matlab.IsChecked = false;
            cb_html.IsChecked = false;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            // Set the programming languages
            counter.SetLanguage(CodingLanguage.Csharp,cb_csharp.IsChecked ?? false);// "?? false" returns false if the left side is null
            counter.SetLanguage(CodingLanguage.Cplus, cb_cplus.IsChecked ?? false);
            counter.SetLanguage(CodingLanguage.C, cb_c.IsChecked ?? false);
            counter.SetLanguage(CodingLanguage.Java, cb_java.IsChecked ?? false);
            counter.SetLanguage(CodingLanguage.Python, cb_python.IsChecked ?? false);
            counter.SetLanguage(CodingLanguage.VHDL, cb_vhdl.IsChecked ?? false);
            counter.SetLanguage(CodingLanguage.MATLAB, cb_matlab.IsChecked ?? false);
            counter.SetLanguage(CodingLanguage.HTML, cb_html.IsChecked ?? false);

            // Count the lines of code, returns true if completed successfully
            if (counter.CountLines(tb_folder.Text)) {

                // Output to textbox
                tb_result.AppendText("-------------------"+ DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "-------------------\n");
                tb_result.AppendText("Root Folder: " + tb_folder.Text + "\n\n");
                tb_result.AppendText(counter.GetLastResult());
                tb_result.AppendText("\n");

            }
        }
    }
}
