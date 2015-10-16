using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.SpeechRecognition;
using Windows.Storage;
using Windows.UI.Popups;

namespace DemoCortana.Helpers
{
    public class VCDHelper
    {
        public async Task InstallVCD(string filePath)
        {
            try
            {
                // creamos una uri de la dirección del archivo
                Uri vcdUri = new Uri(filePath, UriKind.Absolute);

                // almacenamos el archivo
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(vcdUri);

                // instalamos el archivo
                await VoiceCommandManager.InstallCommandSetsFromStorageFileAsync(file);
            }
            catch (Exception ex)
            {

                MessageDialog message = new MessageDialog("Problemas al instalar el archivo VCD " + ex.ToString());
                await message.ShowAsync();
            }
        }
    }
}
