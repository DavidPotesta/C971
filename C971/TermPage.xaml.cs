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
    public partial class TermPage : ContentPage
    {
        public Term _term;
        public MainPage _main;

        public TermPage()
        {
            InitializeComponent();
        }

        public TermPage(Term term,MainPage main)
        {
            _term = term;
            _main = main;
            InitializeComponent();
            coursesListView.ItemTapped += new EventHandler<ItemTappedEventArgs>(ItemTapped);
            Title = term.TermName;


        }
        protected override void OnAppearing()
        {
            termStart.Text = _term.Start.ToString("MM/dd/yyyy");
            termEnd.Text = _term.End.ToString("MM/dd/yyyy");
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Course>();
                var coursesForTerm = con.Query<Course>($"SELECT * FROM Courses WHERE Term = '{_term.Id}'");
                coursesListView.ItemsSource = coursesForTerm;
            }
        }

        async private void btnAddCourse_Clicked(object sender, EventArgs e)
        {
            // Only allow 6 courses per term
            if (getCourseCount() < 6)
            {
                await Navigation.PushModalAsync(new AddCourse(_term,_main));
            }
            else
            {
                // modal windows saying "can't add more than 6 courses"
                await Navigation.PushModalAsync(new CourseMaximumError());
            }
        }

        int getCourseCount()
        {
            int count = 0;
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                var courseCount = con.Query<Course>($"SELECT COUNT(*) FROM Courses WHERE Term = '{_term.Id}'");
                count = courseCount.Count;
            }

            return count;
        }
        async void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Course course = (Course)e.Item;
            await Navigation.PushAsync(new CoursePage(_term,_main,course));
        }
    }
}