using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DesktopBookmarks.Model
{
    class BookmarksTree
    {
        public List<IModelType> Bookmarks = new List<IModelType>();

        public void WriteToFile(string filename)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter xmlWriter = XmlWriter.Create(filename, settings);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Bookmarks");
            foreach (IModelType node in Bookmarks)
            {
                WriteChildNodes(node, ref xmlWriter);
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }

        private void WriteChildNodes(IModelType node, ref XmlWriter writer)
        {
            if(IsFolder(node))
            {
                writer.WriteStartElement("Folder");
                writer.WriteAttributeString("id", node.Id);
                writer.WriteAttributeString("parentId", node.ParentId);
                foreach (IModelType kids in ((Folder)node).Children)
                {
                    WriteChildNodes(kids, ref writer);
                }
            } else
            {
                Bookmark mark = (Bookmark)node;
                writer.WriteStartElement("Bookmark");
                writer.WriteAttributeString("url", mark.Url);
                writer.WriteAttributeString("label", mark.Label);
                writer.WriteAttributeString("id", node.Id);
                writer.WriteAttributeString("parentId", node.ParentId);
            }
            writer.WriteEndElement();
        }

        private bool IsFolder(IModelType type)
        {
            return type.GetType() == typeof(Folder);
        }
    }
}
