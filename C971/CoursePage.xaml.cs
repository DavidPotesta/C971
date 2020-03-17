using C971.Classes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursePage : ContentPage
    {
        public Course _course;
        public Term _term;
        public MainPage _main;
        public List<string> pickerStates = new List<string> { "In Progress", "Completed", "Dropped", "Plan To Take" };
        public List<string> pickerNotificationsStates = new List<string> { "Yes", "No" };
        public CoursePage(Term term, MainPage main, Course course)
        {
            _course = course;
            _term = term;
            _main = main;
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            pickerCourseStatus.ItemsSource = pickerStates;
            pickerCourseStatus.SelectedIndex = pickerStates.FindIndex(status => status == _course.CourseStatus);
            txtCourseTitle.Text = _course.CourseName;
            pickerCourseStatus.SelectedItem = _course.CourseStatus;
            dpStartDate.Date = _course.Start.Date;
            dpEndDate.Date = _course.End.Date;
            txtInstructorName.Text = _course.InstructorName;
            txtInstructorPhone.Text = _course.InstructorPhone;
            txtInstructorEmail.Text = _course.InstructorEmail;
            txtNotes.Text = _course.Notes;
            if(_course.GetNotified == 0)
            {
                pickerNotifications.SelectedIndex = 0;
            }
            else
            {
                pickerNotifications.SelectedIndex = 1;
            }
            base.OnAppearing();
        }

        private void btnEditCourse_Clicked(object sender, EventArgs e)
        {
            _course.CourseName = txtCourseTitle.Text;
            _course.CourseStatus = pickerCourseStatus.SelectedItem.ToString();
            _course.Start = dpStartDate.Date;
            _course.End = dpEndDate.Date;
            _course.InstructorName = txtInstructorName.Text;
            _course.InstructorEmail = txtInstructorEmail.Text;
            _course.InstructorPhone = txtInstructorPhone.Text;
            _course.Notes = txtNotes.Text;
            _course.GetNotified = pickerNotifications.SelectedIndex;
            _course.Term = _term.Id;
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.Update(_course);

                // Maybe don't have to update termListView if OnAppearing() gets called when this modal 
                // is dismissed.....yes we do lol, even though documentation says that OnAppearing() gets
                // called when modal is dismissed.  bug? 
                //https://forums.xamarin.com/discussion/58606/onappearing-not-called-on-android-for-underneath-page-if-page-on-top-was-pushed-modal
                //_main.courses.Remove( Add(newCourse);
                Navigation.PopAsync();
            }
            
        }

        private void btnDiscardChanges_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnViewAssessments_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AssessmentPage());
        }
    }
}