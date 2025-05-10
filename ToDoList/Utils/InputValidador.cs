using System;
using System.Threading;

namespace ToDoList.Utils
{
internal static class InputValidador
    {
        public static T GetValidInput<T>(string mensagem, Func<string, (bool valido, T resultado)> parser)
        {
            while (true)
            {
                Console.Write(mensagem);
                int perguntaX = Console.CursorLeft;
                int perguntaY = Console.CursorTop;

                string entrada = Console.ReadLine();
                var (valido, resultado) = parser(entrada);
                
                if (valido)
                    return resultado;
                else
                {
                    // Mostra a mensagem de erro em vermelho
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(0, perguntaY);
                    Console.Write("Entrada inválida! Tente novamente.");
                    Console.ResetColor();

                    // ⏳ Espera para o usuário ler
                    Thread.Sleep(1500);

                    // Limpar:
                    // 1. A linha da mensagem de erro
                    // 2. A linha onde a pessoa digitou errado
                    Console.SetCursorPosition(0, perguntaY);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, perguntaY + 1);
                    Console.Write(new string(' ', Console.WindowWidth));

                    // Voltar para a posição da pergunta original
                    Console.SetCursorPosition(0, perguntaY);
                }
            }
        }

        public static (bool, int) TryParseInt(string entrada)
        {
            bool sucesso = int.TryParse(entrada, out var valor);
            return (sucesso, valor);
        }

        public static (bool, string) TryParseNonEmptyString(string entrada)
        {
            bool sucesso = !string.IsNullOrWhiteSpace(entrada);
            return (sucesso, entrada?.Trim());
        }

        public static (bool, DateTime) TryParseFutureDate(string entrada)
        {
            bool sucesso = DateTime.TryParse(entrada, out var valor);

            if (sucesso && valor.Date >= DateTime.Today)
                return (true, valor);

            return (false, default);
        }
    }
}
