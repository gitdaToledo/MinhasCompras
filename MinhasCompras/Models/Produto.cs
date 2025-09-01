using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MinhasCompras.Models
{
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string descricao { get; set; }
        public double quantidade { get; set; }
        public double preco { get; set; }
        public double total { get => quantidade * preco; }
    }
}
