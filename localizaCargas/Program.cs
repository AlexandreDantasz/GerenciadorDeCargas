using Menus;
using CargasGollog;
public class Program
{
    static void Main(string[] args)
    { 
        Telas monitor = new Telas();
        Carga carga = new Carga();
        CargaDao dao = new CargaDao();
        int loop = monitor.telaInicial();
        string feedback = "";
        string lixo = "";
        while (loop != 0)
        {
            switch (loop)
            {
                case 0:
                    loop = 0;
                    break;
                case 1:
                    if (monitor.telaIncluir(carga) == 1)
                    {
                        feedback = dao.incluir(carga) == true ? "Carga incluída" : "Não foi possível incluir a carga";
                        Console.WriteLine(feedback);
                        Console.WriteLine("Pressione enter para sair: ");
                        lixo = Console.ReadLine();
                    }
                    break;
                case 2:
                    if (monitor.telaRemover(carga) == 1)
                    {
                        feedback = dao.excluir(carga) == true ? "Carga removida" : "Não foi possível remover a carga";
                        Console.WriteLine(feedback);
                        Console.WriteLine("Pressione enter para sair: ");
                        lixo = Console.ReadLine();
                    }
                    break;
                case 3:
                    int buscas = monitor.telaBuscas();
                    if (buscas == 1)
                    {
                        if (monitor.telaBuscarCarga(carga) == 1)
                        {
                            feedback = dao.buscarCod(carga) == true ? "Carga encontrada" : "Carga não encontrada";
                            Console.WriteLine(feedback);
                            Console.WriteLine("Pressione enter para sair: ");
                            lixo = Console.ReadLine();
                        }
                    }
                    else if (buscas == 2)
                    {
                        if (monitor.telaBuscarNome(carga) == 1)
                        {
                            feedback = dao.buscarNome(carga) == true ? "Busca efetuada" : "Busca não efetuada";
                            Console.WriteLine(feedback);
                            Console.WriteLine("Pressione enter para sair: ");
                            lixo = Console.ReadLine();
                        }
                    }
                    break;
                case 4:
                    Console.Clear();
                    feedback = dao.listarCargas() == true ? "As cargas foram listadas" : "Não foi possível listar as cargas";
                    Console.WriteLine(feedback);
                    Console.WriteLine("Pressione enter para sair: ");
                    lixo = Console.ReadLine();
                    break;
            }
            loop = monitor.telaInicial();
        }
    }
};