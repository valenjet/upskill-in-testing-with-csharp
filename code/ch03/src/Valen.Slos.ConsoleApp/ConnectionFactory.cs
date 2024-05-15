namespace Valen.Slos.ConsoleApp;

using System;
using System.Configuration;

public static class ConnectionFactory
{
    public static string GetConnectionString()
    {
        return "In Memory Database";
    }
}