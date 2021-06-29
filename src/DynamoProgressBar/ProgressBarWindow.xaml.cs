using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Dynamo.Extensions;
using Dynamo.Graph.Nodes;
using Dynamo.Models;
using Dynamo.ViewModels;
using Dynamo.Wpf.Extensions;

namespace DynamoProgressBar
{
    /// <summary>
    /// Interaction logic for ProgressBarWindow.xaml
    /// </summary>
    public partial class ProgressBarWindow : Window
    {
        public static ProgressBar _progressBar;

        public ProgressBarWindow(int nodeCount)
        {
            InitializeComponent();
            _progressBar = this.ExecutionProgressBar;

            _progressBar.Maximum = nodeCount;
        }

        public void Increment()
        {
            double value = _progressBar.Value + 1;
            _progressBar.Dispatcher.Invoke(() => _progressBar.Value = value, DispatcherPriority.Background);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (ProgressBarViewExtension.DynView as DynamoViewModel).Model.ResetEngine(true);

            //_progressBar.Value = 0;
            //_progressBar.Maximum = ProgressBarViewExtension.DynView.HomeSpace.Nodes.Count();

            ProgressBarViewExtension.DynView.HomeSpace.Run();
        }

        private void NOnNodeExecutionEnd(NodeModel obj)
        {
            Application.Current.Dispatcher.Invoke(new Action(Increment));
        }


        //internal void Dispose()
        //{
        //    viewLoadedParams.DynamoWindow.SizeChanged -= DynamoWindow_SizeChanged;
        //}
    }


}
