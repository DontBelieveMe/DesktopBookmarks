using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopBookmarks.Model
{
    public interface IModelType : ICloneable
    {
        string Label { get; set; }
        string Id { get; set; }
        string ParentId { get; set; }
    }
}
