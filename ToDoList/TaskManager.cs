using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ToDoList.Utils;

namespace ToDoList
{
    [Serializable]
    public class TaskManager
    {
        static List<string> Prioridade = new List<string> { "Alta", "Média", "Baixa" };
        static int proximoId = 1;
        static List<Task> tasks = new List<Task>();

        MenuManager menuManager = new MenuManager();

        public struct Task
        {
            public int id;
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

            task.id = proximoId++;

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
            string entradaId = Console.ReadLine();

            if (int.TryParse(entradaId, out int parseid))
            {
                int index = tasks.FindIndex(t => t.id == parseid);
                if (index != -1)
                {
                    Task task = tasks[index];
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

                    Console.WriteLine("\nCategorias disponíveis:\n");
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

                    tasks[index] = task;
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
        }

        public List<Task> ObterTarefasFiltradas()
        {
            string prioridade = null;
            bool? concluida = null;
            string categoria = null;

            Console.Clear();
            Console.WriteLine("====================================");
            Console.WriteLine("         FILTRAR TAREFAS           ");
            Console.WriteLine("====================================");
            Console.WriteLine("Você pode aplicar múltiplos filtros combinados!");
            Console.WriteLine("------------------------------------");

            // Filtro por status
            Console.Write("Deseja filtrar por status? (1 = Concluída, 2 = Pendente, Enter para ignorar): ");
            string statusInput = Console.ReadLine();
            if (statusInput == "1") concluida = true;
            else if (statusInput == "2") concluida = false;

            // Filtro por prioridade
            Console.Write("Deseja filtrar por prioridade? (1 = Alta, 2 = Média, 3 = Baixa, Enter para ignorar): ");
            string prioridadeInput = Console.ReadLine();

            switch (prioridadeInput)
            {
                case "1":
                    prioridade = "alta";
                    break;
                case "2":
                    prioridade = "média";
                    break;
                case "3":
                    prioridade = "baixa";
                    break;
                default:
                    prioridade = null;
                    break;
            }

            // Filtro por categoria
            Console.Write("Deseja filtrar por categoria? (S/N): ");
            string catConfirm = Console.ReadLine();
            if (catConfirm?.Trim().ToUpper() == "S")
            {
                Console.WriteLine("\nCategorias disponíveis:");
                for (int i = 0; i < Category.Categorias.Count; i++)
                    Console.WriteLine($"{i + 1}. {Category.Categorias[i]}");

                Console.Write("Digite o número da categoria: ");
                if (int.TryParse(Console.ReadLine(), out int catIndex) &&
                    catIndex >= 1 && catIndex <= Category.Categorias.Count)
                {
                    categoria = Category.Categorias[catIndex - 1];
                }
            }

            // Filtro por data mais próxima / distante (exclusivo)
            Console.Write("Deseja filtrar por data? (1 = Mais próxima, 2 = Mais distante, Enter para ignorar): ");
            string dataInput = Console.ReadLine();

            if (dataInput == "1")
            {
                return tasks
                    .Where(t => !t.concluida && t.dataVencimento >= DateTime.Today)
                    .OrderBy(t => t.dataVencimento)
                    .Take(1)
                    .ToList();
            }
            else if (dataInput == "2")
            {
                return tasks
                    .Where(t => !t.concluida && t.dataVencimento >= DateTime.Today)
                    .OrderByDescending(t => t.dataVencimento)
                    .Take(1)
                    .ToList();
            }

            // Filtro combinado padrão
            return tasks
                .Where(t =>
                    (prioridade == null || t.prioridade.Equals(prioridade, StringComparison.OrdinalIgnoreCase)) &&
                    (concluida == null || t.concluida == concluida) &&
                    (categoria == null || t.categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase))
                )
                .ToList();
        }

        public void ListTasks(bool pausar = true)
        {
            Console.Clear();

            var tarefasFiltradas = ObterTarefasFiltradas();

            if (tarefasFiltradas.Count > 0)
            {
                Console.Clear();
                menuManager.ListaTarefas();

                for (int i = 0; i < tarefasFiltradas.Count; i++)
                {
                    Task task = tarefasFiltradas[i];

                    string status = task.concluida ? "[X]" : "[ ]";
                    string mensagem = task.concluida ? "Concluída" : task.dataVencimento.ToString("dd/MM/yyyy");

                    Console.Write($"ID: [{task.id}] | {status} {task.titulo} | Categoria: {task.categoria} | Prazo: {mensagem} | Prioridade: ");

                    switch (task.prioridade.ToLower())
                    {
                        case "alta":
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case "média":
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        case "baixa":
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        default:
                            Console.ResetColor();
                            break;
                    }

                    Console.WriteLine(task.prioridade);
                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine("Nenhuma tarefa encontrada com o filtro aplicado!");
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

            if (int.TryParse(input, out int id))
            {
                int index = tasks.FindIndex(t => t.id == id);
                if (index != -1)
                {
                    Task task = tasks[index];
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
        }

        public void RemoveTask()
        {
            ListTasks(pausar: false);
            menuManager.RemoverTarefa();

            if (!tasks.Any())
            {
                Console.WriteLine("\nNenhuma tarefa disponível para remover.");
                Console.WriteLine("\nAperte ENTER para voltar ao menu.");
                Console.ReadLine();
                return;
            }

            int id = InputValidador.GetValidInput<int>(
                "\nDigite o ID da task que você quer remover: ",
                entrada =>
                {
                    bool valido = int.TryParse(entrada, out int valor)
                                  && tasks.Any(t => t.id == valor);
                    return (valido, valor);
                }
            );

            int index = tasks.FindIndex(t => t.id == id);
            if (index == -1)
            {
                Console.WriteLine("\nTarefa não encontrada.");
                Console.WriteLine("Aperte ENTER para voltar ao menu.");
                Console.ReadLine();
                return;
            }

            Console.Write($"\nVocê realmente deseja remover a tarefa '{tasks[index].titulo}'? (S/N): ");
            string confirmacao = Console.ReadLine();

            if (confirmacao.Trim().ToUpper() == "S")
            {
                tasks.RemoveAt(index);
                SaveToFile();
                Console.WriteLine("\nTarefa removida com sucesso!");
            }
            else
            {
                Console.WriteLine("\nA tarefa não foi excluída.");
            }
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
                        writer.WriteLine($"{task.id}|{task.titulo}|{task.descricao}|{task.dataVencimento:yyyy-MM-dd}|{task.categoria}|{task.concluida}|{task.prioridade}");
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
                            if (parts.Length == 7)
                            {
                                Task task = new Task
                                {
                                    id = int.Parse(parts[0]),
                                    titulo = parts[1],
                                    descricao = parts[2],
                                    dataVencimento = DateTime.Parse(parts[3]),
                                    categoria = parts[4],
                                    concluida = bool.Parse(parts[5]),
                                    prioridade = parts[6]
                                };
                                tasks.Add(task);
                            }
                        }
                        if (tasks.Any())
                            proximoId = tasks.Max(t => t.id) + 1;
                        else
                            proximoId = 1;
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