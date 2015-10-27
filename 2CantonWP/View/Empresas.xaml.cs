using _2CantonWP.Model;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Email;
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
    public sealed partial class Empresas : Page
    {
        string idTipoEmpresa;
        Empresa objEmpresaAux;
        public Empresas()
        {
            this.InitializeComponent();
            objEmpresaAux = new Empresa();

        }

        private async void cargarDatos(string pIdTipoEmpresa)
        {
            idTipoEmpresa = pIdTipoEmpresa;

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

        private async void getRutas(string pIdTipoEmpresa)
        {
            ObservableCollection<Ruta> obcRutas = new ObservableCollection<Ruta>();

            try
            {
                IMobileServiceTable<Empresa> empresaTable = App.clientMobileService.GetTable<Empresa>();
                IMobileServiceTableQuery<Empresa> query = empresaTable.Where(e => e.IdTipoEmpresa == pIdTipoEmpresa && e.Visible == true).OrderBy(e => e.Nombre);

                IEnumerable<Empresa> lstRutas = await query.ToListAsync();

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
            string idTipoEmpresa = e.Parameter as string;

            if (!string.IsNullOrWhiteSpace(idTipoEmpresa))
            {

                cargarDatos(idTipoEmpresa);

            }
        }

        private void lstvRutas_ItemClick(object sender, ItemClickEventArgs e)
        {
            Empresa objEmpresa = e.ClickedItem as Empresa;
            objEmpresaAux = objEmpresa;
        }

        private void imgvTelefono_Tapped(object sender, TappedRoutedEventArgs e)
        {


            Windows.ApplicationModel.Calls.PhoneCallManager.ShowPhoneCallUI(objEmpresaAux.TelefonoPrincipal, objEmpresaAux.Nombre);

        }

        private async void imgvEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // representa un receptor del mail
            EmailRecipient sendTo = new EmailRecipient()
            {
                Address = objEmpresaAux.Email
            };

            //genera el objeto mail
            EmailMessage mail = new EmailMessage();
            mail.Subject = "Consulta";

            //agrega los receptores del mail
            mail.To.Add(sendTo);

            //abre una ventana en la que se selecciona el correo con el que se debe enviar
            await EmailManager.ShowComposeNewEmailAsync(mail);

        }

        private async void imgvMapa_Tapped(object sender, TappedRoutedEventArgs e)
        {

            // Assemble the Uri to launch.
            Uri uri = new Uri("ms-walk-to:?destination.latitude=" + objEmpresaAux.Latitud +
                "&destination.longitude=" + objEmpresaAux.Longitud + "&destination.name=Santiago, Costa Rica");
            // The resulting Uri is: "ms-drive-to:?destination.latitude=47.6451413797194
            //  &destination.longitude=-122.141964733601&destination.name=Redmond, WA")

            // Launch the Uri.
            var success = await Windows.System.Launcher.LaunchUriAsync(uri);

            if (success)
            {
                // Uri launched.
            }
            else
            {
                // Uri failed to launch.
            }

        }

        private async void imgvWeb_Tapped(object sender, TappedRoutedEventArgs e)
        {
         
            try
            {
                // Create a Uri object from a URI string 
                var uri = new Uri(@objEmpresaAux.Web);

                // Launch the URI

                // Launch the URI
                var success = await Windows.System.Launcher.LaunchUriAsync(uri);

                if (success)
                {
                    // URI launched
                }
                else
                {
                    // URI launch failed
                }
            }
            catch (Exception error)
            {

            }
        
            
        
    }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            cargarDatos(idTipoEmpresa);
        }
    }
}
