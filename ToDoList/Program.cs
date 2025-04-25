using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ToDoList;

namespace ToDoList
{
    class Program
    {
        enum Menu { Listagem = 1, Adcionar, Editar, Marcar, Remover, Gerenciar, Sair }

        static void Main(string[] args)
        {
            TaskManager taskManager = new TaskManager(); // Moved the instantiation of TaskManager here
            bool escolheuSair = false;

            while (!escolheuSair)
            {
                Console.WriteLine("====================================");
                Console.WriteLine("===== SISTEMA DE TAREFAS - v1.0 ====");
                Console.WriteLine("====================================");

                Console.WriteLine("1. Listar tarefas\n2. Adicionar nova tarefa\n3. Editar tarefa\n4. Marcar tarefa como concluída\n5. Remover tarefas\n6. Gerenciar categorias\n7. Salvar e sair");

                Console.WriteLine("----------------------------------");
                Console.Write("Escolha uma opção: ");
                int intOp = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intOp;

                switch (opcao)
                {
                    case Menu.Listagem:
                        taskManager.ListTasks();
                        break;

                    case Menu.Adcionar:
                        taskManager.AddTask();
                        break;

                    case Menu.Editar:
                        break;

                    case Menu.Marcar:
                        break;

                    case Menu.Remover:
                        taskManager.RemoveTask();
                        break;

                    case Menu.Gerenciar:
                        break;

                    case Menu.Sair:
                        escolheuSair = true;
                        break;
                }
                Console.Clear();
            }
        }
    }
}
