using System;
using System.Threading.Tasks;
using Reacative.Domain.State;

namespace Reacative.Domain.Cats
{
    public class CatsGenerator
    {
        private readonly ICatNameGenerator _nameGenerator;
        private readonly ICatColorGenerator _colorGenerator;

        public CatsGenerator(ICatNameGenerator nameGenerator, ICatColorGenerator colorGenerator)
        {
            _nameGenerator = nameGenerator;
            _colorGenerator = colorGenerator;
        }

        public async Task<CatState[]> GetNext(GameState gameState, int count)
        {
            var catName = await _nameGenerator.Generate(gameState, count);
            var catColor = _colorGenerator.GenerateColor(gameState, count);

            var result = new CatState[count];
            for (int i = 0; i < count; i++)
            {
                var id = Guid.NewGuid().ToString();
                result[i] = new CatState(id, catName[i], catColor[i], false);
            }
            return result;
        }
    }
}