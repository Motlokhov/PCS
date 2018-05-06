using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PCS_Forms.Controls
{
    public class MyStackPanel:StackPanel
    {
        public MyStackPanel(Orientation orientation = System.Windows.Controls.Orientation.Vertical )
        {
            Orientation = orientation;
        }

        public void AddElement(UIElement element)
        {
            Children.Add(element);
        }
    }
}
