using _2CantonWP.Model;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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
    public sealed partial class Rutas : Page
    {
        public Rutas()
        {
            this.InitializeComponent();

            cargarDatos();
        }

        private async void cargarDatos()
        {
            if (App.NetworkAvailable)
            {
                gridError.Visibility = Visibility.Collapsed;

                //Hay conexión a Internet
                progressRing.IsActive = true;
                getRutas();

            }
            else
            {
                //No hay conexión a Internet
                MessageDialog info = new MessageDialog("Verfique la conexión a Internet");
                await info.ShowAsync();

                gridError.Visibility = Visibility.Visible;
            }
        }

        private async void getRutas()
        {
            ObservableCollection<Ruta> obcRutas = new ObservableCollection<Ruta>();

            try
            {
                IMobileServiceTable<Ruta> rutaTable = App.clientMobileService.GetTable<Ruta>();
                IMobileServiceTableQuery<Ruta> query = rutaTable.OrderBy(e => e.Nombre);

                IEnumerable<Ruta> lstRutas = await query.ToListAsync();

                foreach (Ruta item in lstRutas)
                {
                    switch (item.IdEmpresa)
                    {
                        case "0":
                            item.UrlImagen = "ms-appx:///Assets/bus2.png";
                            break;

                        case "1":
                            item.UrlImagen = "ms-appx:///Assets/bus1.png";
                            break;

                        default:
                            break;
                    }
                }


                lstvRutas.ItemsSource = lstRutas;
                progressRing.IsActive = false;

                if (lstRutas.Count() == 0)
                {
                    gridError.Visibility = Visibility.Visible;
                }
            }
            catch (Exception)
            {


            }

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
          
        }


        private void lstvRutas_ItemClick(object sender, ItemClickEventArgs e)
        {
            Ruta objRuta = e.ClickedItem as Ruta;
            NavegarRuta(objRuta.Id);

        }

        private void NavegarRuta(string pIdRuta)
        {
            
            this.Frame.Navigate(typeof(Horarios), pIdRuta);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            cargarDatos();
        }
    }
}
