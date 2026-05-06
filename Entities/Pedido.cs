

using System;

namespace  MultiThradDemo.Entities
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }

        public Pedido(string name, int order)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Order = order;
        }
        
    }
}

