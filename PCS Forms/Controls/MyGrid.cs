using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PCS_Forms.Controls
{
    public class MyGrid:Grid
    {
        public  void CreateDefinitions(int row,int col)
        {
            for (int i = 0; i < row; i++)
                AddRowDefinition();
            for(int j =0;j<col;j++)
                AddColumnDefinition();
        }

        private  void AddRowDefinition()
        {
            this.RowDefinitions.Add(new RowDefinition());
        }

        private  void AddColumnDefinition()
        {
            this.ColumnDefinitions.Add(new ColumnDefinition());
        }

        public  void AddControl( FrameworkElement control, int row, int column,int rowspan,int colspan)
        {
            this.Children.Add(control);
            Grid.SetRow(control, row);
            Grid.SetColumn(control, column);
            Grid.SetColumnSpan(control, colspan);
            Grid.SetRowSpan(control, rowspan);
            
        }

        public void AddControl(FrameworkElement control,int width,int height,Thickness margin)
        {
            this.Children.Add(control);
            control.Width = width;
            control.Height = height;
            control.Margin = margin;
        }
    }
}
