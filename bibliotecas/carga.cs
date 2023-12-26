namespace CargaGollog {

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
    
    public class CargaDao {
        private Carga iCarga;
        public CargaDao() {
            iCarga = new Carga();
        }
    };
}