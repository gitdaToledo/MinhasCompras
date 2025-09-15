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
        string _descricao;
        double _quantidade;
        double _preco;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string descricao { 
            get => _descricao;

            set
            {
                if (value == null)
                {
                    throw new Exception("Descrição é obrigatória");
                }
             
                _descricao = value;
            }
        }
        public double quantidade
        {
            get => _quantidade;

            set
            {
                if (value == 0)
                {
                    throw new Exception("Informe a quantidade");
                }

                _quantidade = value;
            }
        }
        public double preco
        {
            get => _preco;

            set
            {
                if (value == 0)
                {
                    throw new Exception("Informe o preço");
                }

                _preco = value;
            }
        }

        public DateTime datacompra { get; set; } = DateTime.Now;
        
        public double total { get => quantidade * preco; }
    }
}
