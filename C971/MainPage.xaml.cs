using C971.Classes;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace C971
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        List<Term> terms = new List<Term>();
        List<Course> courses = new List<Course>();
        List<Assessment> assessments = new List<Assessment>();
        public MainPage()
        {
            InitializeComponent();
            termsListView.ItemTapped += new EventHandler<ItemTappedEventArgs>(ItemTapped);
        }



        protected override void OnAppearing()
        {
            //var terms = new List<Term>();
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Term>();
                con.CreateTable<Course>();
                con.CreateTable<Assessment>();
                terms = con.Table<Term>().ToList();
                termsListView.ItemsSource = terms;
            }
            if(terms.Any())
            {
                using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
                {
                    con.DeleteAll<Assessment>();
                    con.DeleteAll<Course>();
                    con.DeleteAll<Term>();
                    for (int i = 1; i < 7; i++)
                    {
                        CreateEvaluationData(i);
                    }
                    
                }
            }
            else
            {
                for (int i = 1; i < 7; i++)
                {
                    CreateEvaluationData(i);
                }
            }

            base.OnAppearing();
        }

        private void CreateEvaluationData(int termNumber)
        {
            ////                          EVALUATION DATA CREATION
            ////       ----SAMPLE TERM----
            Term newTerm = new Term();
            newTerm.TermName = "Term " + termNumber.ToString();
            newTerm.Start = new DateTime(2020, 03, 13);
            newTerm.End = new DateTime(2020, 09, 13);
            ////       ----SAMPLE COURSE----
            Course newCourse = new Course();
            newCourse.Term = newTerm.Id;
            newCourse.CourseName = "";
            newCourse.CourseStatus = "";
            newCourse.Start = new DateTime(2020,03,12);
            newCourse.End = new DateTime(2020, 04, 25);
            newCourse.InstructorName = "David Potesta";
            newCourse.InstructorEmail = "dpotest@wgu.edu";
            newCourse.InstructorPhone = "414-768-3782";
            newCourse.Notes = "You must complete the Objective Assessment before the Performance assessment";
            newCourse.GetNotified = 0;
            ////       ----SAMPLE OBJECTIVE ASSESSMENT----
            Assessment newObjectiveAssessment = new Assessment();
            newObjectiveAssessment.AssessmentName = "BOP1";
            newObjectiveAssessment.Start = new DateTime(2020, 04, 11);
            newObjectiveAssessment.End = new DateTime(2020, 04, 11);
            newObjectiveAssessment.AssessType = "Objective";
            newObjectiveAssessment.Course = newCourse.Id;
            newObjectiveAssessment.GetNotified = 0;
            ////       ----SAMPLE PERFORMANCE ASSESSMENT----
            Assessment newPerformanceAssessment = new Assessment();
            newPerformanceAssessment.AssessmentName = "LAG1";
            newPerformanceAssessment.Start = new DateTime(2020, 04, 11);
            newPerformanceAssessment.End = new DateTime(2020, 04, 11);
            newPerformanceAssessment.AssessType = "Performance";
            newPerformanceAssessment.Course = newCourse.Id;
            newPerformanceAssessment.GetNotified = 0;
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.Insert(newTerm);
                con.Insert(newCourse);
                con.Insert(newObjectiveAssessment);
                con.Insert(newPerformanceAssessment);
            }
        }




        private void btnNewTerm_Clicked(object sender, EventArgs e)
        {

        }
        async void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Term term = (Term)e.Item;
            await Navigation.PushAsync(new TermPage(term));
        }
    }
}
