using System;
using eAgenda.ConsoleApp.Compartilhado;

namespace eAgenda.ConsoleApp
{
    public class Item : EntidadeBase
    {
        private string descricao;
        private int status;

        public Item(string descricao, int status)
        {
            this.Descricao = descricao;
            this.Status = status;
        }

        public string Descricao { get => descricao; set => descricao = value; }
        public int Status { get => status; set => status = value; }

        public override string ToString()
        {
            string stringStatus = "";
            if (Status == 1)
                stringStatus = "Concluido";
            else if (Status == 2)
                stringStatus = "Pendente";
            return "ID item: " + id + Environment.NewLine +
                "Descrição: " + Descricao + Environment.NewLine +
                "Status: " + stringStatus + Environment.NewLine;
        }


    }
}