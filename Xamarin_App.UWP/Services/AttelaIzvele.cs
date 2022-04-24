using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Xamarin.Forms;
using Xamarin_App.UWP;

[assembly: Dependency(typeof(AttelaIzvele))]
namespace Xamarin_App.UWP
{
    internal class AttelaIzvele : IAttelaIzvele
    {
        public async Task<Stream> GetImageStreamAsync()
        {
            FileOpenPicker izvele = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary,
            };
            izvele.FileTypeFilter.Add(".jpg");
            izvele.FileTypeFilter.Add(".jpeg");
            izvele.FileTypeFilter.Add(".png");

            StorageFile fails = await izvele.PickSingleFileAsync();

            if (fails == null)
                return null;

            IRandomAccessStreamWithContentType fStream = await fails.OpenReadAsync();
            return fStream.AsStreamForRead();
        }
    }
}
