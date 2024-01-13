using CargasGollog;

namespace Menus
{
    public class Telas
    {
        private bool verificaData(string data)
        {
            if (data.Length < 10 || data.Length > 10) return false;
            if (data[4] != '-' || data[7] != '-') return false;
            for (int i = 0; i < 10; i++)
            {
                if (i != 4 && i != 7 && (data[i] < 48 || data[i] > 57)) return false;
            }
            return true;
        }
        public int telaInicial()
        {
            Console.Clear();
            int resposta;
            Console.WriteLine("-------------------- SISTEMA DE CARGAS --------------------\n");
            Console.WriteLine("Digite um dos números a seguir para realizar uma ação:\n");
            Console.WriteLine("0 - Sair\n");
            Console.WriteLine("1 - Incluir uma carga\n");
            Console.WriteLine("2 - Remover uma carga\n");
            Console.WriteLine("3 - Buscar uma carga\n");
            Console.WriteLine("4 - Listar cargas\n");
            Console.Write("Resposta: ");
            string linha = Console.ReadLine();
            while (linha.Length > 1 || linha.Length == 0 || linha[0] < 48 || linha[0] > 52)
            {
                Console.Clear();
                Console.WriteLine("Resposta inválida, digite novamente\n\n");
                Console.WriteLine("-------------------- SISTEMA DE CARGAS --------------------\n");
                Console.WriteLine("Digite um dos números a seguir para realizar uma ação:\n");
                Console.WriteLine("0 - sair\n");
                Console.WriteLine("1 - Incluir uma carga\n");
                Console.WriteLine("2 - Remover uma carga\n");
                Console.WriteLine("3 - Buscar uma carga\n");
                Console.WriteLine("4 - Listar cargas\n");
                Console.Write("Resposta: ");
                linha = Console.ReadLine();
            }
            resposta = int.Parse(linha);
            return resposta;
        }

        public int telaIncluir(Carga CargaP)
        {
            Console.Clear();
            Console.WriteLine("-------------------- INCLUIR CARGA --------------------\n");
            Console.WriteLine("Caso deseje retornar, basta digitar 0 em qualquer uma das respostas\n\n");
            Console.Write("Digite o código de rastreio: ");
            string linha = Console.ReadLine();
            if (linha.Length != 0 && linha[0] == '0') return 0;
            CargaP.setCodRatreio("'" + linha + "'");
            Console.Write("Digite o nome do cliente completo: ");
            linha = Console.ReadLine();
            if (linha.Length != 0 && linha[0] == '0') return 0;
            CargaP.setNomeCliente("'" + linha + "'");
            Console.Write("Digite a rua: ");
            linha = Console.ReadLine();
            if (linha.Length != 0 && linha[0] == '0') return 0;
            CargaP.setRua("'" + linha + "'");
            Console.Write("Digite o bairro: ");
            linha = Console.ReadLine();
            if (linha.Length != 0 && linha[0] == '0') return 0;
            CargaP.setBairro("'" + linha + "'");
            do
            {
                Console.WriteLine("A data deve ser escrita com base nesse exemplo: AAAA-MM-DD");
                Console.Write("Digite a data de desembarque: ");
                linha = Console.ReadLine();
                if (linha.Length != 0 && linha[0] == '0') return 0;
            }
            while (!verificaData(linha));
            CargaP.setData("'" + linha + "'");
            Console.Write("Digite o volume/peso: ");
            linha = Console.ReadLine();
            if (linha.Length != 0 && linha[0] == '0') return 0;
            CargaP.setVolPeso("'" + linha + "'");
            Console.Write("Digite a descrição da carga: ");
            linha = Console.ReadLine();
            if (linha.Length != 0 && linha[0] == '0') return 0;
            CargaP.setDescricao("'" + linha + "'");
            return 1;
        }

        public int telaRemover(Carga cargaP)
        {
            Console.Clear();
            Console.WriteLine("-------------------- REMOVER CARGA --------------------\n");
            Console.WriteLine("Caso deseje retornar, basta digitar 0 em qualquer uma das respostas\n\n");
            Console.Write("Digite o código de rastreio: ");
            string linha = Console.ReadLine();
            if (linha.Length != 0 && linha[0] == '0') return 0;
            cargaP.setCodRatreio("'" + linha + "'");
            return 1;
        }    

        public int telaBuscas()
        {
            Console.Clear();
            Console.WriteLine("-------------------- BUSCAR CARGA --------------------\n");
            Console.WriteLine("Digite um dos números a seguir para realizar uma ação:\n");
            Console.WriteLine("0 - Sair\n");
            Console.WriteLine("1 - Buscar por código de rastreio\n");
            Console.WriteLine("2 - Buscar cargas por nome do cliente\n");
            Console.Write("Resposta: ");
            int resposta;
            string linha = Console.ReadLine();
            while (linha.Length > 1 || linha.Length == 0 || linha[0] < 48 || linha[0] > 50)
            {
                Console.Clear();
                Console.WriteLine("Resposta inválida, digite novamente\n\n");
                Console.WriteLine("-------------------- BUSCAR CARGA --------------------\n");
                Console.WriteLine("Digite um dos números a seguir para realizar uma ação:\n");
                Console.WriteLine("0 - Sair\n");
                Console.WriteLine("1 - Buscar por código de rastreio\n");
                Console.WriteLine("2 - Buscar cargas por nome do cliente\n");
                Console.Write("Resposta: ");
                linha = Console.ReadLine();
            }
            resposta = int.Parse(linha);
            return resposta;
        }

        public int telaBuscarCarga(Carga cargaP)
        {
            Console.Clear();
            Console.WriteLine("-------------------- BUSCAR CARGA --------------------\n");
            Console.WriteLine("Caso deseje retornar, basta digitar 0 em qualquer uma das respostas\n\n");
            Console.Write("Digite o código de rastreio: ");
            string linha = Console.ReadLine();
            if (linha.Length != 0 && linha[0] == '0') return 0;
            cargaP.setCodRatreio("'" + linha + "'");
            return 1;
        }

        public int telaBuscarNome(Carga cargaP)
        {
            Console.Clear();
            Console.WriteLine("-------------------- BUSCAR CARGA --------------------\n");
            Console.WriteLine("Caso deseje retornar, basta digitar 0 em qualquer uma das respostas\n\n");
            Console.Write("Digite o nome do cliente: ");
            string linha = Console.ReadLine();
            if (linha.Length != 0 && linha[0] == '0') return 0;
            cargaP.setNomeCliente("'" + linha + "'");
            return 1;
        }
    };
}
