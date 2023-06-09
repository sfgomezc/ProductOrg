﻿using ProductOrg.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProductOrg.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PomodoroConfigPage : ContentPage
    {
        public PomodoroConfigPage()
        {
            InitializeComponent();
            BindingContext = new PomodoroConfigViewModel(Navigation);
        }
    }
}