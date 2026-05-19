using System;
using System.Linq;
using Reacative.Domain.State;

namespace Reacative.Domain.Cats
{
    public class CatColorGenerator : ICatColorGenerator
    {
        private readonly Random _random = new Random();
        public CatColor[] GenerateColor(GameState gameState, int count)
        {
            var colors = (CatColor[])Enum.GetValues(typeof(CatColor));
            var generatedColors = gameState.GeneratedCats.Where(c=> !gameState.IsCatHired(c)).Select(c=>c.Color);

            var availableColors = colors.Except(generatedColors).ToList();
            var result = new CatColor[count];
            for (int i = 0; i < count; i++)
            {
                int element;
                if (availableColors.Count <= 0)
                {
                    element = _random.Next(colors.Length);
                    result[i] = colors[element];
                    continue;
                }

                element = _random.Next(availableColors.Count);
                result[i] = colors[element];
                availableColors.RemoveAt(element);
            }
            
            return result;
        }
    }
}