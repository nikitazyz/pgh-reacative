using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reacative.Domain.State;

namespace Reacative.Domain.Cats
{
    public class CatNameGenerator : ICatNameGenerator
    {
        private string[] _names;
        private readonly Random _random = new Random();
        private readonly ICatNamesProvider _catNamesProvider;

        public CatNameGenerator(ICatNamesProvider catNamesProvider)
        {
            _catNamesProvider = catNamesProvider;
        }

        public async Task<string[]> Generate(GameState gameState, int count)
        {
            _names ??= await _catNamesProvider.GetNames();
            var availableNames = _names.Except(gameState.GeneratedCats.Select(c => c.Name)).ToList();
            var result = new string[count];
            for (int i = 0; i < count; i++)
            {
                int element;
                if (availableNames.Count <= 0)
                {
                    element = _random.Next(_names.Length);
                    result[i] = _names[element];
                    continue;
                }
                element = _random.Next(availableNames.Count);
                result[i] = availableNames[element];
                availableNames.RemoveAt(element);
            }

            return result;
        }
    }
}