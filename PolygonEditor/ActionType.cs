using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonEditor
{
    public enum ActionType
    {
        Default,
        AddNewShape,
        Painting,
        MovingVertex,
        MovingEdge,
        MovingShape,
        RemovingVertex,
        OffsettingPolygon
    }
}
