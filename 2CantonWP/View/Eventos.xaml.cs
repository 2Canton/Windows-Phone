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
    public sealed partial class Eventos : Page
    {

        string idTipoEventos;

        public Eventos()
        {
            this.InitializeComponent();
        }

        private async void cargarDatos(string pIdTipoEmpresa)
        {
            idTipoEventos = pIdTipoEmpresa;

            if (App.NetworkAvailable)
            {
                gridError.Visibility = Visibility.Collapsed;

                //Hay conexión a Internet
                progressRing.IsActive = true;
                getRutas(pIdTipoEmpresa);

            }
            else
            {
                //No hay conexión a Internet
                MessageDialog info = new MessageDialog("Verfique la conexión a Internet");
                await info.ShowAsync();

                txtError.Text = "¡Vaya ha ocurrido un error al cargar, verifica la conexión a internet e intenta de nuevo";
                gridError.Visibility = Visibility.Visible;

                
            }
        }

        private async void getRutas(string pIdTipoEvento)
        {
            ObservableCollection<Model.Evento> obcRutas = new ObservableCollection<Model.Evento>();

            try
            {
                IMobileServiceTable<Model.Evento> empresaTable = App.clientMobileService.GetTable<Model.Evento>();
                IMobileServiceTableQuery<Model.Evento> query = empresaTable.Where(e => e.IdTipoEvento == pIdTipoEvento && e.Visible == true).OrderBy(e => e.FechaAux);

                IEnumerable<Model.Evento> lstRutas = await query.ToListAsync();

                if (lstRutas.Count() == 0)
                {
                    txtError.Text = "¡Vaya parece que aún no hay datos en esta categoría!\nAnúnciese con nosotros info@mipuribus.com";
                    gridError.Visibility = Visibility.Visible;
                }


                lstvRutas.ItemsSource = lstRutas;
                progressRing.IsActive = false;
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
            string idTipoEvento = e.Parameter as string;

            if (!string.IsNullOrWhiteSpace(idTipoEvento))
            {

                cargarDatos(idTipoEvento);

            }
        }

        private void lstvRutas_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            cargarDatos(idTipoEventos);
        }
    }
}
