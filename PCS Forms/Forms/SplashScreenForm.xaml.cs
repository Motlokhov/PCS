using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PCS_Forms.Forms
{
    /// <summary>
    /// Interaction logic for SplashScreenForm.xaml
    /// </summary>
    public partial class SplashScreenForm : Window
    {
        public SplashScreenForm()
        {
            InitializeComponent();
            Show();
            HideWindow();
        }

        public async void HideWindow()
        {
            double opacity = Opacity;
            var progress = new Progress<double>(s => Opacity = s);
            
                double result = await Task.Factory.StartNew<double>
                    (
                     () => Worker.OpacityDecrease(progress)
                    );
                Opacity = result;
                Close();
        }

       
    }

    class Worker
    {
        public static double OpacityDecrease(IProgress<double> progress)
        {
            double opacity = 1;
            while (opacity > 0)
            {
                Task.Delay(150).Wait();
                opacity -= 0.05;
                progress.Report(opacity);
            }
            return opacity;
        }
    }
}
