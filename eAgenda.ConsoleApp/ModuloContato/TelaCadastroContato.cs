using System;
using System.Collections.Generic;
using eAgenda.ConsoleApp.Compartilhado;
using System.Linq;

namespace eAgenda.ConsoleApp.ModuloContato
{
    public class TelaCadastroContato : TelaBase
    {
        private RepositorioContato _repositorioContato;
        private Notificador _notificador;

        public TelaCadastroContato(RepositorioContato repositorioContato, Notificador notificador)
            : base("Cadastro de Contato")
        {
            _repositorioContato = repositorioContato;
            _notificador = notificador;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar Todos");
            Console.WriteLine("Digite 5 para Visualizar Agrupado por Cargo");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }
        public void Inserir()
        {
            MostrarTitulo("Cadastro de Contato");

            Contato novoContato = ObterContato();

            _repositorioContato.Inserir(novoContato);

            _notificador.ApresentarMensagem("Contato cadastrado!", TipoMensagem.Sucesso);

        } 

        public void Editar()
        {

            MostrarTitulo("Editando Contato");

            bool temContato = VisualizarRegistros("Pesquisando");


            if (temContato == false)
            {
                _notificador.ApresentarMensagem("Nenhuma contato cadastrada para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroContato = ObterNumeroRegistro();

            Contato contatoAtualizado = ObterContato();

            bool conseguiuEditar = _repositorioContato.Editar(numeroContato, contatoAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Contato editado com sucesso!", TipoMensagem.Sucesso);

        } 

        public void Excluir()
        {
            MostrarTitulo("Excluindo Contato");

            bool temContato = VisualizarRegistros("Pesquisando");

            if (temContato == false)
            {
                _notificador.ApresentarMensagem("Nenhum contato cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroContato = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioContato.Excluir(numeroContato);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Contato excluído com sucesso!", TipoMensagem.Sucesso);
        } 

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            Console.Clear();

            if (tipoVisualizacao == "tela")
                MostrarTitulo("Visualização de Contatos");

            List<Contato> contatos = _repositorioContato.SelecionarTodos();

            if (contatos.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum contato cadastrado", TipoMensagem.Atencao);
                return false;
            }

            foreach (Contato contato in contatos)
                Console.WriteLine(contato.ToString());

            Console.ReadLine();
            return true;
        }

        public bool VisualizarRegistrosPorCargo(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Contato por Cargo");


            List<Contato> contatos = _repositorioContato.SelecionarTodos();

            if (contatos.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum contato cadastrado", TipoMensagem.Atencao);

                return false;
            }

            List<string> cargosExistentes = ObterCargos(contatos);

            foreach (string cargo in cargosExistentes)
            {
                Console.WriteLine("Agrupado pelo cargo: " + cargo);
                foreach (Contato contato in contatos)
                {
                    if (contato.Cargo == cargo)
                        Console.WriteLine(contato);

                    Console.WriteLine();
                }

            }

            Console.ReadLine();
            return true;
        }

        #region metodos privados
        private Contato ObterContato()
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();
            bool emailValido = ValidarEmail(email);
            while(emailValido == false)
            {
                Console.Write("Email Inválido. Digite novamente\n");
                Console.Write("Email: ");
                email = Console.ReadLine();
                ValidarEmail(email);
            }

            Console.Write("Telefone: ");
            string telefone = Console.ReadLine();
            telefone = ValidarTelefone(telefone);

            Console.Write("Empresa: ");
            string empresa = Console.ReadLine();

            Console.Write("Cargo: ");
            string cargo = Console.ReadLine();

            return new Contato(nome, email, telefone, empresa, cargo);
        }
        private int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do Contato: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioContato.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do Contato não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
        private List<string> ObterCargos(List<Contato> contatos)
        {
            List<string> cargosCadastrados = new List<string>();
            foreach (Contato contato in contatos)
            {
                cargosCadastrados.Add(contato.Cargo);
            }
            return cargosCadastrados.Distinct().ToList();
        }

        #endregion

        #region validacoes
        private static string ValidarTelefone(string telefone)
        {
            while (telefone.Length != 9)
            {
                Console.Write("Telefone deve conter 9 digitos.");
                Console.Write("Telefone: ");
                telefone = Console.ReadLine();
            }

            return telefone;
        }

        private static bool ValidarEmail(string email)
        {
            if (email.Contains("@") && email.Contains("."))
                return true;

            return false;
        }

        #endregion

    }
}
