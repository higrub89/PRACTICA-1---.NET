using System;

namespace TelemetriaVehiculo
{
    class Program
    {
        private const double TEMP_MIN = 80.0;
        private const double TEMP_MAX = 105.0;
        private const double PRESION_MIN = 25.0;
        private const double PRESION_MAX = 65.0;
        private const string CELSIUS = "°C";
        private const string PSI = "psi";

        static void Main(string[] args)
        {
            Console.Title = "Sistema de Telemetría";
            MostrarBienvenida();

            string continuar;
            do
            {
                Console.WriteLine("\n--- Nueva lectura ---");
                double temp = LeerDouble($"Temperatura ({CELSIUS}): ");
                double pres = LeerDouble($"Presión ({PSI}): ");

                string estadoTemp = Evaluar(temp, TEMP_MIN, TEMP_MAX, "Temperatura", CELSIUS);
                string estadoPres = Evaluar(pres, PRESION_MIN, PRESION_MAX, "Presión", PSI);

                MostrarEstado(estadoTemp, estadoPres);

                Console.Write("\n¿Otra lectura? (s/n): ");
                continuar = Console.ReadLine()?.Trim().ToLower() ?? "n";
            } while (continuar == "s" || continuar == "si");

            Console.WriteLine("\n¡Sistema finalizado!");
        }

        static void MostrarBienvenida()
        {
            Console.WriteLine("=== SISTEMA DE TELEMETRÍA DEL VEHÍCULO ===");
            Console.WriteLine($"Rangos seguros:");
            Console.WriteLine($"  • Temperatura: {TEMP_MIN}{CELSIUS} - {TEMP_MAX}{CELSIUS}");
            Console.WriteLine($"  • Presión: {PRESION_MIN} {PSI} - {PRESION_MAX} {PSI}\n");
        }

        static double LeerDouble(string msg)
        {
            while (true)
            {
                Console.Write(msg);
                if (double.TryParse(Console.ReadLine(), out double v)) return v;
                Console.WriteLine("Error: número inválido.");
            }
        }

        static string Evaluar(double valor, double min, double max, string nombre, string unidad)
        {
            if (valor < min) return $"[ALERTA] {nombre} BAJA: {valor}{unidad} (mín: {min}{unidad})";
            if (valor > max) return $"[ALERTA] {nombre} ALTA: {valor}{unidad} (máx: {max}{unidad})";
            return $"[OK] {nombre}: {valor}{unidad}";
        }

        static void MostrarEstado(string t, string p)
        {
            Console.WriteLine("\n--- ESTADO ---");
            Console.WriteLine(t);
            Console.WriteLine(p);
            bool alerta = t.Contains("ALERTA") || p.Contains("ALERTA");
            Console.ForegroundColor = alerta ? ConsoleColor.Red : ConsoleColor.Green;
            Console.WriteLine(alerta ? "\nALERTA - Revisar vehículo" : "\nOK - Todo en orden");
            Console.ResetColor();
        }
    }
}