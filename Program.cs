using System;
using System.Threading;
using System.Threading.Tasks;

namespace CancellationToknApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int timeout = 0;

            if(args.Length > 0)
            {
                Int32.TryParse(args[0], out timeout);
            }
            while(timeout <= 0)
            {
                Console.WriteLine("Введите время ожидания");
                if(!Int32.TryParse(Console.ReadLine(), out  timeout) || timeout <= 0)
                    Console.WriteLine("Введены не корректные данные"); 
            }

            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var task = new Task(() => 
            {
                Thread.Sleep(100);
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Прервано выполнение задачи");
                    return;
                }
                Console.WriteLine("Задача выполняется");
                Thread.Sleep(100);
                Console.WriteLine("Задача завершилась успешно");
            },token);
            task.Start();
            Thread.Sleep(timeout);
            cts.Cancel();
            Thread.Sleep(100); 
            Console.WriteLine(task.Status);
        }
    }
}
