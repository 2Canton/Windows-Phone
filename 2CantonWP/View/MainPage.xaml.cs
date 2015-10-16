using _2CantonWP.Model;
using _2CantonWP.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Email;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace _2CantonWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<Opcion> obcOpciones = new ObservableCollection<Opcion>();

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            lstvOpcionesMenu.ItemsSource = obcOpciones;


            obcOpciones.Add(new Opcion() { Id = 0, Nombre = "Historia ", UrlImagen = "ms-appx:///Assets/history.png" });
            obcOpciones.Add(new Opcion() { Id = 1, Nombre = "Rutas de autobuses", UrlImagen = "ms-appx:///Assets/routes.png" });
            obcOpciones.Add(new Opcion() { Id = 2, Nombre = "Sitios de interés", UrlImagen = "ms-appx:///Assets/building.png" });
            obcOpciones.Add(new Opcion() { Id = 3, Nombre = "Eventos", UrlImagen = "ms-appx:///Assets/event.png" });
            obcOpciones.Add(new Opcion() { Id = 4, Nombre = "Religión", UrlImagen = "ms-appx:///Assets/church.png" });
            obcOpciones.Add(new Opcion() { Id = 5, Nombre = "Recolección de basura", UrlImagen = "ms-appx:///Assets/trash.png" });
            obcOpciones.Add(new Opcion() { Id = 6, Nombre = "Noticias", UrlImagen = "ms-appx:///Assets/news.png" });
            obcOpciones.Add(new Opcion() { Id = 7, Nombre = "Facebook", UrlImagen = "ms-appx:///Assets/facebook.png" });
            obcOpciones.Add(new Opcion() { Id = 8, Nombre = "Sitio Web", UrlImagen = "ms-appx:///Assets/website.png" });
            obcOpciones.Add(new Opcion() { Id = 9, Nombre = "Contacto", UrlImagen = "ms-appx:///Assets/message.png" });
            
        }

        private async void lstvOpcionesMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            Opcion objOpcion = e.ClickedItem as Opcion;

            switch (objOpcion.Id)
            {
                case 0:
                    this.Frame.Navigate(typeof(Historia));
                    break;

                case 1:
                    this.Frame.Navigate(typeof(Rutas));
                    break;

                case 2:
                    this.Frame.Navigate(typeof(View.TipoEmpresaView));
                    break;

                case 3:
                    this.Frame.Navigate(typeof(View.Evento));
                    break;

                case 4:
                    this.Frame.Navigate(typeof(View.TipoReligion));
                    break;

                case 5:
                    this.Frame.Navigate(typeof(View.RecoleccionBasuraView));
                    break;

                case 6:
                    this.Frame.Navigate(typeof(View.NoticiasView));
                    break;

                case 7:
                    NavegarUrl(@"https://www.facebook.com/mipuribus");
                    break;

                case 8:
                    NavegarUrl(@"http://www.mipuribus.com");
                    break;

                case 9:
                    EnviarEmail();
                    break;

                default:
                    break;
            }
        }


        private async void EnviarEmail()
        {
            // representa un receptor del mail
            EmailRecipient sendTo = new EmailRecipient()
            {
                Address = "nansoftwareinnovation@gmail.com"
            };

            //generate mail object
            EmailMessage mail = new EmailMessage();
            mail.Subject = "Consulta";

            //add recipients to the mail object
            mail.To.Add(sendTo);
            //mail.Bcc.Add(sendTo);
            //mail.CC.Add(sendTo);

            //open the share contract with Mail only:
            await EmailManager.ShowComposeNewEmailAsync(mail);

        }


        private async void NavegarUrl(string uriToLaunch)
        {
            // Create a Uri object from a URI string 
            var uri = new Uri(uriToLaunch);

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

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }


    }
}
