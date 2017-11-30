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

        private MyWrapPanel _wrapPanel;
        private int _row = 1;
        private const  int  _column = 1;
        private const int  _rowSpan = 5 ;
        private const int _columnSpan = 18;

        public MyTabitem()
        {
            this.CreateChild();
        }

        private void CreateChild()
        {
            this._wrapPanel =new MyWrapPanel();
            this._wrapPanel.ItemHeight = 150;
            this._wrapPanel.ItemWidth = 200;
            this.Background = Brushes.CadetBlue;
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            
            this.AddChild(_wrapPanel);
        }


        public void AddGroupBox(MyGroupBox childGroupBox)
        {
            this._wrapPanel.AddElement(childGroupBox);
            childGroupBox.Margin = new Thickness(0, 40, 0, 10);
            //this.grid.AddControl(childGroupBox, _row, _column, _rowSpan, _columnSpan);
            _row += _rowSpan+1;
        }



       
    }
}
