using System; 
using Cysharp.Threading.Tasks;

namespace MornLib
{
    public interface IMornTween : IDisposable
    {
        UniTask GetAwaiter();
    }
}