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
            await Navigation.PushModalAsync(new AddCourse(_term,_main));
        }
    }
}