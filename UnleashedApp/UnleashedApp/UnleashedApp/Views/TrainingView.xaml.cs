﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnleashedApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrainingView : TabbedPage
    {
        public TrainingView ()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Instance.TrainingViewModel;
        }
    }
}