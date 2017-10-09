using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Database;
using System.Windows.Media;
namespace PCS_Forms.Controls
{

    using Database;
    using PCS_Forms.DataOut;
    using System.Windows;
    public class MyTabitem:TabItem
    {

        MyWrapPanel WrapPanel;
        int Row = 1;
        const  int  Column = 1;
        const int  Rowspan = 5 ;
        const int Columnspan = 18;

        public MyTabitem()
        {
            this.CreateChild();
        }

        private void CreateChild()
        {
            this.WrapPanel =new MyWrapPanel();
            this.WrapPanel.ItemHeight = 150;
            this.WrapPanel.ItemWidth = 200;
            this.Background = Brushes.CadetBlue;
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            
            this.AddChild(WrapPanel);
        }


        public void AddGroupBox(MyGroupBox childGroupBox)
        {
            this.WrapPanel.AddElement(childGroupBox);
            childGroupBox.Margin = new Thickness(0, 40, 0, 10);
            //this.grid.AddControl(childGroupBox, Row, Column, Rowspan, Columnspan);
            Row += Rowspan+1;
        }



       
    }
}
