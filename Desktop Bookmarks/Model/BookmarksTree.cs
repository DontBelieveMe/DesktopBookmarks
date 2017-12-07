using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DesktopBookmarks.Model
{
    class BookmarksTree : ICloneable
    {
        public List<IModelType> Bookmarks = new List<IModelType>();

        public void Read(string filename)
        {
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(filename);
                
            } catch(System.IO.IOException e)
            {
                return;
            }
            if (document.ChildNodes.Count == 0) return;
            XmlNode rootNode = document.ChildNodes[1];
            foreach(XmlNode child in rootNode.ChildNodes)
            {
               IModelType type = ReadNode(child, document);
                Bookmarks.Add(type);
            }
        }

        private IModelType ReadNode(XmlNode node, XmlDocument document)
        {
            if(node.Name == "Folder")
            {
                string label = node.Attributes[0].Value;
                string id = node.Attributes[1].Value;
                string parentId = node.Attributes[2].Value;
                Folder folder = new Folder(label, id, parentId);
                foreach(XmlNode child in node.ChildNodes)
                {
                    IModelType item = ReadNode(child, document);
                    folder.Children.Add(item);
                }
                return folder;

            } else if(node.Name == "Bookmark")
            {
                string url = node.Attributes[0].Value;
                string label = node.Attributes[1].Value;
                string id = node.Attributes[2].Value;
                string parentId = node.Attributes[3].Value;
                return new Bookmark(url, label, id, parentId);
            }
            throw new NotImplementedException();
        }

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
                writer.WriteAttributeString("label", ((Folder)node).Label);
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

        public object Clone()
        {
            BookmarksTree newTree = new BookmarksTree();

            foreach(IModelType node in Bookmarks)
            {
                newTree.Bookmarks.Add(CloneChildNode(node));
            }

            return newTree;
        }

        private IModelType CloneChildNode(IModelType node)
        {
            IModelType model = (IModelType) node.Clone();

            if(model.GetType() == typeof(Folder))
            {
                foreach(IModelType child in ((Folder)node).Children)
                {
                    IModelType clonedChild = CloneChildNode(child);
                    ((Folder)model).Children.Add(clonedChild);
                }
            }

            return model;
        }
    }
}
