using C971.Classes;
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
        //private Term term;

        public TermPage()
        {
            InitializeComponent();
        }

        public TermPage(Term term)
        {
            InitializeComponent();
            Title = term.TermName;
            
            
        }
    }
}