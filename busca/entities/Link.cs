using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace busca.entities
{
    internal class ListItem
    {
        public String label;
        public int weight;
        public ListItem(String label, int weight)
        {
            this.label = label;
            this.weight = weight;
        }
    }
    internal class Link
    {
        public String label;
        public List<ListItem> links = new List<ListItem>();
        public Link(String label)
        {
            this.label = label;
        }
        public void addLink(String label, int weight)
        {
            this.links.Add(new ListItem(label, weight));
        }
    }
}
