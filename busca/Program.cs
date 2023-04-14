using busca.entities;

Boolean end = false;
ListGraph lGp = new ListGraph(false, false);
MatrixGraph mGp = new MatrixGraph(false, false);
Search search = new Search();
Console.Clear();
Console.Write("Você deseja criar um grafo de lista(sim/não)? ");
Boolean listGraph = Console.ReadLine().Trim().ToLower() == "sim";




Boolean reboot = false;
do
{
    try
    {
        if (reboot == true)
        {
            lGp = new ListGraph(false, false);
            mGp = new MatrixGraph(false, false);
            Console.Write("Você deseja criar um grafo de lista(sim/não)? ");
            listGraph = Console.ReadLine().Trim().ToLower() == "sim";
            reboot = false;
        }
        if (listGraph == true)
        {
            lGp = lGp.readFile("C:\\Users\\7251777\\Desktop\\espacoaereo.txt");
            // lGp.showGraph();
        }
        else
        {
            mGp = mGp.readFile("C:\\Users\\7251777\\Desktop\\espacoaereo.txt");
            // mGp.showGraph();
        }
        Console.Write("\nQual o tipo de ação você deseja executar:"
            + "\n 1 - Busca em largura"
            + "\n 2 - Busca em profundidade"
            + "\n 3 - Dijkstra"
            + "\n 4 - Reiniciar "
        );

        String input0 = Console.ReadLine().Trim().ToLower();
        String input1 = "";
        List<String> strs = new List<String>();

        switch (input0)
        {
            case "1":
                Console.Write("Insira a label do vertice de origem:");
                input0 = Console.ReadLine().Trim();
                Console.Write("Insira a label do vertice de destino:");
                input1 = Console.ReadLine().Trim();
                if (listGraph == true)
                {
                    strs = search.broadSearch(lGp, null, input0, input1);
                }
                else
                {
                    strs = search.broadSearch(null, mGp, input0, input1);
                }
                for (int i = 0; i < strs.Count(); i++)
                {
                    Console.Write(strs[i] + ", ");
                }
                break;
            case "2":
                Console.Write("Insira a label do vertice de origem:");
                input0 = Console.ReadLine().Trim();
                Console.Write("Insira a label do vertice de destino:");
                input1 = Console.ReadLine().Trim();
                
                if (listGraph == true)
                {
                    strs = search.deepSearch(lGp, null, input0, input1);
                }
                else
                {
                    strs = search.deepSearch(null, mGp, input0, input1);
                }
                for (int i = 0; i < strs.Count(); i++)
                {
                    Console.Write(strs[i] + ", ");
                }
                break;
            case "3":
                Console.Write("Insira a label do vertice de origem:");
                input0 = Console.ReadLine().Trim();
                Console.Write("Insira a label do vertice de destino:");
                input1 = Console.ReadLine().Trim();
                if (listGraph == true)
                {
                    strs = search.dijkstraSearch(lGp, null, input0, input1);
                }
                else
                {
                    strs = search.dijkstraSearch(null, mGp, input0, input1);
                }
                for (int i = 0; i < strs.Count(); i++)
                {
                    Console.Write(strs[i] + ", ");
                }
                break;
            case "4":
                Console.Clear();
                reboot = true;
                break;
        }
        Console.WriteLine("");
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        Console.WriteLine("\n INPUT ERROR \n");
    }
} while (end == false);
