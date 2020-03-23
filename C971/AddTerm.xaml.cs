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
    public partial class AddTerm : ContentPage
    {
        public MainPage mainPage;
        public AddTerm(MainPage main)
        {
            mainPage = main;
            InitializeComponent();
        }

        private void btnSave_Clicked(object sender, EventArgs e)
        {
            Term newTerm = new Term();
            newTerm.TermName = txtTermTitle.Text;
            newTerm.Start = dpStartDate.Date;
            newTerm.End = dpEndDate.Date;
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.Insert(newTerm);

                // Maybe don't have to update termListView if OnAppearing() gets called when this modal 
                // is dismissed.....yes we do lol, even though documentation says that OnAppearing() gets
                // called when modal is dismissed.  bug? 
               //https://forums.xamarin.com/discussion/58606/onappearing-not-called-on-android-for-underneath-page-if-page-on-top-was-pushed-modal
                mainPage.terms.Add(newTerm);
                Navigation.PopModalAsync();
            }

        }

        private void btnExit_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}