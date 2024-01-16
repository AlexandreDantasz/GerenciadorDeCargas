using Microsoft.Office.Interop.Excel;
using CargasGollog;

namespace CargasExcel
{
    public class GollogExcel
    {
        static public bool criar(string caminho)
        {
            Application excel = new Application();
            Workbook meuWorkbook = excel.Workbooks.Add();
            Worksheet minhaWorksheet = (Worksheet) meuWorkbook.Sheets[1];
            minhaWorksheet.Cells[1, 1] = "Identificação";
            minhaWorksheet.Cells[1, 2] = "Nome";
            minhaWorksheet.Cells[1, 3] = "Rua";
            minhaWorksheet.Cells[1, 4] = "Bairro";
            minhaWorksheet.Cells[1, 5] = "Volumes";
            minhaWorksheet.Cells[1, 6] = "Descrição";
            minhaWorksheet.Cells[1, 7] = "Desembarque";
            string busca = "";
            if (!CargaDao.informaString(ref busca, @".\Gollog.db")) return false;
            string[] informacoes = busca.Split('|');
            for (int i = 0, lin = 2, col = 1; i < informacoes.Length; i++, col++)
            {
                if (col == 8)
                {
                    col = 1;
                    lin++;
                }
                minhaWorksheet.Cells[lin, col] = informacoes[i];
            }
            meuWorkbook.SaveAs(@".\" + caminho);
            meuWorkbook.Close();
            excel.Quit();
            return true;
        }
    }
}
