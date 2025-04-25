using System;
using System.Collections.Generic;

namespace ToDoList
{
    public class TaskManager
    {
        
        static List<Task> tasks = new List<Task>();
        struct Task
        {
            public string titulo;
            public string descricao;
            public DateTime dataVencimento;
            public string categoria;
        }
        public void AddTask()
        {
            Console.Clear();
            Task task = new Task();

            Console.WriteLine("-------------------------------------");
            Console.WriteLine("        ADICIONAR NOVA TAREFA        "); 
            Console.WriteLine("-------------------------------------");

            Console.Write("Título: ");
            task.titulo = Console.ReadLine();

            Console.Write("Descrição: ");
            task.descricao = Console.ReadLine();

            Console.Write("Data de vencimento (dd/mm/aaaa): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime dataVencimento))
            {
                task.dataVencimento = dataVencimento;
            }
            else
            {
                Console.Write("Data inválida. A tarefa não foi adicionada.");
                return;
            }
            Console.WriteLine("");

            Console.WriteLine("\nCategorias disponíveis:");
            foreach (var categoria in Enum.GetValues(typeof(Category.Categoria)))
            {
                Console.WriteLine($"{(int)categoria}. {categoria}");
            }

            Console.Write("\nEscolha uma categoria (número): ");
            if (int.TryParse(Console.ReadLine(), out int categoriaEscolhida) &&
                Enum.IsDefined(typeof(Category.Categoria), categoriaEscolhida))
            {
                task.categoria = ((Category.Categoria)categoriaEscolhida).ToString();
            }
            else
            {
                Console.WriteLine("Categoria inválida. A tarefa não foi adicionada.");
                return;
            }

            tasks.Add(task);

            Console.WriteLine("Tarefa adicionada com sucesso!");
        }

        public void ListTasks(bool pausar = true)
        {
            Console.Clear();
            if (tasks.Count > 0)
            {
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("          TAREFAS PENDENTES          ");
                Console.WriteLine("-------------------------------------");
                int i = 0;

                foreach (Task task in tasks)
                {
                    Console.WriteLine($"ID: [{i}] [] {task.titulo} - Categoria: {task.categoria} - Vence em: {task.dataVencimento.ToString("dd/MM/yyyy")}");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("Nenhuma tarefa cadastrada!");
            }
            Console.WriteLine();
            if (pausar)
            {
                Console.WriteLine("Aperte ENTER para voltar ao menu.");
                Console.ReadLine();
            }
        }


        public void CompleteTask(int id) 
        { 

        }

        public void RemoveTask() 
        { 
            ListTasks(false);

            Console.WriteLine("Digite o ID da task que você quer remover: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int id) && id >= 0 && id < tasks.Count)
            {
                tasks.RemoveAt(id);
                Console.WriteLine("Tarefa removida com sucesso!");
                //Salvar();

                Console.WriteLine();
                Console.WriteLine("Aperte ENTER para voltar ao menu.");
            }
            else
            {
                Console.WriteLine("ID inválido ou entrada inválida!");
            }
            Console.ReadLine();
        }

        public void SaveToFile(string path) 
        { 

        }
        public void LoadFromFile(string path) 
        {

        }
    }
}
