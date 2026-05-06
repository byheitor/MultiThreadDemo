using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MultiThradDemo.Entities;
using MultiThreadDemo.Services;


const int totalPedidos = 200_000;

// Limita quantas threads rodam ao mesmo tempo.
// Sem isso, 200k tasks simultâneas competem por CPU e memória.
//SemaphoreSlim é thread-safe e não bloqueia a thread — usa await.
const int maxParalelo = 50;

var semaphore = new SemaphoreSlim(maxParalelo);

var processor = new PedidoProcessor();
var tasks = new List<Task>();

var stopWatch = System.Diagnostics.Stopwatch.StartNew();

// ──────────────────────────────────────────────
// Geração e disparo dos pedidos
// ──────────────────────────────────────────────

for (int i = 1; i < totalPedidos; i++)
{
    var pedido = new Pedido(name: $"Produto - {i}", order: i);

    // Aguarda uma vaga no semáforo sem bloquear a thread principal
    await semaphore.WaitAsync();

    var task = Task.Run(() =>
        {
            try
            {
                processor.ProcessarPedido(pedido);
            }
            finally
            {
                // Libera a vaga para o próximo pedido entrar
                semaphore.Release();
            }
        }
    );

    tasks.Add(task);
}

// ──────────────────────────────────────────────
//Aguarda todos os pedidos terminarem
// ──────────────────────────────────────────────
await Task.WhenAll(tasks);

stopWatch.Stop();

Console.WriteLine($"\n✅ Todos os {totalPedidos:N0} pedidos processados em {stopWatch.Elapsed.TotalSeconds:F2}s");