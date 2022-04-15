using System;
using System.Collections.Generic;
using eAgenda.ConsoleApp.Compartilhado;

namespace eAgenda.ConsoleApp.ModuloTarefa
{
    public class TelaCadastroTarefa : TelaBase
    {
        private RepositorioTarefa _repositorioTarefa;
        private Notificador _notificador;

        private static int contadorIdItem = 0;
        public TelaCadastroTarefa(RepositorioTarefa repositorioTarefa, Notificador notificador) 
            : base("Cadastro de Tarefas")
        {
            _repositorioTarefa = repositorioTarefa;
            _notificador = notificador;
        }
        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar");
            Console.WriteLine("Digite 5 para Visualizar Tarefas Pendentes");
            Console.WriteLine("Digite 6 para Visualizar Tarefas Concluidas");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Tarefa");

            Tarefa novaTarefa = ObterTarefa();

             _repositorioTarefa.Inserir(novaTarefa);

            _notificador.ApresentarMensagem("Tarefa cadastrada!", TipoMensagem.Sucesso);

        }

        public void Editar()
        {

            MostrarTitulo("Editando Tarefa");
            
            bool temTarefa = VisualizarRegistros("Pesquisando");
           

            if (temTarefa == false)
            {
                _notificador.ApresentarMensagem("Nenhuma tarefa cadastrada para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroTarefa = ObterNumeroRegistro();

            Tarefa tarefaAtualizada = ObterTarefaEditada();

            bool conseguiuEditar = _repositorioTarefa.Editar(numeroTarefa, tarefaAtualizada);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Tarefa editada com sucesso!", TipoMensagem.Sucesso);

        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Tarefa");

            bool temTarefa = VisualizarRegistros("Pesquisando");

            if (temTarefa == false)
            {
                _notificador.ApresentarMensagem("Nenhuma tarefa cadastrada para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroTarefa = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioTarefa.Excluir(numeroTarefa);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Tarefa excluída com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            Console.Clear();

            if (tipoVisualizacao == "tela")
                MostrarTitulo("Visualização de Tarefas");

        List<Tarefa> tarefas = _repositorioTarefa.SelecionarTodos();

            if(tarefas.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma tarefa cadastrada", TipoMensagem.Atencao);
                return false;
            }

            foreach (Tarefa tarefa in tarefas)
                if (tarefa.Prioridade == 1)
                    Console.WriteLine(tarefa.ToString());

            foreach (Tarefa tarefa in tarefas)
                if (tarefa.Prioridade == 2)
                    Console.WriteLine(tarefa.ToString());

            foreach (Tarefa tarefa in tarefas)
                if (tarefa.Prioridade == 3)
                    Console.WriteLine(tarefa.ToString());


            Console.ReadLine();
            return true;
        }

        public bool VisualizarTarefasPendentes(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Tarefa Pendente");

            List<Tarefa> tarefasPendentes = _repositorioTarefa.SelecionarTodos();

            if (tarefasPendentes.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma tarefa disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Tarefa tarefaPendente in tarefasPendentes)
                if (tarefaPendente.Percentual < 100 && tarefaPendente.Prioridade == 1)
                    Console.WriteLine(tarefaPendente.ToString());

            foreach (Tarefa tarefaPendente in tarefasPendentes)
                if (tarefaPendente.Percentual < 100 && tarefaPendente.Prioridade == 2)
                    Console.WriteLine(tarefaPendente.ToString());

            foreach (Tarefa tarefaPendente in tarefasPendentes)
                if (tarefaPendente.Percentual < 100 && tarefaPendente.Prioridade == 3)
                    Console.WriteLine(tarefaPendente.ToString());

            Console.ReadLine();

            return true;
        }

        public bool VisualizarTarefasConcluidas(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Tarefa Concluída");

            List<Tarefa> tarefasConcluidas = _repositorioTarefa.SelecionarTodos();

            if (tarefasConcluidas.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma tarefa disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Tarefa tarefaConcluida in tarefasConcluidas)
                if (tarefaConcluida.Percentual == 100 && tarefaConcluida.Prioridade == 1)
                    Console.WriteLine(tarefaConcluida.ToString());

            foreach (Tarefa tarefaConcluida in tarefasConcluidas)
                if (tarefaConcluida.Percentual == 100 && tarefaConcluida.Prioridade == 2)
                    Console.WriteLine(tarefaConcluida.ToString());

            foreach (Tarefa tarefaConcluida in tarefasConcluidas)
                if (tarefaConcluida.Percentual == 100 && tarefaConcluida.Prioridade == 3)
                    Console.WriteLine(tarefaConcluida.ToString());

            Console.ReadLine();

            return true;
        }

        #region metodos privados
        private Tarefa ObterTarefa()
        {
            Console.WriteLine("Digite o titulo da tarefa: ");
            string titulo = Console.ReadLine();

            DateTime dataCriacao = DateTime.Now;

            Console.WriteLine("Prioridade: ");
            Console.WriteLine("1 - alta | 2 - normal | 3 - baixa");
            int prioridade = int.Parse(Console.ReadLine());

            List<Item> listaItem = new List<Item>();

            bool continuarAddItem = false;

            do
            {
                Console.WriteLine("Adicionar item na tarefa: \n");
                Item item = ObterItem();
                item.id = ++contadorIdItem;
                listaItem.Add(item);

                Console.WriteLine("\nAdicionar outro item? 1 - sim | 2 - não");
                string opcaoContinuar = Console.ReadLine();
                if (opcaoContinuar == "1")
                    continuarAddItem = true;
                else
                    continuarAddItem = false;
                
            } while (continuarAddItem);


             int countConcluidos = 0;

            foreach (Item itens in listaItem)
                if (itens.Status == 1)
                    countConcluidos++;

            int percentual = 100 * countConcluidos / listaItem.Count;


            return new Tarefa(titulo, dataCriacao,  prioridade, percentual, listaItem);
        }
        private int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da Tarefa: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioTarefa.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID da Tarefa não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
        private Tarefa ObterTarefaEditada()
        {

            Console.WriteLine("Digite o titulo da tarefa: ");
            string titulo = Console.ReadLine();

            DateTime dataCriacao = DateTime.Now;

            Console.WriteLine("Prioridade: ");
            Console.WriteLine("1 - alta | 2 - normal | 3 - baixa");
            int prioridade = int.Parse(Console.ReadLine());

            List<Item> listaItem = new List<Item>();

            bool continuarAddItem = false;

            do
            {
                Console.WriteLine("Adicionar item na tarefa: \n");
                Item item = ObterItem();
                item.id = ++contadorIdItem;
                listaItem.Add(item);

                Console.WriteLine("\nAdicionar outro item? 1 - sim | 2 - não");
                string opcaoContinuar = Console.ReadLine();
                if (opcaoContinuar == "1")
                    continuarAddItem = true;
                else
                    continuarAddItem = false;

            } while (continuarAddItem);




            int countConcluidos = 0;

            foreach (Item itens in listaItem)
                if (itens.Status == 1)
                    countConcluidos++;

            int percentual = 100 * countConcluidos / listaItem.Count;


            return new Tarefa(titulo, dataCriacao, prioridade, percentual, listaItem);

        }
        private Item ObterItem()
        {
            
            Console.Write("Digite a descrição do item: ");
            string stringDescricao = Console.ReadLine();

            Console.Write("Status: 1 - Concluido | 2 - Pendente): ");
            int statusItem = int.Parse(Console.ReadLine());

            return new Item(stringDescricao, statusItem);
        }

        #endregion

    }
}
