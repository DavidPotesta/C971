﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseMaximumError : ContentPage
    {
        public CourseMaximumError()
        {
            InitializeComponent();
        }

        private async void btnCloseTermAddError_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}