using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopBookmarks.Model
{
    public class Folder : IModelType
    {
        public string Label { get; set; }
        public string Id { get; set; }
        public string ParentId { get; set; }

        public List<IModelType> Children = new List<IModelType>();
        
        public Folder(string label, string id, string parentId)
        {
            this.Label = label;
            this.Id = id;
            this.ParentId = parentId;
        }
    }
}
