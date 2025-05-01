using System;
using System.Collections.Generic;
using System.IO;
using ToDoList.Utils;

namespace ToDoList
{
    [Serializable]
    public class TaskManager
    {
        static List<string> Prioridade = new List<string> { "Alta", "Média", "Baixa" };
        static List<Task> tasks = new List<Task>();

        MenuManager menuManager = new MenuManager();

        struct Task
        {
            public string titulo;
            public string descricao;
            public DateTime dataVencimento;
            public string categoria;
            public bool concluida;
            public string prioridade;

        }
        public void AddTask()
        {
            Console.Clear();
            Task task = new Task();

            menuManager.AdicionarNovaTarefa();

            task.titulo = InputValidador.GetValidInput<string>("Título: ", InputValidador.TryParseNonEmptyString);

            task.descricao = InputValidador.GetValidInput<string>("Descrição: ", InputValidador.TryParseNonEmptyString);

            task.dataVencimento = InputValidador.GetValidInput<DateTime>("Data de vencimento (dd/MM/yyyy): ", InputValidador.TryParseFutureDate);

            Console.WriteLine("\nPrioridades disponíveis:\n");
            for (int i = 0; i < Prioridade.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Prioridade[i]}");
            }

            int prioridadeEscolhida = InputValidador.GetValidInput<int>(
                "\nEscolha uma prioridade: ",
                entrada =>
                {
                    bool valido = int.TryParse(entrada, out int valor)
                                  && valor > 0
                                  && valor <= Prioridade.Count;
                    return (valido, valor);
                }
            );
            task.prioridade = Prioridade[prioridadeEscolhida - 1];

            Console.WriteLine("\nCategorias disponíveis:");
            for (int i = 0; i < Category.Categorias.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Category.Categorias[i]}");
            }

            int categoriaEscolhida = InputValidador.GetValidInput<int>(
                "Escolha uma categoria (número): ",
                entrada =>
                {
                    bool valido = int.TryParse(entrada, out int valor)
                                  && valor > 0
                                  && valor <= Category.Categorias.Count;
                    return (valido, valor);
                }
            );
            task.categoria = Category.Categorias[categoriaEscolhida - 1];

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
            ListTasks(false);

            menuManager.EditarTarefa();

            Console.Write("Digite o ID da tarefa que deseja editar: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int id) && id >= 0 && id < tasks.Count)
            {
                Task task = tasks[id];
                Console.WriteLine($"Tarefa atual: {task.titulo}");
                Console.WriteLine();

                task.titulo = InputValidador.GetValidInput<string>("Novo título: ", InputValidador.TryParseNonEmptyString);

                task.descricao = InputValidador.GetValidInput<string>("Nova descrição: ", InputValidador.TryParseNonEmptyString);

                task.dataVencimento = InputValidador.GetValidInput<DateTime>("Nova data de vencimento (dd/mm/aaaa): ", InputValidador.TryParseFutureDate);

                Console.WriteLine("\nPrioridades disponíveis:\n");
                for (int i = 0; i < Prioridade.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {Prioridade[i]}");
                }

                int prioridadeEscolhida = InputValidador.GetValidInput<int>(
                    "\nEscolha uma prioridade: ",
                    entrada =>
                    {
                        bool valido = int.TryParse(entrada, out int valor)
                                      && valor > 0
                                      && valor <= Prioridade.Count;
                        return (valido, valor);
                    }
                );
                task.prioridade = Prioridade[prioridadeEscolhida - 1];

                Console.WriteLine("\nCategorias disponíveis:");
                for (int i = 0; i < Category.Categorias.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {Category.Categorias[i]}");
                }
                Console.WriteLine();
                Console.Write("Alterar categoria: ");

                int categoriaEscolhida = InputValidador.GetValidInput<int>(
                    "Escolha uma categoria (número): ",
                    entrada =>
                    {
                        bool valido = int.TryParse(entrada, out int valor)
                                      && valor > 0
                                      && valor <= Category.Categorias.Count;
                        return (valido, valor);
                    }
                );
                task.categoria = Category.Categorias[categoriaEscolhida - 1];

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
                menuManager.ListaTarefas();

                int i = 0;

                foreach (Task task in tasks)
                {
                    string status = task.concluida ? "[X]" : "[ ]";
                    string mensagem = task.concluida ? "Concluída" : task.dataVencimento.ToString("dd/MM/yyyy");

                    Console.WriteLine($"ID: [{i}] | {status} {task.titulo} | Categoria: {task.categoria} | Prazo: {mensagem}| Prioridade: {task.prioridade}");
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
            ListTasks(false);
            menuManager.MarcarTarefa();

            Console.WriteLine("Digite o ID da tarefa que deseja marcar como concluída: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int id) && id >= 0 && id < tasks.Count)
            {
                Task task = tasks[id];
                task.concluida = true;
                tasks[id] = task;
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
            menuManager.RemoverTarefa();

            if (tasks.Count == 0)
            {
                Console.WriteLine("\nNenhuma tarefa disponível para remover.");
                Console.WriteLine();
                Console.WriteLine("Aperte ENTER para voltar ao menu.");
                Console.ReadLine();
                return;
            }
            
            int id = InputValidador.GetValidInput<int>(
                "\nDigite o ID da task que você quer remover: ",
                entrada =>
                {
                    bool valido = int.TryParse(entrada, out int valor)
                                  && valor >= 0
                                  && valor < tasks.Count;
                    return (valido, valor);
                }
            );

            tasks.RemoveAt(id);
            Console.WriteLine($"\nTarefa removida com sucesso!");
            SaveToFile();

            Console.WriteLine("\nAperte ENTER para voltar ao menu.");
            Console.ReadLine();
        }

        public void SaveToFile()
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tasks.txt");
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var task in tasks)
                    {
                        writer.WriteLine($"{task.titulo}|{task.descricao}|{task.dataVencimento:yyyy-MM-dd}|{task.categoria}|{task.concluida}|{task.prioridade}");
                    }
                }
                Console.WriteLine("Tarefas salvas com sucesso no arquivo de texto!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao salvar as tarefas:");
                Console.WriteLine(ex.Message);
            }
        }

        public void LoadFromFile()
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tasks.txt");
                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        tasks.Clear(); // Limpa a lista antes de carregar
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            // Divide a linha em partes usando o delimitador "|"
                            string[] parts = line.Split('|');
                            if (parts.Length == 6)
                            {
                                Task task = new Task
                                {
                                    titulo = parts[0],
                                    descricao = parts[1],
                                    dataVencimento = DateTime.Parse(parts[2]),
                                    categoria = parts[3],
                                    concluida = bool.Parse(parts[4]),
                                    prioridade = parts[5]
                                };
                                tasks.Add(task);
                            }
                        }
                    }
                    Console.WriteLine("Tarefas carregadas com sucesso do arquivo de texto!");
                }
                else
                {
                    Console.WriteLine("Arquivo de tarefas não encontrado. Criando um novo arquivo.");
                    tasks = new List<Task>();
                    SaveToFile();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao carregar as tarefas:");
                Console.WriteLine(ex.Message);
                tasks = new List<Task>();
            }
        }

    }
}