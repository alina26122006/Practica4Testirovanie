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

            Console.WriteLine("1. Расчет кредита:");
            double payment = calculator.CalculatePayment(100000, 5.0, 10);
            Console.WriteLine($"Ежемесячный платеж: {payment:F2}");

            double overpayment = calculator.CalculateOverpayment(100000, 5.0, 10);
            Console.WriteLine($"Переплата: {overpayment:F2}");

            Console.WriteLine("\n2. График платежей (первые 3 месяца):");
            var schedule = calculator.GetPaymentSchedule(100000, 5.0, 10, DateTime.Now);
            for (int i = 0; i < Math.Min(3, schedule.Count); i++)
            {
                Console.WriteLine(schedule[i]);
            }

            Console.WriteLine("\n3. Конвертация валют:");
            try
            {
                double converted = calculator.ConvertCurrency(1000, "USD", "EUR");
                Console.WriteLine($"1000 USD = {converted:F2} EUR");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка конвертации: {ex.Message}");
            }

            double commission = calculator.CalculateCommission(1000, 1.5);
            Console.WriteLine($"Комиссия за обмен 1000: {commission:F2}");

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

            var unknownHistory = calculator.GetExchangeRateHistory("ABC");
            Console.WriteLine($"История для ABC: {unknownHistory.Count} записей");

            Console.WriteLine("\n5. Проблемные сценарии:");

            try
            {
                double negativePayment = calculator.CalculatePayment(-100000, 5.0, 10);
                Console.WriteLine($"Платеж при отрицательной сумме: {negativePayment:F2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при отрицательной сумме: {ex.Message}");
            }

            try
             {
                double zeroRatePayment = calculator.CalculatePayment(100000, 0, 10);
                Console.WriteLine($"Платеж при нулевой ставке: {zeroRatePayment:F2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при нулевой ставке: {ex.Message}");
            }

            Console.WriteLine("\n6. Проблемы с типами данных:");

            double preciseCalculation = calculator.CalculatePayment(100000.01, 5.12345, 10);
            Console.WriteLine($"Точный расчет с double: {preciseCalculation:F10}");

            double rounded = Math.Round(preciseCalculation, 2);
            Console.WriteLine($"После округления: {rounded:F2}");
            Console.WriteLine($"Разница: {preciseCalculation - rounded:F10}");
        }
    }

    public class ProblematicUsage
    {
        public void DemonstrateCommonIssues()
        {
            var calc = new FinanceCalculator();

            double result1 = calc.CalculatePayment(500000, 7.5, 15);
            double x = 500000;  
            double y = 7.5;
            int z = 15;         
            double result2 = calc.CalculatePayment(x, y, z);

            double payment1 = calc.CalculatePayment(100000, 5.0, 10);
            double payment2 = calc.CalculatePayment(200000, 5.0, 10);
            double payment3 = calc.CalculatePayment(100000, 6.0, 10);

            var schedule = calc.GetPaymentSchedule(100000, 5.0, 10, DateTime.Now);
            double currency = calc.ConvertCurrency(1000, "USD", "EUR");
            calc.SaveExchangeRate("USD", 1.0);

        }

        public void DemonstrateErrorScenarios()
        {
            var calc = new FinanceCalculator();
            try
            {
                calc.CalculatePayment(100000, 0, 10);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при нулевой ставке: {ex.Message}");
            }

            try
            {
                calc.CalculatePayment(-100000, 5.0, -10);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при отрицательных значениях: {ex.Message}");
            }

            try
            {
                calc.ConvertCurrency(1000, "XYZ", "USD");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка конвертации: {ex.Message}");
            }

            var longSchedule = calc.GetPaymentSchedule(100000, 5.0, 30, DateTime.Now);
            Console.WriteLine($"Размер графика для 30 лет: {longSchedule.Count} записей");
        }
    }
}

