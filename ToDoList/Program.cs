using System;
using ToDoList.Utils;

namespace ToDoList
{
    [System.Serializable]
    class Program
    {
        enum Menu { Listagem = 1, Adcionar, Editar, Marcar, Remover, Gerenciar, Filtrar, Sair }

        static void Main(string[] args)
        {
            
            TaskManager taskManager = new TaskManager();
            Category category = new Category();
            MenuManager menuManager = new MenuManager();

            taskManager.LoadFromFile();
            category.LoadFromFileCategories();
            bool escolheuSair = false;

            while (!escolheuSair)
            {
                menuManager.Home();
                int intOp = InputValidador.GetValidInput<int>(
                    "Escolha uma opção: ",
                    entrada =>
                    {
                        bool valido = int.TryParse(entrada, out int valor)
                                      && valor >= 1 && valor <= 7; // Validando o intervalo de opções
                        return (valido, valor);
                    }
                );
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

                    case Menu.Filtrar:
                        taskManager.ObterTarefasFiltradas();
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