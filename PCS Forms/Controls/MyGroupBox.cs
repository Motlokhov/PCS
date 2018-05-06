using System.Windows.Controls;
using System.Windows;
namespace PCS_Forms.Controls
{
    using System.Windows;
    using Database;
    public class MyGroupBox:GroupBox
    {
        private MyStackPanel _stackPanel;

        public MyGroupBox()
        {
            this._stackPanel = new MyStackPanel(Orientation.Horizontal);
            //this._stackPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            //this._stackPanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            this.AddChild(_stackPanel);
            
        }

        public MyStackPanel GetStackPanel()
        {
            return _stackPanel;
        }

        public void AddElement(UIElement element)
        {
            _stackPanel.AddElement(element);
            
        }
    }
}
