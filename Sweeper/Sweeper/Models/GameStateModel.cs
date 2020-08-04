using Sweeper.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sweeper.Models
{
    public class GameStateModel : BaseModel
    {
        public GameStateModel(IPropertyRepository repo, bool loadFromRepo) : base(repo)
        {


        }
    }
}
