using System.Data;
using System.Data.SQLite;

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
        private string caminhoDoBanco = @"C:\Users\alexa\Documents\Gollog\bibliotecas\CargasGollog\Gollog.db";
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
            comandoSql.CommandText = "INSERT INTO Cargas(codRastreio, nomeCliente, rua, bairro, volPeso, descricao, data) ";
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
            if (leitorDados.Read())
            {
                // se houver algo para ler, deve mostrar no terminal e retornar true ao fim da função
                string linha = "Código: " + leitorDados["codRastreio"] + "\nCliente: " + leitorDados["nomeCliente"] + "\n";
                linha += "Rua: " + leitorDados["rua"] + "\nBairro: " + leitorDados["bairro"] + "\n";
                linha += "Vol e peso: " + leitorDados["volPeso"] + "\nDescrição: ";
                linha += leitorDados["descricao"] + "\nData: " + leitorDados["data"];
                Console.WriteLine(linha);
                verificador = 1;
            }
            conexaoSql.Close();
            return verificador == 1 ? true : false;
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
            while (leitorDados.Read())
            { 
                // se houver algo para ler, deve mostrar no terminal e retornar true ao fim da função
                string linha = "Código: " + leitorDados["codRastreio"] + "\nCliente: " + leitorDados["nomeCliente"] + "\n";
                linha += "Rua: " + leitorDados["rua"] + "\nBairro: " + leitorDados["bairro"] + "\n";
                linha += "Vol e peso: " + leitorDados["volPeso"] + "\nDescrição: ";
                linha += leitorDados["descricao"] + "\nData: " + leitorDados["data"] + "\n";
                Console.WriteLine(linha);
                verificador = 1;
            }
            conexaoSql.Close();
            return verificador == 1 ? true : false;
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
            comandoSql.CommandText = @"SELECT * FROM Cargas ORDER BY data ASC;";
            comandoSql.CommandText += ";";
            // executando a leitura
            leitorDados = comandoSql.ExecuteReader();
            int verificador = 0;
            while (leitorDados.Read())
            {
                // se houver algo para ler, deve mostrar no terminal e retornar true ao fim da função
                string linha = "Código: " + leitorDados["codRastreio"] + "\nCliente: " + leitorDados["nomeCliente"] + "\n";
                linha += "Rua: " + leitorDados["rua"] + "\nBairro: " + leitorDados["bairro"] + "\n";
                linha += "Vol e peso: " + leitorDados["volPeso"] + "\nDescrição: ";
                linha += leitorDados["descricao"] + "\nData: " + leitorDados["data"] + "\n";
                Console.WriteLine(linha);
                verificador = 1;
            }
            conexaoSql.Close();
            return verificador == 1 ? true : false;
        }
    };
}
