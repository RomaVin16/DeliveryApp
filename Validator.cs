using Serilog;

namespace DeliveryApp
{
    public class Validator
    {
        private readonly List<string>? _acceptableDistricts;

        public Validator(IConfiguration configuration)
        {
            _acceptableDistricts = configuration.GetSection("ValidDistricts").Get<List<string>>();
        }

        /// <summary>
        ///  Ввод времени 
        /// </summary>
        /// <returns></returns>
        public DateTime InputTheTimeFirstDelivery()
        {
            DateTime firstDeliveryDateTime = default;

            var isCorrect = false;

            while (!isCorrect)
            {
                Console.Write("Введите время первой доставки: ");
                var oneDeliveryTimeInput = Console.ReadLine();

                if (DateTime.TryParse(oneDeliveryTimeInput, out firstDeliveryDateTime))
                {
                    isCorrect = true;
                    Log.Information("Время первой доставки {OneDeliveryTime} введено корректно.", firstDeliveryDateTime);
                }
                else
                {
                    Console.WriteLine("Некорректный формат времени. Введите еще раз.");
                    Log.Warning("Формат введенных данных некоректен: {Input}", oneDeliveryTimeInput);
                }
            }

            return firstDeliveryDateTime;
        }

        /// <summary>
        /// Ввод названия района 
        /// </summary>
        /// <returns></returns>
        public string InputDistrict()
        {
            string? district = null;
            var isCorrect = false;


            while (!isCorrect)
            {
                Console.WriteLine("Доступные районы: ");
                foreach (var acceptableDistrict in _acceptableDistricts)
                {
                    Console.WriteLine($"- {acceptableDistrict}");
                }

                Console.WriteLine();
                Console.Write("Введите название района: ");
district = Console.ReadLine();

    if (_acceptableDistricts.Contains(district))
    {
        isCorrect = true;
        Log.Information("Район {District} введен корректно.", district);
                }
     else
    {
        Console.WriteLine("Район не найден. Введите еще раз.");
        Log.Information("Район {District} не найден.", district);
                }
            }

            return district;
        }
    }
}
