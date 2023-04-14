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
        public double weight;
        public ListItem(String label, double weight)
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
        public void addLink(String label, double weight)
        {
            this.links.Add(new ListItem(label, weight));
        }
    }
}
