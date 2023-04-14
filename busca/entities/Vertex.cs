using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace busca.entities
{
    internal class Vertex
    {
        public String labelColumn;
        public String labelRow;
        public double weight;

        public Vertex(String labelColumn, String labelRow)
        {
            this.labelColumn = labelColumn;
            this.labelRow = labelRow;
            this.weight = 0;
        }
    }
}
