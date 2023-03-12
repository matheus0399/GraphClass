using System;
public class Graph
{
    public Boolean directed;
    public Boolean pondered;
    public List<Link> linkedGraph = new List<Link>();
    public List<List<Vertice>> arrayGraph = new List<List<Vertice>>();
    
    public Graph(Boolean directed, Boolean pondered) {
        this.directed = directed;
        this.pondered = pondered;
        this.linkedGraph = new List<Link>();
        this.arrayGraph = new List<List<Vertice>>();
    }

    public Boolean inserirVertice(String label) {
        if(this.hasVertice(label) == false){
            Link l = new Link(label);
            this.linkedGraph.Add(l);
            Vertice v = new Vertice(label,label);
            this.addNewVertice(v);
            return true;
        }else{
            Console.WriteLine("Label already exists");
            return false;
        }
    }

    public Boolean removerVertice(string label){
        if(this.hasVertice(label) == true){
            this.removeFromArray(label);
            this.removeFromLinked(label);
            return true;
        }else{
            Console.WriteLine("Vertice does not exist");
            return false;
        }
    }

    public String labelVertice(int indice, int type = 0) {
        if(type == 0) {
            for(int i = 0; i < this.linkedGraph.Count; i++) {
                if(i == indice) {
                    return this.linkedGraph[i].label;
                }
            }
        }
        if(this.arrayGraph.Count - 1 >= indice && indice > 0){
            return this.arrayGraph[indice][0].labelRow;
        }
        return "Not Found";
    }

    public Boolean hasVertice(String label) {
        foreach(Link l in this.linkedGraph) {
            if (l.label == label) {
                return true;
            }
        }
        return false;
    }

    public void addNewVertice(Vertice v) {
        List<Vertice> vList = new List<Vertice>();
        vList.Add(v);
        foreach(List<Vertice> vl in this.arrayGraph) {
            String label = vl[0].labelRow;
            vl.Add(new Vertice(v.labelRow,label));
            vList.Add(new Vertice(label,v.labelRow));
        }
        this.arrayGraph.Add(vList);
    }

    public void removeFromLinked(String label) {
        foreach(Link l in this.linkedGraph) {
            if(l.label == label) {
                this.linkedGraph.Remove(l);
                break;
            }
        }
        foreach(Link l in this.linkedGraph) {
            foreach(ListItem li in l.links) {
                if(li.label == label) {
                    l.links.Remove(li);
                    break;
                }
            }
        }
    }

    public void removeFromArray(String label) {
        var index = -1;
        for(int i = 0; i < this.arrayGraph.Count; i++) {
            if(this.arrayGraph[i][0].labelRow == label) {
                index = i;
                break;
            }
        }
        this.arrayGraph.Remove(this.arrayGraph[index]);
        for(int i = 0; i < this.arrayGraph.Count; i++) {
            this.arrayGraph[i].Remove(this.arrayGraph[i][index]);
        }
    }

    public Boolean inserirAresta(String origem, String destino, int peso = 1) {
        if(origem == destino) {
            Console.WriteLine("You cannot create a link to the same vertice");
            return false;
        }
        if(this.pondered == false) {
            peso = 1;
        }
        else{
            if(peso <= 0) {
                Console.WriteLine("For pondered graphs we need the weight of the vertice");
                return false;
            }
        }
        if(this.hasVertice(origem) == false || this.hasVertice(destino) == false) {
            Console.WriteLine("One of the vertices on your input does not exist");
            return false;
        }
        int index0 = -1;
        int index1 = -1;

        for(int i = 0; i < this.arrayGraph.Count; i++) {
            if(index0 != -1 && index1 != -1) break;
            if(this.arrayGraph[i][0].labelRow == origem) {
                index0 = i;
                continue;
            }
            if(this.arrayGraph[i][0].labelRow == destino) {
                index1 = i;
            }
        }

        if(this.existeAresta(index0, index1) == false) {
            this.addLinkOnLinked(origem, destino, peso);
            this.addLinkOnArray(origem, destino, peso);
            return true;
        }else {
            Console.WriteLine("Link Already exists");
            return false;
        }
    }

    public Boolean removerAresta(int origem, int destino) {
        if(origem == destino){
            Console.WriteLine("Vertices do not have links to themselves");
            return false;
        }
        if(this.existeAresta(origem, destino) == true) {
            this.removeLinkOnLinked(origem, destino);
            this.removeLinkOnArray(origem, destino);
            return true;
        }else {
            Console.WriteLine("Link does not exist");
            return false;
        }
    }

    public Boolean existeAresta(int origem, int destino, int type = 0) {
        if(origem == destino) return false;
        String label = this.labelVertice(destino);
        if(type == 0) {
            for(int i = 0; i < this.linkedGraph.Count; i++) {
                if(i == origem) {
                    foreach(ListItem li in this.linkedGraph[i].links) {
                        if(li.label == label && li.weight != 0) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }else{
            if(this.arrayGraph[origem][destino].weight > 0) {
                return true;
            }
            return false;
        }
    }

    public void addLinkOnLinked(String origem, String destino, int peso) {
        foreach(Link l in this.linkedGraph) {
            if(l.label == origem) {
                l.addLink(destino, peso);
            }if(this.directed == false && l.label == destino) {
                l.addLink(origem, peso);
            }
        }
    }

    public void addLinkOnArray(String origem, String destino, int peso) {
        int indexColumn = -1;
        int indexRow = -1;
        for(int i = 0; i < this.arrayGraph.Count; i++) {
            if(indexColumn != -1 && indexRow != -1) {
                break;
            }
            if(this.arrayGraph[i][0].labelRow == origem) {
                indexRow = i;
            }
            if(this.arrayGraph[i][0].labelRow == destino) {
                indexColumn = i;
            }
        }
        this.arrayGraph[indexRow][indexColumn].weight = peso;
        if(this.directed == false) {
            this.arrayGraph[indexColumn][indexRow].weight = peso;
        }
    }

    public void removeLinkOnLinked(int origem, int destino) {
        String label = this.labelVertice(destino);
        String label2 = this.labelVertice(origem);
        for(int i = 0; i < this.linkedGraph.Count; i++) {
            if(i == origem) {
                foreach(ListItem li in this.linkedGraph[i].links) {
                    if (li.label == label) {
                        this.linkedGraph[i].links.Remove(li);
                        break;
                    }
                }
            }
        }
        if (this.directed == false){
            for(int i = 0; i < this.linkedGraph.Count; i++) {
                if (this.linkedGraph[i].label == label) {
                    foreach(ListItem li in this.linkedGraph[i].links) {
                        if (li.label == label2) {
                            this.linkedGraph[i].links.Remove(li);
                            break;
                        }
                    }
                }
            }
        }
    }
    
    public void removeLinkOnArray(int origem, int destino) {
        this.arrayGraph[origem][destino].weight = 0;
        if(this.directed == false) {
            this.arrayGraph[destino][origem].weight = 0;
        }
    }

    public int pesoAresta(int origem, int destino, int type = 0) {
        if(origem == destino) return 0;
        if(type == 0) {
            String label = "";
            for(int i = 0; i <  this.linkedGraph.Count; i++) {
                if(i == destino) {
                    label = this.linkedGraph[i].label;
                    break;
                }
            }
            for(int i = 0; i <  this.linkedGraph.Count; i++) {
                if(i == origem) {
                    for(int j = 0; j <  this.linkedGraph[i].links.Count; j++) {
                        if(this.linkedGraph[i].links[j].label == label && this.linkedGraph[i].links[j].weight > 0) {
                            return this.linkedGraph[i].links[j].weight;
                        }
                    }
                }
            }
            return -1;
        }else{
            if(this.arrayGraph.Count - 1 >= origem && this.arrayGraph[origem].Count - 1 >= destino) {
                if(this.arrayGraph[origem][destino].weight > 0) {
                    return this.arrayGraph[origem][destino].weight;
                }
                return -1;
            }
            return -1;
        }
    }

    public List<String> retornarVizinhos(int vertice, int type = 0) {
        List<String> retorno = new List<String>();
        if(type == 0) {
            if(this.linkedGraph.Count - 1 >= vertice) {
                for(int i = 0; i < this.linkedGraph.Count; i++) {
                    if(i == vertice) {
                        foreach(ListItem li in this.linkedGraph[i].links) {
                            retorno.Add(li.label);
                        }
                        break;
                    }
                }
            }
            else {
                Console.WriteLine("Vertice Does not exist");
                return new List<string>();
            }
        }else{
            if(this.arrayGraph.Count - 1 >= vertice) {
                foreach(Vertice v in this.arrayGraph[vertice]) {
                    if(v.weight > 0) {
                        retorno.Add(v.labelColumn);
                    }
                }
            }
            else {
                Console.WriteLine("Vertice Does not exist");
                return new List<string>();
            }
        }
        return retorno;
    }

    public void imprimeGrafo() {
        Console.WriteLine("\nGrafo de Lista");
        foreach(Link l in this.linkedGraph) {
            Console.Write(l.label + " --> ");
            foreach(ListItem li in l.links) {
                Console.Write(li.label + ", ");
            }
            Console.Write("\n");
        }
        Console.WriteLine("\nGrafo Array\n");
        for(int i = 0; i < this.arrayGraph.Count; i++) {
            Console.WriteLine(i + "= " + this.arrayGraph[i][0].labelRow);
        }
        Console.WriteLine("");
        for(int i = 0; i < this.arrayGraph.Count; i++) {
            Console.Write(i + ": ");
            for(int j = 0; j < this.arrayGraph.Count; j++) {
                Console.Write(this.arrayGraph[i][j].weight + ", ");
            }
            Console.Write("\n");
        }
    }
}