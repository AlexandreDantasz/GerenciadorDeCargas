using System.Data;
using System.Data.SQLite;
using System.Globalization;

namespace CargasGollog {

    /*  Essa classe vai ser usada para representar uma carga da Gollog.
        Deve conter código de rastreio, nome do cliente, rua, bairro, volume e peso da carga, 
        descrição e data do desembarque   
    */

    public class Carga {
        private string codigoRastreio;
        private string nomeCliente;
        private string rua;
        private string bairro;
        private string volPeso;
        private string descricao;
        private string data;

        public Carga() { // inicializa os atributos
            codigoRastreio = "";
            nomeCliente = "";
            rua = "";
            bairro = "";
            volPeso = "";
            descricao = "";
            data = "";
        }

        public void setCodRatreio(string newCodRastreio) {
            codigoRastreio = newCodRastreio;
        }
        public string getCodRastreio() {
            return codigoRastreio;
        }
        public void setNomeCliente(string newNomeCliente) {
            nomeCliente = newNomeCliente;
        }
        public string getNomeCliente() {
            return nomeCliente;
        }
        public void setRua(string newRua) {
            rua = newRua;
        }
        public string getRua() {
            return rua;
        }
        public void setBairro(string newBairro) {
            bairro = newBairro;
        }
        public string getBairro() {
            return bairro;
        }
        public void setVolPeso(string newVolPeso) {
            volPeso = newVolPeso;
        }
        public string getVolPeso() {
            return volPeso;
        }
        public void setDescricao(string newDescricao) {
            descricao = newDescricao;
        }
        public string getDescricao() {
            return descricao;
        }
        public void setData(string newData) {
            data = newData;
        }
        public string getData() {
            return data;
        }
    };

    /*
       Essa classe será usada para fazer acesso ao banco de dados que será usado para armazenar
       informações de cargas da Gollog.
    */

    public class CargaDao
    {
        private string caminhoDoBanco = @".\Gollog.db";

        private string tornarSeparador(string linha)
        {
            string tempString = "";
            for (int i = 0; i < linha.Length; i++)
            {
                if (linha[i] != ' ') tempString += '-';
                else
                {
                    if (i + 1 < linha.Length && linha[i + 1] == ' ') tempString += "-";
                    else tempString += " ";
                }
            }
            return tempString;
        }

        private void formatarSaida(SQLiteDataReader leitor, int[] vet, string resultado)
        {
            var schemaTable = leitor.GetSchemaTable();
            int i = 0, sizeRow;
            string nomeColunas = "";
            foreach (DataRow row in schemaTable.Rows)
            {
                nomeColunas += row.Field<string>("ColumnName");
                sizeRow = row.Field<string>("ColumnName").Length;
                if (vet[i] < sizeRow) vet[i] = sizeRow;
                int size = Math.Abs(vet[i] - row.Field<string>("ColumnName").Length) + 1;
                for (int k = 0; k < size; k++) nomeColunas += " ";
                i++;
            }
            Console.WriteLine(nomeColunas);
            nomeColunas = tornarSeparador(nomeColunas);
            Console.WriteLine(nomeColunas);
            int j = 0;
            //string[] splitColunas = nomeColunas.Split(' ');
            string[] splitResultado = resultado.Split('|');
            string saida = "";
            for (i = 0; i < splitResultado.Length; i++, j++)
            {
                if (j == 7)
                {
                    j = 0;
                    saida += '\n';
                }
                saida += splitResultado[i];
                for (int k = 0; k < vet[j] - splitResultado[i].Length + 1; k++)
                    saida += " ";
            }
            Console.WriteLine(saida);
        }
        public bool incluir(Carga cargaP)
        {
            // realizando a abertura do banco de dados
            SQLiteConnection conexaoSql;
            conexaoSql = new SQLiteConnection($"Data Source={caminhoDoBanco};Version=3");
            // tentativa de abertura do banco de dados, caso algo esteja errado
            // a função irá mostrar a mensagem de falha para o console e retornar false
            try
            {
                conexaoSql.Open();
            }
            catch (Exception e)
            {
                conexaoSql.Close();
                Console.WriteLine(e.ToString());
                return false;
            }
            // realizando a inserção de uma nova carga no banco de dados
            SQLiteCommand comandoSql = conexaoSql.CreateCommand();
            // preparando a linha de comando que irá para o SGBD
            comandoSql.CommandText = "INSERT INTO Cargas(codRastreio, nomeCliente, rua, bairro, volPeso, descricao, desembarque) ";
            comandoSql.CommandText += "VALUES(" + cargaP.getCodRastreio() + ", " + cargaP.getNomeCliente() + ", ";
            comandoSql.CommandText += cargaP.getRua() + ", " + cargaP.getBairro() + ", " + cargaP.getVolPeso() + ", ";
            comandoSql.CommandText += cargaP.getDescricao() + ", " + cargaP.getData() + ");";
            // tentativa de execução do comando, caso dê errado
            // a função deve retornar false para indicar uma falha
            try
            {
                comandoSql.ExecuteNonQuery();
            }
            catch
            {
                conexaoSql.Close();
                return false;
            }
            conexaoSql.Close();
            return true;
        }

        // excluir é uma função que deleta uma carga usando o código de rastreio como
        // chave de busca
        public bool excluir(Carga cargaP)
        {
            // realizando a abertura do banco de dados
            SQLiteConnection conexaoSql;
            conexaoSql = new SQLiteConnection($"Data Source={caminhoDoBanco};Version=3");
            // tentativa de abertura do banco de dados, caso algo esteja errado
            // a função irá mostrar a mensagem de falha para o console e retornar false
            try
            {
                conexaoSql.Open();
            }
            catch (Exception e)
            {
                conexaoSql.Close();
                Console.WriteLine(e.ToString());
                return false;
            }
            // criando a linha de comando para o banco de dados
            SQLiteCommand comandoSql = conexaoSql.CreateCommand();
            comandoSql.CommandText = "DELETE FROM Cargas WHERE codRastreio = " + cargaP.getCodRastreio() + ";";
            // tentando realizar a execução do comando
            try
            {
                comandoSql.ExecuteNonQuery();
            }
            catch
            { 
                // se chegar aqui, significa que o comando deu errado
                conexaoSql.Close();
                return false; 
            }
            // se chegar aqui, significa que a carga foi excluída com sucesso
            conexaoSql.Close();
            return true;
        }

        // buscarCod é uma função que busca uma carga usando o código da carga
        public bool buscarCod(Carga cargaP)
        {
            // realizando a abertura do banco de dados
            SQLiteConnection conexaoSql;
            conexaoSql = new SQLiteConnection($"Data Source={caminhoDoBanco};Version=3");
            // tentativa de abertura do banco de dados, caso algo esteja errado
            // a função irá mostrar a mensagem de falha para o console e retornar false
            try
            {
                conexaoSql.Open();
            }
            catch (Exception e)
            {
                conexaoSql.Close();
                Console.WriteLine(e.ToString());
                return false;
            }
            // criando a linha de comando para o banco de dados
            SQLiteCommand comandoSql = conexaoSql.CreateCommand();
            // essa variável é responsável por fazer a leitura de uma consulta
            // no sqlite
            SQLiteDataReader leitorDados;
            comandoSql.CommandText = @"SELECT * FROM Cargas WHERE codRastreio = " + cargaP.getCodRastreio();
            comandoSql.CommandText += ";";
            // executando a leitura
            leitorDados = comandoSql.ExecuteReader();
            int verificador = 0;
            string linha = "";
            int[] vet = {0, 0, 0, 0, 0, 0, 0};
            while (leitorDados.Read())
            {
                // se houver algo para ler, deve mostrar no terminal e retornar true ao fim da função
                for (int i = 0; i < leitorDados.FieldCount; i++)
                {
                    string temp = (string) leitorDados.GetValue(i);
                    linha += leitorDados.GetValue(i) + "|"; 
                    if (temp.Length > vet[i])
                    {
                        vet[i] = temp.Length;
                    }
                }
                verificador = 1;
            }
            if (verificador == 1) formatarSaida(leitorDados, vet, linha);
            conexaoSql.Close();
            return verificador == 1;
        }

        // buscarNome é uma função que busca uma carga usando o nome de um cliente como chave de busca
        public bool buscarNome(Carga cargaP)
        {
            // realizando a abertura do banco de dados
            SQLiteConnection conexaoSql;
            conexaoSql = new SQLiteConnection($"Data Source={caminhoDoBanco};Version=3");
            // tentativa de abertura do banco de dados, caso algo esteja errado
            // a função irá mostrar a mensagem de falha para o console e retornar false
            try
            {
                conexaoSql.Open();
            }
            catch (Exception e)
            {
                conexaoSql.Close();
                Console.WriteLine(e.ToString());
                return false;
            }
            // criando a linha de comando para o banco de dados
            SQLiteCommand comandoSql = conexaoSql.CreateCommand();
            // essa variável é responsável por fazer a leitura de uma consulta
            // no sqlite
            SQLiteDataReader leitorDados;
            comandoSql.CommandText = @"SELECT * FROM Cargas WHERE nomeCliente = " + cargaP.getNomeCliente();
            comandoSql.CommandText += ";";
            // executando a leitura
            leitorDados = comandoSql.ExecuteReader();
            int verificador = 0;
            string linha = "";
            int[] vet = { 0, 0, 0, 0, 0, 0, 0 };
            while (leitorDados.Read())
            {
                // se houver algo para ler, deve mostrar no terminal e retornar true ao fim da função
                for (int i = 0; i < leitorDados.FieldCount; i++)
                {
                    string temp = (string)leitorDados.GetValue(i);
                    linha += leitorDados.GetValue(i) + "|";
                    if (temp.Length > vet[i])
                    {
                        vet[i] = temp.Length;
                    }
                }
                verificador = 1;
            }
            if (verificador == 1) formatarSaida(leitorDados, vet, linha);
            conexaoSql.Close();
            return verificador == 1;
        }

        public bool listarCargas()
        {
            // realizando a abertura do banco de dados
            SQLiteConnection conexaoSql;
            conexaoSql = new SQLiteConnection($"Data Source={caminhoDoBanco};Version=3");
            // tentativa de abertura do banco de dados, caso algo esteja errado
            // a função irá mostrar a mensagem de falha para o console e retornar false
            try
            {
                conexaoSql.Open();
            }
            catch (Exception e)
            {
                conexaoSql.Close();
                Console.WriteLine(e.ToString());
                return false;
            }
            // criando a linha de comando para o banco de dados
            SQLiteCommand comandoSql = conexaoSql.CreateCommand();
            // essa variável é responsável por fazer a leitura de uma consulta
            // no sqlite
            SQLiteDataReader leitorDados;
            comandoSql.CommandText = @"SELECT * FROM Cargas ORDER BY desembarque ASC;";
            comandoSql.CommandText += ";";
            // executando a leitura
            leitorDados = comandoSql.ExecuteReader();
            int verificador = 0;
            string linha = "";
            int[] vet = {0, 0, 0, 0, 0, 0, 0};
            while (leitorDados.Read())
            {
                // se houver algo para ler, deve mostrar no terminal e retornar true ao fim da função
                for (int i = 0; i < leitorDados.FieldCount; i++)
                {
                    string temp = (string)leitorDados.GetValue(i);
                    linha += leitorDados.GetValue(i) + "|";
                    if (temp.Length > vet[i])
                    {
                        vet[i] = temp.Length;
                    }
                }
                verificador = 1;
            }
            if (verificador == 1) formatarSaida(leitorDados, vet, linha);
            conexaoSql.Close();
            return verificador == 1;
        }

        static public bool informaString(string saida, string caminhoDoBanco)
        {
            // realizando a abertura do banco de dados
            SQLiteConnection conexaoSql;
            conexaoSql = new SQLiteConnection($"Data Source={caminhoDoBanco};Version=3");
            // tentativa de abertura do banco de dados, caso algo esteja errado
            // a função irá mostrar a mensagem de falha para o console e retornar false
            try
            {
                conexaoSql.Open();
            }
            catch (Exception e)
            {
                conexaoSql.Close();
                Console.WriteLine(e.ToString());
                return false;
            }
            // criando a linha de comando para o banco de dados
            SQLiteCommand comandoSql = conexaoSql.CreateCommand();
            // essa variável é responsável por fazer a leitura de uma consulta
            // no sqlite
            SQLiteDataReader leitorDados;
            comandoSql.CommandText = @"SELECT * FROM Cargas ORDER BY desembarque ASC;";
            comandoSql.CommandText += ";";
            // executando a leitura
            leitorDados = comandoSql.ExecuteReader();
            int verificador = 0;
            while (leitorDados.Read())
            {
                // se houver algo para ler, deve mostrar no terminal e retornar true ao fim da função
                for (int i = 0; i < leitorDados.FieldCount; i++)
                {
                    string temp = (string) leitorDados.GetValue(i);
                    saida += leitorDados.GetValue(i) + "|";
                }
                verificador = 1;
            };
            conexaoSql.Close();
            return verificador == 1;
        }
    };
}
