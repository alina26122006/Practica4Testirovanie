using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica4Testirovanie;

namespace Primer
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Демонстрация легаси-кода финансовых расчетов ===\n");

            var calculator = new FinanceCalculator();

            // 1. Расчет кредита с непонятными параметрами
            Console.WriteLine("1. Расчет кредита:");
            double payment = calculator.CalculatePayment(100000, 5.0, 10);
            Console.WriteLine($"Ежемесячный платеж: {payment:F2}");

            double overpayment = calculator.CalculateOverpayment(100000, 5.0, 10);
            Console.WriteLine($"Переплата: {overpayment:F2}");

            // 2. График платежей с магическими числами
            Console.WriteLine("\n2. График платежей (первые 3 месяца):");
            var schedule = calculator.GetPaymentSchedule(100000, 5.0, 10, DateTime.Now);
            for (int i = 0; i < Math.Min(3, schedule.Count); i++)
            {
                Console.WriteLine(schedule[i]);
            }

            // 3. Конвертация валют с передачей строк
            Console.WriteLine("\n3. Конвертация валют:");
            try
            {
                double converted = calculator.ConvertCurrency(1000, "USD", "EUR");
                Console.WriteLine($"1000 USD = {converted:F2} EUR");

                // Неправильные коды валют - будет исключение
                // double wrongConvert = calculator.ConvertCurrency(1000, "USD", "ABC");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка конвертации: {ex.Message}");
            }

            // 4. Расчет комиссии
            double commission = calculator.CalculateCommission(1000, 1.5);
            Console.WriteLine($"Комиссия за обмен 1000: {commission:F2}");

            // 5. Работа с историей курсов
            Console.WriteLine("\n4. История курсов валют:");
            calculator.SaveExchangeRate("EUR", 0.88);
            calculator.SaveExchangeRate("EUR", 0.87);
            calculator.SaveExchangeRate("EUR", 0.89);

            var history = calculator.GetExchangeRateHistory("EUR");
            Console.WriteLine($"История курсов EUR ({history.Count} записей):");
            foreach (var record in history)
            {
                Console.WriteLine($"  {record.Date:yyyy-MM-dd HH:mm} - {record.Rate:F4}");
            }

            // 6. Проблемный код - попытка получить историю несуществующей валюты
            var unknownHistory = calculator.GetExchangeRateHistory("ABC");
            Console.WriteLine($"История для ABC: {unknownHistory.Count} записей");

            // 7. Демонстрация проблем с валидацией
            Console.WriteLine("\n5. Проблемные сценарии:");

            try
            {
                // Отрицательная сумма кредита - код не проверяет
                double negativePayment = calculator.CalculatePayment(-100000, 5.0, 10);
                Console.WriteLine($"Платеж при отрицательной сумме: {negativePayment:F2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при отрицательной сумме: {ex.Message}");
            }

            try
            {
                // Нулевая процентная ставка - возможны математические ошибки
                double zeroRatePayment = calculator.CalculatePayment(100000, 0, 10);
                Console.WriteLine($"Платеж при нулевой ставке: {zeroRatePayment:F2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при нулевой ставке: {ex.Message}");
            }

            // 8. Демонстрация проблем с типами данных
            Console.WriteLine("\n6. Проблемы с типами данных:");

            // Использование double для финансовых расчетов (проблемы с точностью)
            double preciseCalculation = calculator.CalculatePayment(100000.01, 5.12345, 10);
            Console.WriteLine($"Точный расчет с double: {preciseCalculation:F10}");

            // Проблемы округления
            double rounded = Math.Round(preciseCalculation, 2);
            Console.WriteLine($"После округления: {rounded:F2}");
            Console.WriteLine($"Разница: {preciseCalculation - rounded:F10}");
        }
    }

    // Дополнительные примеры проблемного кода
    public class ProblematicUsage
    {
        public void DemonstrateCommonIssues()
        {
            var calc = new FinanceCalculator();

            // ПРОБЛЕМА 1: Магические числа в коде использования
            double result1 = calc.CalculatePayment(500000, 7.5, 15);

            // ПРОБЛЕМА 2: Непонятные вызовы методов
            double x = 500000;  // Что это? Сумма кредита? 
            double y = 7.5;     // Что это? Процентная ставка?
            int z = 15;         // Что это? Срок в годах?
            double result2 = calc.CalculatePayment(x, y, z);

            // ПРОБЛЕМА 3: Копипаста кода
            // Дублирование расчетов для разных условий
            double payment1 = calc.CalculatePayment(100000, 5.0, 10);
            double payment2 = calc.CalculatePayment(200000, 5.0, 10);
            double payment3 = calc.CalculatePayment(100000, 6.0, 10);

            // ПРОБЛЕМА 4: Смешение ответственности
            // Один класс делает всё: и кредиты, и валюты, и историю
            var schedule = calc.GetPaymentSchedule(100000, 5.0, 10, DateTime.Now);
            double currency = calc.ConvertCurrency(1000, "USD", "EUR");
            calc.SaveExchangeRate("USD", 1.0);

            // ПРОБЛЕМА 5: Отсутствие единого стандарта
            // Для кредитов используем double, для дат - DateTime, для валют - string
        }

        public void DemonstrateErrorScenarios()
        {
            var calc = new FinanceCalculator();

            // Сценарии, которые могут сломать код:

            // 1. Деление на ноль при нулевой процентной ставке
            try
            {
                calc.CalculatePayment(100000, 0, 10);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при нулевой ставке: {ex.Message}");
            }

            // 2. Отрицательные значения
            try
            {
                calc.CalculatePayment(-100000, 5.0, -10);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при отрицательных значениях: {ex.Message}");
            }

            // 3. Несуществующие валюты
            try
            {
                calc.ConvertCurrency(1000, "XYZ", "USD");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка конвертации: {ex.Message}");
            }

            // 4. Проблемы с производительностью при больших сроках
            var longSchedule = calc.GetPaymentSchedule(100000, 5.0, 30, DateTime.Now);
            Console.WriteLine($"Размер графика для 30 лет: {longSchedule.Count} записей");
        }
    }
}

