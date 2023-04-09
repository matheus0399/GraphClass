using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace busca.entities
{
    internal class Node {
        public String label;
        public String last;
        public Boolean closed;
        public int distance;
        public Node(String label, String last, Boolean closed, int distance) { 
            this.label = label;
            this.last = last;
            this.closed = closed;
            this.distance = distance;
        }
    }
    internal class Search
    {
        public String origin = "";
        public Search() { }

        public List<String> deepSearch(ListGraph lGp, MatrixGraph mGp, String origin, String destination) {
            if (origin == destination) {
                Console.WriteLine("Choose different vertices to navigate");
                return new List<String>();
            }
            this.origin = origin;
            if (lGp == null)
            {
                if (mGp.getVertexIndex(origin) == -1)
                {
                    Console.WriteLine("origin vertex does not exists");
                    return new List<String>();
                }
                return this.deepSearchMethod1(mGp, new List<String>(), origin, destination, new List<String>());
            }
            if (lGp.getVertexIndex(origin) == -1)
            {
                Console.WriteLine("origin vertex does not exists");
                return new List<String>();
            }
            return this.deepSearchMethod2(lGp, new List<String>(), origin, destination, new List<String>());
        }

        public List<String> broadSearch(ListGraph lGp, MatrixGraph mGp, String origin, String destination)
        {
            if (origin == destination)
            {
                Console.WriteLine("Choose different vertices to navigate");
                return new List<String>();
            }
            this.origin = origin;
            if (lGp == null)
            {
                if (mGp.getVertexIndex(origin) == -1)
                {
                    Console.WriteLine("origin vertex does not exists");
                    return new List<String>();
                }
                return this.broadSearchMethod1(mGp, new List<String>(), origin, destination, new List<String>());
            }
            if (lGp.getVertexIndex(origin) == -1)
            {
                Console.WriteLine("origin vertex does not exists");
                return new List<String>();
            }
            return this.broadSearchMethod2(lGp, new List<String>(), origin, destination, new List<String>());
        }

        public List<String> dijkstraSearch(ListGraph lGp, MatrixGraph mGp, String origin, String destination)
        {
            if (origin == destination)
            {
                Console.WriteLine("Choose different vertices to navigate");
                return new List<String>();
            }
            this.origin = origin;
            if (lGp == null)
            {
                if (mGp.getVertexIndex(origin) == -1)
                {
                    Console.WriteLine("origin vertex does not exists");
                    return new List<String>();
                }
                return this.dijkstraSearchMethod1(mGp, origin, destination);
            }
            if (lGp.getVertexIndex(origin) == -1)
            {
                Console.WriteLine("origin vertex does not exists");
                return new List<String>();
            }
            return this.dijkstraSearchMethod2(lGp, origin, destination);
        }

        public List<String> deepSearchMethod1(MatrixGraph mGp, List<String> ret, String origin, String destination, List<String> visited) {
            visited.Add(origin);
           
            if (origin == destination) {
                return ret;
            }

            List<String> labels = mGp.getNeighbors(mGp.getVertexIndex(origin));

            foreach (String label in labels) {
                if (visited.Contains(label)) {
                    continue;
                }
                if (this.deepSearchMethod1(mGp, ret, label, destination, visited) != null) {
                    ret.Insert(0, label);
                    if (this.origin == origin)
                    {
                        ret.Insert(0, origin);
                    }
                    return ret;
                }
            }
            if (this.origin == origin) {
                Console.WriteLine("could not get to the destination");
                return visited;  
            }
            return null;
        }

        public List<String> deepSearchMethod2(ListGraph lGp, List<String> ret, String origin, String destination, List<String> visited) {
            visited.Add(origin);
            if (origin == destination)
            {
                return ret;
            }

            List<String> labels = lGp.getNeighbors(lGp.getVertexIndex(origin));

            foreach (String label in labels)
            {
                if (visited.Contains(label))
                {
                    continue;
                }
                if (this.deepSearchMethod2(lGp, ret, label, destination, visited) != null)
                {
                    ret.Insert(0, label);
                    if (this.origin == origin)
                    {
                        ret.Insert(0, origin);
                    }
                    return ret;
                }
            }
            if (this.origin == origin)
            {
                Console.WriteLine("could not get to the destination");
                return visited;
            }
            return null;
        }

        public List<String> broadSearchMethod1(MatrixGraph mGp, List<String> ret, String origin, String destination, List<String> visited)
        {
            if (this.origin == origin)
            {
                visited.Add(origin);
            }
            List<String> labels = mGp.getNeighbors(mGp.getVertexIndex(origin));
            List<String> visit = new List<String>(); 
            foreach (String label in labels)
            {
                if (visited.Contains(label) == false)
                {
                    visit.Add(label);
                }
                if (label == destination) {
                    ret.Insert(0, label); 
                    return ret;
                }
            }
            foreach (String label in visit)
            {
                if (visited.Contains(label) == true)
                {
                    continue;
                }
                visited.Add(label);
                if (this.broadSearchMethod1(mGp, ret, label, destination, visited) != null)
                {
                    ret.Insert(0, label);
                    if (this.origin == origin)
                    {
                        ret.Insert(0, origin);
                    }
                    return ret;
                }
            }
            if (this.origin == origin)
            {
                Console.WriteLine("could not get to the destination");
                return visited;
            }
            return null;
        }

        public List<String> broadSearchMethod2(ListGraph lGp, List<String> ret, String origin, String destination, List<String> visited) {
            if (this.origin == origin)
            {
                visited.Add(origin);
            }
            List<String> labels = lGp.getNeighbors(lGp.getVertexIndex(origin));
            List<String> visit = new List<String>();
            foreach (String label in labels)
            {
                if (visited.Contains(label) == false)
                {
                    visit.Add(label);
                }
                if (label == destination)
                {
                    ret.Insert(0, label);
                    return ret;
                }
            }
            foreach (String label in visit)
            {
                if (visited.Contains(label) == true)
                {
                    continue;
                }
                visited.Add(label);
                if (this.broadSearchMethod2(lGp, ret, label, destination, visited) != null)
                {
                    ret.Insert(0, label);
                    if (this.origin == origin)
                    {
                        ret.Insert(0, origin);
                    }
                    return ret;
                }
            }
            if (this.origin == origin)
            {
                Console.WriteLine("could not get to the destination");
                return visited;
            }
            return null;
        }

        public List<String> dijkstraSearchMethod1(MatrixGraph mGp, String origin, String destination) {
            List<Node> nodes = new List<Node>();
            Node first = new Node(origin, "-", false, 0);
            
            foreach (List<Vertex> lv in mGp.graph) {
                if (lv[0].labelRow != origin) { 
                    nodes.Add(new Node(lv[0].labelRow, "-", false, 0));
                }
            }
            List<String> lbs = mGp.getNeighbors(mGp.getVertexIndex(first.label));
            if (lbs.Count() == 0) {
                Console.WriteLine("could not get to the destination");
                return new List<String>();
            }
            foreach (String label in lbs)
            {
                if (label != origin)
                {
                    int weight = mGp.linkWeight(mGp.getVertexIndex(first.label), mGp.getVertexIndex(label));

                    if (nodes.Find(x => x.label == label).distance == 0 || weight + first.distance < nodes.Find(x => x.label == label).distance)
                    {
                        nodes.Find(x => x.label == label).last = first.label;
                        nodes.Find(x => x.label == label).distance = weight + first.distance;
                    }
                }
            }
            first.closed = true;
            nodes.Insert(0, first);
            do
            {
                foreach (Node node in nodes)
                {
                    if (node.closed == true) continue;
                    List<String> labels = mGp.getNeighbors(mGp.getVertexIndex(node.label));
                    if (node.distance == 0)
                    {
                        Boolean end = true;
                        foreach(Node nd in nodes) {
                            if (nd.label != node.label && mGp.linkExists(mGp.getVertexIndex(nd.label), mGp.getVertexIndex(node.label)) == true) {
                                end = false;
                            }
                        }
                        if (end == true) { 
                            node.closed = true;
                        }
                        continue;
                    }
                    foreach (String label in labels)
                    {
                        if (label != origin)
                        {
                            int weight = mGp.linkWeight(mGp.getVertexIndex(node.label), mGp.getVertexIndex(label));

                            if (nodes.Find(x => x.label == label).distance == 0 || weight + node.distance < nodes.Find(x => x.label == label).distance)
                            {
                                nodes.Find(x => x.label == label).last = node.label;
                                nodes.Find(x => x.label == label).distance = weight + node.distance;
                            }
                        }
                    }
                    node.closed = true;
                }
            } while (nodes.FindAll(x => x.closed == false).Count() != 0);
            foreach (Node node in nodes)
            {
                Console.Write(node.last + " " + node.label + " " + node.closed.ToString() + " " + node.distance.ToString() + "\n");
            }
            Console.WriteLine();
            List<String> strs = new List<String>();
            Boolean foundIt = false;
            Node searchNode = nodes.Find(x => x.label == destination);
            while (foundIt == false) {
                strs.Insert(0, searchNode.label);
                if (searchNode.label == origin) {
                    foundIt = true;
                    break;
                }
                if (searchNode.last == "-")
                {
                    break;
                }
                searchNode = nodes.Find(x => x.label == searchNode.last);
            }

            if (foundIt == true) return strs;
            Console.WriteLine("could not get to the destination");
            return new List<String>();
        }

        public List<String> dijkstraSearchMethod2(ListGraph lGp, String origin, String destination) {
            List<Node> nodes = new List<Node>();
            Node first = new Node(origin, "-", false, 0);

            foreach (Link lv in lGp.graph)
            {
                if (lv.label != origin)
                {
                    nodes.Add(new Node(lv.label, "-", false, 0));
                }
            }
            List<String> lbs = lGp.getNeighbors(lGp.getVertexIndex(first.label));
            if (lbs.Count() == 0)
            {
                Console.WriteLine("could not get to the destination");
                return new List<String>();
            }
            foreach (String label in lbs)
            {
                if (label != origin)
                {
                    int weight = lGp.linkWeight(lGp.getVertexIndex(first.label), lGp.getVertexIndex(label));

                    if (nodes.Find(x => x.label == label).distance == 0 || weight + first.distance < nodes.Find(x => x.label == label).distance)
                    {
                        nodes.Find(x => x.label == label).last = first.label;
                        nodes.Find(x => x.label == label).distance = weight + first.distance;
                    }
                }
            }
            first.closed = true;
            nodes.Insert(0, first);
            do
            {
                foreach (Node node in nodes)
                {
                    if (node.closed == true) continue;
                    List<String> labels = lGp.getNeighbors(lGp.getVertexIndex(node.label));
                    if (node.distance == 0)
                    {
                        Boolean end = true;
                        foreach (Node nd in nodes)
                        {
                            if (nd.label != node.label && lGp.linkExists(lGp.getVertexIndex(nd.label), lGp.getVertexIndex(node.label)) == true)
                            {
                                end = false;
                            }
                        }
                        if (end == true)
                        {
                            node.closed = true;
                        }
                        continue;
                    }
                    foreach (String label in labels)
                    {
                        if (label != origin)
                        {
                            int weight = lGp.linkWeight(lGp.getVertexIndex(node.label), lGp.getVertexIndex(label));

                            if (nodes.Find(x => x.label == label).distance == 0 || weight + node.distance < nodes.Find(x => x.label == label).distance)
                            {
                                nodes.Find(x => x.label == label).last = node.label;
                                nodes.Find(x => x.label == label).distance = weight + node.distance;
                            }
                        }
                    }
                    node.closed = true;
                }
            } while (nodes.FindAll(x => x.closed == false).Count() != 0);
            foreach (Node node in nodes)
            {
                Console.Write(node.last + " " + node.label + " " + node.closed.ToString() + " " + node.distance.ToString() + "\n");
            }
            Console.WriteLine();
            List<String> strs = new List<String>();
            Boolean foundIt = false;
            Node searchNode = nodes.Find(x => x.label == destination);
            while (foundIt == false)
            {
                strs.Insert(0, searchNode.label);
                if (searchNode.label == origin)
                {
                    foundIt = true;
                    break;
                }
                if (searchNode.last == "-")
                {
                    break;
                }
                searchNode = nodes.Find(x => x.label == searchNode.last);
            }

            if (foundIt == true) return strs;
            Console.WriteLine("could not get to the destination");
            return new List<String>();
        }
    }
}
