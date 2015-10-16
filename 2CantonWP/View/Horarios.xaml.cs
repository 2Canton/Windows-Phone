using _2CantonWP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechRecognition;
using Windows.UI.Popups;
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
    public sealed partial class Horarios : Page
    {
        string idRuta = "0";
        public Horarios()
        {
            this.InitializeComponent();
        }

        private async void cargarDatos(string pIdRuta)
        {
            if (App.NetworkAvailable)
            {
                //Hay conexión a Internet
                progressRing.IsActive = true;
                idRuta = pIdRuta;
                getHorariosRutas(pIdRuta);

            }
            else
            {
                //No hay conexión a Internet
                MessageDialog info = new MessageDialog("Verfique la conexión a Internet");
                await info.ShowAsync();
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (!string.IsNullOrEmpty(e.Parameter.ToString()))
            {
                // verificamos si se activo por voz
                if (e.NavigationMode == NavigationMode.New)
                {
                    var result = e.Parameter as SpeechRecognitionResult;
                    string idRuta = "1";

                    switch (result.Text)
                    {
                        case "Ruta Desamparaditos":
                            idRuta = "149";
                            break;

                        case "Ruta Grifo Alto":
                            idRuta = "150";
                            break;

                        case "Ruta La Legua":
                            idRuta = "169";
                            break;

                        case "Ruta Mercedes Norte":
                            idRuta = "3";
                            break;

                        case "Ruta Polka":
                            idRuta = "151";
                            break;

                        case "Ruta Pozos":
                            idRuta = "148";
                            break;

                        case "Ruta San Juan":
                            idRuta = "1";
                            break;

                        case "Ruta San Rafael":
                            idRuta = "2";
                            break;

                        case "Ruta San Ramón":
                            idRuta = "171";
                            break;

                        case "Ruta Turrubares":
                            idRuta = "152";
                            break;

                        case "Ruta Zapatón":
                            idRuta = "198";
                            break;

                        default:
                            break;
                    }

                    cargarDatos(idRuta);

                }
            }
            else
            {
                string pidRuta = e.Parameter as string;

                if (!string.IsNullOrWhiteSpace(pidRuta))
                {

                    cargarDatos(pidRuta);

                }
            }
            
        }

        private async void getHorariosRutas(string pIdRuta)
        {
            ObservableCollection<Ruta> obcRutas = new ObservableCollection<Ruta>();

            try
            {


                List<Horario> lstHorarios = await App.clientMobileService.InvokeApiAsync<List<Horario>>("horarioruta", HttpMethod.Get, new Dictionary<string, string> { { "id", pIdRuta } });


                ObservableCollection<Horario> obcHorario = new ObservableCollection<Horario>();
                Random random = new Random();
                string rutaImagen = "";

                foreach (Horario item in lstHorarios)
                {

                    switch (random.Next(1, 10))
                    {
                        case 1:
                            rutaImagen = "ms-appx:///Assets/calendar1.png";
                            break;

                        case 2:
                            rutaImagen = "ms-appx:///Assets/calendar2.png";
                            break;

                        case 3:
                            rutaImagen = "ms-appx:///Assets/calendar3.png";
                            break;

                        case 4:
                            rutaImagen = "ms-appx:///Assets/calendar4.png";
                            break;

                        case 5:
                            rutaImagen = "ms-appx:///Assets/calendar5.png";
                            break;

                        case 6:
                            rutaImagen = "ms-appx:///Assets/calendar6.png";
                            break;

                        case 7:
                            rutaImagen = "ms-appx:///Assets/calendar7.png";
                            break;

                        case 8:
                            rutaImagen = "ms-appx:///Assets/calendar8.png";
                            break;

                        case 9:
                            rutaImagen = "ms-appx:///Assets/calendar9.png";
                            break;

                        case 10:
                            rutaImagen = "ms-appx:///Assets/calendar10.png";
                            break;

                        default:
                            rutaImagen = "ms-appx:///Assets/calendar1.png";
                            break;
                    }

                    item.UrlImagen = rutaImagen;

                    obcHorario.Add(item);
                }


                lstvRutas.ItemsSource = obcHorario;
                progressRing.IsActive = false;
            }
            catch (Exception)
            {


            }

        }

        private void lstvRutas_ItemClick(object sender, ItemClickEventArgs e)
        {
            Horario objHorario = e.ClickedItem as Horario;

            this.Frame.Navigate(typeof(DescripcionHorario), new RutaHorario() { idRuta = idRuta, idHorario = objHorario.Id });
        }
    }
}
