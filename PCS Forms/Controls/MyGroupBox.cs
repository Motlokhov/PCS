using System.Windows.Controls;
using System.Windows;
namespace PCS_Forms.Controls
{
    using System.Windows;
    using Database;
    public class MyGroupBox:GroupBox
    {
        private MyWrapPanel _wrappanel;

        public MyGroupBox()
        {
            this._wrappanel = new MyWrapPanel();
            this._wrappanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            this._wrappanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            this.AddChild(_wrappanel);
        }


        public void AddElement(UIElement element)
        {
            _wrappanel.AddElement(element);
            
        }

        public void WrapPanelAlignment(HorizontalAlignment horizontal,VerticalAlignment vertical)
        {
            this._wrappanel.HorizontalAlignment = horizontal;
            this._wrappanel.VerticalAlignment = vertical;
        }
    }
}
