using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ToDoList.Utils;


namespace ToDoList
{
    [Serializable]
    public class Category
    {
        MenuManager menuManager = new MenuManager();

        public static List<string> Categorias = new List<string> { "Casa", "Pessoal", "Estudo" };
        enum Gerenciador { Listar = 1, Adicionar, Remover, Voltar }

        public void MenuCategory()
        {

            bool voltarMenuPrincipal = false;

            while (!voltarMenuPrincipal)
            {
                Console.Clear();
                menuManager.HomeCategorias();

                int intOp = int.Parse(Console.ReadLine());
                Gerenciador opcao = (Gerenciador)intOp;

                switch (opcao)
                {
                    case Gerenciador.Listar:
                        ListCategory();
                        break;

                    case Gerenciador.Adicionar:
                        AddCategory();
                        break;

                    case Gerenciador.Remover:
                        RemoveCategory();
                        break;

                    case Gerenciador.Voltar:
                        voltarMenuPrincipal = true;
                        break;
                }
            }
        }
        public void ListCategory(bool pausar = true)
        {
            Console.Clear();
            menuManager.ListaCategorias();

            for (int i = 0; i < Categorias.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Categorias[i]}");
            }

            if (pausar)
            {
                Console.WriteLine("Aperte ENTER para voltar ao menu.");
                Console.ReadLine();
            }
        }

        public void AddCategory()
        {
            Console.Clear();
            menuManager.AdicionarNovaCategoria();

            string nomeCategoria = InputValidador.GetValidInput<string>(
                "Nome da nova categoria: ",
                entrada =>
                {
                    bool valido = !string.IsNullOrWhiteSpace(entrada)
                                  && !Categorias.Contains(entrada.Trim(), StringComparer.OrdinalIgnoreCase);
                    return (valido, entrada?.Trim());
                }
            );

            Categorias.Add(nomeCategoria);
            SaveToFileCategories();

            Console.WriteLine();
            Console.WriteLine($"Categoria '{nomeCategoria}' adicionada com sucesso!");
            Console.WriteLine();
            Console.WriteLine("Aperte ENTER para voltar ao menu.");
            Console.ReadLine();
        }

        public void RemoveCategory()
        {
            Console.Clear();
            menuManager.RemoverCategoria();

            ListCategory(false);
            Console.WriteLine();

            if (Categorias.Count == 0)
            {
                Console.WriteLine("Nenhuma categoria disponível para remover.");
                Console.WriteLine("Aperte ENTER para voltar ao menu.");
                Console.ReadLine();
                return;
            }

            int index = InputValidador.GetValidInput<int>(
                "Digite o número da categoria a ser removida: ",
                entrada =>
                {
                    bool valido = int.TryParse(entrada, out int valor)
                                  && valor > 0
                                  && valor <= Categorias.Count;
                    return (valido, valor);
                }
            );

            string categoriaRemovida = Categorias[index - 1];
            Categorias.RemoveAt(index - 1);
            SaveToFileCategories();

            Console.WriteLine();
            Console.WriteLine($"Categoria '{categoriaRemovida}' removida com sucesso!");
            Console.WriteLine();
            Console.WriteLine("Aperte ENTER para voltar ao menu.");
            Console.ReadLine();
        }

        public void SaveToFileCategories()
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categories.txt");
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var categoria in Categorias)
                    {
                        writer.WriteLine(categoria);
                    }
                }
                Console.WriteLine($"Categorias salvas com sucesso no arquivo: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao salvar as categorias:");
                Console.WriteLine(ex.Message);
            }
        }

        public void LoadFromFileCategories()
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categories.txt");
                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        Categorias.Clear(); // Limpa a lista antes de carregar
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (!string.IsNullOrWhiteSpace(line))
                            {
                                Categorias.Add(line);
                            }
                        }
                    }
                    Console.WriteLine($"Categorias carregadas com sucesso do arquivo: {filePath}");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Arquivo de categorias não encontrado. Criando um novo arquivo com valores padrão.");
                    Categorias = new List<string> { "Casa", "Pessoal", "Estudo" };
                    SaveToFileCategories();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao carregar as categorias:");
                Console.WriteLine(ex.Message);
                Categorias = new List<string> { "Casa", "Pessoal", "Estudo" };
                SaveToFileCategories(); // Recria o arquivo com valores padrão
            }
        }
    }
}
