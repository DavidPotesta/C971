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
    public partial class AddCourse : ContentPage
    {
        public Term termPage;
        public MainPage mainPage;
        Dictionary<string, bool> notificationsDict = new Dictionary<string, bool>
        {
            { "Yes",true},
            {"No",false}
        };
        public AddCourse(Term term,MainPage main)
        {
            termPage = term;
            mainPage = main;
            InitializeComponent();
        }

        private void btnSave_Clicked(object sender, EventArgs e)
        {

                Course newCourse = new Course();
                newCourse.CourseName = txtCourseTitle.Text;
                newCourse.CourseStatus = txtCourseStatus.Text;
                newCourse.Start = dpStartDate.Date;
                newCourse.End = dpEndDate.Date;
                newCourse.InstructorName = txtCourseInstructorName.Text;
                newCourse.InstructorEmail = txtInstructorEmail.Text;
                newCourse.InstructorPhone = txtInstructorPhone.Text;
                newCourse.Notes = txtNotes.Text;
                newCourse.GetNotified = pickerNotifications.SelectedIndex;
                newCourse.Term = termPage.Id;
                using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
                {
                    con.Insert(newCourse);

                    // Maybe don't have to update termListView if OnAppearing() gets called when this modal 
                    // is dismissed.....yes we do lol, even though documentation says that OnAppearing() gets
                    // called when modal is dismissed.  bug? 
                    //https://forums.xamarin.com/discussion/58606/onappearing-not-called-on-android-for-underneath-page-if-page-on-top-was-pushed-modal
                    mainPage.courses.Add(newCourse);
                    Navigation.PopModalAsync();
                }


        }

    }
}