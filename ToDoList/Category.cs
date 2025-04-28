using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


namespace ToDoList
{
    [Serializable]
    public class Category
    {
        public static List<string> Categorias = new List<string> { "Casa", "Pessoal", "Estudo" };
        enum Gerenciador { Listar = 1, Adicionar, Remover, Voltar }

        public void MenuCategory()
        {
            bool voltarMenuPrincipal = false;

            while (!voltarMenuPrincipal)
            {
                Console.Clear();
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("             CATEGORIAS              ");
                Console.WriteLine("-------------------------------------");

                Console.WriteLine();

                Console.WriteLine("1. Listar categorias\n2. Adicionar nova categoria\n3. Remover categoria\n4. Voltar");
                Console.WriteLine();
                Console.Write("Escolha uma opção: ");
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
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("          CATEGORIAS ATUAIS          ");
            Console.WriteLine("-------------------------------------");

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
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("          ADICIONAR CATEGORIA        ");
            Console.WriteLine("-------------------------------------");
            Console.Write("Nome da nova categoria: ");
            string nomeCategoria = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(nomeCategoria) && !Categorias.Contains(nomeCategoria))
            {
                Categorias.Add(nomeCategoria);
                Console.WriteLine();
                Console.WriteLine("Categoria adicionada com sucesso!");
                SaveToFileCategories();
                Console.WriteLine();

            }
            else
            {
                Console.WriteLine("Categoria inválida ou já existente.");
                Console.WriteLine();
            }

            Console.WriteLine("Aperte ENTER para voltar ao menu.");
            Console.ReadLine();
        }

        public void RemoveCategory()
        {
            Console.Clear();
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("           REMOVER CATEGORIA         ");
            Console.WriteLine("-------------------------------------");
            Console.Write("Nome da nova categoria: ");

            ListCategory(false);
            Console.WriteLine();

            Console.Write("Digite o número da categoria a ser removida: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= Categorias.Count)
            {
                string categoriaRemovida = Categorias[index - 1];
                Categorias.RemoveAt(index - 1);
                Console.WriteLine();
                SaveToFileCategories();
                Console.WriteLine($"Categoria '{categoriaRemovida}' removida com sucesso!");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Opção inválida!");
            }

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
