using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Dynamo.Controls;
using Dynamo.Extensions;
using Dynamo.Graph.Nodes;
using Dynamo.Models;
using Dynamo.ViewModels;
using Dynamo.Wpf.Extensions;
using ProtoCore.AST;

namespace DynamoProgressBar
{
    public class ProgressBarViewExtension : IViewExtension
    {
        public static DynamoView view;

        internal MenuItem ProgressBarMenuItem;

        private ProgressBarWindow ProgressBarView;
        internal ProgressBarViewModel ViewModel;

        public void Dispose() { /*ProgressBarView.Dispose();*/ }


        public void Startup(ViewStartupParams viewStartupParams)
        {
            
        }

        public void Loaded(ViewLoadedParams p)
        {
            view = p.DynamoWindow as DynamoView;

            ProgressBarMenuItem = new MenuItem {Header = "Show Progress Bar"};
            ProgressBarMenuItem.Click += (sender, args) =>
            {
                DynView.HomeSpace.RunSettings.RunType = RunType.Manual;

                foreach (var node in DynView.CurrentSpace.Nodes)
                {
                    node.NodeExecutionEnd += NodeOnNodeExecutionEnd;
                }

                ProgressBarView = new ProgressBarWindow(p.CurrentWorkspaceModel.Nodes.Count());
                ProgressBarView.Show();
            };

            p.AddMenuItem(MenuBarType.View,ProgressBarMenuItem);
        }

        


        private void NodeOnNodeExecutionEnd(NodeModel obj)
        {
            obj.NodeExecutionEnd -= NodeOnNodeExecutionEnd;

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                ProgressBarView.Increment();
            }));

        }


        public string Name => "DynamoProgressBarExtension";
        public string UniqueId => "00B5A1FD-C7AA-4798-8903-984EA295851A";
        public static DynamoViewModel DynView => view.DataContext as DynamoViewModel;

        public void Shutdown()
        {
            
        }

    }
}
