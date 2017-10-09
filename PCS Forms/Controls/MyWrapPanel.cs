using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PCS_Forms.Controls
{
    public class MyWrapPanel:WrapPanel
    {
        public MyWrapPanel()
        {
        }

        public void AddElement(UIElement element)
        {
            this.Children.Add(element);
        }
    }
}
