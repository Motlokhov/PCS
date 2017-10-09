using System.Windows.Controls;
namespace PCS_Forms.Controls
{
    using System.Windows;
    using Database;
    public class MyGroupBox:GroupBox
    {
        MyWrapPanel wrappanel;
        public MyGroupBox()
        {
            this.wrappanel = new MyWrapPanel();
            this.wrappanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            this.wrappanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            this.AddChild(wrappanel);
        }


        public void AddElement(UIElement element)
        {
            wrappanel.AddElement(element);
            
        }
    }
}
