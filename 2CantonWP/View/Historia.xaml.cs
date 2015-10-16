using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace _2CantonWP.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Historia : Page
    {
        public Historia()
        {
            this.InitializeComponent();

            String resenia = " \nPuriscal es el cantón 4 de la provincia de San José de Costa Rica. Su capital es la ciudad de Santiago de Puriscal.\n";

            resenia += "\nEn el cantón de Puriscal se encuentran varios patrimonios históricos nacionales, tales como el Templo de Barbacoas, el Templo de Pedernal y el emblemático Antiguo Templo Católico de Puriscal, decretado como Patrimonio histórico arquitectónico de Costa Rica. Otro monumento emblemático es El Sapo, que está situado en el Parque del Agricultor de Puriscal.\n";

            resenia += "\nAntiguamente, la región que hoy corresponde al cantón de Puriscal fue conocida como Cola de Pavo, así denominada por dos comerciantes, Jorge y Jesús Retana, que comerciaban en este lugar cuando en él solo había pocas familias residentes. El nombre del cantón proviene de la evolución de la palabra purisco, la que se refiere al momento en el que el frijol está en flor. Puriscal en sus orígenes fue un lugar conocido por sus sembradíos de frijoles.\n";



            txtvHistoria.Text = resenia;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}
