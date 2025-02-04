
Random pasajeros = new Random();
Random desmontarPasajero = new Random();
 
int hora = 6;
int capacidadBus = 70;
int pasajeroEsperando = pasajeros.Next(100);
int pasajeroMontado = 0;
int pasajeroDesmontado = 0;
Console.WriteLine(pasajeroEsperando);

int[] bus = new int[70];

for(int i = 0; i < bus.Length; i++)
{
    if(pasajeroEsperando > 0)
    {
        bus[i] = 1;
        pasajeroMontado++;
        pasajeroEsperando--;
    }
}

Console.WriteLine($"\nHora: {hora}");
Console.WriteLine($"\nParada: Nagua");
Console.WriteLine($"\nPasajeros montados: {pasajeroMontado}");

//for (int i = 0; i < bus.Length; i++)
//{
//    Console.WriteLine(bus[i]);
//}

for (int i = 0; i < bus.Length; i++)
{
    if (bus[i] == 1)
    {
        int demostar = desmontarPasajero.Next(0,2);
        if(demostar > 0)
        {
            bus[i] = 0;
            pasajeroDesmontado++;
        }
    }
}

Console.WriteLine($"\n\n\nHora: {hora}");
Console.WriteLine($"\nParada: San Francisco de Macorís");
Console.WriteLine($"\nPasajeros desmontados: {pasajeroDesmontado}");
Console.WriteLine($"\nPasajeros montados: {pasajeroMontado - pasajeroDesmontado}");

//for (int i = 0; i < bus.Length; i++)
//{
//    Console.WriteLine(bus[i]);
//}