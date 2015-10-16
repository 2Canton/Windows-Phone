using _2CantonWP.Model;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Text;
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
    public sealed partial class DescripcionHorario : Page
    {
        public DescripcionHorario()
        {
            this.InitializeComponent();
        }

        private async void cargarDatos(string pIdRuta, string pIdHorario)
        {
            if (App.NetworkAvailable)
            {
                //Hay conexión a Internet
                progressRing.IsActive = true;

                getHorarios(pIdRuta, pIdHorario);

            }
            else
            {
                //No hay conexión a Internet
                MessageDialog info = new MessageDialog("Verfique la conexión a Internet");
                await info.ShowAsync();
            }
        }

        private async void getHorarios(string pIdRuta, string pIdHorario)
        {
            IMobileServiceTable<CarreraRuta> tableCarreraRuta = App.clientMobileService.GetTable<CarreraRuta>();

            List<CarreraRuta> lstHorarios = await tableCarreraRuta.Where(e => e.idRuta == pIdRuta && e.idHorario == pIdHorario).OrderBy(e => e.hora).ToListAsync();

            TextBlock txtParada;
            TextBlock txtHora;

            int contador = 1;
            RowDefinition gridRow;

            string pattern = "HH:mm";


            DateTime parsedDate;



            int horaAux = 0;
            string sufijo = " am";
            string hora;

            foreach (CarreraRuta item in lstHorarios)
            {
                gridRow = new RowDefinition();

                grdHorario.RowDefinitions.Add(gridRow);

                // Add the first text cell to the Grid
                txtParada = new TextBlock();
                txtParada.Text = await getNombreParada(item.idSitioSalida);
                txtParada.FontSize = 12;
                txtParada.FontWeight = FontWeights.Bold;
                txtParada.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                Grid.SetRow(txtParada, contador);
                Grid.SetColumn(txtParada, 0);

                // Add the second text cell to the Grid
                txtHora = new TextBlock();

                try
                {

                    hora = Convert.ToDateTime(item.hora).ToString("HH:mm", CultureInfo.InvariantCulture);
                    horaAux = int.Parse(hora.Substring(0, 2));
                    if (horaAux == 12)
                    {
                        sufijo = " md";
                    }
                    else if (horaAux > 12)
                    {
                        sufijo = " pm";
                    }
                    txtHora.Text = hora + sufijo; ;
                }
                catch (Exception s)
                {


                }

                txtHora.FontSize = 12;
                txtHora.FontWeight = FontWeights.Bold;
                txtHora.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                Grid.SetRow(txtHora, contador);
                Grid.SetColumn(txtHora, 1);

                grdHorario.Children.Add(txtParada);
                grdHorario.Children.Add(txtHora);

                contador += 1;
            }
            progressRing.IsActive = false;

        }

        private async Task<string> getNombreParada(string pIdParada)
        {
            IMobileServiceTable<Parada> tableParada = App.clientMobileService.GetTable<Parada>();

            Parada objParada = await tableParada.LookupAsync(pIdParada);

            return objParada.nombre;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RutaHorario objRutaHorario = e.Parameter as RutaHorario;

            if (objRutaHorario != null)
            {
                cargarDatos(objRutaHorario.idRuta, objRutaHorario.idHorario);
            }
        }
    }
}
