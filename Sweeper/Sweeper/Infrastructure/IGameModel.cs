using Sweeper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sweeper.Infrastructure
{
    public interface IGameModel
    {
        IBoardModel Board { get; set; }
        IPropertyRepository Repo { get; set; }
    }
}
