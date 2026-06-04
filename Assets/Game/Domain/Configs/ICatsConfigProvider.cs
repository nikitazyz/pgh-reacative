using System.Collections.Generic;
using System.Threading.Tasks;
using Reacative.Domain.Cats;

namespace Reacative.Domain.Configs
{
    public interface ICatsConfigProvider : ICatNamesProvider
    {
        public int HeadHunterSize { get; }
        public int HireBaseCost { get; }
        public int HireCostMultiplier { get; }
    }
}