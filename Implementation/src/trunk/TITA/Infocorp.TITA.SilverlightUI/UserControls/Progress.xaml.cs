using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Infocorp.TITA.SilverlightUI.UserControls
{
    public partial class Progress : UserControl
    {
        public Progress()
        {
            InitializeComponent();
            //animacion.Begin();
            canvas.Opacity = 0.0;
            gridBK.Visibility = Visibility.Collapsed;
            mostrarOcultar.Completed += new EventHandler(mostrarOcultar_Completed);
        }
        bool pararAnim = false;
        void mostrarOcultar_Completed(object sender, EventArgs e)
        {
            if (pararAnim)
                animacion.Stop();
        }

        public void play()
        {
            mostrarOcultar_value.Value = 1.0;
            mostrarOcultar.Begin();
            gridBK.Visibility = Visibility.Visible;
            pararAnim = false;
            animacion.Begin();
        }
        public void stop()
        {
            mostrarOcultar_value.Value = 0.0;
            mostrarOcultar.Begin();
            gridBK.Visibility = Visibility.Collapsed;
            pararAnim = true;
        }
    }
}
