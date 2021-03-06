﻿using _2CantonWP.Model;
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
    public sealed partial class NoticiasView : Page
    {
        public NoticiasView()
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
            ObservableCollection<Noticia> obcRutas = new ObservableCollection<Noticia>();

            try
            {
                IMobileServiceTable<Noticia> rutaTable = App.clientMobileService.GetTable<Noticia>();
                IMobileServiceTableQuery<Noticia> query = rutaTable.OrderBy(e => e.Nombre);

                IEnumerable<Noticia> lstRutas = await query.ToListAsync();

               
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
        }

        private void lstvRutas_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
