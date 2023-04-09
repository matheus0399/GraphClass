using busca.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace busca.entities
{
    internal class ListGraph
    {
        public Boolean directed;
        public Boolean pondered;
        public List<Link> graph = new List<Link>();
        public ListGraph(Boolean directed, Boolean pondered)
        {
            this.directed = directed;
            this.pondered = pondered;
            this.graph = new List<Link>();
        }

        public Boolean addVertex(String label)
        {
            if (this.hasVertex(label) == false)
            {
                Link l = new Link(label);
                this.graph.Add(l);
                return true;
            }
            else
            {
                Console.WriteLine("Label already exists");
                return false;
            }
        }

        public String labelVertex(int index)
        {
            for (int i = 0; i < this.graph.Count; i++)
            {
                if (i == index)
                {
                    return this.graph[i].label;
                }
            }
            return "Not Found";
        }

        public Boolean hasVertex(String label)
        {
            foreach (Link l in this.graph)
            {
                if (l.label == label)
                {
                    return true;
                }
            }
            return false;
        }

        public Boolean removeVertex(String label)
        {
            if (this.hasVertex(label) == true)
            {
                foreach (Link l in this.graph)
                {
                    if (l.label == label)
                    {
                        this.graph.Remove(l);
                        break;
                    }
                }
                foreach (Link l in this.graph)
                {
                    foreach (ListItem li in l.links)
                    {
                        if (li.label == label)
                        {
                            l.links.Remove(li);
                            break;
                        }
                    }
                }
                return true;
            }
            else
            {
                Console.WriteLine("Vertex does not exist");
                return false;
            }
        }

        public Boolean addLink(String origin, String destination, int weight = 1)
        {
            if (origin == destination)
            {
                Console.WriteLine("You cannot create a link to the same vertex");
                return false;
            }
            if (this.pondered == false)
            {
                weight = 1;
            }
            else
            {
                if (weight <= 0)
                {
                    Console.WriteLine("For pondered graphs we need the weight of the vertex");
                    return false;
                }
            }
            if (this.hasVertex(origin) == false || this.hasVertex(destination) == false)
            {
                Console.WriteLine("One of the vertices on your input does not exist");
                return false;
            }

            if (this.linkExists(this.getVertexIndex(origin), this.getVertexIndex(destination)) == false)
            {
                this.addLinkProcess(origin, destination, weight);
                return true;
            }
            else
            {
                Console.WriteLine("Link Already exists");
                return false;
            }
        }

        public int getVertexIndex(String label) {
            for (int i = 0; i < this.graph.Count; i++) {
                if (this.graph[i].label == label) { 
                    return i;
                }
            }
            return -1;
        }

        public void addLinkProcess(String origin, String destination, int weight)
        {
            foreach (Link l in this.graph)
            {
                if (l.label == origin)
                {
                    l.addLink(destination, weight);
                }
                if (this.directed == false && l.label == destination)
                {
                    l.addLink(origin, weight);
                }
            }
        }

        public Boolean removeLink(int origin, int destination)
        {
            if (origin == destination)
            {
                Console.WriteLine("Vertices do not have links to themselves");
                return false;
            }
            if (this.linkExists(origin, destination) == true)
            {
                String label = this.labelVertex(destination);
                String label2 = this.labelVertex(origin);
                for (int i = 0; i < this.graph.Count; i++)
                {
                    if (i == origin)
                    {
                        foreach (ListItem li in this.graph[i].links)
                        {
                            if (li.label == label)
                            {
                                this.graph[i].links.Remove(li);
                                break;
                            }
                        }
                    }
                }
                if (this.directed == false)
                {
                    for (int i = 0; i < this.graph.Count; i++)
                    {
                        if (this.graph[i].label == label)
                        {
                            foreach (ListItem li in this.graph[i].links)
                            {
                                if (li.label == label2)
                                {
                                    this.graph[i].links.Remove(li);
                                    break;
                                }
                            }
                        }
                    }
                }
                return true;
            }
            else
            {
                Console.WriteLine("Link does not exist");
                return false;
            }
        }

        public Boolean linkExists(int origin, int destination)
        {
            if (origin == destination) return false;
            String label = this.labelVertex(destination);
            for (int i = 0; i < this.graph.Count; i++)
            {
                if (i == origin)
                {
                    foreach (ListItem li in this.graph[i].links)
                    {
                        if (li.label == label && li.weight != 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }


        public int linkWeight(int origin, int destination)
        {
            if (origin == destination) return 0;
            
            String label = "";
            for (int i = 0; i < this.graph.Count; i++)
            {
                if (i == destination)
                {
                    label = this.graph[i].label;
                    break;
                }
            }
            for (int i = 0; i < this.graph.Count; i++)
            {
                if (i == origin)
                {
                    for (int j = 0; j < this.graph[i].links.Count; j++)
                    {
                        if (this.graph[i].links[j].label == label && this.graph[i].links[j].weight > 0)
                        {
                            return this.graph[i].links[j].weight;
                        }
                    }
                }
            }
            return 0;
        }

        public List<String> getNeighbors(int vertex)
        {
            List<String> _ret = new List<String>();
            
            if (this.graph.Count - 1 >= vertex)
            {
                for (int i = 0; i < this.graph.Count; i++)
                {
                    if (i == vertex)
                    {
                        foreach (ListItem li in this.graph[i].links)
                        {
                            _ret.Add(li.label);
                        }
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Vertex Does not exist");
                return new List<string>();
            }

            return _ret;
        }

        public void showGraph()
        {
            Console.WriteLine("\nList graph");
            foreach (Link l in this.graph)
            {
                Console.Write(l.label + " --> ");
                foreach (ListItem li in l.links)
                {
                    Console.Write(li.label + ", ");
                }
                Console.Write("\n");
            }
        }

        public ListGraph readFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            string[] line_one = lines[0].Split(' ');
            Boolean directed = false;
            Boolean pondered = false;
            if (line_one[2] == "1")
            {
                directed = true;
            }
            if (line_one[3] == "1")
            {
                pondered = true;
            }
            ListGraph ret = new ListGraph(directed, pondered);
            for (int i = 1; i < lines.Count(); i++)
            {
                string[] info = lines[i].Split(' ');
                if (ret.hasVertex(info[0]) == false)
                {
                    ret.addVertex(info[0]);
                }
                if (ret.hasVertex(info[1]) == false)
                {
                    ret.addVertex(info[1]);
                }
                ret.addLink(info[0], info[1], int.Parse(info[2]));
            }
            return ret;
        }
    }
}