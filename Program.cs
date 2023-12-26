using System;
using CargaGollog;

public class Program {
    static void Main(string[] args) {
        Carga trem = new Carga();
        trem.setBairro("1234");
        Console.WriteLine(trem.getBairro());
    }
};