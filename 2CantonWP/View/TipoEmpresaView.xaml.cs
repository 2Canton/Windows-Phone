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
    public sealed partial class TipoEmpresaView : Page
    {
        public TipoEmpresaView()
        {
            this.InitializeComponent();

            cargarDatos();
        }

        private async void cargarDatos()
        {
            if (App.NetworkAvailable)
            {
                //Hay conexión a Internet
                progressRing.IsActive = true;
                getRutas();

            }
            else
            {
                //No hay conexión a Internet
                MessageDialog info = new MessageDialog("Verfique la conexión a Internet");
                await info.ShowAsync();
            }
        }

        private async void getRutas()
        {
            ObservableCollection<TipoEmpresa> obcTipoEmpresa = new ObservableCollection<TipoEmpresa>();

            try
            {
                IMobileServiceTable<TipoEmpresa> tipoEmpresaTable = App.clientMobileService.GetTable<TipoEmpresa>();
                IMobileServiceTableQuery<TipoEmpresa> query = tipoEmpresaTable.OrderBy(e => e.Nombre);

                IEnumerable<TipoEmpresa> lstTipoEmpresa = await query.ToListAsync();

                lstvRutas.ItemsSource = lstTipoEmpresa;
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
        }

        private void lstvRutas_ItemClick(object sender, ItemClickEventArgs e)
        {
            TipoEmpresa objEmpresa = e.ClickedItem as TipoEmpresa;
            this.Frame.Navigate(typeof(Empresas), objEmpresa.Id);
        }
    }
}
