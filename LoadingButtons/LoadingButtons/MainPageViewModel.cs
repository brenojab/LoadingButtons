using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LoadingButtons.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        bool _isLoading = false;

        public string ButtonText { get; set; }

        public Command ButtonClickCommand { get; set; }

        public MainPageViewModel()
        {
            ButtonClickCommand = new Command(() =>
            {
                _isLoading = !_isLoading;

                ButtonText = $"Is buttom loading? {_isLoading}";
            });



        }
    }
}
