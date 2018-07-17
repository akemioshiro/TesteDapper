using System;

namespace Dapper
{
    public class Cliente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Email { get; set; }
        public DateTime DataHoraInclusao { get; set; }
    }
}
