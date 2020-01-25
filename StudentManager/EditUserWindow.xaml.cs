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
    /// Interaction logic for EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        private readonly IStudentRepository studentRepository;
        private readonly Student student;
        public EditUserWindow(Student student)
        {
            InitializeComponent();

            studentRepository = new StudentCsvRepository(PathConst.FilePath);

            this.student = student;

            Loaded += EditUserWindow_Loaded;
        }

        private void EditUserWindow_Loaded(object sender, RoutedEventArgs e)
        {
            TxtId.Text = Convert.ToString(student.Id);
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidatorHelper.ValidateIntInTextBox(TxtAge, out var studentAge) || !ValidatorHelper.ValidateStringInTextBox(TxtName) || !ValidatorHelper.ValidateStringInTextBox(TxtSurname))
            {
                return;
            }

            var newStudent = new Student(student.Id, TxtName.Text, TxtSurname.Text, studentAge);

            studentRepository.Edit(student.Id, newStudent);

            Close();
        }
    }
}
