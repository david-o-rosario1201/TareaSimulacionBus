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
string[] paradas = { "Nagua", "San Francisco de Macorís" };
int paradaIndex = 0;
int primerosEnSubir = 0;




// Variables que pueden afectar el tiempo con sus probabilidades
bool ponchadura = rand.Next(1, 11) <= 2;
bool lluvia = rand.Next(1, 11) <= 3;
bool trafico = rand.Next(1, 11) <= 4;
bool accidente = rand.Next(1, 21) == 1;

Dictionary<string, InfoParada> pasajerosPorParada = new Dictionary<string, InfoParada>
{
    { "Nagua", new InfoParada { PasajerosEnEspera = 0, PasajerosTotales = 0, VecesSalida = 0 } },
    { "San Francisco de Macorís", new InfoParada { PasajerosEnEspera = 0, PasajerosTotales = 0, VecesSalida = 0 } }
};



while (!detenerViaje) // Bucle que se ejecuta hasta que se presione la barra espaciadora
{
    int pasajeroEsperando = pasajeros.Next(100);

    DesmontarPasajeros(ref pasajeroDesmontado);

    Thread.Sleep(3000);

    MontarPasajeros(ref pasajeroEsperando, ref primerosEnSubir);

    paradaIndex = (paradaIndex + 1) % 2;

    Retraso();

    AumentarMinutos(15);
    hora++;

    Thread.Sleep(2000);
}

Console.WriteLine("\n¡Viaje terminado!\n\n");
MostrarResultadoTamanoParada();

void MontarPasajeros(ref int pasajeroEsperando, ref int primerosEnSubir)
{
    int pasajerosNuevosMontados = 0;
    //pasajeroMontado = 0;
    //string paradaActual = paradas[paradaIndex];
    
    pasajerosPorParada[paradas[paradaIndex]].PasajerosTotales += pasajeroEsperando;

    Console.WriteLine(" =========================================================");
    Console.WriteLine($"  [{hora:D2}:{minutos:D2}] Bus sale de Estación {paradas[paradaIndex]}");
    Console.WriteLine(" =========================================================");
    Console.WriteLine($"  Pasajeros que habían en la Parada: {pasajerosPorParada[paradas[paradaIndex]].PasajerosEnEspera}");
    Console.WriteLine($"  Pasajeros nuevos: {pasajeroEsperando}");

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
            else if (pasajeroEsperando > 0)
            {
                bus[i] = 1;
                pasajeroMontado++;
                pasajerosNuevosMontados++;
                pasajeroEsperando--;
                //if (pasajeroDesmontado > 0)
                //    pasajeroDesmontado--;
            }
        }
    }

    primerosEnSubir += pasajeroEsperando;
    pasajerosPorParada[paradas[paradaIndex]].PasajerosEnEspera = primerosEnSubir;
    pasajerosPorParada[paradas[paradaIndex]].VecesSalida++;

    Console.WriteLine($"  Pasajeros que se desmontados: {pasajeroDesmontado}");
    Console.WriteLine($"  Pasajeros que se montados: {pasajerosNuevosMontados}");
    Console.WriteLine($"  Total de Pasajeros en el bus: {pasajeroMontado}");
    Console.WriteLine($"  Pasajeros en espera del regreso de la guagua: {pasajerosPorParada[paradas[paradaIndex]].PasajerosEnEspera}");
    Console.WriteLine("  Hora estimada de viaje: 1 hora y 15 minutos"); 
    Console.WriteLine(" ---------------------------------------------------------\n\n\n");
}

void DesmontarPasajeros(ref int pasajeroDesmontado)
{
    pasajeroDesmontado = 0;

    for (int i = 0; i < bus.Length; i++)
    {
        if (bus[i] == 1)
        {
            int desmontar = desmontarPasajero.Next(0, 2);
            if (desmontar > 0)
            {
                bus[i] = 0;
                pasajeroDesmontado++;
                if(pasajeroMontado > 0)
                    pasajeroMontado--;
            }
        }
    }
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

class InfoParada
{
    public int PasajerosEnEspera { get; set; }
    public int PasajerosTotales { get; set; }
    public int VecesSalida { get; set; }
}



//for(int i = 0; i < bus.Length; i++)
//{
//    if(pasajeroEsperando > 0)
//    {
//        bus[i] = 1;
//        pasajeroMontado++;
//        pasajeroEsperando--;
//    }
//}

//Console.WriteLine($"\nHora: {hora:D2}:{minutos:D2} AM");
//Console.WriteLine($"\nParada: Nagua");
//Console.WriteLine($"\nPasajeros montados: {pasajeroMontado}");
//Console.WriteLine("--------------------------------------------------------------------------");

//Thread.Sleep(6000);

//for (int i = 0; i < bus.Length; i++)
//{
//    if (bus[i] == 1)
//    {
//        int demostar = desmontarPasajero.Next(0,2);
//        if(demostar > 0)
//        {
//            bus[i] = 0;
//            pasajeroDesmontado++;
//        }
//    }
//}

//minutos += 15;
//if (minutos >= 60)
//{
//    minutos -= 60;
//    hora++;
//}
//hora++;

//Console.WriteLine($"\n\nHora: {hora:D2}:{minutos:D2} AM");
//Console.WriteLine($"\nParada: San Francisco de Macorís");
//Console.WriteLine($"\nPasajeros desmontados: {pasajeroDesmontado}");
//Console.WriteLine($"\nPasajeros montados: {pasajeroMontado - pasajeroDesmontado}");
//Console.WriteLine("--------------------------------------------------------------------------");