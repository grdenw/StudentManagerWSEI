using System;
using StudentManager.Core.Models;
using StudentManager.Core.Repositories.Abstractions;
using StudentManager.Core.Repositories.Implementations;
using StudentManager.Core.Utils;
using System.Windows;
using StudentManager.Const;

namespace StudentManager
{
    /// <summary>
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        private readonly IStudentRepository studentRepository;
        private int studentId;
        public AddUserWindow()
        {
            InitializeComponent();

            studentRepository = new StudentCsvRepository(PathConst.FilePath);

            Loaded += AddUserWindow_Loaded;
        }

        private async void AddUserWindow_Loaded(object sender, RoutedEventArgs e)
        {
            studentId = await studentRepository.GenerateId();

            TxtId.Text = Convert.ToString(studentId);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidatorHelper.ValidateIntInTextBox(TxtAge, out var studentAge) || !ValidatorHelper.ValidateStringInTextBox(TxtName) || !ValidatorHelper.ValidateStringInTextBox(TxtSurname))
            {
                return;
            }

            var student = new Student(studentId,  TxtName.Text, TxtSurname.Text, studentAge);

            studentRepository.Add(student);

            Close();
        }
    }
}
