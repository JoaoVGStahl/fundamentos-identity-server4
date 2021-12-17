﻿using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace SkyCommerce.Data.Configuration
{
    public class ProviderConfiguration
    {
        private readonly string _connectionString;
        public static ProviderConfiguration With;
        private static readonly string MigrationAssembly = typeof(ProviderConfiguration).GetTypeInfo().Assembly.GetName().Name;

        public static void Build(string connString)
        {
            if (With is null)
                With = new ProviderConfiguration(connString);
        }

        public ProviderConfiguration(string connString) => _connectionString = connString;

        public Action<DbContextOptionsBuilder> SqlServer =>
             options => options.UseSqlServer(_connectionString, sql => sql.MigrationsAssembly(MigrationAssembly));

        public Action<DbContextOptionsBuilder> MySql =>
             options => options.UseMySql(_connectionString, sql => sql.MigrationsAssembly(MigrationAssembly));

        public Action<DbContextOptionsBuilder> Postgre =>
             options => options.UseNpgsql(_connectionString, sql => sql.MigrationsAssembly(MigrationAssembly));

        public Action<DbContextOptionsBuilder> Sqlite =>
            options => options.UseSqlite(_connectionString, sql => sql.MigrationsAssembly(MigrationAssembly));

        public Action<DbContextOptionsBuilder> InMemory =>
            options => options.UseInMemoryDatabase(nameof(InMemory));

    }
}