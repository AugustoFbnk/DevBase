using DevBase.Domain.Entidades.Cadastros;
using DevBase.Infra.Data.Interfaces.Repositorios.Cadastros;
using DevBase.Util.ExtensionMethods;
using System;

namespace DevBase.Services.Test.Environments.Cadastros.DesenvolvedorEnvironments
{
    public static class DesenvolvedorEnvironment
    {
        public static void ConfigurarEnvironment(IDesenvolvedorRepositorio desenvolvedorRepositorio)
        {
            MontarBaseDeDesenvolvedores(desenvolvedorRepositorio);
        }

        private async static void MontarBaseDeDesenvolvedores(IDesenvolvedorRepositorio desenvolvedorRepositorio)
        {
            var dataNascimentoDev1 = new DateTime(2000, 01, 01);
            var dev1 = new Desenvolvedor
            {
                DataNascimento = dataNascimentoDev1,
                Hobby = "Assistir filmes",
                Idade = dataNascimentoDev1.CalcularIdade(),
                Nome = "Dev 1",
                Sexo = Sexo.Feminino
            };
            await desenvolvedorRepositorio.Create(dev1);

            var dataNascimentoDev2 = new DateTime(1992, 02, 01);
            var dev2 = new Desenvolvedor
            {
                DataNascimento = dataNascimentoDev2,
                Hobby = "Assistir filmes",
                Idade = dataNascimentoDev2.CalcularIdade(),
                Nome = "Dev 2",
                Sexo = Sexo.Feminino
            };
            await desenvolvedorRepositorio.Create(dev2);

            var dataNascimentoDev3 = new DateTime(1993, 02, 01);
            var dev3 = new Desenvolvedor
            {
                DataNascimento = dataNascimentoDev3,
                Hobby = "Assistir filmes",
                Idade = dataNascimentoDev3.CalcularIdade(),
                Nome = "Dev 3",
                Sexo = Sexo.Masculino
            };
            await desenvolvedorRepositorio.Create(dev3);

            var dataNascimentoDev4 = new DateTime(1993, 05, 10);
            var dev4 = new Desenvolvedor
            {
                DataNascimento = dataNascimentoDev4,
                Hobby = "Assistir filmes",
                Idade = dataNascimentoDev4.CalcularIdade(),
                Nome = "Dev 4",
                Sexo = Sexo.Masculino
            };
            await desenvolvedorRepositorio.Create(dev4);

            var dataNascimentoDev5 = new DateTime(1991, 12, 10);
            var dev5 = new Desenvolvedor
            {
                DataNascimento = dataNascimentoDev5,
                Hobby = "Assistir filmes",
                Idade = dataNascimentoDev5.CalcularIdade(),
                Nome = "Dev 5",
                Sexo = Sexo.Feminino
            };
            await desenvolvedorRepositorio.Create(dev5);

            var dataNascimentoDev6 = new DateTime(2002, 10, 13);
            var dev6 = new Desenvolvedor
            {
                DataNascimento = dataNascimentoDev6,
                Hobby = "Assistir filmes",
                Idade = dataNascimentoDev6.CalcularIdade(),
                Nome = "Dev 6",
                Sexo = Sexo.Feminino
            };
            await desenvolvedorRepositorio.Create(dev6);

            var dataNascimentoDev7 = new DateTime(1980, 10, 13);
            var dev7 = new Desenvolvedor
            {
                DataNascimento = dataNascimentoDev7,
                Hobby = "Assistir filmes",
                Idade = dataNascimentoDev7.CalcularIdade(),
                Nome = "Dev 7",
                Sexo = Sexo.Feminino
            };
            await desenvolvedorRepositorio.Create(dev7);

            var dataNascimentoDev8 = new DateTime(1987, 10, 13);
            var dev8 = new Desenvolvedor
            {
                DataNascimento = dataNascimentoDev8,
                Hobby = "Assistir filmes",
                Idade = dataNascimentoDev8.CalcularIdade(),
                Nome = "Dev 8",
                Sexo = Sexo.Masculino
            };
            await desenvolvedorRepositorio.Create(dev8);

            var dataNascimentoDev9 = new DateTime(1995, 11, 13);
            var dev9 = new Desenvolvedor
            {
                DataNascimento = dataNascimentoDev9,
                Hobby = "Assistir filmes",
                Idade = dataNascimentoDev9.CalcularIdade(),
                Nome = "Dev 9",
                Sexo = Sexo.Masculino
            };
            await desenvolvedorRepositorio.Create(dev9);

            var dataNascimentoDev10 = new DateTime(1992, 11, 20);
            var dev10 = new Desenvolvedor
            {
                DataNascimento = dataNascimentoDev10,
                Hobby = "Assistir filmes",
                Idade = dataNascimentoDev10.CalcularIdade(),
                Nome = "Dev 10",
                Sexo = Sexo.Feminino
            };
            await desenvolvedorRepositorio.Create(dev10);

            var dataNascimentoDev11 = new DateTime(1998, 08, 08);
            var dev11 = new Desenvolvedor
            {
                DataNascimento = dataNascimentoDev11,
                Hobby = "Assistir filmes",
                Idade = dataNascimentoDev11.CalcularIdade(),
                Nome = "Dev 11",
                Sexo = Sexo.Feminino
            };
            await desenvolvedorRepositorio.Create(dev11);

            var dataNascimentoDev12 = new DateTime(1998, 09, 08);
            var dev12 = new Desenvolvedor
            {
                DataNascimento = dataNascimentoDev12,
                Hobby = "Assistir filmes",
                Idade = dataNascimentoDev12.CalcularIdade(),
                Nome = "Dev 12",
                Sexo = Sexo.Masculino
            };
            await desenvolvedorRepositorio.Create(dev12);
        }
    }
}
