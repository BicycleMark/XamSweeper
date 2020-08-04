using Sweeper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sweeper.Infrastructure
{
    public interface IGridFactory
    {
        GridPoint NDXToGridPoint(int ndx, int r, int c);
        int GridPointToNDX(GridPoint gp, int r, int c );
        List<GridPoint> MinePlacements(GridPoint excludeThisPoint, int r, int c, List<GridPoint> desiredLocations = null);

    }
}
