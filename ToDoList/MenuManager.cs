using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public class MenuManager
    {
        public void Home()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("===== SISTEMA DE TAREFAS - v2.0.0 ====");
            Console.WriteLine("======================================");
            Console.WriteLine();
            Console.WriteLine("1. Listar tarefas\n2. Adicionar nova tarefa\n3. Editar tarefa\n4. Marcar tarefa como concluída\n5. Remover tarefas\n6. Gerenciar categorias\n7. Sair");

            Console.WriteLine("----------------------------------");
        }

        public void AdicionarNovaTarefa()
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("        ADICIONAR NOVA TAREFA        ");
            Console.WriteLine("-------------------------------------");
        }

        public void EditarTarefa()
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("           EDITAR TAREFA            ");
            Console.WriteLine("-------------------------------------");
        }

        public void ListaTarefas()
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("          LISTA DE TAREFAS           ");
            Console.WriteLine("-------------------------------------");
        }

        public void RemoverTarefa()
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("           REMOVER TAREFA           ");
            Console.WriteLine("-------------------------------------");
        }

        public void MarcarTarefa()
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("       MARCAR TAREFA CONCLUÍDA       ");
            Console.WriteLine("-------------------------------------");
        }

        public void HomeCategorias()
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("             CATEGORIAS              ");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine();
            Console.WriteLine("1. Listar categorias\n2. Adicionar nova categoria\n3. Remover categoria\n4. Voltar");
            Console.WriteLine();
            Console.Write("Escolha uma opção: ");
        }

        public void ListaCategorias()
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("          CATEGORIAS ATUAIS          ");
            Console.WriteLine("-------------------------------------");
        }

        public void AdicionarNovaCategoria()
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("          ADICIONAR CATEGORIA        ");
            Console.WriteLine("-------------------------------------");
        }

        public void RemoverCategoria()
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("           REMOVER CATEGORIA         ");
            Console.WriteLine("-------------------------------------");
        }
    }
}
