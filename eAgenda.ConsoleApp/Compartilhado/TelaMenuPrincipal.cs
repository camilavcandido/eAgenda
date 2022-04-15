using System;
using eAgenda.ConsoleApp.ModuloTarefa;
using eAgenda.ConsoleApp.ModuloContato;
using eAgenda.ConsoleApp.ModuloCompromisso;

namespace eAgenda.ConsoleApp.Compartilhado
{
    public class TelaMenuPrincipal
    {
        private RepositorioTarefa _repositorioTarefa;
        private TelaCadastroTarefa _telaCadastroTarefa;

        private RepositorioContato _repositorioContato;
        private TelaCadastroContato _telaCadastroContato;

        private RepositorioCompromisso _repositorioCompromisso;
        private TelaCadastroCompromisso _telaCadastroCompromisso;

        public TelaMenuPrincipal(Notificador notificador)
        {
            _repositorioTarefa = new RepositorioTarefa();
            _telaCadastroTarefa = new TelaCadastroTarefa(_repositorioTarefa, notificador);
            _repositorioContato = new RepositorioContato();
            _telaCadastroContato = new TelaCadastroContato(_repositorioContato, notificador);
            _repositorioCompromisso = new RepositorioCompromisso();
            _telaCadastroCompromisso = new TelaCadastroCompromisso(_repositorioCompromisso, notificador, _repositorioContato, _telaCadastroContato);

        }

        public string MostrarOpcoes()
        {
            Console.Clear();

            Console.WriteLine("eAgenda");

            Console.WriteLine();

            Console.WriteLine("Digite 1 para Gerenciar Tarefas");
            Console.WriteLine("Digite 2 para Gerenciar Contatos");
            Console.WriteLine("Digite 3 para Gerenciar Compromissos");


            Console.WriteLine("Digite s para sair");

            string opcaoSelecionada = Console.ReadLine();

            return opcaoSelecionada;
        }

        public TelaBase ObterTela()
        {
            string opcao = MostrarOpcoes();

            TelaBase tela = null;

            if (opcao == "1")
                tela = _telaCadastroTarefa;
            else if (opcao == "2")
                tela = _telaCadastroContato;
            else if (opcao == "3")
                tela = _telaCadastroCompromisso;

            return tela;
        }

    }
}
