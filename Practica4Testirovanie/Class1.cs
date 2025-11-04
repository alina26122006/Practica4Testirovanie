using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica4Testirovanie
{
    public class FinanceCalculator
    {
        public double CalculatePayment(double a, double b, int c)
        {
            double r = b / 12 / 100;
            double n = c * 12;
            double x = Math.Pow(1 + r, n);
            return a * r * x / (x - 1);
        }

        public double CalculateOverpayment(double a, double b, int c)
        {
            double monthly = CalculatePayment(a, b, c);
            return monthly * c * 12 - a;
        }

        public List<string> GetPaymentSchedule(double amount, double rate, int years, DateTime startDate)
        {
            var result = new List<string>();
            double monthly = CalculatePayment(amount, rate, years);
            double balance = amount;
            double totalInterest = 0;

            for (int i = 0; i < years * 12; i++)
            {
                double interest = balance * (rate / 12 / 100);
                double principal = monthly - interest;
                balance -= principal;
                totalInterest += interest;

                DateTime paymentDate = startDate.AddMonths(i);
                string scheduleEntry = $"{paymentDate:yyyy-MM-dd} | {monthly:F2} | {principal:F2} | {interest:F2} | {balance:F2}";
                result.Add(scheduleEntry);

                if (balance < 0.01) break;
            }

            return result;
        }

        public double ConvertCurrency(double amount, string from, string to)
        {
            Dictionary<string, double> rates = new Dictionary<string, double>()
            {
                {"USD", 1.0},
                {"EUR", 0.85},
                {"GBP", 0.73},
                {"JPY", 110.0},
                {"RUB", 75.0}
            };

            if (!rates.ContainsKey(from) || !rates.ContainsKey(to))
                throw new Exception("Unknown currency");

            double usdAmount = amount / rates[from];
            return usdAmount * rates[to];
        }

        public double CalculateCommission(double amount, double commissionRate)
        {
            return amount * commissionRate / 100;
        }

        public void SaveExchangeRate(string currency, double rate)
        {
            // Простая "база данных" в памяти
            if (!exchangeRateHistory.ContainsKey(currency))
                exchangeRateHistory[currency] = new List<ExchangeRateRecord>();

            exchangeRateHistory[currency].Add(new ExchangeRateRecord
            {
                Date = DateTime.Now,
                Rate = rate
            });
        }

        public List<ExchangeRateRecord> GetExchangeRateHistory(string currency)
        {
            if (exchangeRateHistory.ContainsKey(currency))
                return exchangeRateHistory[currency];
            return new List<ExchangeRateRecord>();
        }

        private Dictionary<string, List<ExchangeRateRecord>> exchangeRateHistory = new Dictionary<string, List<ExchangeRateRecord>>();
    }

    public class ExchangeRateRecord
    {
        public DateTime Date { get; set; }
        public double Rate { get; set; }
    }
}
