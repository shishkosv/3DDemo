using System;
using System.Collections.Generic;

namespace Oiler.Infrastructure.Constants
{
    public static class Constants
    {
        public static readonly List<string> Cities = new List<string> { "Austin", "News York", "Moscow" };

        public static readonly List<string> ViewModes = new List<string> { "Chart", "3D" };

        public static readonly List<string> VisualModels = new List<string> { "Chimney", "House", "Well", "Surface Plot"};

        public const string ChimneyModelName = "Chimney";

        public const string HouseModelName = "House";

        public const string WellModelName = "Well";

        public static readonly string SurfacePloModelName = "Surface Plot";
    }
}
