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
    public partial class EditAssessmentPage : ContentPage
    {
        public Course _course;
        public Assessment _assessment;
        public MainPage _main;
        public List<string> pickerStates = new List<string> { "Objective", "Performance" };
        public List<string> pickerNotificationsStates = new List<string> { "Yes", "No" };
        public EditAssessmentPage(Course course,MainPage main,Assessment assessment)
        {
            _course = course;
            _assessment = assessment;
            _main = main;
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            pickerAssessmentType.ItemsSource = pickerStates;
            pickerAssessmentType.SelectedIndex = pickerStates.FindIndex(status => status == _assessment.AssessType);
            txtAssessmentName.Text = _assessment.AssessmentName;
            dpDueDate.Date = _assessment.End.Date;
            if (_assessment.GetNotified == 0)
            {
                pickerNotifications.SelectedIndex = 0;
            }
            else
            {
                pickerNotifications.SelectedIndex = 1;
            }
            base.OnAppearing();
        }

            private void btnDiscardChanges_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnEditCourse_Clicked(object sender, EventArgs e)
        {
            _assessment.AssessmentName = txtAssessmentName.Text;
            _assessment.AssessType = pickerAssessmentType.SelectedItem.ToString();
            _assessment.End = dpDueDate.Date;
            _assessment.GetNotified = pickerNotifications.SelectedIndex;
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.Update(_assessment);
                Navigation.PopAsync();
            }
        }
    }
}