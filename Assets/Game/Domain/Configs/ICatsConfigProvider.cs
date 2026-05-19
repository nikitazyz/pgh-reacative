using System.Collections.Generic;

namespace Reacative.Domain.Configs
{
    public interface ICatsConfigProvider
    {
        public int HeadHunterSize { get; }
        public int HireBaseCost { get; }
        public int HireCostMultiplier { get; }
        public IEnumerable<string> Names { get; }
    }
}