using System;
using eAgenda.ConsoleApp.Compartilhado;
using eAgenda.ConsoleApp.ModuloContato;

namespace eAgenda.ConsoleApp.ModuloCompromisso
{
    public class Compromisso : EntidadeBase
    {
        private string assunto;
        private string local;
        private DateTime dataCompromisso;
        private DateTime horaInicio;
        private DateTime horaTermino;
        private Contato contato;

        public Compromisso(string assunto, string local, DateTime dataCompromisso, DateTime horaInicio, DateTime horaTermino, Contato contato)
        {
            this.Assunto = assunto;
            this.Local = local;
            this.DataCompromisso = dataCompromisso;
            this.HoraInicio = horaInicio;
            this.HoraTermino = horaTermino;
            this.contato = contato;
        }

        public string Assunto { get => assunto; set => assunto = value; }
        public string Local { get => local; set => local = value; }
        public DateTime DataCompromisso { get => dataCompromisso; set => dataCompromisso = value; }
        public DateTime HoraInicio { get => horaInicio; set => horaInicio = value; }
        public DateTime HoraTermino { get => horaTermino; set => horaTermino = value; }

        public override string ToString()
        {

            return "ID: " + id + Environment.NewLine +
                "Assunto: " + Assunto + Environment.NewLine +
                "Local: " + Local + Environment.NewLine +
                "Data: " + DataCompromisso + Environment.NewLine +
                "Hora de Inicio: " + HoraInicio + Environment.NewLine +
                "Hora de Termino: " + HoraTermino + Environment.NewLine +
                "Compromisso relacionado ao contato: " + contato.Nome;

        }


    }
}
