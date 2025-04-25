using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public class Category
    {
        public enum Categoria { Casa = 1, Pessoal, Estudo }
        enum Gerenciador { Listar = 1, Adicionar, Remover, Voltar }
        //public string Name { get; set; }
        public static List<string> CategoriasAdicionais = new List<string>();

        TaskManager taskManager = new TaskManager();

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
        public void ListCategory()
        {
            Console.Clear();
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("          CATEGORIAS ATUAIS          ");
            Console.WriteLine("-------------------------------------");

            foreach (var categoria in Enum.GetValues(typeof(Categoria)))
            {
                Console.WriteLine($"{(int)categoria}. {categoria}");
            }

            int index = Enum.GetValues(typeof(Categoria)).Length + 1;
            foreach (var categoria in CategoriasAdicionais)
            {
                Console.WriteLine($"{index}. {categoria}");
                index++;
            }

            Console.WriteLine();
            Console.WriteLine("Aperte ENTER para voltar ao menu de categorias.");
            Console.ReadLine();
        }

        public void AddCategory()
        {
            Console.Clear();
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("          ADICIONAR CATEGORIA        ");
            Console.WriteLine("-------------------------------------");
            Console.Write("Nome da nova categoria: ");
            string nomeCategoria = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(nomeCategoria) && !CategoriasAdicionais.Contains(nomeCategoria))
            {
                CategoriasAdicionais.Add(nomeCategoria);
                Console.WriteLine("Categoria adicionada com sucesso!");
                taskManager.SaveToFile();
            }
            else
            {
                Console.WriteLine("Categoria inválida ou já existente.");
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

            ListCategory();

            Console.Write("Digite o nome da categoria a ser removida: ");
            string nomeCategoria = Console.ReadLine();

            if (CategoriasAdicionais.Contains(nomeCategoria))
            {
                CategoriasAdicionais.Remove(nomeCategoria);
                Console.WriteLine("Categoria removida com sucesso!");
                taskManager.SaveToFile();
            }
            else
            {
                Console.WriteLine("Categoria inválida ou não existe.");
            }
        }

    }
}
