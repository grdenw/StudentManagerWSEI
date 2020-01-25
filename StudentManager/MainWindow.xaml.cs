using StudentManager.Core.Repositories.Abstractions;
using StudentManager.Core.Repositories.Implementations;
using System.Windows;
using StudentManager.Core.Models;
using StudentManager.Const;

namespace StudentManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IStudentRepository studentRepository;
        public MainWindow()
        {
            InitializeComponent();

            studentRepository = new StudentCsvRepository(PathConst.FilePath);

            Loaded += MainWindowLoaded;
        }

        private async void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            StudentDataGrid.ItemsSource = await studentRepository.GetAll();
        }

        private async void AddNew_Click(object sender, RoutedEventArgs e)
        {
            var addUserWindow = new AddUserWindow();

            addUserWindow.ShowDialog();

            StudentDataGrid.ItemsSource = await studentRepository.GetAll();
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (StudentDataGrid.SelectedItem == null)
            {
                MessageBox.Show(this, "Select row!", "Error!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                var student = (Student) StudentDataGrid.SelectedItem;

                var editUserWindow = new EditUserWindow(student);

                editUserWindow.ShowDialog();

                StudentDataGrid.ItemsSource = await studentRepository.GetAll();
            }
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (StudentDataGrid.SelectedItem == null)
            {
                MessageBox.Show(this, "Select row!", "Error!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                var student = (Student)StudentDataGrid.SelectedItem;

                studentRepository.Delete(student.Id);

                StudentDataGrid.ItemsSource = await studentRepository.GetAll();
            }
        }
    }
}
