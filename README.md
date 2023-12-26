Criação do banco de dados com sqlite3:

sqlite3
.open Gollog.db

CREATE TABLE IF NOT EXISTS Cargas(codRastreio TEXT PRIMARY KEY NOT NULL, nomeCliente TEXT, rua TEXT, bairro TEXT, volPeso TEXT, descricao TEXT, data TEXT);
