using System;
using eAgenda.ConsoleApp.Compartilhado;
using System.Collections.Generic;

namespace eAgenda.ConsoleApp.ModuloTarefa
{
    public class Tarefa : EntidadeBase
    {
        private string titulo;
        private DateTime dataCriacao;
        private DateTime dataConclusao;
        private int prioridade;
        private int percentual;
        private List<Item> itens;

        public string Titulo { get => titulo; set => titulo = value; }
        public DateTime DataCriacao { get => dataCriacao; set => dataCriacao = value; }
        public DateTime DataConclusao { get => dataConclusao; set => dataConclusao = value; }
        public int Prioridade { get => prioridade; set => prioridade = value; }
        public int Percentual { get => percentual; set => percentual = value; }

        public Tarefa(string titulo, DateTime dataCriacao, int prioridade)
        {
            this.Titulo = titulo;
            this.DataCriacao = dataCriacao;
            this.Prioridade = prioridade;
        }

        public Tarefa(string titulo, DateTime dataCriacao, int prioridade, int percentual, List<Item> itens)
        {
            this.Titulo = titulo;
            this.DataCriacao = dataCriacao;
            this.Prioridade = prioridade;
            this.Percentual = percentual;
            this.itens = itens;
        }

        public override string ToString()
        {
            string strPrioridade = "";
            if (Prioridade == 1)
                strPrioridade = "Alta";
            else if (Prioridade == 2)
                strPrioridade = "Normal";
            else if(Prioridade == 3)
                strPrioridade = "Baixa";

            return "ID Tarefa: " + id + Environment.NewLine +
                 "Titulo: " + Titulo + Environment.NewLine +
                 "Data Criação: " + DataCriacao + Environment.NewLine +
                 "Data Conclusão: " + DataConclusao + Environment.NewLine +
                 "Prioridade: " + strPrioridade + Environment.NewLine +
                 "Percentual: " + Percentual + "%" + Environment.NewLine +
                 "Items da Tarefa: " + Environment.NewLine +
                  ListarItemsTarefa();
        }

        private string ListarItemsTarefa()
        {
            string itemsString = "";

            foreach (Item item in itens)
                itemsString += item.ToString() + "\n";

            return itemsString;
        }

    }
}
