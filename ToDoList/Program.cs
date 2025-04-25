using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ToDoList;
using System.Runtime.Serialization.Formatters.Binary;

namespace ToDoList
{
    [System.Serializable] // Move the Serializable attribute to the class declaration  
    class Program
    {
        enum Menu { Listagem = 1, Adcionar, Editar, Marcar, Remover, Gerenciar, Sair }

        static void Main(string[] args)
        {
            
            TaskManager taskManager = new TaskManager();
            Category category = new Category();

            taskManager.LoadFromFile();
            bool escolheuSair = false;

            while (!escolheuSair)
            {
                Console.WriteLine("====================================");
                Console.WriteLine("===== SISTEMA DE TAREFAS - v1.0 ====");
                Console.WriteLine("====================================");

                Console.WriteLine("1. Listar tarefas\n2. Adicionar nova tarefa\n3. Editar tarefa\n4. Marcar tarefa como concluída\n5. Remover tarefas\n6. Gerenciar categorias\n7. Sair");

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
                        taskManager.EditTask();
                        break;

                    case Menu.Marcar:
                        taskManager.CompleteTask();
                        break;

                    case Menu.Remover:
                        taskManager.RemoveTask();
                        break;

                    case Menu.Gerenciar:
                        category.MenuCategory();
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
