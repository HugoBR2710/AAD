﻿CREATE PROCEDURE ObterFuncionarios AS BEGIN SELECT TOP (1000) [NIF],[Nome],[Apelido],[DataNasc],[DataAdmissao],[Morada],[Email],[CPCP],[Localidade] FROM [AAD].[dbo].[Funcionario] Join CP on Funcionario.CPCP = Cp.CP; END;