MultiThread Demo — .NET 8

Exemplo didático de processamento paralelo com Task, ThreadPool e SemaphoreSlim em .NET 8.

O que o projeto faz

Gera 200.000 pedidos e os processa em paralelo, controlando quantas threads rodam ao mesmo tempo via SemaphoreSlim.

Estrutura

MultiThreadDemo/
├── Entities/
│   └── Pedido.cs          # Entidade com Id (Guid), Name e Order
├── Services/
│   └── PedidoProcessor.cs # Lógica de processamento de cada pedido
└── Program.cs             # Geração, disparo e controle das tasks

Conceitos demonstrados

ConceitoOndeTask.Run()Envia trabalho para o ThreadPoolSemaphoreSlimControla o máximo de threads simultâneasawait Task.WhenAll()Aguarda todas as tasks sem bloquearStopwatchMede o tempo total de execução

Por que o SemaphoreSlim?
Sem controle, disparar 200k tasks ao mesmo tempo causa:

Contenção excessiva no ThreadPool
Alto consumo de memória
Degradação de performance (context switching)

Com SemaphoreSlim(50), no máximo 50 tasks rodam ao mesmo tempo. O resto aguarda vez de forma não-bloqueante (await WaitAsync()).
Como rodar
bashdotnet run
Requer .NET 8 SDK.
