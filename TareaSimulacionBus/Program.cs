bool detenerViaje = false;

Thread keyListener = new Thread(DetectarTecla);
keyListener.Start();

Random pasajeros = new Random();
Random desmontarPasajero = new Random();
 
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


while (!detenerViaje) // Bucle que se ejecuta hasta que se presione la barra espaciadora
{
    int pasajeroEsperando = pasajeros.Next(100);

    DesmontarPasajeros(ref pasajeroDesmontado);

    Thread.Sleep(3000);

    MontarPasajeros(ref pasajeroEsperando, ref primerosEnSubir);

    paradaIndex = (paradaIndex + 1) % 2;

    AumentarHora();

    Thread.Sleep(2000);
}

Console.WriteLine("\n¡Viaje terminado!");

void MontarPasajeros(ref int pasajeroEsperando, ref int primerosEnSubir)
{
    int pasajerosNuevosMontados = 0;
    Console.WriteLine($"\nHora: {hora:D2}:{minutos:D2} AM");
    Console.WriteLine($"\nParada: {paradas[paradaIndex]}");
    Console.WriteLine($"Pasajeros que habían en la Parada: {primerosEnSubir}");
    Console.WriteLine($"Pasajeros nuevos: {pasajeroEsperando}");

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

    primerosEnSubir = pasajeroEsperando;

    Console.WriteLine($"Pasajeros que se desmontados: {pasajeroDesmontado}");
    Console.WriteLine($"Pasajeros que se montados: {pasajerosNuevosMontados}");
    Console.WriteLine($"Total de Pasajeros en el bus: {pasajeroMontado}");
    Console.WriteLine($"Pasajeros en espera del regreso de la guagua: {primerosEnSubir}");
    Console.WriteLine("--------------------------------------------------------------------------");
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

void AumentarHora()
{
    minutos += 15;
    if (minutos >= 60)
    {
        minutos -= 60;
        hora++;
    }
    hora++;
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