bool detenerViaje = false;

Thread keyListener = new Thread(DetectarTecla);
keyListener.Start();

Random pasajeros = new Random();
Random desmontarPasajero = new Random();
Random rand = new Random();
 
int hora = 6;
int minutos = 0;

int capacidadBus = 70;
//int pasajeroEsperando = pasajeros.Next(100);
int pasajeroMontado = 0;
int pasajeroDesmontado = 0;
int[] bus = new int[70];
int[] pasajerosQueSeQuedan = new int[70];
int[] pasajerosQueSeVan;
string[] paradas = { "Nagua", "San Francisco de Macorís" };
int paradaIndex = 0;
int primerosEnSubir = 0;
int perdidaDeDinero = 0;
int precioPasaje = 100;
int totalPasajeroQueNoSeBajan = 0;
int pasajerosNuevosMontados = 0;




// Variables que pueden afectar el tiempo con sus probabilidades
bool ponchadura = rand.Next(1, 11) <= 2;
bool lluvia = rand.Next(1, 11) <= 3;
bool trafico = rand.Next(1, 11) <= 4;
bool accidente = rand.Next(1, 21) == 1;

Dictionary<string, InfoParada> pasajerosPorParada = new Dictionary<string, InfoParada>
{
    { "Nagua", new InfoParada { PrimerosEnEntrar = 0, PasajerosTotales = 0, VecesSalida = 0, PasajerosEnEspera = 0 } },
    { "San Francisco de Macorís", new InfoParada { PrimerosEnEntrar = 0, PasajerosTotales = 0, VecesSalida = 0, PasajerosEnEspera = 0 } }
};



while (!detenerViaje)
{
    int pasajerosNuevos = pasajeros.Next(100);
    int pasajerosNue = pasajerosNuevos;

    DesmontarPasajeros(ref pasajeroDesmontado);

    Thread.Sleep(3000);

    MontarPasajeros(ref pasajerosNue, ref primerosEnSubir);

    AumentarMinutos(15);
    hora++;

    //MostrarTabla(pasajerosNuevos, pasajerosNuevosMontados);

    Retraso();

    paradaIndex = (paradaIndex + 1) % 2;
    Thread.Sleep(2000);
}

Console.WriteLine("\n¡Viaje terminado!\n\n");
MostrarResultadoTamanoParada();
MostrarResultadoPerdidaDeDinero();

void MontarPasajeros(ref int pasajerosNuevos, ref int primerosEnSubir)
{
    int pasajerosNuevosMontados = 0;
    //pasajeroMontado = 0;
    //string paradaActual = paradas[paradaIndex];

    pasajerosPorParada[paradas[paradaIndex]].PasajerosTotales += pasajerosNuevos;
    primerosEnSubir = pasajerosPorParada[paradas[paradaIndex]].PasajerosEnEspera;

    paradaIndex = (paradaIndex + 1) % 2;
    int cantidadPasajerosAntes = pasajerosPorParada[paradas[paradaIndex]].PasajerosTotales;
    paradaIndex = (paradaIndex + 1) % 2;

    Console.WriteLine(" =========================================================");
    Console.WriteLine($"  [{hora:D2}:{minutos:D2}] Bus sale de Estación {paradas[paradaIndex]}");
    Console.WriteLine(" =========================================================");
    Console.WriteLine($"  Capacidad de la guagua: {capacidadBus}");
    Console.WriteLine($"  Precio de pasaje: ${precioPasaje}.00");
    Console.WriteLine($"  Pasajeros que habían en espera del bus: {pasajerosPorParada[paradas[paradaIndex]].PasajerosEnEspera}");
    Console.WriteLine($"  Pasajeros nuevos: {pasajerosNuevos}");

    for (int i = 0; i < bus.Length; i++)
    {
        if (bus[i] == 0 && pasajeroMontado < capacidadBus)
        {
            if (primerosEnSubir > 0)
            {
                bus[i] = 1;
                pasajeroMontado++;
                pasajerosNuevosMontados++;
                primerosEnSubir--;
            }
            else if (pasajerosNuevos > 0)
            {
                bus[i] = 1;
                pasajeroMontado++;
                pasajerosNuevosMontados++;
                pasajerosNuevos--;
                //if (pasajeroDesmontado > 0)
                //    pasajeroDesmontado--;
            }
        }
    }

    primerosEnSubir += pasajerosNuevos;
    pasajerosPorParada[paradas[paradaIndex]].PasajerosEnEspera = primerosEnSubir;
    pasajerosPorParada[paradas[paradaIndex]].VecesSalida++;
    pasajerosPorParada[paradas[paradaIndex]].PrimerosEnEntrar = primerosEnSubir;

    Console.WriteLine($"  Pasajeros que se desmontados: {pasajeroDesmontado}");
    Console.WriteLine($"  Pasajeros que se montados: {pasajerosNuevosMontados}");
    Console.WriteLine($"  Pasajeros que no se desmontaron: {cantidadPasajerosAntes - pasajeroDesmontado}");
    Console.WriteLine($"  Total de Pasajeros en el bus: {pasajeroMontado}");
    Console.WriteLine($"  Pasajeros en espera del regreso del bus: {pasajerosPorParada[paradas[paradaIndex]].PasajerosEnEspera}");
    Console.WriteLine("  Hora estimada de viaje: 1 hora y 15 minutos");
    Console.WriteLine(" ---------------------------------------------------------\n\n\n");
}

void DesmontarPasajeros(ref int pasajeroDesmontado)
{
    pasajeroDesmontado = 0;
    int pasajerosQueNoSeBajanEnEstaParada = 0;

    for (int i = 0; i < bus.Length; i++)
    {
        if (bus[i] == 1)
        {
            int desmontar = desmontarPasajero.Next(0, 2);
            if (desmontar > 0)
            {
                bus[i] = 0;
                pasajeroDesmontado++;
                if (pasajeroMontado > 0)
                    pasajeroMontado--;
            }
            else
            {
                pasajerosQueNoSeBajanEnEstaParada++;
            }
        }
    }

    totalPasajeroQueNoSeBajan += pasajerosQueNoSeBajanEnEstaParada;
    perdidaDeDinero += precioPasaje * pasajerosQueNoSeBajanEnEstaParada;

}

void AumentarMinutos(int min)
{
    minutos += min;
    if (minutos >= 60)
    {
        minutos -= 60;
        hora++;
    }
}
void DetectarTecla()
{
    while (true)
    {
        if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Spacebar)
        {
            detenerViaje = true;
            break;
        }
        Thread.Sleep(100);
    }
}

void Retraso()
{
    if (ponchadura)
    {
        Console.WriteLine(" *********************************************************");
        Console.WriteLine("  Se ha ponchado una goma. +20 min");
        Console.WriteLine(" *********************************************************\n\n\n");
        AumentarMinutos(20);
    }
    if (lluvia)
    {
        int retraso = rand.Next(10, 31);
        Console.WriteLine(" *********************************************************");
        Console.WriteLine($"  Lluvia intensa. +{retraso} min");
        Console.WriteLine(" *********************************************************\n\n\n");
        AumentarMinutos(retraso);
    }
    if (trafico)
    {
        int retraso = rand.Next(5, 16);
        Console.WriteLine(" *********************************************************");
        Console.WriteLine($"  Tráfico pesado. +{retraso} min");
        Console.WriteLine(" *********************************************************\n\n\n");
        AumentarMinutos(retraso);
    }
    if (accidente)
    {
        int retraso = rand.Next(15, 46);
        Console.WriteLine(" *********************************************************");
        Console.WriteLine($"  Accidente en la vía. +{retraso} min");
        Console.WriteLine(" *********************************************************\n\n\n");
        AumentarMinutos(retraso);
    }
}

void MostrarResultadoTamanoParada()
{
    foreach (var parada in pasajerosPorParada)
    {
        Console.WriteLine("===================================================================");
        Console.WriteLine($"Parada: {parada.Key}");
        Console.WriteLine($"Total pasajeros que llegaron: {parada.Value.PasajerosTotales}");
        Console.WriteLine($"Veces que salió el bus: {parada.Value.VecesSalida}");
        Console.WriteLine($"Tamaño estimado de la parada: {parada.Value.PasajerosTotales/parada.Value.VecesSalida} personas");
        //Console.WriteLine("----------------------------------------------------------------\n\n");
        Console.WriteLine("===================================================================\n\n");
    }
}

void MostrarResultadoPerdidaDeDinero()
{
    Console.WriteLine($"Se quedaron en el bus un total de {totalPasajeroQueNoSeBajan} pasajeros");
    Console.WriteLine($"La pérdida total de dinero es de ${perdidaDeDinero:N2}:");

}

//void MostrarTabla(int pasajerosNuevos, int pasajerosNuevosMontados)
//{
//    Console.WriteLine(" =========================================================");
//    Console.WriteLine($"  [{hora:D2}:{minutos:D2}] Bus sale de Estación {paradas[paradaIndex]}");
//    Console.WriteLine(" =========================================================");
//    Console.WriteLine($"  Pasajeros que habían en la Parada: {pasajerosPorParada[paradas[paradaIndex]].PasajerosEnEspera}");
//    Console.WriteLine($"  Pasajeros nuevos: {pasajerosNuevos}");

//    Console.WriteLine($"  Pasajeros que se desmontados: {pasajeroDesmontado}");
//    Console.WriteLine($"  Pasajeros que se montados: {pasajerosNuevosMontados}");
//    Console.WriteLine($"  Total de Pasajeros en el bus: {pasajeroMontado}");
//    Console.WriteLine($"  Pasajeros en espera del regreso de la guagua: {pasajerosPorParada[paradas[paradaIndex]].PrimerosEnEntrar}");
//    Console.WriteLine("  Hora estimada de viaje: 1 hora y 15 minutos");
//    Console.WriteLine(" ---------------------------------------------------------\n\n\n");
//}

class InfoParada
{
    public int PrimerosEnEntrar { get; set; }
    public int PasajerosTotales { get; set; }
    public int VecesSalida { get; set; }
    public int PasajerosEnEspera { get; set; }
}