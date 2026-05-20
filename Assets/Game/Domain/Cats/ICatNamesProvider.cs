using System.Threading.Tasks;

namespace Reacative.Domain.Cats
{
    public interface ICatNamesProvider
    {
        public Task<string[]> GetNames();
    }
}