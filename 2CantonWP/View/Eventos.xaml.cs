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

        ParametroAux objParametro = new ParametroAux();
        private Windows.Media.SpeechSynthesis.SpeechSynthesizer speechSynthesizer;

        IEnumerable<Model.Evento> lstRutas;

        public Eventos()
        {
            this.InitializeComponent();
        }

        private async void cargarDatos(string pIdTipoEmpresa)
        {
            objParametro.Id = pIdTipoEmpresa;

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

                lstRutas = await query.ToListAsync();

                if (lstRutas.Count() == 0)
                {
                    txtError.Text = "¡Vaya parece que aún no hay datos en esta categoría!\nAnúnciese con nosotros info@mipuribus.com";
                    gridError.Visibility = Visibility.Visible;
                }


                if (objParametro.startMediaPlayer)
                {
                    startTextToSpeech();
                }

                lstvRutas.ItemsSource = lstRutas;
                progressRing.IsActive = false;


                


            }
            catch (Exception)
            {


            }

        }

        private async void startTextToSpeech()
        {
            try
            {

                this.speechSynthesizer = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                var voicesInstalled = from voiceInformation in Windows.Media.SpeechSynthesis.SpeechSynthesizer.AllVoices select voiceInformation;

                if (voicesInstalled.Count() > 0)
                {
                    var voiceInformation = voicesInstalled.ElementAt(0) as Windows.Media.SpeechSynthesis.VoiceInformation;
                    this.speechSynthesizer.Voice = voiceInformation;

                    string mensajeLeer = "";
                    if(lstRutas.Count() != 0)
                    {
                        mensajeLeer = "Información de eventos " + lstRutas.ElementAt(0).Descripcion;
                    }
                    else
                    {
                        mensajeLeer = "Aún no hay eventos en esta categoría";
                    }

                    var stream = await this.speechSynthesizer.SynthesizeTextToStreamAsync(mensajeLeer);
                    feedbackMediaElement.SetSource(stream, stream.ContentType);
                    feedbackMediaElement.Play();
                }
            }
            catch (Exception exception)
            {
                var messageDialog = new Windows.UI.Popups.MessageDialog(exception.Message, "Exception");
                messageDialog.ShowAsync().GetResults();
            }
        }
    

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ParametroAux objParametroAux = e.Parameter as ParametroAux;

            if (objParametroAux != null)
            {
                objParametro = objParametroAux;
                cargarDatos(objParametroAux.Id);

            }
        }

        private void lstvRutas_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            cargarDatos(objParametro.Id);
        }

        private void btnSpeech_Click(object sender, RoutedEventArgs e)
        {
            startTextToSpeech();
        }

        private void btnSpeechStop_Click(object sender, RoutedEventArgs e)
        {
            feedbackMediaElement.Stop();
        }
    }
}
