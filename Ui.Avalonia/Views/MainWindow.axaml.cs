using System;
using Avalonia.Controls;
using System.Collections.ObjectModel;
using TipTracker.Models;

namespace TipTracker.Views
{
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void DataGrid_Initialized(object? sender, EventArgs e)
        {
            //if (sender is DataGrid dgSender)
            //{
            //    dgSender.InvalidateVisual();
            //    dgSender.Columns[0].Sort();
            //}
        }

        private void DataGrid_DataContextChanged(object? sender, EventArgs e)
        {
            if (sender is DataGrid dgSender)
            {
                dgSender.InvalidateVisual();
                dgSender.Columns[0].Sort();
            }
        }

        private void StyledElement_OnDataContextChanged(object? sender, EventArgs e)
        {
            
        }
    }
}