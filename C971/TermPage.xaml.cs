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
        private Term _term;

        public TermPage()
        {
            InitializeComponent();
        }

        public TermPage(Term term)
        {
            _term = term;
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
    }
}