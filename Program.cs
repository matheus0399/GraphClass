using System.IO;
namespace app
{
    class Program
    {
        static void Main(String[] args) {
            Boolean end = false;  
            Graph gp;
            Console.Clear();
            Console.Write("O grafo que você quer ciar é direcional(sim/não)? ");
            Boolean directed = Console.ReadLine().Trim().ToLower() == "sim";
            Console.Write("O grafo que você quer ciar é poderado(sim/não)? ");
            Boolean pondered = Console.ReadLine().Trim().ToLower() == "sim";;
            gp = new Graph(directed, pondered);
            Boolean reboot = false;
            do{
                try {
                    if(reboot == true) {
                        Console.Write("O grafo que você quer ciar é direcional(sim/não)? ");
                        directed = Console.ReadLine().Trim().ToLower() == "sim";
                        Console.Write("O grafo que você quer ciar é poderado(sim/não)? ");
                        pondered = Console.ReadLine().Trim().ToLower() == "sim";;
                        gp = new Graph(directed, pondered);
                        reboot = false;
                    }
                    Console.Write("\nQual o tipo de ação você deseja executar(Obs: o input desejado está entra parenteses):"
                        +"\nInserir vertice     (iv)"
                        +"\nRemover vertice     (rv)"
                        +"\nRetornar Vizinhos   (vi)"
                        +"\nPeso Aresta         (pa)"
                        +"\nExiste Aresta       (ea)"
                        +"\nRemover Aresta      (ra)"
                        +"\nLabel Vertice       (lv)"
                        +"\nInserir Aresta      (ia)"
                        +"\nImprime Grafo       (ig)"
                        +"\nBegin from scratch  (bs)"
                        +"\nClear terminal      (ct)"
                        +"\nExist program       (exit): "
                    );

                    String input0 = Console.ReadLine().Trim().ToLower();
                    String input1 = "";
                    int type = 0;
                    int index0 = 0;
                    int index1 = 0;
                    if(input0.Length > 2 && input0 != "exit") {
                        Console.WriteLine("Comando não encontrado");
                        continue;
                    }
                    switch (input0) { 
                        case "iv":
                            Console.Write("Insira a label do vertice:");
                            input0 = Console.ReadLine().Trim();
                            if(input0.Length == 0) {
                                Console.WriteLine("É necessario ao menos 1 caractere para criar um vertice!");
                            }else{
                                Console.WriteLine(gp.inserirVertice(input0));
                            }
                            break;
                        case "rv":
                            Console.Write("Insira a label do vertice:");
                            input0 = Console.ReadLine().Trim();
                            if(input0.Length == 0) {
                                Console.WriteLine("É necessario ao menos 1 caractere para remover um vertice!");
                            }else{
                                Console.WriteLine(gp.removerVertice(input0));
                            }
                            break;
                        case "vi":
                            Console.Write("Insira o indice do vertice:");
                            index0 = int.Parse(Console.ReadLine().Trim());
                            Console.Write("Qual tipo de estrutura você deseja usar nessa operação(0 para Lista qualquer outro número para Array)?");
                            type = int.Parse(Console.ReadLine().Trim());
                            List<String> arr = gp.retornarVizinhos(index0, type);
                            Console.Write(" [ ");
                            for(int i = 0; i < arr.Count; i++) {
                                if(i + 1 != arr.Count) {
                                    Console.Write(arr[i] + ", ");
                                } else {
                                    Console.Write(arr[i]);
                                }
                            }
                            Console.Write(" ]\n");
                            break;
                        case "pa":
                            Console.Write("Insira o indice do vertice origem:");
                            index0 = int.Parse(Console.ReadLine().Trim());
                            Console.Write("Insira o indice do vertice destino:");
                            index1 = int.Parse(Console.ReadLine().Trim());
                            Console.Write("Qual tipo de estrutura você deseja usar nessa operação(0 para Lista qualquer outro número para Array)?");
                            type = int.Parse(Console.ReadLine().Trim());
                            Console.WriteLine(gp.pesoAresta(index0, index1,type));
                            break;
                        case "ea":
                            Console.Write("Insira o indice do vertice origem:");
                            index0 = int.Parse(Console.ReadLine().Trim());
                            Console.Write("Insira o indice do vertice destino:");
                            index1 = int.Parse(Console.ReadLine().Trim());
                            Console.Write("Qual tipo de estrutura você deseja usar nessa operação(0 para Lista qualquer outro número para Array)?");
                            type = int.Parse(Console.ReadLine().Trim());
                            Console.WriteLine(gp.existeAresta(index0, index1, type));
                            break;
                        case "ra":
                            Console.Write("Insira o indice do vertice origem:");
                            index0 = int.Parse(Console.ReadLine().Trim());
                            Console.Write("Insira o indice do vertice destino:");
                            index1 = int.Parse(Console.ReadLine().Trim());
                            Console.WriteLine(gp.removerAresta(index0, index1));
                            break;
                        case "lv":
                            Console.Write("Insira o indice do vertice:");
                            index0 = int.Parse(Console.ReadLine().Trim());
                            Console.Write("Qual tipo de estrutura você deseja usar nessa operação(0 para Lista qualquer outro número para Array)?");
                            type = int.Parse(Console.ReadLine().Trim());
                            Console.WriteLine(gp.labelVertice(index0, type));
                            break;
                        case "ia":
                            Console.Write("Insira a label do vertice origem:");
                            input0 = Console.ReadLine().Trim();
                            Console.Write("Insira a label do vertice destino:");
                            input1 = Console.ReadLine().Trim();
                            if(pondered == true) {
                                Console.Write("Insira o peso da aresta:");
                                type = int.Parse(Console.ReadLine().Trim());
                                Console.WriteLine(gp.inserirAresta(input0, input1, type));
                            }else{
                                Console.WriteLine(gp.inserirAresta(input0, input1));
                            }
                            break;
                        case "ig":
                            gp.imprimeGrafo();
                            break;
                        case "bs":
                            reboot = true;
                            if(end == false) {
                                Console.Clear();
                            }
                            break;
                        case "ct":
                            Console.Clear();
                            break;
                        case "exit":
                            end = true;
                            break;
                    }
                    Console.WriteLine("");
                }catch(Exception e){
                    Console.WriteLine(e.Message);
                    Console.WriteLine("\n INPUT ERROR \n");
                }
            }while(end == false);
        }
    }
}
