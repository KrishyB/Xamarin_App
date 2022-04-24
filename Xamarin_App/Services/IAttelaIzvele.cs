using System.IO;
using System.Threading.Tasks;

namespace Xamarin_App
{
    public interface IAttelaIzvele
    {
        Task<Stream> GetImageStreamAsync();
    }
}
