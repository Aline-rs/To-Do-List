using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ToDoList
{
    public class TaskManager
    {
        static List<Task> tasks = new List<Task>();
        [System.Serializable]
        struct Task
        {
            public string titulo;
            public string descricao;
            public DateTime dataVencimento;
            public string categoria;
            public bool concluida;
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
                Console.WriteLine();
                Console.WriteLine("Data inválida. A tarefa não foi adicionada.");
                Console.WriteLine("Aperte ENTER para voltar ao menu.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("\nCategorias disponíveis:");
            for (int i = 0; i < Category.Categorias.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Category.Categorias[i]}");
            }

            Console.Write("\nEscolha uma categoria (número): ");
            if (int.TryParse(Console.ReadLine(), out int categoriaEscolhida) &&
                categoriaEscolhida > 0 && categoriaEscolhida <= Category.Categorias.Count)
            {
                task.categoria = Category.Categorias[categoriaEscolhida - 1];
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Categoria inválida. A tarefa não foi adicionada.");
                Console.WriteLine("Aperte ENTER para voltar ao menu.");
                Console.ReadLine();
                return;
            }

            tasks.Add(task);
            SaveToFile();

            Console.WriteLine();
            Console.WriteLine("Tarefa adicionada com sucesso!");
            Console.WriteLine();
            Console.WriteLine("Aperte ENTER para voltar ao menu.");
            Console.ReadLine();
        }

        public void EditTask()
        {
            ListTasks(false); // Lista as tarefas sem pausar

            Console.WriteLine("-------------------------------------");
            Console.WriteLine("           EDITAR TAREFAS            ");
            Console.WriteLine("-------------------------------------");

            Console.Write("Digite o ID da tarefa que deseja editar: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int id) && id >= 0 && id < tasks.Count)
            {
                Task task = tasks[id];
                Console.WriteLine($"Tarefa atual: {task.titulo}");
                Console.WriteLine();

                Console.Write("Novo título: ");
                task.titulo = Console.ReadLine();

                Console.Write("Nova descrição: ");
                task.descricao = Console.ReadLine();

                Console.Write("Nova data de vencimento (dd/mm/aaaa): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime novaDataVencimento))
                {
                    task.dataVencimento = novaDataVencimento;
                }
                else
                {
                    Console.WriteLine("Data inválida. A tarefa não foi atualizada.");
                    return;
                }
                Console.WriteLine("\nCategorias disponíveis:");
                for (int i = 0; i < Category.Categorias.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {Category.Categorias[i]}");
                }
                Console.WriteLine();
                Console.Write("Alterar categoria: ");
                if (int.TryParse(Console.ReadLine(), out int categoriaEscolhida) &&
                    categoriaEscolhida > 0 && categoriaEscolhida <= Category.Categorias.Count)
                {
                    task.categoria = Category.Categorias[categoriaEscolhida - 1];
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Categoria inválida. A tarefa não foi adicionada.");
                    Console.WriteLine("Aperte ENTER para voltar ao menu.");
                    Console.ReadLine();
                    return;
                }

                tasks[id] = task;
                SaveToFile();
                Console.WriteLine();
                Console.WriteLine($"Tarefa '{task.titulo}' atualizada com sucesso!");
                Console.WriteLine();
                Console.WriteLine("Aperte ENTER para voltar ao menu.");
                Console.ReadLine();
            }
            else
            {
                Console.ReadLine();
                Console.WriteLine("ID inválido ou entrada inválida!");
                Console.ReadLine();
                Console.WriteLine("Aperte ENTER para voltar ao menu.");
            }
        }

        public void ListTasks(bool pausar = true)
        {
            Console.Clear();
            if (tasks.Count > 0)
            {
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("          LISTA DE TAREFAS           ");
                Console.WriteLine("-------------------------------------");
                int i = 0;

                foreach (Task task in tasks)
                {
                    string status = task.concluida ? "[X]" : "[ ]"; // Exibe o status da tarefa
                    string mensagem = task.concluida ? "Concluída" : task.dataVencimento.ToString("dd/MM/yyyy"); // Mostra "Concluída" ou a data

                    Console.WriteLine($"ID: [{i}] | {status} {task.titulo} | Categoria: {task.categoria} - {mensagem}");
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


        public void CompleteTask()
        {
            ListTasks(false); // Lista as tarefas sem pausar

            Console.WriteLine("Digite o ID da tarefa que deseja marcar como concluída: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int id) && id >= 0 && id < tasks.Count)
            {
                Task task = tasks[id];
                task.concluida = true; // Marca a tarefa como concluída
                tasks[id] = task; // Atualiza a tarefa na lista
                SaveToFile();

                Console.WriteLine($"Tarefa '{task.titulo}' marcada como concluída com sucesso!");
            }
            else
            {
                Console.WriteLine("ID inválido ou entrada inválida!");
                Console.WriteLine("Aperte ENTER para voltar ao menu.");
                Console.ReadLine();
            }

            Console.WriteLine();
            Console.WriteLine("Aperte ENTER para voltar ao menu.");
            Console.ReadLine();
        }

        public void RemoveTask()
        {
            ListTasks(false);

            Console.WriteLine("Digite o ID da task que você quer remover: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int id) && id >= 0 && id < tasks.Count)
            {
                tasks.RemoveAt(id);
                Console.WriteLine($"Tarefa removida com sucesso!");
                SaveToFile();

                Console.WriteLine();
                Console.WriteLine("Aperte ENTER para voltar ao menu.");
            }
            else
            {
                Console.WriteLine("ID inválido ou entrada inválida!");
                Console.WriteLine("Aperte ENTER para voltar ao menu.");
                Console.ReadLine();
            }
            Console.ReadLine();
        }

        public void SaveToFile()
        {
            using (FileStream stream = new FileStream("tasks.txt", FileMode.OpenOrCreate))
            {
                BinaryFormatter encoder = new BinaryFormatter();
                encoder.Serialize(stream, tasks);
            }
        }

        public void LoadFromFile()
        {
            FileStream stream = new FileStream("tasks.txt", FileMode.OpenOrCreate);
            try
            {

                BinaryFormatter enconder = new BinaryFormatter();

                tasks = (List<Task>)enconder.Deserialize(stream);

                if (tasks == null)
                {
                    tasks = new List<Task>();
                }
            }
            catch (Exception e)
            {
                tasks = new List<Task>();
            }

            stream.Close();
        }

    }
}
