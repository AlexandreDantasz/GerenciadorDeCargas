using CargasGollog;

public class Program
{
    static void Main(string[] args)
    {
        Carga trem = new Carga();
        trem.setCodRatreio("'1254'");
        trem.setNomeCliente(@"'Alexandre Dantas de Mendonça'");
        trem.setRua(@"'Crisântemo'");
        trem.setBairro(@"'Jardim Bonito'");
        trem.setVolPeso(@"'1/200g'");
        trem.setDescricao(@"'Produto eletrônico importado'");
        trem.setData(@"'2023-12-26'");
        CargaDao dao = new CargaDao();
        dao.incluir(trem);
    }
};