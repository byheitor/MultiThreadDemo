using System;
using MultiThradDemo.Entities;

namespace MultiThreadDemo.Services;

public class PedidoProcessor
{
    public void ProcessarPedido(Pedido pedido)
    {   
        // Simula um trabalho assíncrono (ex: gravar no banco, chamar API)
        Console.WriteLine($"Pedido {pedido.Order} - {pedido.Name} processado");
    }
    
}