using DellyShopApp.Annotations;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace DellyShopApp.ViewModel
{
    public partial class Page3 : ContentPage, INotifyPropertyChanged
    {
        private string _LabelText;
        public string LabelText
        {
            get { return _LabelText; }
            set
            {
                _LabelText = value;
                RaisePropertyChanged("LabelText");

            }
        }
        private ICommand _ExpandContractCommand;
        private bool _TextExpanded;

        public bool TextExpanded
        {
            get { return _TextExpanded; }
            set
            {
                _TextExpanded = value;
                RaisePropertyChanged("TextExpanded");
            }
        }
        public Page3()
        {
            InitializeComponent();
            LabelText = "Can any one help me on this?\nI want set read more option for multiline text end of label when i click on that read more it will expand or navigate to the any other page. Please help me on this .\n\nThanks in advance.";

            this.BindingContext = this;
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        public ICommand ExpandContractCommand
        {
            get
            {
                if (_ExpandContractCommand == null)
                {
                    _ExpandContractCommand = new Command(() => {
                        TextExpanded = !TextExpanded;
                    });
                }

                return _ExpandContractCommand;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;


        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}