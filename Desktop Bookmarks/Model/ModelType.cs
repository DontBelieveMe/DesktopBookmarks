using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopBookmarks.Model
{
    public interface IModelType
    {
        string Label { get; set; }
        string Id { get; set; }
    }
}
