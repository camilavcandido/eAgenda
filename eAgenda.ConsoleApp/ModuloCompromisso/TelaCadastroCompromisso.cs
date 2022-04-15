using System;
using System.Collections.Generic;
using eAgenda.ConsoleApp.Compartilhado;
using eAgenda.ConsoleApp.ModuloContato;

namespace eAgenda.ConsoleApp.ModuloCompromisso
{
    public class TelaCadastroCompromisso : TelaBase
    {
        private RepositorioCompromisso _repositorioCompromisso;
        private Notificador _notificador;
        private RepositorioContato _repositorioContato;
        private TelaCadastroContato _telaCadastroContato;

        public TelaCadastroCompromisso(RepositorioCompromisso repositorioCompromisso, Notificador notificador,
            RepositorioContato repositorioContato, TelaCadastroContato telaCadastroContato)
            : base("Cadastro de Compromisso")
        {
            _repositorioCompromisso = repositorioCompromisso;
            _notificador = notificador;
            _repositorioContato = repositorioContato;
            _telaCadastroContato = telaCadastroContato;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar Todos");
            Console.WriteLine("Digite 5 para Visualizar Compromissos Futuros");
            Console.WriteLine("Digite 7 para Visualizar Compromissos Futuros Por Período");
            Console.WriteLine("Digite 6 para Visualizar Compromissos Passados");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }
        public void Inserir()
        {
            MostrarTitulo("Cadastro de Contato");

            Compromisso novoCompromisso = ObterCompromisso();

            _repositorioCompromisso.Inserir(novoCompromisso);

            _notificador.ApresentarMensagem("Compromisso cadastrado!", TipoMensagem.Sucesso);

        }

        public void Editar()
        {

            MostrarTitulo("Editando Contato");

            bool temCompromisso = VisualizarRegistros("Pesquisando");


            if (temCompromisso == false)
            {
                _notificador.ApresentarMensagem("Nenhuma compromisso cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroCompromisso = ObterNumeroRegistro();

            Compromisso compromissoAtualizado = ObterCompromisso();

            bool conseguiuEditar = _repositorioCompromisso.Editar(numeroCompromisso, compromissoAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Compromisso editado com sucesso!", TipoMensagem.Sucesso);

        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Compromisso");

            bool temContato = VisualizarRegistros("Pesquisando");

            if (temContato == false)
            {
                _notificador.ApresentarMensagem("Nenhum compromisso cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroCompromisso = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioCompromisso.Excluir(numeroCompromisso);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Compromisso excluído com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            Console.Clear();

            if (tipoVisualizacao == "tela")
                MostrarTitulo("Visualização de Compromisso");

            List<Compromisso> compromissos = _repositorioCompromisso.SelecionarTodos();

            if (compromissos.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum compromisso cadastrado", TipoMensagem.Atencao);
                return false;
            }

            foreach (Compromisso compromisso in compromissos)
                Console.WriteLine(compromisso.ToString());

            Console.ReadLine();
            return true;
        }

        public bool VisualizarCompromissosFuturos(string tipoVisualizacao)
        {
            Console.Clear();

            if (tipoVisualizacao == "tela")
                MostrarTitulo("Visualização de Compromisso");

            List<Compromisso> compromissosFuturos = _repositorioCompromisso.SelecionarTodos();

            if (compromissosFuturos.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum compromisso cadastrado", TipoMensagem.Atencao);
                return false;
            }

            foreach (Compromisso compromisso in compromissosFuturos)
                if (compromisso.DataCompromisso > DateTime.Now)
                    Console.WriteLine(compromisso.ToString());

            Console.ReadLine();
            return true;
        }

        public bool VisualizarCompromissosPassados(string tipoVisualizacao)
        {
            Console.Clear();

            if (tipoVisualizacao == "tela")
                MostrarTitulo("Visualização de Compromisso");

            List<Compromisso> compromisosPassados = _repositorioCompromisso.SelecionarTodos();

            if (compromisosPassados.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum compromisso cadastrado", TipoMensagem.Atencao);
                return false;
            }

            foreach (Compromisso compromisso in compromisosPassados)
                if (compromisso.DataCompromisso < DateTime.Now)
                    Console.WriteLine(compromisso.ToString());

            Console.ReadLine();
            return true;
        }

        public bool VisualizarCompromissosPorPeriodo(string tipoVisualizacao)
        {
            Console.Clear();

            if (tipoVisualizacao == "tela")
                MostrarTitulo("Visualização de Compromisso por Periodo");

            List<Compromisso> compromissosFuturos = _repositorioCompromisso.SelecionarTodos();

            if (compromissosFuturos.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum compromisso cadastrado", TipoMensagem.Atencao);
                return false;
            }

            Console.Write("Filtrar a partir de (DD/MM/AAAA): ");
            DateTime filtroDataInicio = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Até a data (DD/MM/AAAA): ");
            DateTime filtroDataFim = Convert.ToDateTime(Console.ReadLine());

            foreach (Compromisso compromisso in compromissosFuturos)
                if (compromisso.DataCompromisso > DateTime.Now)
                    if (compromisso.DataCompromisso >= filtroDataInicio && compromisso.DataCompromisso <= filtroDataFim)
                        Console.WriteLine(compromisso.ToString());

            Console.ReadLine();
            return true;
        }

        #region metodos privados
        private Compromisso ObterCompromisso()
        {

            Console.Write("Assunto: ");
            string assunto = Console.ReadLine();

            Console.Write("Local: ");
            string local = Console.ReadLine();

            Console.Write("Data: ");
            DateTime data = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Hora de inicio: ");
            DateTime horaInicio = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Hora de termino: ");
            DateTime horaTermino = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Selecione um contato da sua agenda para relacionar este compromisso: ");
            Contato contato = ObterContato();
            if (contato == null)
                return null;

            return new Compromisso(assunto, local, data, horaInicio, horaTermino, contato);

        }
        private int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do Compromisso: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioCompromisso.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do Compromisso não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

        private Contato ObterContato()
        {
            bool temContato = _telaCadastroContato.VisualizarRegistros("Pesquisando");

            if (!temContato)
            {
                _notificador.ApresentarMensagem("Nenhum contato cadastrado.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o ID do contato: ");
            int idContato = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Contato contatoSelecionado = _repositorioContato.SelecionarRegistro(idContato);

            return contatoSelecionado;
        }

        #endregion

    }
}
