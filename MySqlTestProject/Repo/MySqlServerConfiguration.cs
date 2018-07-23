using System;
public static class MySqlServerConfiguration
{
    public static string ConnectionString { get {return ticketsCS; }}
    private static string ticketsCS= "Server=localhost;User ID=root;password=adf456;Database=Tickets;";      

}