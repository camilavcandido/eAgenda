using System;
using eAgenda.ConsoleApp.Compartilhado;
using eAgenda.ConsoleApp.ModuloTarefa;
using eAgenda.ConsoleApp.ModuloContato;
using eAgenda.ConsoleApp.ModuloCompromisso;

namespace eAgenda.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Notificador notificador = new Notificador();
            TelaMenuPrincipal menuPrincipal = new TelaMenuPrincipal(notificador);

            while (true)
            {
                TelaBase telaSelecionada = menuPrincipal.ObterTela();

                if (telaSelecionada is null)
                    return;

                string opcaoSelecionada = telaSelecionada.MostrarOpcoes();

                if (telaSelecionada is ITelaCadastravel)
                    GerenciarCadastroBasico(telaSelecionada, opcaoSelecionada);
                else if (telaSelecionada is TelaCadastroTarefa)
                    GerenciarCadastroTarefa(telaSelecionada, opcaoSelecionada);
                else if (telaSelecionada is TelaCadastroContato)
                    GerenciarCadastroContato(telaSelecionada, opcaoSelecionada);
                else if (telaSelecionada is TelaCadastroCompromisso)
                    GerenciarCadastroComprimisso(telaSelecionada, opcaoSelecionada);


            }

            static void GerenciarCadastroBasico(TelaBase telaSelecionada, string opcaoSelecionada)
            {
                ITelaCadastravel telaCadastroBasico = telaSelecionada as ITelaCadastravel;

                if (telaCadastroBasico is null)
                    return;

                if (opcaoSelecionada == "1")
                    telaCadastroBasico.Inserir();

                else if (opcaoSelecionada == "2")
                    telaCadastroBasico.Editar();

                else if (opcaoSelecionada == "3")
                    telaCadastroBasico.Excluir();

                else if (opcaoSelecionada == "4")
                    telaCadastroBasico.VisualizarRegistros("Tela");
            }

            static void GerenciarCadastroTarefa(TelaBase telaSelecionada, string opcaoSelecionada)
            {
                TelaCadastroTarefa telaCadastroTarefa = telaSelecionada as TelaCadastroTarefa;

                if (telaCadastroTarefa is null)
                    return;

                if (opcaoSelecionada == "1")
                    telaCadastroTarefa.Inserir();
                else if (opcaoSelecionada == "2")
                    telaCadastroTarefa.Editar();
                else if (opcaoSelecionada == "3")
                    telaCadastroTarefa.Excluir();
                else if (opcaoSelecionada == "4")
                    telaCadastroTarefa.VisualizarRegistros("Tela");
                else if (opcaoSelecionada == "5")
                    telaCadastroTarefa.VisualizarTarefasPendentes("Tela");
                else if (opcaoSelecionada == "6")
                    telaCadastroTarefa.VisualizarTarefasConcluidas("Tela");


            }

            static void GerenciarCadastroContato(TelaBase telaSelecionada, string opcaoSelecionada)
            {
                TelaCadastroContato telaCadastroContato = telaSelecionada as TelaCadastroContato;

                if (telaCadastroContato is null)
                    return;

                if (opcaoSelecionada == "1")
                    telaCadastroContato.Inserir();

                else if (opcaoSelecionada == "2")
                    telaCadastroContato.Editar();

                else if (opcaoSelecionada == "3")
                    telaCadastroContato.Excluir();

                else if (opcaoSelecionada == "4")
                    telaCadastroContato.VisualizarRegistros("Tela");

                else if (opcaoSelecionada == "5")
                    telaCadastroContato.VisualizarRegistrosPorCargo("Tela");
            }

            static void GerenciarCadastroComprimisso(TelaBase telaSelecionada, string opcaoSelecionada)
            {
                TelaCadastroCompromisso telaCadastroCompromisso = telaSelecionada as TelaCadastroCompromisso;

                if (telaCadastroCompromisso is null)
                    return;

                if (opcaoSelecionada == "1")
                    telaCadastroCompromisso.Inserir();

                else if (opcaoSelecionada == "2")
                    telaCadastroCompromisso.Editar();

                else if (opcaoSelecionada == "3")
                    telaCadastroCompromisso.Excluir();

                else if (opcaoSelecionada == "4")
                    telaCadastroCompromisso.VisualizarRegistros("Tela");
                else if (opcaoSelecionada == "5")
                    telaCadastroCompromisso.VisualizarCompromissosFuturos("Tela");
                else if (opcaoSelecionada == "6")
                    telaCadastroCompromisso.VisualizarCompromissosPassados("Tela");
                else if (opcaoSelecionada == "7")
                    telaCadastroCompromisso.VisualizarCompromissosPorPeriodo("Tela");
            }

        }
    }
}