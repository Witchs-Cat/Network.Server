using Network.Server.Wpf.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Network.Server.Wpf.ViewModel
{
    public class MainViewModel : BaseViewModel
    {

        private string _endPonit;
        private bool _autoEndPointSelection;
        private Visibility _endPointInputVisibility;

        public MainViewModel()
        {
            _endPonit = "0.0.0.0:8080";
            _autoEndPointSelection = true;
            _endPointInputVisibility = Visibility.Collapsed;

            ChangeVisibilityCommand = new((param) =>
               EndPointInputVisibility = AutoEndPointSelection ? Visibility.Collapsed : Visibility.Visible);

            RunServerCommand = new((param) => { });
            StopServerCommand = new((param) => { });
        }

        public LambdaCommand ChangeVisibilityCommand { get; }
        public LambdaCommand RunServerCommand { get; }
        public LambdaCommand StopServerCommand { get; }

        public string EndPoint
        {
            get => _endPonit;
            set => SetProperty(ref _endPonit, value);
        }

        public bool AutoEndPointSelection
        {
            get => _autoEndPointSelection;
            set => SetProperty(ref _autoEndPointSelection, value);
        }

        public Visibility EndPointInputVisibility
        {
            get => _endPointInputVisibility;
            set => SetProperty(ref _endPointInputVisibility, value);
        }

    }
}
